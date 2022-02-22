using System.Collections.Generic;

namespace Days.Game.Object.Infra
{
    /// <summary>
    /// Object 구성으로 이루어진 Party
    /// </summary>
    public class Party
    {
        public int Index;           // Party Index
        public List<int> Object;    // Unit Index
        
        
        public ushort GetAvgSpeed()
        {
            return default;
        }
    }
}