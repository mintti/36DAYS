using System.Collections.Generic;
using Days.Game.Infra;
using Days.Game.Object.Infra;
using Days.Util.Infra;

namespace Days.Data.Infra
{
    public class PlayerData
    {
        public PlayerData()
        {
            UnitList = new List<ObjectInfo>();
            DungeonList = new List<Dungeon>();
            PartyList = new List<Party>();
        }
        /*==============================================================
        Daily Data
        ==============================================================*/ 
        public ushort Day { get; set; }
        public string KeyCode { get; set; }

        public void UpdateDay() => Day++;
        
        /*==============================================================
        [Fixed] Player Data
        ==============================================================*/

        public List<ObjectInfo> UnitList { get; set; }
        public List<Dungeon> DungeonList { get; set; }

        /*==============================================================
        Dynamic Player Data
        ==============================================================*/ 
        public List<Party> PartyList { get; set; }

        
    }
}