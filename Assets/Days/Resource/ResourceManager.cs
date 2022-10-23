using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Days.Data.Script;
using Days.Game.Combat.Infra;
using Days.Game.Combat.Skill;
using Days.Game.Object.Infra;
using Days.Game.Object.Infra.Model;
using Days.Resource.Model;

namespace Days.Resource
{
    public static class ResourceManager
    {
        /*==============================================================
                                  Unit Info
        ==============================================================*/ 
        private static List<Weapon> WeaponList { get; set; }
        private static List<Character> CharacterList { get; set; }

        /*==============================================================
                                  Dungeon Info
        ==============================================================*/ 
        public static List<Enemy> EnemyList { get; set; }
        public static List<SkillModel> EnemySkillList { get; set; }
        private static List<Dungeon> DungeonList { get; set; }
        public static SkillModel[][] SkillList { get; set; }

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
                new Character(ClassType.None ,10, 10, 10 ), 
                new Character(ClassType.Warrior, 10, 10, 10 ),  
                new Character(ClassType.Archer, 10, 10, 10 ),  
                new Character(ClassType.Magician,10, 10, 10 ),  
                new Character(ClassType.Bard,10, 10, 10 ), 
                new Character(ClassType.Magician,10, 10, 10 ),
            };


            var testState = new Stat() {Hp = 10, Power = 10, Speed = 10};
            var testSkills = new List<int>(){ (int)EnemySkillType.Hit};
            EnemyList = new List<Enemy>()
            {
                new Enemy(){Index = 0, DungeonIndex = 0, Level = 1, Name = "테스터A", BaseStat = testState, SkillList = testSkills},
                new Enemy(){Index = 1, DungeonIndex = 0, Level = 1, Name = "테스터B", BaseStat = testState, SkillList = testSkills},
                new Enemy(){Index = 2, DungeonIndex = 0, Level = 1, Name = "테스터C", BaseStat = testState, SkillList = testSkills},
                new Enemy(){Index = 3, DungeonIndex = 1, Level = 1, Name = "테스터D", BaseStat = testState, SkillList = testSkills},
                new Enemy(){Index = 4, DungeonIndex = 2, Level = 1, Name = "테스터E", BaseStat = testState, SkillList = testSkills}
            };

            #region 스킬 초기화
            SkillList = new SkillModel[5][];
            var classIndex = (byte) ClassType.Warrior;
            var testAttackSkillInfo = new SkillInfo() {Type = SkillType.Attack, Number = 100};
            var testHealSkillInfo = new SkillInfo() {Type = SkillType.Heal, Number = 100};
            SkillList[classIndex] = new SkillModel[2]
            {
                new SkillModel()
                {
                    Name = "찌르기",
                    Index = (int)WarriorSkill.Lunge,
                    ClassIndex = classIndex,
                    TargetType = TargetType.SingleTargetSkill,
                    SelectType = SelectType.Target,
                    SkillTypeList = new List<SkillInfo>(){testAttackSkillInfo}
                },
                new SkillModel()
                {
                    Name = "휘두르기",
                    Index = (int)WarriorSkill.Swing,
                    ClassIndex = classIndex,
                    TargetType = TargetType.AbsolutelyNonTargetSkill,
                    SelectType = SelectType.Target,
                    SkillTypeList = new List<SkillInfo>(){testAttackSkillInfo}
                },
            };
            classIndex = (int) ClassType.Archer;
            SkillList[(int) ClassType.Archer] = new SkillModel[1]
            {
                new SkillModel()
                {
                    Name = "쏘기",
                    Index = (int) ArcherSkill.Shoot,
                    ClassIndex = classIndex,
                    Area = 5,
                    TargetType = TargetType.SingleTargetSkill,
                    SelectType = SelectType.Target,
                    SkillTypeList = new List<SkillInfo>(){testAttackSkillInfo}
                },
            };
            classIndex = (int) ClassType.Magician;
            SkillList[(int) ClassType.Magician] = new SkillModel[1]
            {
                new SkillModel()
                {
                    Name = "파이어볼",
                    Index = (int) MagicianSkill.FireBall,
                    ClassIndex = classIndex,
                    Area = 5,
                    TargetType = TargetType.SingleTargetSkill,
                    SelectType = SelectType.Target,
                    SkillTypeList = new List<SkillInfo>(){testAttackSkillInfo}
                },
            };
            classIndex = (int) ClassType.Bard;
            SkillList[(int) ClassType.Bard] = new SkillModel[1]
            {
                new SkillModel()
                {
                    Name = "그레고리안",
                    Index = (int) BardSkill.Gregorian,
                    ClassIndex = classIndex,
                    Area = 2,
                    TargetType = TargetType.AreaNonTargetSkill,
                    SelectType = SelectType.TargetWithinGrid,
                    SkillTypeList = new List<SkillInfo>(){testHealSkillInfo}
                },
            };
            #endregion

            #region Enemy Skill

            EnemySkillList = new List<SkillModel>()
            {
                new SkillModel()
                {
                    
                },
                new SkillModel()
                {
                    Name = "공격",
                    Index =(int) EnemySkillType.Hit,
                    Area = 1,
                    TargetType = TargetType.SingleTargetSkill,
                    SelectType = SelectType.Target,
                    SkillTypeList = new List<SkillInfo>(){testAttackSkillInfo}
                }
            };
            #endregion
        }

        public static Character GetCharacter(int index) => CharacterList[index];
        public static Weapon GetWeapon(int index) => WeaponList[index];
        public static Dungeon GetDungeon(int index) => DungeonList[index];

        public static Enemy GetEnemy(int index) => EnemyList[index];
        public static SkillModel GetEnemySkill(int index) => EnemySkillList[index];
        public static SkillModel GetSkill(int classIndex, int skillIndex) => SkillList[classIndex][skillIndex];
        

    }
}