using System;
using Days.Game.Combat.Infra;
using Days.Game.Combat.Script;
using UnityEngine;

namespace Days.Game.Combat.ViewModel
{
    public class FieldViewModel : MonoBehaviour
    {
        private FieldController _fieldController;
        private SpriteRenderer _renderer;

        /// <summary>
        /// 현재 필드 위에 존재하는 Entity의 정보
        /// </summary>
        public ICombatTarget OnEntity;

        public void Start()
        {
            _renderer ??= GetComponent<SpriteRenderer>();
        }

        public void Init(FieldController fieldController, int x, int y)
        {
            _fieldController = fieldController;

            transform.localPosition = new Vector2(x, y);
            
            ChangeFieldType(FieldType.None);
        }

        #region User Event(Behavior)
        /// <summary>
        /// 필드 선택 이벤트
        /// </summary>
        private void OnMouseUp()
        {
            _fieldController.SelectedEvent(this);
        }
        #endregion

        #region Field Type Change

        public void ChangeFieldType(FieldType type)
        {
            _renderer ??= GetComponent<SpriteRenderer>();
            
            switch (type)
            {
                case FieldType.None :
                    _renderer.color = Color.white;
                    break;
                case FieldType.Select :
                    _renderer.color = Color.green;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region 필드 위 타겟 관리

        public void EnterTarget(ICombatTarget target)
        {
            if (target != null)
            {
                OnEntity = target;   
            }
        }

        public void ExitTarget()
        {
            OnEntity = null;
        }
        #endregion
    }
}