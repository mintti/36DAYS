using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Days.Common
{
    public class Starter : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            this.GetComponent<MainManager>().ExecuteMainManager();
        }
    }
}