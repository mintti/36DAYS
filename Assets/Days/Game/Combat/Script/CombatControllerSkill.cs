using System;
using System.Collections.Generic;
using System.Linq;
using Days.Game.Combat.Infra;
using Days.Resource.Model;
using UnityEngine;
using Grid = Days.Util.Infra.Grid;

namespace Days.Game.Combat.Script
{
    public partial class CombatController : MonoBehaviour
    {
        #region unit

        /// <summary>
        /// 사용자가 UI에서 사용할 스킬을 선택 완료
        /// </summary>
        // public void SelectedCombatAction(SkillModel skill, List<ICombatTarget> targets)
        // {
        //     var caster = _sequenceStack.First();
        //     caster.GetBehavior().Execute(objects);
        // }
       
        // /// <summary>
        // /// 사용자가 UI에서 사용할 스킬을 선택 완료
        // /// </summary>
        // public async void SelectedCombatAction(SkillModel skill)
        // {
        //     var caster = _sequenceStack.First();
        //     var objects = ExecuteCombatAction(skill);
        //     
        //     // 액션 사용
        //     caster.GetBehavior().Execute(objects);
        // }
        //
        //
        // /// <summary>
        // /// 사용자가 UI에서 사용할 스킬을 선택하여 대상 선택하는 것을 대기
        // /// </summary>
        // private object[] ExecuteCombatAction(SkillModel skill)
        // {
        //     var objects = new object[2];
        //     objects[0] = skill;
        //
        //     IEnumerable<Grid> gridList;
        //     switch (skill.SelectType)
        //     {
        //         case SelectType.Grid:
        //             gridList = FieldController.SelectCombatAction(skill);
        //             objects[1] = gridList.First();
        //             break;
        //         case SelectType.Target:
        //             gridList = FieldController.SelectCombatAction(skill);
        //
        //             var targets = new List<ICombatTarget>();
        //             foreach (var grid in gridList)
        //             {
        //                 // 타겟에 해당하는 대상을 구하여 전달
        //             }
        //
        //             objects[1] = targets;
        //             break;
        //     }
        //
        //     return objects;
        // }

        #endregion

        #region Auto Action : Target Searh
        private void EnemyTargetTrace(int target)
        {
            //Debug.Log($"{_entityList[target].name}에게 이동");
        }
        
        /// <summary>
        /// 근접의 유닛을 탐색 후 반환
        /// </summary>
        private ICombatTarget EnemyTargetSearchCloseBy()
        {
            return _entityList.FirstOrDefault(x => x._entityType == EntityType.Unit);
        }

        /// <summary>
        /// 스킬이 대상에게 사용 가능한지 체크
        /// </summary>
        /// <returns></returns>
        private bool CheckToUseSkill(ICombatTarget caster, SkillModel skill, ICombatTarget target)
        {
            if (true)
            {
                return false;
            }
            
            return true;
        }

        #endregion
    }
}