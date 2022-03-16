using System.Collections.Generic;

namespace Days.Resource.Model
{
    public abstract class DungeonModel
    {
        public byte Index { get; set; }
        public ushort TotalLength { get; set; }
        public List<byte> Monsters { get; set; }
        public List<byte> Events { get; set; }
    }
}