using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Days.Game.Script;
using static Days.Resource.Resource;
using Debug = UnityEngine.Debug;

namespace Days.Game.Object.Infra
{
    public class Party
    {
        private GameManager _gameManager { get; set; }
        
        public byte DungeonIndex { get; set; }
        public ushort Key { get; set; }
        public PartyState State { get; set; }
        public List<byte> UnitsIndex { get; set; }
        public ushort Length { get; set; }
        public ushort GoalLength { get; set; }
        /// <summary>
        /// ushort : Length, byte : Event Index
        /// </summary>
        public List<Tuple<ushort, byte>> Events { get; set; }


        public Party()
        {
            
        }

        public Party(GameManager gameManager) => _gameManager = gameManager;
        
        public Party(Party party)
        {
            
        }

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
                case PartyState.DEFAULT:
                {
                    Length += GetAvgSpeed();

                    if (Length >= GoalLength)
                    {
                        State = PartyState.ARRIVAL;
                    }
                    break;   
                }
                case PartyState.FIGHT:
                {
                    // WAIT.... 
                    
                    break;
                }
                case PartyState.EVENT:
                {
                    break;
                }
                case PartyState.EVENT_WAIT:
                {
                    // 이벤트 진행 중일 때. 초진행의 경우
                    //  default || 와 같이 진행하는 것이 맞지 안나 생각..
                    break;
                }
                case PartyState.ARRIVAL:
                {
                    break;
                }
                case PartyState.RETREAT:
                {
                    
                    break;
                }
                case PartyState.DIE:
                    break;
                default: 
                    break;
            }

            Debug.Log($"[{State.ToString()}] {Length} / {GoalLength}");
            
        }

        public void Retreat()
        {
            
        }
        
        public ushort GetAvgSpeed()
        {
            var avgSpeed =  UnitsIndex.Average(x=> _gameManager.GetPlayerData().UnitList[x].State.Speed);
            
            return (ushort)avgSpeed;
        }
    }
}