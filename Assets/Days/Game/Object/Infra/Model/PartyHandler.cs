using System.Collections.Generic;
using System.Linq;
using Days.Game.Object.Infra.Const;
using Days.Game.Script;
using Debug = UnityEngine.Debug;
using static Days.Game.Object.Infra.Const.PartyConst;

namespace Days.Game.Object.Infra.Model
{
    /// <summary>
    /// 파티 제어를 위한 함수
    /// </summary>
    public class PartyHandler : PartyModel
    {
        private GameManager _gameManager { get; set; }

        #region Variable

        private bool _executedAction;
        #endregion
        
        public PartyHandler()
        {
            
        }

        public PartyHandler(GameManager gameManager) => _gameManager = gameManager;
        
        public PartyHandler(PartyHandler party)
        {
            
        }

        public void UpdateState(PartyState value) => State = value;
        
        
        public void CreateDungeonEvent()
        {
            
        }

        /// <summary>
        /// 매 초 파티의 상태에 따른 던전 탑색 이벤트 진행
        /// </summary>
        public void Advance()
        {
            switch(State)
            {
                // 기본 탐색 상태
                case PartyState.Default:
                {
                    // 진행 거리 += 평균 속도 
                    RunningDistance +=  GetAvgSpeed(); 
                    if (RunningDistance >= GoalLength)
                    {
                        // 도착
                        State = PartyState.Arrival;
                    }
                    else
                    {
                        // 이벤트가 존재하는지 체크
                        var curEvt = Events.FirstOrDefault(x => x.Execution == false);
                        if (curEvt != null)
                        {
                            // 해당 이벤트 상태로 전환
                            State = PartyEventInfo[curEvt.EventType];

                            if (State == PartyState.Fight || State == PartyState.EventWait)
                            {
                                _executedAction = false;
                                _gameManager.GetUIManager().GetMapController().NotiDungeon(dungeonIdx: DungeonIndex);
                            }
                        }
                    }
                    break;   
                }
                // 사용자의 전투입력을 대기 중인 상태
                case PartyState.Fight:
                {
                    if (_executedAction)
                    {
                        
                    }
                    break;
                }
                // 자동 수행 가능한 랜덤 이벤트 발생하여 수행
                case PartyState.Event:
                {
                    break;
                }
                // 사용자 입력이 필요한 이벤트가 발생하여 수행
                case PartyState.EventWait:
                {
                    // 이벤트 진행 중일 때. 초진행의 경우
                    //  default || 와 같이 진행하는 것이 맞지 안나 생각..
                    if (_executedAction)
                    {
                        
                    }
                    
                    break;
                }
                // case PartyState.ARRIVAL:
                // {
                //     break;
                // }
                // case PartyState.RETREAT:
                // {
                //     
                //     break;
                // }
                // case PartyState.DIE:
                //     break;
                default: 
                    break;
            }

            Debug.Log($"[{State.ToString()}] {RunningDistance} / {GoalLength}");
            
        }

        public void Retreat()
        {
            
        }
        
        public ushort GetAvgSpeed()
        {
            var avgSpeed =  UnitsIndex.Average(x=> _gameManager.GetPlayerData().UnitList[x].Stat.Speed);
            
            return (ushort)avgSpeed;
        }
    }
}