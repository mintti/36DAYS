using System.Collections;
using System.Collections.Generic;
using Days.Data.Infra;
using Days.UI.Infra;
using UnityEngine;

namespace Days.UI.ViewModel.Popup
{
    public class ProtoTestPopupViewModel : MonoBehaviour, IPopupViewModel
    {
        private PlayerData _playerData;

        #region IPopupViewModel
        public void Init(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void ActiveGameObject(object obj)
        {
            
        }
        #endregion
        
        
    }
}
