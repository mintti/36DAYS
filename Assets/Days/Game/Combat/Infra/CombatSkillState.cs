using System.ComponentModel;

namespace Days.Game.Combat.Infra
{
    /// <summary>
    /// 전투 스킬 상태 Enum
    /// </summary>
    public enum CombatSkillState
    {
        [Description("올 수 없음")]
        None,
        [Description("사용 가능한 상태")]
        Can,
        [Description("사용하기한 게이지가 모이지 않은 상태")]
        NoGauge,
        [Description("스킬 사용할 대상이 존재하지 않는 상태")]
        NoTarget,
        [Description("스킬이 봉인된 상태")]
        Lock,
    }
}