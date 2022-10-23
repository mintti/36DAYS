using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Days.Game.Background.Script;
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
        
        /// <summary>
        /// OS 정보 초기화
        /// </summary>
        public bool Init(GameManager gameManager)
        {
            _gameManager = gameManager;
            
            _scheduler = this.GetComponent<Scheduler>();
            _timer = this.GetComponent<Timer>();
            
            
            _scheduler.Init(this);
            _timer.Init(this, _scheduler.TimerNotification);

            Test();
            SetSchedulerState(true);
            return true;
        }

        #region Test
        private void Test()
        {
            // 일회성 테스크 테스트
            _scheduler.CreateTask("test", TestFunc);
        }
        private void TestFunc()
        {
            Debug.Log("Run Test Function!!!!");
        }
        #endregion
        
        /// <summary>
        /// 매개변수로 받은 isActive(bool) 값에 따라 Scheduler Coroutine 동작시킴
        /// </summary>
        /// <param name="isActive"></param>
        public void SetSchedulerState(bool isActive)
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

        public void Run() => _timer.Run();
        public void Stop() => _timer.Stop();
        public void Pause() => _timer.Pause();

        public void ExecuteCommand(string command)
        {
            
        }
        
    }
}