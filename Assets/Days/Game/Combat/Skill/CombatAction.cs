using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Days.Game.Combat.Infra;
using Days.Game.Combat.Script;
using Days.Resource;
using Days.Resource.Model;
using Days.Util.Infra;

namespace Days.Game.Combat.Skill
{
    /// <summary>
    /// 베이스
    /// 스킬 동작 부분은 SKill 참고
    /// </summary>
    public class CombatAction
    {
        /// <summary>
        /// 해당 스킬의 실질적인 동작 
        /// </summary>
        public SkillActionInstance ActionInstance { get; set; }
        
        public virtual void Execute(ICombatTarget caster)
        {
            // nothing
        }
        
        public virtual void Execute(ICombatTarget caster,
                                    Grid grid)
        {
            // nothing
        }

        public virtual void Execute(SkillModel skill,
                                    ICombatTarget caster,
                                    List<ICombatTarget> targets)
        {
            // nothing
        }
        
        //public abstract List<Vector2> GetArea();
    }
    
    /// <summary>
    /// 이동
    /// </summary>
    public class Move : CombatAction
    {
        public override void Execute(ICombatTarget caster,
                             Grid grid)
        {
            caster.GetViewModel().SetPosition(grid.GetVector());
        }
    }
}