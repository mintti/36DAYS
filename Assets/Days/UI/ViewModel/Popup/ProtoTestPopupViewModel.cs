using System.Collections;
using System.Collections.Generic;
using Days.Data.Infra;
using Days.UI.Infra;
using UnityEngine;

namespace Days.UI.ViewModel
{
    public class ProtoTestPopupViewModel : MonoBehaviour, IPopupViewModel
    {
        private PlayerData _playerData;
        public void Init(PlayerData playerData)
        {
            _playerData = playerData;
        }

        
        
        
    }
}