using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using util = Days.Util.Script.UtilityService;

namespace Days.System.Script
{
   public class SystemService : MonoBehaviour
    {
        #region Initialize
        public bool Init()
        {
            if (!ReadResource())
            {
                util.PrintErrorLog("[SYSTEM] Failed to read resource data during the system initialized process.");
                return false;
                
            }
            
            return true;
        }


        private bool ReadResource()
        {
            return true;
        }

        #endregion
        
    }
}