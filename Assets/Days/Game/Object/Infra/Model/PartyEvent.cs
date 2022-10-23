using Days.Game.Object.Infra.Const;

namespace Days.Game.Object.Infra
{
    public class PartyEvent
    {
        public bool Execution { get; set; }
        public ushort Point { get; set; }
        public PartyEventType EventType { get; set; }
    }
}