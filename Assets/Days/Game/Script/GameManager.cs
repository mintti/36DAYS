using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Days.Data.Infra;
using Days.Game.Object.Infra;
using Days.Game.OS.Script;
using Days.System.Script;
using UnityEngine;
using UnityEngine.UI;
using static Days.Resource.Resource;
using util = Days.Util.Script.UtilityService;

namespace Days.Game.Sciprt
{
    public class GameManager : MonoBehaviour
    {
        private MainManager _mainManager;
        private GameService _gameService;
        private OsManager _osManager;
        private UIManager _uiManager;

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
        
        public bool Init(MainManager mainManager)
        {
            _mainManager = mainManager;
            _gameService = GetComponentInChildren<GameService>();
            _osManager = GetComponentInChildren<OsManager>();
            _uiManager = GetComponentInChildren<UIManager>();
            
            
            // os 
            if (!_osManager.ExecuteOsManager(this))
            {
                Debug.Log("Failed to initialize os.");
            }
            _osManager.GetScheduler().CreateAlarm("1Tick", Increase, "", 1);
            
            
            // ui
            if (!_uiManager.ExecuteManager(this))
            {
                Debug.Log("Failed to initialize ui.");
            }
            
            
            // data
            PlayerData = _mainManager.GetDataManager().GetPlayerData();

            
            // Update Default View 
            _uiManager.UpdatePlayerDataView(_playerData);
            _uiManager.UpdateGameDataView(_currentGameData);

            Debug.Log("Game Setting Complete.");
            return true;
        }

        
        
        #region 게임 구동 관련 
        
        /// <summary>
        /// 게임 구동 전 세팅
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
            
            _osManager.Stop();
            
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
            
            
            // 던전에 들어간 파티 정보 정보 업데이트
            PlayerData.PartyList?.ForEach(x => x.Advance());
            PlayerData.PartyList?.Where(x=>x.State == PartyState.ARRIVAL).ToList().ForEach(x=> ReturnPartyDungeon(x));

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
        public void CreateUnit(ObjectInfo unit)
        {
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
            // 파티 정보를 생성하여 플레이어 데이터에 추가
            PlayerData.PartyList.Add(
                new Party(this)
                {
                    Key = _gameService.CreateDungeonKey(),
                    DungeonIndex = dungeonIndex,
                    UnitsIndex = unitsIndex,
                    Length = 0,
                    GoalLength = goalLenght,
                    State = PartyState.DEFAULT,
                    //Events = _gameService.CreateDungeonEvent(DungeonResources[dungeonIndex]),
                }
            );
            
            // 유닛들의 상태 파티 상태로 변경
            unitsIndex.ForEach(index => PlayerData.UnitList[index].ObjectState = ObjectState.PARTY);
            
        }

        /// <summary>
        /// 파티가 던전에서 돌아옵니다.
        /// </summary>
        public void ReturnPartyDungeon(Party party)
        {
            var state = party.State;

            switch(state)
            {
                case PartyState.ARRIVAL :
                    // 보상
                    break;
                case PartyState.RETREAT :
                    // 일부 보상 및 패널티
                    break;
                case PartyState.DIE:
                    // 죽음
                    break;
            }
            
            // 파티에서 돌아온 유닛 대기 상태로 업데이트
            foreach (var index in party.UnitsIndex)
            {
                var objStt = PlayerData.UnitList[index].ObjectState;
                ObjectInfo unit;
                // IF, 죽은 상태가 아닐때만 업데이트
                if (objStt != ObjectState.DIE)
                {
                    unit = PlayerData.UnitList[index];
                    
                    unit.ObjectState = ObjectState.WAIT;
                    
                    Debug.Log($"{unit.Name} state is {unit.State.ToString()}");
                }
            }

            PlayerData.PartyList.Remove(party);
        }

        #endregion
    }
}