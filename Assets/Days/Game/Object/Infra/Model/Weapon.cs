namespace Days.Game.Object.Infra.Model
{
    public abstract class Weapon
    {
        public byte Index { get; set; }
        public ushort Damage { get; set; }
        public byte Weight { get; set; }
        public Stat SpecialAbility { get; set; }
        
        
    }
}