namespace Days.Game.Background.Infra
{
    public enum StatusEffectType : int
    {
        /// <summary>
        /// 주기적으로 일정한 횟수간
        /// </summary>
        Count = 0,
        /// <summary>
        /// 주기적으로 동작하는 타입
        /// </summary>
        Constant,
        /// <summary>
        /// 고정적인 타임
        /// </summary>
        Fixed,
        /// <summary>
        /// 특정한 조건일 때, 발생하는 타입
        /// </summary>
        Particular 
    }
}