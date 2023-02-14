using System;
using System.Runtime.Remoting.Messaging;

namespace Days.Game.Object.Infra.Model
{
    public class CurrentStatus : ICloneable
    {
        private Stat _baseStat { get; }

        private ushort _hp;
        public ushort Hp
        {
            get => _hp;
            set
            {
                _hp = value;
                
                // 체력은 상한치를 넘을 수 없음
                if (_hp > _baseStat.Hp)
                {
                    _hp = _baseStat.Hp;
                }
            }
        }
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
            _baseStat = stat;
            
            Hp = stat.Hp;
            Power = stat.Power;
            Speed = stat.Speed;
        }
        
        /// <summary>
        /// 효과 적용 메서드
        /// </summary>
        /// <param name="currentState">전체 효과</param>
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