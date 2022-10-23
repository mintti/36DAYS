using System.Collections.Generic;

namespace Days.Game.Combat.Enemy
{
    /// <summary>
    /// 정해진 스킬의 순서대로 사용
    /// </summary>
    public class SkillRoutineSequence : SkillRoutine
    {
        private List<byte> _skillList;
        private byte _count;
        
        public override void Execute()
        {
            var index = _skillList.Count % _count;
            var skill = _skillList[index];
            
            // 스킬 사거리 내 대상이 존재한다면 or 논 타겟 스킬일 경우
            if (false)
            {
                
            }
            else
            {
                // 대상의 방향으로 이동
                byte target = Search();
                TargetTrace(target);
            }
        }

        public bool FindTarget()
        {

            return true;
        }

        public SkillRoutineSequence()
        {
            _skillList = new List<byte>();
            _count = 0;
        }
    }
}