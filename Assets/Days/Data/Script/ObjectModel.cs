using Days.Game.Object.Infra;

namespace Days.Data.Script
{
    /// <summary>
    /// Object DB
    /// </summary>
    public class ObjectModel
    {
        public byte Index { get; set; }
        public string Name { get; set; }
        public byte Job  { get; set; }
        public byte Weapon { get; set; }
        public CurrentState CurrentState { get; set; }
        
        //public Appearance Appearance { get; set; }
    }
}