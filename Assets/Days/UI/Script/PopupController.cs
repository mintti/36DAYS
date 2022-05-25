using System.Collections.Generic;
using System.Linq;
using Days.Game.Script;
using Days.UI.Infra;
using Days.UI.ViewModel;
using UnityEngine;

namespace Days.UI.Script
{
    public class PopupController : MonoBehaviour
    {
        private GameManager _gameManager;
        
        private List<PopupHandler> _popupViewModels;
        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        void Init()
        {
            _gameManager = FindObjectsOfType<GameManager>()?.First();
            
            // 팝업 초기화
            _popupViewModels = FindObjectsOfType<PopupHandler>().ToList();

            if (_gameManager != null)
            {
                foreach (var pvm in _popupViewModels)
                {
                    pvm.gameObject.SetActive(true);
                    pvm.InitChildren(_gameManager.GetPlayerData());
                    pvm.gameObject.SetActive(false);
                }
            }
        }
    }
}
