using System;
using System.Threading;
using Days.Game.Background.Infra;
using Days.Game.Combat.Script;
using Days.Game.Combat.ViewModel;
using Days.Game.Object.Infra;
using Days.Game.Object.Infra.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Days.Game.Combat.Infra
{
    public class CombatEntityViewModel : MonoBehaviour, ICombatViewModel
    {
        #region External Variable
        public GameObject TurnFlag;
        public Text Hp;
        public GameObject CanSelectTargetObject;
        #endregion

        private UIEntityStatus _ui;
        private Camera _camera;
        private ICombatTarget _handler;
        private ICombatInfo _objectInfo;
        public void Init(ICombatTarget handler, ICombatInfo combatInfo, Camera camera)
        {
            _camera = camera;
            
            _handler = handler;
            _objectInfo = combatInfo;
            TurnFlag.SetActive(false);
            CanSelectTargetObject.SetActive(false);

            GetComponent<SpriteRenderer>().color = _handler.GetEntityType() == EntityType.Unit ? Color.green : Color.magenta ;
        }

        public void ConnectedUI(UIEntityStatus ui) => _ui = ui;
        
        public void SetActive(bool value) => gameObject.SetActive(value);
        
        
        #region object positon control
        public void SetPosition(Vector3 position)
        {
            transform.localPosition = position;
        }
 
        public Vector3 GetPosition()
        {
            return transform.localPosition;
        }

        public Vector3 GetScreenPosition()
        {
            if (_camera != null)
            {
                var vec = _camera.WorldToScreenPoint(this.transform.position); 
                return vec; 
            }
            else return Vector3.zero;
        }
        
        #endregion

        #region 엔티티 상태에 따른 뷰 변경

        public void TurnStart()
        {
            TurnFlag.SetActive(true);
        }

        public void TurnEnd()
        {
            TurnFlag.SetActive(false);
        }

        /// <summary>
        /// 턴 업데이트 명령
        /// </summary>
        public void UpdateState()
        {
            _ui.UpdateStatus();
        }
    
        public void UpdateSelectMode(bool canSelect)
        {
            CanSelectTargetObject.SetActive(canSelect);
        }

        #endregion
    }
}