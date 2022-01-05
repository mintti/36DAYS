using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Days.Common;
using Days.System.Data;

using util = Days.Infra.Service.UtilityService;

namespace Days.System
{
    public class SystemManager : MonoBehaviour
    {
        private MainManager _mainManager;

        #region Child Manager

        private DataManager _dataManager;
        private ResourceManager _resourceManager;

        #endregion
        
        /// <summary>
        /// First called
        /// </summary>
        /// <returns> 동작에 실패한 경우 FALSE를 반환합니다.</returns>
        public bool ExecuteSystemManager(MainManager mainManager)
        {
            _mainManager = mainManager;
            
            if (!this.GetComponent<InitializeSystem>().Initialize())
            {
                util.PrintErrorLog("Failed to initialize system.");
                return false;
            }

            return true;
        }
    }
}
