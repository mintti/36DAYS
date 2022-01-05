using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Days.System;
using Days.Game;
using Days.Game.OS;
using Days.ViewModels;
using util = Days.Infra.Service.UtilityService;

namespace Days.Common
{
    public class MainManager : MonoBehaviour
    {
        private void Awake()
        {
            // 한 하늘 아래 MainManager 개체는 2개 일 수 없기에 ... 
            var obj = FindObjectsOfType<MainManager>();
            
            if ( obj is null)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (obj.First() != this)
                {
                    Destroy(gameObject);
                }
            }
            
            throw new NotImplementedException();
        }

        #region Child Manager

        private SystemManager _systemManager;
        private GameManager _gameManager;
        private OsManager _osManager;
        
        #endregion

        #region Variable
        
        private byte LoadCount = 0;
        

        #endregion

        #region Property

        public SystemManager GetSystemManager() { return _systemManager;}
        public GameManager GetGameManager() { return _gameManager;}
        public OsManager GetOsManager() { return _osManager;}

        #endregion
        
        
        public void ExecuteMainManager()
        {
            LoadCount++;
            
            _systemManager ??= GetComponent<SystemManager>();
            _gameManager ??= GetComponent<GameManager>();
            _osManager ??= GetComponent<OsManager>();
            
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

        /// <summary>
        /// 게임 실행 시, 리소스 초기화 및 사용자 데이타 로드
        /// </summary>
        private bool Initialized()
        {
            
            // 퍼블리싱? 
            
            // ----------- 비동기로 실행할 것 ------------
            
            // 화면 UI
            
            // Initialize resources and reading player data.
            if (!_systemManager.ExecuteSystemManager(this))
            {
                return false;
            }

            
            // -----------------------------------------
            
            return true;
        }

        #region This code related to the start scene.
        /// <summary>
        /// user action : 시작 화면에서 [게임 시작] 선택
        /// execution   : 메인 씬 이동 및 데이터 기반으로 게임 설정
        /// </summary>
        /// <returns> 시작 실패 시.. </returns>
        public bool ExecuteGameStart()
        {
            // ----------- 비동기로 실행할 것 ------------
            
            // 씬 이동 및 화면 UI
            
            // Game setting based on player data.
            if (!_gameManager.ExecuteGameManager((this)))
            {
                // 다시 메인 화면으로 이동 할 것...
                return false;
            }

            // -----------------------------------------

            return true;
        }

        #endregion
    }
}

