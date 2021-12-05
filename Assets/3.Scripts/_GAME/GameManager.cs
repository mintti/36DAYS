using System.Collections;
using System.Collections.Generic;
using Days.Common;
using UnityEngine;
using util = Days.Service.UtilityService;

namespace Days.Game
{
    public class GameManager : MonoBehaviour
    {
        private MainManager _mainManager;
        
        public bool ExecuteGameManager(MainManager mainManager)
        {
            _mainManager = mainManager;
            
            if (!this.GetComponent<InitializeGame>().Initialize())
            {
                util.PrintErrorLog("Failed to initialize system.");
                return false;
            }

            return ExecuteGameSetting();
        }

        public bool ExecuteGameManager()
        {
            if (!ExecuteGameSetting())
            {
                util.PrintErrorLog("[SYSTEM] Failed to execute game setting during the game initialized process.");
                return false;
            }

            return true;
        }
        
        
        private bool ExecuteGameSetting()
        {
            
            return true;
        }
    }
}