using System;
using System.Collections.Generic;
using Days.Game.Combat.Skill;

namespace Days.Game.Combat.Enemy
{   
    public abstract class SkillRoutine
    {
        protected Func<byte> Search;
        protected Func<byte,byte> TargetTrace;
        public abstract void Execute();
    }

    


}