using Days.Game.Combat.Infra;
using Days.Resource.Model;
using UnityEngine;

namespace Days.Game.Combat.ViewModel
{
    public class CombatSkillViewModel : MonoBehaviour
    {
        private SkillModel _skill;

        /// <summary>
        /// 스킬 상태
        /// </summary>
        private CombatSkillState _state;

        public void Init(SkillModel skill)
        {
            _skill = skill;
        }
        
        public void UpdateSkillState(CombatSkillState state)
        {
            _state = state;
        }
    }
}
