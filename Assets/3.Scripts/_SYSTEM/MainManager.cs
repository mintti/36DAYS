using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Days.System;
using Days.Game;
using util = Days.Service.UtilityService;

namespace Days.Common
{
    public class MainManager : MonoBehaviour
    {
        #region Child Manager

        private SystemManager _systemManager;
        private GameManager _gameManager;
        
        #endregion

        #region Variable

        private byte LoadCount = 0;
        

        #endregion
        
        public void ExecuteMainManager()
        {
            LoadCount++;
            
            _systemManager ??= GetComponent<SystemManager>();
            _gameManager ??= GetComponent<GameManager>();
            
            // set system ui 
            // Method()

            if (!Initialized())
            {
                if (LoadCount <= 5)
                {
                    ExecuteMainManager();
                }
                else
                {
                    // 시도 횟수 초과로 종료?
                }
            }
            else
            {
                LoadCount = 0;
            }
        }

        private bool Initialized()
        {
            if (!_systemManager.ExecuteSystemManager(this))
            {
                return false;
            }

            if (!_gameManager.ExecuteGameManager((this)))
            {
                return false;
            }
            
            return true;
        }
        
    }
}

