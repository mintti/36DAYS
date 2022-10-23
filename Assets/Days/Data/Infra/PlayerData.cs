using System.Collections.Generic;
using Days.Game.Infra;
using Days.Game.Object.Infra;
using Days.Game.Object.Infra.Model;
using Days.Util.Infra;

namespace Days.Data.Infra
{
    public class PlayerData
    {
        public PlayerData()
        {
            UnitList = new List<UnitInfo>();
            DungeonList = new List<DungeonModel>();
            PartyList = new List<PartyHandler>();
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

        public List<UnitInfo> UnitList { get; set; }
        public List<DungeonModel> DungeonList { get; set; }

        /*==============================================================
        Dynamic Player Data
        ==============================================================*/ 
        public List<PartyHandler> PartyList { get; set; }

        
    }
}