using System.Collections.Generic;

namespace Days.Game.Object.Infra
{
    public enum ObjectState
    {
        WAIT,       // 대기
        GUARD,      // 경계
        PARTY,      // 파티 상태
        DIE         // 죽음
        
    }
    public class State
    {
        /*==============================================================
                                    기본 스텟
        ==============================================================*/ 
        public ushort Hp { get; set; }
        public ushort Power { get; set; }
        public ushort Speed { get; set; }
        
        /*==============================================================
                                    부가 스텟
        ==============================================================*/ 
        public byte AttackSpeed { get; set; }
        public byte Evasion { get; set; }
        public byte Guard { get; set; }
        public byte MagicGuard { get; set; }


        public State()
        {
            
        }

    }

    public class Weapon
    {
        public byte Index { get; set; }
        public ushort Damage { get; set; }
        public byte Weight { get; set; }
        public State SpecialAbility { get; set; }
    }

    public class Character
    {
        /// <summary>
        /// 직업이 가지는 기본 스텟
        /// </summary>
        public State BaseState { get; set; }
        
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

        /// <summary>
        /// 직업이 가지는 기본 스텟 설정
        /// </summary>
        public Character(ushort hp, ushort power, ushort speed)
        {
            BaseState = new State()
            {
                Hp = hp,
                Power = power,
                Speed = speed,
            };
        }
    }
    
    // Party ... 
    public enum PartyState
    {
        /// <summary> 기본 상태 </summary>
        DEFAULT,
        /// <summary> 전투 중 </summary>
        FIGHT,
        /// <summary> 이벤트 발생 </summary>
        EVENT,
        /// <summary> 이벤트 진행 중 </summary>
        EVENT_WAIT,
        /// <summary> 목적지 도착 </summary>
        ARRIVAL,
        /// <summary> 도망 </summary>
        RETREAT,
        /// <summary> 전멸 </summary>
        DIE
    }
}