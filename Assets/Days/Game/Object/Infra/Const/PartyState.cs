namespace Days.Game.Object.Infra.Const
{
    public enum PartyState
    {
        /// <summary> 기본 상태 </summary>
        Default,

        /// <summary> 전투 중 </summary>
        Fight,

        /// <summary> 이벤트 발생 </summary>
        Event,

        /// <summary> 이벤트 진행 중 </summary>
        EventWait,

        /// <summary> 목적지 도착 </summary>
        Arrival,

        /// <summary> 도망 </summary>
        Retreat,

        /// <summary> 전멸 </summary>
        Die
    }
}