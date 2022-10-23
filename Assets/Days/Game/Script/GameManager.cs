using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Days.Data.Infra;
using Days.Data.Script;
using Days.Game.Background.Script;
using Days.Game.Combat.Script;
using Days.Game.Object.Infra;
using Days.Game.Object.Infra.Const;
using Days.Game.Object.Infra.Model;
using Days.Game.OS.Script;
using Days.Resource;
using Days.System.Script;
using UnityEngine;
using UnityEngine.UI;
using util = Days.Util.Script.UtilityService;

namespace Days.Game.Script
{
    public class GameManager : MonoBehaviour
    {
        private MainManager _mainManager;
        private GameService _gameService;
        private OsManager _osManager;
        private UIManager _uiManager;
        private BackgroundManager _backgroundManager;

        private CombatController _combatController;
        
        // 복제된 사용자 정보
        private PlayerData _playerData;
        private PlayerData PlayerData
        {
            get => _playerData;
            set
            {
                _playerData = value; 
                _uiManager.UpdatePlayerDataView(_playerData);
            }
        }

        public PlayerData GetPlayerData() => _playerData; // Dummy
        
        
        // 현제 게임 데이타 
        private GameData _currentGameData;

        private GameData CurrentGameData
        {
            get => _currentGameData;
            set
            {
                // 하단 dummy로 옮겨짐
                // _uiManager.UpdateGameDataView(_currentGameData);
                _currentGameData = value;
            }
        }

        #region Variable Quick Public Property
        public UIManager GetUIManager() => _uiManager;
        public BackgroundManager GetBackgroundManager() => _backgroundManager;
        public CombatController SetCombatController(CombatController controller) => _combatController = controller;
        #endregion
        
        /// <summary>
        /// 초기 값을 설정한다. 해당 객체가 가지는 각 매니저 객체 또한 초기화
        /// </summary>
        /// <param name="mainManager"></param>
        public bool InitManager(MainManager mainManager)
        {
            _mainManager = mainManager;
            _gameService = GetComponentInChildren<GameService>();
            _osManager = GetComponentInChildren<OsManager>();
            _uiManager = GetComponentInChildren<UIManager>();
            _backgroundManager = GetComponentInChildren<BackgroundManager>();
            
            // Init OS Manager
            if (!_osManager.Init(this))
            {
                util.PrintErrorLog("Failed to initialize os.");
            }
            else
            {
                // 초기화 성공 시, Gama Manager에서 사용할 기본적인 동작 알람을 등록
                _osManager.GetScheduler().CreateAlarm("1Tick", Increase, "", 1);
                _osManager.GetScheduler().CreateAlarm("BG1Tick", _backgroundManager.Increase, "", 1);
            }

            // Init UI Manager
            if (!_uiManager.Init(this))
            {
                Debug.Log("Failed to initialize ui.");
            }
            
            // Init Background Manager
            _backgroundManager.Init(this);

            Debug.Log("Game Setting Complete.");
            return true;
        }

        /// <summary>
        /// 플레이어 데이타 로드 및 화면 설정
        /// 씬 이동 직후 실행되는 함수
        /// </summary>
        /// <returns></returns>
        public bool InitData()
        {
            // Main Manager에서 로드됬던 데이타를 설정. (데이타가 없던 경우, 기본 데이타로 로드됨) 
            _playerData = _mainManager.GetDataManager().GetPlayerData();
            _playerData ??= _gameService.CreatePlayerData();

            _uiManager.InitView();
            // Update default views based on data.
            // _uiManager.UpdatePlayerDataView(_playerData);
            // _uiManager.UpdateGameDataView(_currentGameData);
            
            return true;
        }

        /// <summary>
        /// UI Manager 에서 View Setting들이 완료되면 동작 
        /// </summary>
        public void CompletedViewSetting()
        {
            PreRun();
        }
        
        
        #region 게임 구동 관련

        /// <summary>
        /// 게임 구동 전 Game Data 설정
        /// </summary>
        public async void PreRun()
        {
            // Game Data 초기화
            CurrentGameData = _gameService.InitialData();
            
            // 키 값에 따른 이벤트 생성 및 스테이지에 배치
            if (!_gameService.ExecuteGameSetting())
            {
                util.PrintErrorLog("[GAME] Failed to execute game setting.");
            }
            
            //  이벤트 부여
            _backgroundManager.ExecutePreEvent();
            
            // 실행
            Run();
        }
        
        /// <summary>
        /// 게임 구동
        /// </summary>
        private void Run()
        {
            Debug.Log("Run.");

            // 타이머 실행
            _osManager.Run();
        }

