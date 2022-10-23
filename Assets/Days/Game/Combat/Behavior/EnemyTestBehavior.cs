using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Days.Game.Combat.Infra;
using Days.Game.Combat.Script;
using Days.Game.Combat.Skill;
using Days.Resource.Model;
using Days.Util.Infra;
using UnityEngine;
using Grid = Days.Util.Infra.Grid;

namespace Days.Game.Combat.Behavior
{
    /// <summary>
    /// 기본 추척 및 단일 스킬을 사용하는 몬스터
    /// </summary>
    public class EnemyTestBehavior : ICombatBehavior
    {
        private List<SkillModel> _skillList;
        private Dictionary<byte, CombatAction> _skillActionDict;

        private ICombatTarget _caster;
        private Func<ICombatTarget> _targetSearchAction;
        private Action<int> _targetTraceAction;
        private Func<ICombatTarget, SkillModel, ICombatTarget, bool> _checkToUseSkillAction;

        public void Init(ICombatTarget caster)
        {
            // noting
        }

        /// <summary>
        /// 몬스터 초기화
        /// </summary>
        public void Init(ICombatTarget caster,
                             Func<ICombatTarget> targetSearchAction,
                             Action<int> targetTraceAction,
                             Func<ICombatTarget, SkillModel, ICombatTarget, bool> checkToUseSkillAction)
        {
            _caster = caster;
            _skillList = _caster.GetCombatInfo().GetSkillList();
            _skillActionDict = new Dictionary<byte, CombatAction>();
            
            _targetSearchAction = targetSearchAction;
            _targetTraceAction = targetTraceAction;
            _checkToUseSkillAction = checkToUseSkillAction;
        }
        
        public void Execute()
        {
            ICombatTarget target = _targetSearchAction?.Invoke();
            SkillModel skill = _skillList.First();
            
            // 스킬 사용이 유효
            if (_checkToUseSkillAction?.Invoke(_caster, skill, target) == true)
            {
                var targets = new List<ICombatTarget>() {target};
                Execute(skill, targets);
            }
            else
            {
                // 유효하지 않으면 대상 추적
                if (target != null)
                {
                    _targetTraceAction(target.GetIndex());
                }
            }
            
            _caster.ExecuteTurnEnd();
        }

        /// <summary>
        /// 스킬 사용을 위한 인덱스 및 정보 전달
        /// arg = [0]: skill Model  [1]: null/Grid/ICombatTargetList
        /// </summary>
        public void Execute(SkillModel skill, List<ICombatTarget> targets)
        {
            // 스킬 정보 로드
            var index = (byte) _skillList.IndexOf(skill);

            if (skill == null) return;

            // 스킬 정보가 없다면 스킬 정보를 로드
            if (!(_skillActionDict.ContainsKey(index)))
            {
                _skillActionDict.Add(index, SkillManager.GetSkill(skill.ClassIndex, skill.Index));
            }

            // 스킬 사용
            switch (skill.SelectType)
            {
                case SelectType.None:
                    _skillActionDict[index].Execute(_caster);
                    break;
                case SelectType.TargetWithinGrid:
                case SelectType.Target:
                    _skillActionDict[index].Execute(skill, _caster, targets);
                    break;
                default: break;
            }
        }
    }
}