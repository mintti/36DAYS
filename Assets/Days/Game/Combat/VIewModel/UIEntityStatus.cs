using System;
using Days.Game.Combat.Infra;
using Days.Game.Combat.Script;
using Days.Game.Object.Infra.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Days.Game.Combat.ViewModel
{
    /// <summary>
    /// UI에 표시되는 개체의 정보
    /// </summary>
    public class UIEntityStatus : MonoBehaviour
    {
        #region External Variable
        public Text HpText;
        #endregion

        private RectTransform _rect;
        private ICombatInfo _info;
        private ICombatViewModel _vm;
        public void Init(CombatEntityHandler handler)
        {
            _rect = this.GetComponent<RectTransform>();
            
            _info = handler.GetCombatInfo();
            _vm = handler.GetViewModel();
            _vm.ConnectedUI(this);
            UpdateStatus();
        }

        /// <summary>
        /// UI 표시 위치를 Entity가 이동한 위치로 변경
        /// </summary>
        private void UpdatePosition()
        {
            _rect.position = _vm.GetScreenPosition();
        }
        
        /// <summary>
        /// 상태가 변경됨
        /// </summary>
        public void UpdateStatus()
        {
            UpdatePosition();
            HpText.text = GetHpText();
        }

        private string GetHpText()
        {
            var status = _info.GetCurrentStatus();
            var stat = _info.GetStat();
            return $"{status.Hp.ToString()}/{stat.Hp.ToString()}";
        }
        
        public void Destroy()
        {
            Destroy(this);
        }
    }
}