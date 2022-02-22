//#define WRITE_LOG_SECOND

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Days.Util.Infra;

namespace Days.Game.OS.Script
{
    public class Timer : MonoBehaviour
    {
        private OsManager _osManager;

        #region Variable
        private Del _noti;              // 1초마다 호출될 스케쥴러 함수
        private IEnumerator _timer;     // 타이머 코루틴
        
#if WRITE_LOG_SECOND
        private uint second;
#endif
        #endregion
        

        public void Init(OsManager osManager, Del noti)
        {
            _osManager = osManager;
            _noti = noti;
            
            _timer = TimerSec();
            
            Reset();
        }
        
        
        #region TIMER CONTROL

        public void Run()
        {
            StartCoroutine("TimerSec");
        }

        public void Pause()
        {
            
        }

        /// <summary>
        /// Expire
        /// </summary>
        public void Stop()
        {
            StopCoroutine("TimerSec");
            Reset();
        }

        /// <summary>
        /// 내부 변수 초기화
        /// </summary>
        public void Reset()
        {
#if WRITE_LOG_SECOND
            second = 0;
#endif
        }

        IEnumerator TimerSec()
        {
            yield return null;
            while (true)
            {
                yield return new WaitForSeconds(1.0f);
#if WRITE_LOG_SECOND
                Debug.Log(++second);
#endif
                _noti();
            }
        }

        #endregion
    }
}