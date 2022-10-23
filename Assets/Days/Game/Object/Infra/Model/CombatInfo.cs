using System.Collections.Generic;
using Days.Resource.Model;

namespace Days.Game.Object.Infra.Model
{
    /// <summary>
    /// 전투 이벤트 정보가 담긴
    /// </summary>
    public class CombatInfo
    {
        public byte DungeonIndex { get; set; }
        
        public PartyModel PartyInfo { get; set; }
        public List<byte> EnemyList { get; set; }
    }
}