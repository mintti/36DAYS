using System.Collections;
using System.Collections.Generic;
using Days.Data.Infra;
using Days.System.Script;
using UnityEngine;

namespace Days.Data.Script
{
    public class DataManager : MonoBehaviour
    {
        private MainManager _mainManager;
        private DataService _dataService;
        
        // property
        private PlayerData _playerData;
        public PlayerData GetPlayerData() => _playerData;

        /// <summary>
        /// First call
        /// </summary>
        public bool Init(MainManager mainManager)
        {
            _mainManager = mainManager;
            _dataService = GetComponentInChildren<DataService>();
            
            // Read local DB file.
            _playerData ??= _dataService.ReadPlayerData();
            if (_playerData is null)
            {
                Debug.Log("[DATA] Failed to read user data during the data initialized process.");
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// 저장
        /// </summary>
        public void Save(PlayerData playerData)
        {
            if (!_dataService.WritePlayerData(playerData))
            {
                // 저장 실패 메세지 및 방안
            }

            _playerData = playerData;
        }
    }
}