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
        public static List<Character> CharacterList { get; private set; }

        /*==============================================================
                                  Dungeon Info
        ==============================================================*/ 
        public static List<ObjectModel> MonsterList { get; set; }
        public static List<Dungeon> DungeonList { get; private set; }


        /// <summary>
        /// 임시로 사용되는 Init 
        /// </summary>
        public static void Init()
        {
            DungeonList = new List<Dungeon>()
            {
                new Dungeon(){Index = 0, Name = "첫번째 던전", TotalLength = 100},
                new Dungeon(){Index = 1, Name = "두번째 던전", TotalLength = 100},
                new Dungeon(){Index = 2, Name = "세번째 던전", TotalLength = 100},
                new Dungeon(){Index = 3, Name = "네번째 던전", TotalLength = 100},
                new Dungeon(){Index = 4, Name = "다섯번째 던전", TotalLength = 100},
                
            };
            
            CharacterList = new List<Character>()
            {
                new Character(10, 10, 10 ),    // 모험가
                new Character(10, 10, 10 ),    // 전사
                new Character(10, 10, 10 ),    // 마법사
                new Character(10, 10, 10 ),    // 성직자
                new Character(10, 10, 10 ),    // 궁수
                new Character(10, 10, 10 ),
            };   
        }
    }
}