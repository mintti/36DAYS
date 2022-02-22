namespace Days.Data.Infra
{
    public class PlayerData
    {
        public ushort Day { get; set; }
        public string KeyCode { get; set; }

        public void UpdateDay() => Day++;
    }
}