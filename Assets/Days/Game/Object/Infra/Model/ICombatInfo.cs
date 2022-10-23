using System.Collections.Generic;
using Days.Game.Combat.Skill;
using Days.Resource.Model;

namespace Days.Game.Object.Infra.Model
{
    /// <summary>
    /// 스탯 정보 인터페이스
    /// </summary>
    public interface ICombatInfo
    {
        CurrentStatus GetCurrentStatus();
        
        List<SkillModel> GetSkillList();

        Stat GetStat();
    }
}