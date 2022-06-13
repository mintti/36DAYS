using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Days.Data.Infra;
using UnityEngine;
using UnityEngine.SceneManagement;

using Days.Game.Script;
using Days.Game.OS.Script;
using Days.Data.Script;
using Days.Resource;
using util = Days.Util.Script.UtilityService;

namespace Days.System.Script
{
    public class MainManager : MonoBehaviour
    {
        #region Child Manager

        private SystemManager _systemManager;
        private GameManager _gameManager;
        private DataManager _dataManager;
        
        #endregion

        #region Variable

        /// <summary>
        /// Whether to initialize system.
        /// </summary>
        private bool _isLoaded;

        #endregion

        #region Property

        public SystemManager GetSystemManager() => _systemManager;
        public GameManager GetGameManager() => _gameManager;
        public DataManager GetDataManager() => _dataManager;

        #endregion
        
        private void Awake()
        {
            // 한 하늘 아래 MainManager 개체는 2개 일 수 없기에 ... 
            var obj = FindObjectsOfType<MainManager>();
            
            if ( obj.Length == 1)
            {
                DontDestroyOnLoad(gameObject);
                _isLoaded = false;
            }
            else
            {
                if (obj.First() != this)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void Start()
        {
            // 시스템 초기화는 한 번만 수행한다.
            if (_isLoaded == false)
            {
                if (!Init())
                {
                    
                }
            }
        }

        /// <summary>
        /// 게임 실행 시, 리소스 초기화 및 사용자 데이타 로드
        /// </summary>
        private bool Init()
        {
            //  _loadCount++;
            
            // 객체 설정
            _systemManager ??= GetComponentInChildren<SystemManager>();
            _gameManager ??= GetComponentInChildren<GameManager>();
            _dataManager ??= GetComponentInChildren<DataManager>();
            
            
            // 퍼블리싱 내용 표시
            
            
            // ----------- 비동기로 실행할 것 ( Progress Bar / 외 기능) ------------
            
            // 화면 UI
            
            // Read player data
            if (!_dataManager.Init(this))
            {
                return false;
            }
            
            // Initialize resources
            if (!_systemManager.Init(this))
            {
                return false;
            }

            
            // -----------------------------------------
            

            // 이후 시스템 초기화 하지 않음.
            _isLoaded = true;
            
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
            // ----------- 비동기로 실행할 것 ( Progress Bar / Game Setting )------------
            
            // Init Game Manager
            if (!_gameManager.InitManager(this))
            {
                // 다시 메인 화면으로 이동 할 것...
                return false;
            }
            
            // 씬 이동 및 화면 UI
            SceneManager.LoadScene("MainScene");
            // Progress Bar 실행...
            // code.. . 

            
            // Game setting based on player data.
            if (!_gameManager.InitData())
            {
                // 게임 로드 실패 메인 매세지 설정
                return false;
            }

            // -----------------------------------------
            return true;
        }

        #endregion The start scene related code the end.

        #region This code related to the data control.

        /// <summary>
        /// 데이타 세이브
        /// 게임 시간으로 매 하루하루 호출된다.
        /// </summary>
        public void Save(PlayerData playerData)
        {
            _dataManager.WritePlayerData(playerData);
        }
        

        #endregion
    }
}

