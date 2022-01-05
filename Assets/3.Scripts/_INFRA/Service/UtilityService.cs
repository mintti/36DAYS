using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using util = Days.Infra.Service.UtilityService;

namespace Days.Infra.Service
{
    public class UtilityService
    {
        public static void PrintErrorLog(string text)
        {
            Debug.Log(text);
        }
    }
}
