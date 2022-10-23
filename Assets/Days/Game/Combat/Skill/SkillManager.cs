using System.Runtime.CompilerServices;
using Days.Resource;
using Days.Resource.Model;
using UnityEngine;

namespace Days.Game.Combat.Skill
{
    public static class SkillManager
    {
        public static CombatAction GetSkill(byte classIndex, byte skillIndex)
        {
            var skill = ResourceManager.GetSkill(classIndex, skillIndex);

            CombatAction action ;

            switch (skill.TargetType)
            {
                case TargetType.SingleTargetSkill:
                    action = new SingleTargetSkill(skill.Area);
                    break;
                case TargetType.MultiTargetSkill:
                    action = new MultiTargetSkill()
                    {

                    };
                    break;
                case TargetType.AbsolutelyNonTargetSkill:
                    action = new AbsolutelyNonTargetSkill()
                    {

                    };
                    break;
                case TargetType.RelativeNonTargetSkill:
                    action = new RelativeNonTargetSkill();
                    break;  
                case TargetType.AreaNonTargetSkill:
                    action = new AreaNonTargetSkill(skill.Area);
                    break;
                default:
                    action = new Move();
                    break;
            }

            return action;
        }
    }
}