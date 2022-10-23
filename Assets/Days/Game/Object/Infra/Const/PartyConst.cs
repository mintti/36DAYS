using System.Collections.Generic;

namespace Days.Game.Object.Infra.Const
{
    public static class PartyConst
    {
        public static readonly Dictionary<PartyEventType, PartyState> PartyEventInfo 
            = new Dictionary<PartyEventType, PartyState>()
        {
            {PartyEventType.Combat, PartyState.Fight},
            {PartyEventType.Trap, PartyState.Event},
            {PartyEventType.GetItem, PartyState.Event},
            {PartyEventType.HolyPlace, PartyState.EventWait},
        };
    }
}