        /// <summary>
        /// Timer Expire 
        /// </summary>
        public void Stop()
        {
            Debug.Log("Stop.");
            
            // 데이타 중지 및 초기화
            _osManager.Stop();
            _backgroundManager.ExecutePreEvent();
            
            
            var playerDataCopy = PlayerData;
            
            // 데이타 생성
            playerDataCopy = _gameService.PreAutomation(playerDataCopy);
            
            // 데이타 저장
            _mainManager.Save(playerDataCopy);
            
            //재개
            PlayerData = playerDataCopy;
            Invoke(nameof(PreRun),5 );
        }
        
        #endregion

        #region 게임 상태 업데이트

        /// <summary>
        /// 1초 증가할 때마다 실행
        /// </summary>
        private void Increase()
        {
            CurrentGameData.UpdateTime();
            
            // dummy
            _uiManager.UpdateGameDataView(_currentGameData);
            
            // dummy 03 아티펙트 초당 체력 회복 1p
            //PlayerData.UnitList?.ForEach(x=> Debug.Log($"{x.Name} HP : {x.CurrentStatus.Hp}"));
                
            // 던전에 들어간 파티 정보 정보 업데이트
            PlayerData.PartyList?.ForEach(x => x.Advance());
            PlayerData.PartyList?.Where(x=>x.State == PartyState.Arrival).ToList().ForEach(x=> ReturnPartyDungeon(x));

            if (_gameService.CheckExpiration(CurrentGameData))
            {
                // 하루 사이클 완료
                Stop();
            }
        }

        #endregion

        #region Unit / Party / Dungeon
        /*==============================================================
                                     Unit
        ==============================================================*/ 
        public void CreateUnit(EntityModel unitModel)
        {
            var unit = _gameService.ConvertModelToInto(unitModel);
            PlayerData.UnitList.Add(unit);
        }
        
        
        /*==============================================================
                                    Dungeon
        ==============================================================*/ 
        /// <summary>
        /// 던전에 유닛을 투입합니다.
        /// </summary>
        public void SendPartyDungeon(List<byte> unitsIndex, byte dungeonIndex, ushort goalLenght)
        {
            var events = _gameService.CreateDungeonEvent(ResourceManager.GetDungeon(dungeonIndex)); 
            // 파티 정보를 생성하여 플레이어 데이터에 추가
            PlayerData.PartyList.Add(
                new PartyHandler(this)
                {
                    Key = GameService.CreateDungeonKey(),
                    DungeonIndex = dungeonIndex,
                    UnitsIndex = unitsIndex,
                    RunningDistance = 0,
                    GoalLength = goalLenght,
                    State = PartyState.Default,
                    Events = events,
                }
            );
            
            // 유닛들의 상태 파티 상태로 변경
            unitsIndex.ForEach(index => PlayerData.UnitList[index].ObjectState = ObjectState.PARTY);
            
        }

        /// <summary>
        /// 파티가 던전에서 돌아옵니다.
        /// </summary>
        public void ReturnPartyDungeon(PartyHandler party)
        {
            var state = party.State;

            switch(state)
            {
                case PartyState.Arrival :
                    // 보상
                    break;
                case PartyState.Retreat :
                    // 일부 보상 및 패널티
                    break;
                case PartyState.Die:
                    // 죽음
                    break;
            }
            
            // 파티에서 돌아온 유닛 대기 상태로 업데이트
            foreach (var index in party.UnitsIndex)
            {
                var objStt = PlayerData.UnitList[index].ObjectState;
                UnitInfo unit;
                // IF, 죽은 상태가 아닐때만 업데이트
                if (objStt != ObjectState.DIE)
                {
                    unit = PlayerData.UnitList[index];
                    
                    unit.ObjectState = ObjectState.WAIT;
                    
                    Debug.Log($"{unit.Name} state is {unit.ObjectState.ToString()}");
                }
            }

            PlayerData.PartyList.Remove(party);
        }

        /// <summary>
        /// 던전에서 발생한 전투 이벤트 처리
        /// </summary>
        public void ExecuteCombat(PartyHandler party)
        {
            var combatInfo = _gameService.CreateCombatInfo(party);
            
            _osManager.Pause();
            _uiManager.SetCombatView();
            _combatController.ExecuteCombat(combatInfo);
        }

        /// <summary>
        /// 전투 종료 후 결과 반영 및 화면 전환
        /// </summary>
        public void EndCombat(string result)
        {
            // 결과 반영
            
            // 뷰 전환
            _uiManager.SetDefaultView();
            
            // 진행 재개
            _osManager.Run();
        }
        #endregion

        #region Background Event
        /*==============================================================
                                   Artifact
        ==============================================================*/
        public void ActiveArtifact(int index)
        {
            
        }
        #endregion
    }
}