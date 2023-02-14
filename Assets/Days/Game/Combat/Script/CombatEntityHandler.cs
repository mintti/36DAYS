using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Days.Game.Combat.Infra;
using Days.Game.Object.Infra.Model;
using Days.Resource.Model;
using Days.Util.Infra;
using UnityEngine;
using Grid = UnityEngine.Grid;

namespace Days.Game.Combat.Script
{
    /// <summary>
    /// 컨트롤러에서 전트 중인 오브젝트를 제어하기 위한 핸들러
    /// ObjectInfo에 대한 정보도 포함하고 있다.
    /// </summary>
    public class CombatEntityHandler : MonoBehaviour, ICombatTarget
    {
        private ICombatBehavior _combatBehavior { get; set; }
        private ICombatViewModel _viewModel;

        #region Combat-related Variables

        public EntityType _entityType { get; set; }
        /// <summary>
        /// 구분을 위해 전투 시작 시 부여받는 인덱스
        /// </summary>
        private int _index;
        
        /// <summary>
        /// 해당 클래스를 구성하기위해 기반이 되는 오브젝트 정보
        /// </summary>
        private ICombatInfo _baseInfo;
        
        /// <summary>
        /// 현재 오브젝트의 상태
        /// </summary>
        public Stack<CombatState> State { get; set; }
            
        
        public event Del ExecuteAction;
        public event Del PreAction;
        public event Del PostAction;

        public int GetSpeed() => _baseInfo.GetStat().Speed;
        public int GetIndex() => _index;
        public ICombatViewModel GetViewModel() => _viewModel;
        public ICombatBehavior GetBehavior() => _combatBehavior;
        public ICombatInfo GetCombatInfo() => _baseInfo;
        public EntityType GetEntityType() => _entityType;

        #endregion
        
        /// <summary>
        /// 공통 초기화
        /// </summary>
        /// <param name="index">컨트롤러에서 빠른 인덱스 값 찾기에 사용</param>
        /// <param name="entityType">Unit or Enemy</param>
        /// <param name="info">baseData</param>
        /// <param name="combatBehavior">행동규칙. 반드시 초기화가 완료된 값을 전달</param>
        /// <param name="turnEndAction">컨트롤러에 전달할 턴 종료 액션</param>
        public void Init(CombatController controller,
                         int index,
                         EntityType entityType,
                         ICombatInfo info,
                         ICombatBehavior combatBehavior,
                         Action turnEndAction)
        {
            // 초기화
            _index = index;
            _entityType = entityType;
            _baseInfo = info;
            _combatBehavior = combatBehavior;
            State = new Stack<CombatState>();
            
            // 오브젝트 정보를 기반으로 스킬 및 스텟 구성
            _viewModel = GetComponent<CombatEntityViewModel>();
            _viewModel.Init(this, info, controller.BattleCamera);

            
            // 턴 시작 전 이벤트 등록
            PreAction = new Del(() => _viewModel.TurnStart());

            // 턴 진행 이벤트 등록
            ExecuteAction = new Del(ExecuteBehavior);
            
            // 턴 종료 이벤트 등록
            PostAction = new Del(() => _viewModel.TurnEnd());
            PostAction += new Del(turnEndAction);
        }

        public void Execute()
        {
            PreAction?.Invoke();
            ExecuteAction?.Invoke();
        }
        
        /// <summary>
        /// 현재 개체의 상태를 확인하여, 동작 선택
        /// </summary>
        private void ExecuteBehavior()
        {
            switch (State.First())
            {
                case Infra.CombatState.Wait :
                    _combatBehavior.Execute();
                    break;
                case Infra.CombatState.Stun:
                    State.Pop();
                    // cc 이펙트
                    break;
                case Infra.CombatState.Die:
                    // 말이 없다.
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ChangeSelectMode(bool canSelect)
        {
            _viewModel.UpdateSelectMode(canSelect);
        }

        /// <summary>
        /// 턴 종료
        /// </summary>
        public void ExecuteTurnEnd()
        {
            PostAction?.Invoke();
        }

        #region Skill Action

        public void Move(Util.Infra.Grid grid)
        {
            _viewModel.SetPosition(new Vector2(){x = grid.x, y = grid.y});
        }
        public void Hit()
        {
            // Hit Action
        }

        public void BeHit(ushort damage)
        {
            CurrentStatus status =GetCombatInfo().GetCurrentStatus(); 
            status.Hp -= damage;
            if (status.Hp <= 0)
            {
                // 주금
            }
            GetViewModel().UpdateState();
        }

        public void Buff(SkillInfo skill, ushort volume)
        {
            switch (skill.Type)
            {
                case SkillType.Heal:
                    GetCombatInfo().GetCurrentStatus().Hp += volume;
                    break;
                default:
                    break;
            }
            GetViewModel().UpdateState();
        }

        public void Debuff()
        {
            
        }
        #endregion
    }
}
