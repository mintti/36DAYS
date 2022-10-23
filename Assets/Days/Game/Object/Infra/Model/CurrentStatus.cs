using System;
using System.Runtime.Remoting.Messaging;

namespace Days.Game.Object.Infra.Model
{
    public class CurrentStatus : ICloneable
    {
        public ushort Hp { get; set; }
        public ushort Mp { get; set; }
        public byte Stress { get; set; }
        public byte Gauge { get; set; }
        public byte Hunger { get; set; }
        
        private ushort Power { get; set; }
        private ushort Speed { get; set; }

        public CurrentStatus(){}
        /// <summary>
        /// Test
        /// </summary>
        /// <param name="stat"></param>
        public CurrentStatus(Stat stat)
        {
            Hp = stat.Hp;
            Power = stat.Power;
            Speed = stat.Speed;
        }
        
        public void ExecuteStatusEffect(CurrentStatus currentState)
        {
            Hp += currentState.Hp;
            Mp += currentState.Mp;
            Stress += currentState.Stress;
            Gauge += currentState.Gauge;
            Hunger += currentState.Hunger;

            Power = currentState.Power;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region Get State

        public ushort GetPower() => Power;
        
        #endregion
    }
}