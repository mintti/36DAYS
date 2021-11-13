using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Days.GameSystem.Data
{
    public class PlayerData : MonoBehaviour
    {
        /// <summary>
        /// User Play Date
        /// </summary>
        private byte _date;
        public byte Date { get { return _date; } set { _date = value; } }

        /// <summary>
        /// Player State.
        /// true : Start
        /// false: End
        /// </summary>
        private bool _isStart;
        public bool IsStart { get { return _isStart; } set { _isStart = value; } }



        /* ======================================================
        
          ======================================================= */



        public void SampleData()
        {
            Date = 0;
            IsStart = true;
        }
    }

}