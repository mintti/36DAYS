using System.Collections.Generic;

namespace Days.Resource.Model
{
    /// <summary>
    /// [Resource] 던전이 가지는 정보 
    /// </summary>
    public abstract class Dungeon
    {
        public byte Index { get; set; }
        public ushort TotalLength { get; set; }
        public List<byte> Monsters { get; set; }
        public List<byte> Events { get; set; }
    }
}