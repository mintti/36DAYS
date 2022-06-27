using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Days.UI.Prefab.ViewModel
{
    public class SelectorViewModel : MonoBehaviour
    {
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected;}
            set
            {
                _isSelected = value;
                selectedObj.SetActive(_isSelected);
            }
        }

        public GameObject selectedObj;

        public void Start()
        {
            IsSelected = false;
        }

        
        public void MouseClickEvent()
        {
            IsSelected = !IsSelected;
        }

    }

}