using System.Collections.Generic;
using Days.Game.Object.Infra.Model;

namespace Days.Resource.Model
{
    /// <summary>
    /// 몬스터 정보
    /// </summary>
    public class Enemy
    {
        /// <summary>
        /// Enemy Index
        /// </summary>
        public byte Index { get; set; }
        
        /// <summary>
        /// Dungeon Index
        /// </summary>
        public byte DungeonIndex { get; set; }
        
        /// <summary>
        /// 몬스터 이름
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 몬스터 난이도 
        /// </summary>
        public byte Level { get; set; }
        
        /// <summary>
        /// 몬스터가 가지는 기본 스텟
        /// </summary>
        public Stat BaseStat { get; set; }
        
        public List<int> SkillList { get; set; }

        // public ActionType ActionType { get; set; };
        
        // public Skin Skin { get; set; } 
    }
}