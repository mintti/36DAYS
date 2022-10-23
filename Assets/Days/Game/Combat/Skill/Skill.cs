using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Days.Game.Combat.Infra;
using Days.Game.Object.Infra.Model;
using Days.Resource;
using Days.Resource.Model;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;


/*
 * Target : *area, target
 *   SingleTarget : target = 1 
 *   MultiTarget  : target = n
 *
 * NonTarget
 *   Absolutely   : coordinate [9,4]
 *   Relative     : coordinate List<byte[,]>
 */
namespace Days.Game.Combat.Skill
{
    /// <summary>
    /// SKill Base
    /// </summary>
    public abstract class Skill : CombatAction
    {
        //ublic virtual SkillType SkillType { get; set; }

        /// <summary>
        /// 스킬 사용
        /// </summary>
        public virtual void Execute()//CurrentStatus state
        {
            
        }
        
        /// <summary>
        /// 예상 데미지 계산
        /// </summary>
        protected virtual int GetDamage(int damage)
        {
            return damage;
        }

        /// <summary>
        /// 효과 추가
        /// </summary>
        public virtual void MoreEffect()
        {
            
        }
    }

    /// <summary>
    /// 타겟 스킬
    /// </summary>
    public abstract class TargetSKill : Skill
    {
        // 허용 영역
        public byte Area { get; set; }
        protected byte Target { get; set; }
    }
    
    /// <summary>
    /// 논타겟 스킬
    /// </summary>
    public class NonTargetSkill : Skill
    {
    }
    
    /// <summary>
    /// 단일 타겟 스킬
    /// </summary>
    public class SingleTargetSkill : TargetSKill
    {
        public SingleTargetSkill(byte area)
        {
            Target = 1;
            Area = area;
        }
    }
    
    /// <summary>
    /// 다중 타겟 스킬 (횟수)
    /// </summary>
    public class MultiTargetSkill : TargetSKill
    {
        
    }

    /// <summary>
    /// 절대 좌표 스킬 (필드에 해당 위치)
    /// </summary>
    public class AbsolutelyNonTargetSkill : NonTargetSkill
    {
        public virtual byte[,] Coordinate { get; set; } = new byte[9, 4];
    }

    /// <summary>
    /// 상대 좌표 스킬 (오브젝트의 위치를 기준)
    /// </summary>
    public class RelativeNonTargetSkill : NonTargetSkill
    {
        public List<(byte horizen, byte vertical)> Coordinate { get; set; }
    }

    public class AreaNonTargetSkill : NonTargetSkill
    {
        public byte Area { get; private set; }

        public AreaNonTargetSkill(byte area)
        {
            Area = area;
        }

        public override void Execute(SkillModel skill, 
                                     ICombatTarget caster, 
                                     List<ICombatTarget> targets)
        {
            var action = SkillFactory.GetSkillInstance(skill);
            
            foreach (var target in targets)
            {
                // 대상이 아니면 continue;
                switch (skill.SelectType)
                {
                    case SelectType.TargetWithinGrid:
                    case SelectType.Target:
                        if(caster.GetEntityType().Equals(target.GetEntityType()))
                            continue;
                        break;
                    case SelectType.None :
                        break;
                    default:
                        break;
                }

                action.ExecuteToTarget(caster, target);
            }
            
            action.ExecuteToCaster(caster);
        }
    }
}