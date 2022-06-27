using System;
using System.Collections.Generic;
using System.Linq;
using Days.Game.Script;
using Days.UI.Infra;
using Days.UI.ViewModel;
using Days.UI.ViewModel.Popup;
using UnityEngine;

namespace Days.UI.Script
{
    
    public class PopupController : MonoBehaviour
    {
        private enum Popup : int
        {
            UnitListPopup = 0,
            DungeonInfoPopup,
            DummyPopup,
        }
        
        private UIManager _uiManager;
        private List<PopupHandler> _popupHandlers;
        
        // Start is called before the first frame update
        void Start()
        {
            var gameManager = FindObjectsOfType<GameManager>()?.First();
            
            // 팝업 수집 및 하이어라키 배치 순서대로 정렬
            var handlers = FindObjectsOfType<PopupHandler>();
            Array.Sort(handlers, (a, b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));
            _popupHandlers = handlers.ToList();
            
            
            if (gameManager != null)
            {
                _uiManager = gameManager.GetUIManager();
                _uiManager.ConnectPopupController(this);
                
                foreach (var pvm in _popupHandlers)
                {
                    pvm.gameObject.SetActive(true);
                    pvm.InitChildren(gameManager.GetPlayerData());
                    pvm.gameObject.SetActive(false);
                }
            }
        }

        #region Show Popup

        public void ShowDungeonPopup(int dungeonIndex)
        {
            _popupHandlers[(int)Popup.DungeonInfoPopup].PopupActive(dungeonIndex);
        }
        #endregion
    }
}
