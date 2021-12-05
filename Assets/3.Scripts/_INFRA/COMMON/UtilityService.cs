using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using util = Days.Service.UtilityService;

namespace Days.Service
{
    public class UtilityService
    {
        public static void PrintErrorLog(string text)
        {
            Debug.Log(text);
        }
    }
}
