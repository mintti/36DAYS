using System;
using Days.Game.Combat.Infra;
using Days.Game.Combat.Script;
using Days.Game.Object.Infra.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Days.Game.Combat.ViewModel
{
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
        }

        public void UpdatePosition()
        {
            _rect.position = _vm.GetScreenPosition();
        }
        
        public void UpdateStatus()
        {
            UpdatePosition();
            var status = _info.GetCurrentStatus();
            var stat = _info.GetStat();
            HpText.text = $"{status.Hp.ToString()}/{stat.Hp.ToString()}";
        }
        
        public void Destroy()
        {
            Destroy(this);
        }
    }
}