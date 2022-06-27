using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Days.UI.Script.Util
{
    /// <summary>
    /// 스크롤 뷰의 드래그 인식 시키기
    /// </summary>
    public class ScrollViewDragHandlerSlave : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler, IInitializePotentialDragHandler
    {
        private ScrollRect _scrollRect;
        
        public void Init(ScrollRect scrollRect)
        {
            if (_scrollRect == null)
            {
                _scrollRect = scrollRect;
            }
        }
        
        public void OnBeginDrag(PointerEventData e)
        {
            _scrollRect?.OnBeginDrag(e);
        }

        public void OnDrag(PointerEventData e)
        {
            _scrollRect?.OnDrag(e);
        }

        public void OnEndDrag(PointerEventData e)
        {
            _scrollRect?.OnEndDrag(e);
        }


        public void OnInitializePotentialDrag(PointerEventData e)
        {
            _scrollRect?.OnInitializePotentialDrag(e);
        }

        public void OnScroll(PointerEventData e)
        {
            _scrollRect?.OnScroll(e);
        }
    }
}
