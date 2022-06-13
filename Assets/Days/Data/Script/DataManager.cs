using System;
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
        
        // property
        private PlayerData _playerData;
        public PlayerData GetPlayerData() => _playerData;

        /// <summary>
        /// First call
        /// </summary>
        public bool Init(MainManager mainManager)
        {
            _mainManager = mainManager;
            
            // Read local DB file.
            _playerData ??= ReadPlayerData();

            return true;
        }

        /// <summary>
        /// 로컬에 존재하는 사용자 데이타를 로드합니다.
        /// 기존 데이타가 존재하지 않는 경우, null로 설정합니다.
        /// </summary>
        public PlayerData ReadPlayerData()
        {
            // 기존 플레이어 데이터가 존재하지 않는 경우, null 리턴
            PlayerData playerData = null;

            try
            {
                if (false) 
                {
                    Debug.Log("reads the player data.");
                
                    playerData = new PlayerData();
                
                    Debug.Log("Completed to read the player data.");
                }
            }
            catch (Exception e)
            {
                playerData = null;
                Debug.Log("[DATA] Failed to read user data during the data initialized process.");
            }

            return playerData;
        }

        /// <summary>
        /// 사용자 데이타를 저장합니다. 
        /// </summary>
        /// <returns>저장 성공 여부</returns>
        public bool WritePlayerData(PlayerData playerData)
        {
            // 만일을 위해 data에 내용을 복사 후, 해당 내용을 저장합니다. 
            var data = playerData;

            try
            {

            }
            catch (Exception e)
            {
                // 저장 실패 메세지 및 방안
                return false;
            }
            
            _playerData = data;
            
            return true;
        }
    }
}