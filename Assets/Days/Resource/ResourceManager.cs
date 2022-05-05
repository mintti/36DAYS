using System.Collections.Generic;
using Days.Data.Script;
using Days.Game.Object.Infra;
using Days.Resource.Model;

namespace Days.Resource
{
    public static class ResourceManager
    {
        /*==============================================================
                                  Unit Info
        ==============================================================*/ 
        public static List<Weapon> WeaponList { get; set; }
        public static List<Character> CharacterList { get; set; }

        /*==============================================================
                                  Dungeon Info
        ==============================================================*/ 
        public static List<ObjectModel> MonsterList { get; set; }
        public static List<Dungeon> DungeonList { get; set; }
    }
}