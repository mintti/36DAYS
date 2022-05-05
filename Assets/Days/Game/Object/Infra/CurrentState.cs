using Days.Game.Background.Infra;

namespace Days.Game.Object.Infra
{
    public class CurrentState
    {
        public ushort Hp { get; set; }
        public ushort Mp { get; set; }
        public byte Stress { get; set; }
        public byte Gauge { get; set; }
        public byte Hunger { get; set; }


        public void ExecuteStatusEffect(CurrentState currentState)
        {
            Hp += currentState.Hp;
            Mp += currentState.Mp;
            Stress += currentState.Stress;
            Gauge += currentState.Gauge;
            Hunger += currentState.Hunger;
        }
    }
}