using System;
using System.Collections;
using System.Collections.Generic;


namespace Days.Data.Infra
{
    /// <summary>
    /// 
    /// </summary>
    public class GameData
    {
        #region Game TIme Infomation
        public ushort Time { get; set; }
        public ushort TimeCycle { get; set; }

        public void UpdateTime() => ++Time; 

        #endregion

    }
}
