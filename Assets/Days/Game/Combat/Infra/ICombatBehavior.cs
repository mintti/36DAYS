using System;
using System.Collections.Generic;
using Days.Game.Combat.Script;
using Days.Resource.Model;
using Days.Util.Infra;
using UnityEngine.PlayerLoop;

namespace Days.Game.Combat.Infra
{
    /// <summary>
    /// 행동 제어 인터페이스
    /// </summary>
    public interface ICombatBehavior
    {
        /// <summary>
        /// 유닛 초기화
        /// </summary>
        void  Init(ICombatTarget caster);

        /// <summary>
        /// 자동 액션용 초기화
        /// </summary>
        void Init(ICombatTarget caster,
            Func<ICombatTarget> targetSearchAction,
            Action<int> targetTraceAction,
            Func<ICombatTarget, SkillModel, ICombatTarget, bool> checkToUseSkillAction);
        
        
        /// <summary>
        /// 자동 수행 로직이 존재할 때, 해당 메서드를 통해, 하단의 대상에 전달
        /// </summary>
        void Execute();

        /// <summary>
        /// 동작 수행
        /// arg : 동작 수행을 위해 추가 입력이 필요한 경우 해당 필드를 통해 전달 
        /// </summary>
        void Execute(SkillModel skill, List<ICombatTarget> targets);
    }
}