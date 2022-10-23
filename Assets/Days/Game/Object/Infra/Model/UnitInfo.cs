using System.Collections.Generic;
using System.Linq;
using Days.Game.Background.Infra;
using Days.Resource;
using Days.Resource.Model;
using NotImplementedException = System.NotImplementedException;

namespace Days.Game.Object.Infra.Model
{
    public class UnitInfo : ICombatInfo
    {
        public byte Index { get; set; }
        public string Name { get; set; }
        public Stat Stat { get; set; }
        public Character Character { get; set; }
        public Weapon Weapon { get; set; }
        
        public ObjectState ObjectState { get; set; }

        public CurrentStatus CurrentStatus;
        private ICombatInfo _combatInfoImplementation;

        #region ICombatInfo
        public CurrentStatus GetCurrentStatus()
        {
            return CurrentStatus.Clone() as CurrentStatus;
        }

        public List<SkillModel> GetSkillList()
        {
            return ResourceManager.SkillList[Character.Index].ToList();
        }

        public Stat GetStat()
        {
            return Stat.Clone() as Stat;
        }

        public List<byte> SkillInfo()
        {
            throw new NotImplementedException();
        }

        #endregion
        /// <summary>
        /// (Background) Status Effect 적용
        /// </summary>
        public void ExecuteStatusEffect(StatusEffect statusEffect)
        {
            CurrentStatus.ExecuteStatusEffect(statusEffect.CurrentStatus);
        }
    }
}