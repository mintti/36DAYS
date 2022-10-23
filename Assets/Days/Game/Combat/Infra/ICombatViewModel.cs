using Days.Game.Combat.Script;
using Days.Game.Combat.ViewModel;
using Days.Game.Object.Infra.Model;
using UnityEngine;

namespace Days.Game.Combat.Infra
{
    /// <summary>
    /// 사용자 뷰 상에서의 엔티티 제어.
    /// 위치 및 효과 적용
    /// </summary>
    public interface ICombatViewModel
    {
        void Init(ICombatTarget handler, ICombatInfo combatInfo, Camera camera);
        void ConnectedUI(UIEntityStatus ui);
        void SetPosition(Vector3 position);

        void UpdateState();
        
        Vector3 GetPosition();
        Vector3 GetScreenPosition();

        void TurnStart();
        void TurnEnd();
    }
}