namespace Days.Game.Infra
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DungeonModel
    {
        public int Index { get; set; }
        public int CurrentLength { get; set; }
        public ushort TotalLength { get; set; }
        public byte DungeonKey { get; set; }

        public void UpdateData()
        {
            
        }

        public void AppendReward()
        {
            
        }
    }
}