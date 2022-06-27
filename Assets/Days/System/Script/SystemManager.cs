using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Days.Data.Script;
using Days.Resource;
using util = Days.Util.Script.UtilityService;

namespace Days.System.Script
{
    public class SystemManager : MonoBehaviour
    {
        private MainManager _mainManager;
        private SystemService _systemService;
        #region Child Manager

        private DataManager _dataManager;
        #endregion
        
        /// <summary>
        /// First call.
        /// </summary>
        /// <returns> 동작에 실패한 경우 FALSE를 반환합니다.</returns>
        public bool Init(MainManager mainManager)
        {
            _mainManager = mainManager;
            _systemService = GetComponentInChildren<SystemService>();
            
            if (!_systemService.Init())
            {
                util.PrintErrorLog("Failed to initialize system.");
                return false;
            }
            
            

            Debug.Log("System initialization successful.");
            return true;
        }
    }
}
