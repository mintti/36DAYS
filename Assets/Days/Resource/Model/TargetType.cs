namespace Days.Resource.Model
{
    public enum TargetType
    {
        None = 0,
        SingleTargetSkill,
        MultiTargetSkill,
        /// <summary>
        /// 상대 좌표
        /// </summary>
        RelativeNonTargetSkill,
        /// <summary>
        /// 절대 좌표 (필드 기준)
        /// </summary>
        AbsolutelyNonTargetSkill,
        /// <summary>
        /// 본인 위치 기준하여 전방향의 Area만큼 타겟 설정
        /// </summary>
        AreaNonTargetSkill
    }
}