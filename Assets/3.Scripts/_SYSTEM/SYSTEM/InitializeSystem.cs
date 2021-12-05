using System;
using System.Collections;
using System.Collections.Generic;
using Days.Service;
using UnityEngine;

using util = Days.Service.UtilityService;

namespace Days.System
{
    public class InitializeSystem : MonoBehaviour
    {
        // Start is called before the first frame update

        public bool Initialize()
        {
            if (!ReadResource())
            {
                util.PrintErrorLog("[SYSTEM] Failed to read resource data during the system initialized process.");
                return false;
                
            }

            if (!ReadUserData())
            {
                Debug.Log("[SYSTEM] Failed to read user data during the system initialized process.");
                return false;
            }
            
            return true;
        }


        private bool ReadResource()
        {
            return true;
        }

        private bool ReadUserData()
        {
            
            return true;
        }

    }
}