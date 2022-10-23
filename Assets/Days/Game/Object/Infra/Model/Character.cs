using Days.Resource.Model;
using UnityEngine;

namespace Days.Game.Object.Infra.Model
{
    public class Character
    {
        public int Index { get; set; }
        /// <summary>
        /// 직업이 가지는 기본 스텟
        /// </summary>
        public Stat BaseStat { get; set; }
        
        /// <summary>
        /// 무기가 가지는 기본 공격력
        /// </summary>
        public ushort Damage { get; set; }
        
        /// <summary>
        /// 특수 능력치 / 특수 스킬에서 사용 가능
        /// </summary>
        public byte SpecialAbility { get; set; }

        public Character()
        {
            
        }

        public Sprite JobIcon { get; set; }
        
        /// <summary>
        /// 직업이 가지는 기본 스텟 설정
        /// </summary>
        public Character(ClassType classType, ushort hp, ushort power, ushort speed)
        {
            Index = (int)classType;  
            
            BaseStat = new Stat()
            {
                Hp = hp,
                Power = power,
                Speed = speed,
            };
        }
    }
}