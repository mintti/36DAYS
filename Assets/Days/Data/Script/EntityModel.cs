using Days.Game.Object.Infra;
using Days.Game.Object.Infra.Model;

namespace Days.Data.Script
{
    /// <summary>
    /// Object DB
    /// </summary>
    public class EntityModel
    {
        public byte Index { get; set; }
        public string Name { get; set; }
        public int ClassIndex  { get; set; }
        public byte Weapon { get; set; }
        public CurrentStatus CurrentStatus { get; set; }
        
        //public Appearance Appearance { get; set; }
    }
}