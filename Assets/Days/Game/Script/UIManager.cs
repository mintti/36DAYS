using System;
using System.Collections;
using Days.Data.Infra;
using UnityEngine;
using Days.Util.Infra;

using Days.Game.ViewModel;
using Days.UI.Script;

namespace Days.Game.Script
{
    public delegate void GameDel(GameData data);
    public delegate void PlayerDel(PlayerData data);
    
    /// <summary>
    /// 
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        private GameManager _gameManager;

        #region Variables
        /// <summary>
        /// Game Data가 업데이트 될 때마다 실행되는 이벤트들
        /// </summary>
        private event GameDel GamaDataEvent;
        
        /// <summary>
        /// Player Data가 업데이트 될 때마다 실행되는 이벤트들
        /// </summary>
        private event PlayerDel PlayerDataEvent;
        
        
        private MapController _mapController;
        private PopupController _popupController;
        #endregion  

        #region Method 
        /// <summary>
        /// Init UI Manager 
        /// </summary>
        public bool Init(GameManager gameManager)
        {
            _gameManager = gameManager;

            _mapController = null;
            _popupController = null;
            return true;
        }



        #region Setting Method
        /// <summary>
        /// 각 컨드롤러들을 세팅
        /// </summary>
        public void InitView()
        {
            StartCoroutine(nameof(SetControllerAsync));
        }

        /// <summary>
        /// UI Manager와 관련된 컨트롤러들을 설정을 수행. 그 후 Player Data 기반으로 화면을 설정
        /// </summary>
        private IEnumerator SetControllerAsync()
        {
            // 각 컨트롤들이 전부 전달될 때까지 대기
            yield return new WaitUntil(() => _mapController != null);
            yield return new WaitUntil(() => _popupController != null);
            
            Debug.Log("[UI Manager] Completed setting of ui-related controllers.");
            
            SetView();
            
            _gameManager.CompletedViewSetting();
        }
        
        #region Set Controllers
        /// <summary>
        /// Connect MapController. ViewModel에서 Map Controller가 자신 객체를 직접 전달
        /// </summary>
        public void ConnectMapController(MapController mapController) => _mapController = mapController;
        public void ConnectPopupController(PopupController popupController) => _popupController = popupController;

        public MapController GetMapController() => _mapController;
        public PopupController GetPopupController() => _popupController;

        #endregion
        #endregion
       
        
        
        #endregion
        
        #region Periodic Method
        /// <summary>
        /// Game data가 즉각적으로 반영되어야하는 UI들 추가
        /// </summary>
        public void AddGameDataEvent(GameDel func) =>  GamaDataEvent += func;
        
        /// <summary>
        /// Player data가 즉각적으로 반영되어야하는 UI들 추가
        /// </summary>
        public void AddPlayerDataEvent(PlayerDel func) => PlayerDataEvent += func;

        public void RemoveGameDataEvent(GameDel func) =>  GamaDataEvent -= func;
        public void RemovePlayerDataEvent(PlayerDel func) => PlayerDataEvent -= func;
        
        /// <summary>
        /// Game Data 업데이트에 따라 등록된 UI 이벤트들에 데이타 전달
        /// </summary>
        public void UpdateGameDataView(GameData gameData)
        {
            GamaDataEvent?.Invoke(gameData);
        }

        /// <summary>
        /// Player Data 업데이트에 따라 등록된 UI 이벤트들에 데이타 전달
        /// </summary>
        public void UpdatePlayerDataView(PlayerData playerData)
        {
            PlayerDataEvent?.Invoke(playerData);
        }
        #endregion
    }
}