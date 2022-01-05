using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Days.Service.Type;

namespace Days.Game.OS
{
    public class Timer : MonoBehaviour
    {
        private OsManager _osManager;

        private DDelegate _noti1Sec;
        private IEnumerator _timer;
        
        private bool _state;
        

        public void Initialize(OsManager osManager, DDelegate noti1Sec)
        {
            _osManager = osManager;
            _noti1Sec = noti1Sec;
            
            _timer = TimerSec();
            _state = false;
        }
        #region TIMER CONTROL

        public void Start() => _state = true;
        public void Pause() => _state = false;
        
        public void Reset()
        {
            if (_state)
            {
                StopCoroutine(_timer);
                Pause();
            }
            StartCoroutine(_timer);
        }

        IEnumerator TimerSec()
        {
            while (true)
            {
                yield return new WaitUntil(() => _state);
                yield return new WaitForSeconds(1.0f);

                _noti1Sec();
            }
        }
        #endregion
    }
}