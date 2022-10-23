using System;
using System.Collections.Generic;
using System.Threading;
using Days.Game.Combat.Infra;
using Days.Game.Combat.Script;
using Days.Game.Combat.Skill;
using Days.Resource;
using Days.Resource.Model;
using Days.Util.Infra;
using UnityEngine;
using Grid = Days.Util.Infra.Grid;

namespace Days.Game.Combat.Behavior
{
    /// <summary>
    /// 유닛이 가지는 행동 로직
    /// </summary>
    public class UnitBehavior : ICombatBehavior
    {
        private ICombatTarget _caster;
        private List<SkillModel> _skillList;
        private Dictionary<byte, CombatAction> _skillActionDict;

        public void Init(ICombatTarget caster)
        {
            _caster = caster;
            _skillList = _caster.GetCombatInfo().GetSkillList();
            _skillActionDict = new Dictionary<byte, CombatAction>();

        }

        public void Init(ICombatTarget caster, 
            Func<ICombatTarget> targetSearchAction,
            Action<int> targetTraceAction,
            Func<ICombatTarget, SkillModel, ICombatTarget, bool> checkToUseSkillAction)
        {
            // noting
        }

        public void Execute()
        {
            // noting
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
                CombatAction action = SkillManager.GetSkill(skill.ClassIndex, skill.Index);
                action.ActionInstance = SkillFactory.GetSkillInstance(skill);
                
                _skillActionDict.Add(index, action);
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

            _caster.ExecuteTurnEnd();
        }
    }
}