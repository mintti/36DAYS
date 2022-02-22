using System;
using System.Collections;
using System.Collections.Generic;
using Days.Util.Rule;
using UnityEngine;

/// <summary>
/// Dummy
/// Game View를 제어하는 클래스입니다.
/// </summary>
public class ViewModelHandler : MonoBehaviour
{
    public int CurrentViewIndex;
    public GameObject[] Views = new GameObject[3];

    private void Start()
    {
        foreach (var view in Views)
        {
            view.gameObject.SetActive(false);
        }

        CurrentViewIndex = 1;
        Views[CurrentViewIndex].gameObject.SetActive(true);
    }

    public void SetView(int index)
    {
        if (CurrentViewIndex != index)
        {
            Views[CurrentViewIndex].gameObject.SetActive(false);
            CurrentViewIndex = index;
            Views[CurrentViewIndex].gameObject.SetActive(true);
            
        }
    }
}
