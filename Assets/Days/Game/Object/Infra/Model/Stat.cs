using System;

namespace Days.Game.Object.Infra.Model
{
    public class Stat : ICloneable
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


        public Stat()
        {
            
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}