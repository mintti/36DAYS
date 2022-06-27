using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Days.UI.Script.Util
{
    public class ScrollViewDragHandlerMaster : MonoBehaviour
    {
        private ScrollRect _scrollRect;
        public GameObject Content;
        public void OnEnable()
        {
            if (_scrollRect == null)
            {
                _scrollRect = GetComponent<ScrollRect>();
            }

            var slaves = Content.GetComponentsInChildren<ScrollViewDragHandlerSlave>().ToList();
            slaves.ForEach(s => s.Init(_scrollRect));
        }
    }
}
