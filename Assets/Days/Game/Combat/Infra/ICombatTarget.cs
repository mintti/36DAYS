using System;
using System.Collections.Generic;
using Days.Game.Object.Infra.Model;
using Days.Resource.Model;
using Days.Util.Infra;

namespace Days.Game.Combat.Infra
{
    /// <summary>
    /// 스킬 사용되는 주체/대상으로써 필요한 정보
    /// </summary>
    public interface ICombatTarget
    {
        int GetIndex();
        EntityType GetEntityType();
        
        ICombatViewModel GetViewModel();
        ICombatInfo GetCombatInfo();

        ICombatBehavior GetBehavior();
        void ExecuteTurnEnd();
        
        // Action
        void Move(Grid grid);
        void Hit();
        void BeHit(ushort damage);
        void Buff(SkillInfo skill, ushort volume);
        void Debuff();
    }
}