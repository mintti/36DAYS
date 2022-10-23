using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using util = Days.Util.Script.UtilityService;

namespace Days.Util.Script
{
    public class UtilityService : MonoBehaviour
    {
        public static void PrintErrorLog(string text)
        {
            Debug.Log(text);
        }

        public static void DestroyAllChildren(GameObject parent)
        {
            foreach (Transform child in parent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
