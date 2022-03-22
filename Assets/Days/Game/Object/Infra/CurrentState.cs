namespace Days.Game.Object.Infra
{
    public abstract class CurrentState
    {
        public ushort Hp { get; set; }
        public ushort Mp { get; set; }
        public byte Stress { get; set; }
        public byte Gauge { get; set; }
        public byte Hunger { get; set; }
    }
}