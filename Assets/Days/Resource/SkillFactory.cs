using System;
using Days.Game.Combat.Infra;
using Days.Resource.Model;
using Days.Util.Infra;

namespace Days.Resource
{
    public static class SkillFactory
    {
        public static SkillActionInstance GetSkillInstance(SkillModel skill)
        {
            var instance = new SkillActionInstance(skill);

            instance ??= new SkillActionInstance();
            return instance;
            
        }
    }

    public class SkillActionInstance
    {
        private ICombatTarget _caster;
        private ICombatTarget _target;
        
        private delegate void ActionDel();
        private event ActionDel targetAction;
        private event ActionDel casterAction;

        public SkillActionInstance()
        {
        }
        
        public SkillActionInstance(SkillModel skill)
        {
            foreach (var skillInfo in skill.SkillTypeList)
            {
                switch (skillInfo.Type)
                {
                    case SkillType.None:
                        break;
                    case SkillType.Move:
                        break;
                    case SkillType.Entity:
                        break;
                    case SkillType.Grid:
                        break;
                    // Attack
                    case SkillType.Attack:
                    case SkillType.Magic:
                        targetAction += () =>
                        {
                            ushort power = _caster.GetCombatInfo().GetCurrentStatus().GetPower();
                            _target.BeHit(power);
                        };
                        break;
                    //Buff
                    case SkillType.Heal:
                        targetAction += () =>
                        {
                            ushort power = _caster.GetCombatInfo().GetCurrentStatus().GetPower();
                            _target.Buff(skillInfo, power);
                        };
                        break;
                    case SkillType.Cure:
                    case SkillType.Purify:
                    case SkillType.Strengthen:
                        break;
                    case SkillType.Weaken:
                        break;
                    case SkillType.Poison:
                        break;
                    case SkillType.Stun:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }   
            }
        }

        public void ExecuteToTarget(ICombatTarget caster, ICombatTarget target)
        {
            _caster = caster;
            _target = target;
            targetAction?.Invoke();
        }

        public void ExecuteToCaster(ICombatTarget caster)
        {
            _caster = caster;
            _target = caster;
            targetAction?.Invoke();
        }
    }
}