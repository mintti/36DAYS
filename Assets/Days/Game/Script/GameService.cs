using System;
using System.Collections;
using System.Collections.Generic;
using Days.Data.Infra;
using Days.Data.Script;
using UnityEngine;

using Days.Game.Infra;
using Days.Game.Object.Infra;
using Days.Resource;
using Days.Resource.Model;
using Random = System.Random;
using util = Days.Util.Script.UtilityService;

namespace Days.Game.Script
{
    public class GameService : MonoBehaviour
    {
        #region 게임 초기 데이타 생성 관련 함수
        /// <summary>
        /// 새 Player Data를 생성합니다.
        /// </summary>
        public PlayerData CreatePlayerData()
        {
            Debug.Log("Creates the player data.");
            var playerData = new PlayerData()
            {
                Day = 0,
                KeyCode = "...."
            };

            playerData.DungeonList = GetDungeonList(playerData.KeyCode);
            
            Debug.Log("Completed to create the player data.");
            
            return playerData;
        }
        
        /// <summary>
        /// 입력된 키코드 기반으로 던전 리스트를 생성
        /// </summary>
        private List<DungeonModel> GetDungeonList(string keycode)
        {
            var dlist = new List<DungeonModel>();

            // 테스트용
            for (int i = 0; i < 5; i++)
            {
                dlist.Add(new DungeonModel()
                {
                    Index = i,
                    DungeonKey = 0
                });
            }
            
            return dlist;
        }

        #endregion
        #region 게임 세팅 관련 함수
        /// <summary>
        /// 게임에서 사용자가 다음 날로 넘어가는 경우, 아래의 내용을 설정하고 저장합니다.
        /// - 게임 상 다음 날로 넘어가는 경우 특정한 설정이 필요한 경우
        /// - 랜덤 키 값을 생성합니다. 해당 키 값을 기반으로 각 이벤트들을 설정합니다.
        /// </summary>
        public PlayerData PreAutomation(PlayerData playerData)
        {
            try
            {
                // Set Game Data About Next Day
                playerData.UpdateDay();
                
                // Create Random Key
                playerData.KeyCode = CreateKeyCode();
            }
            catch (Exception e)
            {
                // 추후 조치 필요
                util.PrintErrorLog("[GAME] Failed to Pre-Automation data during the game initialized process.");
                throw;
            }
            
            return playerData;
        }

        private string CreateKeyCode()
        {
            Random random = new Random();
            return random.Next().ToString();
        }
        
        // ================================================================================================
        /// <summary>
        /// 앞서 PreAutomation 중 생성된 키 값을 기반으로 게임 내부 데이타를 설정합니다.
        /// </summary>
        public bool ExecuteGameSetting()
        {
            // Create Event
            
            
            // Execute Stage Setting
            
            return true;
        }

        private void CreateEvent()
        {
            
        }

        private void ExecuteStageSetting()
        {
            
        }
        #endregion

        #region 게임 진행 관련 함수
        /*==============================================================
                                     Game
        ==============================================================*/
        /// <summary>
        /// 게임 시간의 하루 도달을 확인
        /// </summary>
        public bool CheckExpiration(GameData gameData)
        {
            return gameData.Time % gameData.TimeCycle == 0;
        }
        #endregion
        
        /// <summary>
        /// 게임 시간을 초기화
        /// </summary>
        public GameData InitialData()
        {
            return new GameData() { Time = 0, TimeCycle = 10};
        }
        
        /*==============================================================
                                        UNIT
        ==============================================================*/

        public ObjectInfo ConvertModelToInto(ObjectModel model)
        {
            ObjectInfo info = new ObjectInfo()
            {
                Index = model.Index,
                Name = model.Name,
                CurrentState = model.CurrentState
            };
            
            // 스텟 설정을 위한 정보 읽기
            var jobInfo = ResourceManager.CharacterList[model.Job];
            
            
            // 값 설정
            info.State = jobInfo.BaseState;
            
            // other info ref
            // 
            return info;
        }
        
        /*==============================================================
                                    Dungeon
        ==============================================================*/

        public static ushort CreateDungeonKey()  // input Keys..
        {

            return 0;
        }
        public List<Tuple<ushort, byte>> CreateDungeonEvent(Dungeon dungeon)
        {
            var events = new List<Tuple<ushort, byte>>();

            return events;
        }
        
        
        
        /*==============================================================
                                    Artifact
        ==============================================================*/
    }
}
