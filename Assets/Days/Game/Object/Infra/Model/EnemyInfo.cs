using System.Collections.Generic;
using Days.Game.Combat.Skill;
using Days.Resource;
using Days.Resource.Model;
using NotImplementedException = System.NotImplementedException;

namespace Days.Game.Object.Infra.Model
{
    /// <summary>
    /// 해당 객체 생명주기 조절
    /// </summary>
    public class EnemyInfo : ICombatInfo
    {
        private Enemy _enemyData;
        private CurrentStatus _currentStatus;
        private Stat _stat;
        public void Init(Enemy enemy)
        {
            _enemyData = enemy;
            _stat = enemy.BaseStat.Clone() as Stat;
            if (_stat != null)
            {
                _currentStatus = new CurrentStatus(_stat);
            }
        }
        
        public CurrentStatus GetCurrentStatus()
        {
            return _currentStatus;
        }

        public List<SkillModel> GetSkillList()
        {
            var list = new List<SkillModel>();
            
            foreach (var skillIdx in _enemyData.SkillList)
            {
                list.Add(ResourceManager.GetEnemySkill(skillIdx));
            }

            return list;
        }

        public Stat GetStat()
        {
            return _stat;
        }
    }
}