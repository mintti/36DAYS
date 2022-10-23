using System.Collections.Generic;
using Days.Game.Object.Infra.Const;

namespace Days.Game.Object.Infra.Model
{
    /// <summary>
    /// [DB] 
    /// </summary>
    public class PartyModel
    {
        public byte DungeonIndex { get; set; }
        public ushort Key { get; set; }
        
        public PartyState State { get; set; }
        public List<byte> UnitsIndex { get; set; }
        public ushort RunningDistance { get; set; }
        public ushort GoalLength { get; set; }
        
        public List<PartyEvent> Events { get; set; }
    }
}