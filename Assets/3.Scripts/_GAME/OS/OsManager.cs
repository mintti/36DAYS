using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

using util = Days.Infra.Service.UtilityService;

using Days.Common;
using Days.System;

namespace Days.Game.OS
{
    public class OsManager : MonoBehaviour
    {
        private MainManager _mainManager;

        private Scheduler _scheduler;
        private Timer _timer;

        #region Property
        
        public Scheduler GetScheduler() { return _scheduler; }
        
        #endregion
        
        public bool ExecuteOsManager(MainManager mainManager)
        {
            _mainManager = mainManager;

            if (!Initialize())
            {
                util.PrintErrorLog("Failed to initialize os.");
            }

            return true;
        } 
        
        
        /// <summary>
        /// OS 정보 초기화
        /// </summary>
        private bool Initialize()
        {
            _scheduler = gameObject.AddComponent<Scheduler>();
            _scheduler.Initialize(this);

            _timer = gameObject.AddComponent<Timer>();
            _timer.Initialize(this, _scheduler.TimerNotification);
            
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