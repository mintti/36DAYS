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
        public ushort Hp { get; set; }
        public ushort CurrentHp { get; set; }
        public ushort Power { get; set; }
        public ushort Speed { get; set; }
        
        public byte AttackSpeed { get; set; }
        public byte Evasion { get; set; }
        public byte Guard { get; set; }
        public byte MagicGuard { get; set; }
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
        public byte Index { get; set; }
        public State BaseState { get; set; }
        
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