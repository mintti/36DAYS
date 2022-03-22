using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Days.Game.Script;
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
            _scheduler = this.GetComponent<Scheduler>();
            _timer = this.GetComponent<Timer>();
            
            
            _scheduler.Init(this);
            _timer.Init(this, _scheduler.TimerNotification);

            
            _scheduler.CreateTask("test", TestFunc);
            SetSchdulerState(true);
            return true;
        }

        private void TestFunc()
        {
            Debug.Log("Run Test Function!!!!");
        }
        
        public void SetSchdulerState(bool isActive)
        {
            if (isActive)
            {
                _scheduler.ActiveSchedule();
            }
            else
            {
                _scheduler.InactiveSchedule();
            }
        }
        public void Run()
        {
            _timer.Run();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void ExecuteCommand(string command)
        {
            
        }
        
    }
}