using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Days.Game.Object.Infra;
using UnityEngine;
using State = Days.Game.Object.Script.State;

namespace Days.Game.Combat.Script
{
    public class CombatController : MonoBehaviour
    {
        private List<ObjectInfo> _combatants;
        private Stack<ObjectInfo> _sequenceStack;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ExecuteCombat()
        {
            // 초기화
            Init();
		
            while(true)
            {
                // 턴 시작 초기화 및 이벤트 (Sequence Stack)
                ExecuteInitTurn();
		
                while(_sequenceStack.Count > 0)
                {
                    // Object에게 동작 명령
                    var actionReport = _sequenceStack.First().CombatHandler.Action();
			
                    #region 반환된 보고서를 기반으로 로직 수행
                    
                    // 만약 아군이든 적군이든 죽으면 수행
                    if (actionReport.UpdateObjectState)
                    {
                        
                    }
                    
                    // if, speed is changed then update sequence.
                    if(actionReport.UpdateSpeed)
                    {
                        UpdateSequenceStack();
                    }
                    
                    #endregion
                }
            }
            // 종료
            End();

            return true;
        }

        #region 전체적인 전투 세팅

        // 전체적인 전투 세팅 
        private void Init()
        {
            // Init
            _combatants = new List<ObjectInfo>();

            // 전투 전 이벤트 실행
            ExecutePreCombatEvent();
        }

        private void ExecutePreCombatEvent()
        {
            
        }

        private void End()
        {
            
        }

        private void ExecutePostCombatEvent()
        {
            
        }
        #endregion

        #region 전투 진행 중 수행 함수
        private void ExecuteInitTurn()
        {
            
        }

        private void UpdateSequenceStack()
        {
            // 턴이 종료되지 않은 Ready 상태인...
            _sequenceStack = (Stack<ObjectInfo>) _combatants.Where( x=> x.CombatHandler.State == State.Ready )
                                                            .OrderByDescending(x=>x.State.Speed)
                                                            .Select(x=>x);
        }
        #endregion
        

    }
}