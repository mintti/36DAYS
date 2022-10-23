using System.ComponentModel;

namespace Days.Resource.Model
{
    public enum SkillType : byte
    {
        None = 0,
        // Common
        Move,
        Entity,
        Grid,

        // Hit
        Attack,
        Magic,
        
        // Buff
        [Description("물리")]
        Heal,
        [Description("정신")]
        Cure,
        [Description("정화")]
        Purify,
        [Description("강화")]
        Strengthen,
        
        // Debuff
        [Description("약화")]
        Weaken,
        Poison,
        
        // CC
        Stun,
    }

    public struct SkillInfo
    {
        public SkillType Type { get; set; }
        public ushort Number { get; set; }
    }
}