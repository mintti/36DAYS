using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Days.Game.Sciprt;
using UnityEngine;

using util = Days.Util.Script.UtilityService;

using Days.System.Script;

namespace Days.Game.OS.Script
{
    public class OsManager : MonoBehaviour
    {
        private GameManager _gameManager;

        private Scheduler _scheduler;
        private Timer _timer;

        #region Property
        
        public Scheduler GetScheduler() { return _scheduler; }
        
        #endregion
        
        public bool ExecuteOsManager(GameManager gameManager)
        {
            _gameManager = gameManager;

            if (!Init())
            {
                util.PrintErrorLog("Failed to initialize os.");
            }

            return true;
        } 
        
        
        /// <summary>
        /// OS 정보 초기화
        /// </summary>
        private bool Init()
        {
            _scheduler = gameObject.AddComponent<Scheduler>();
            _scheduler.Init(this);

            _timer = gameObject.AddComponent<Timer>();
            _timer.Init(this, _scheduler.TimerNotification);
            
            return true;
        }

        public void ExecuteOs()
        {
            
        }

        public void ExecuteCommand(string command)
        {
            
        }
        
    }
}