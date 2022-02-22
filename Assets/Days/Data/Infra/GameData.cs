using System;
using System.Collections;
using System.Collections.Generic;
using Days.Game.Object.Infra;
using Days.Util.Infra;


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

        #region Party Infomation

        public Party[] Party { get; set; }

        #endregion
    }
}
