using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public void Touch()
    {
        Vector3 spacepos = Camera.main.WorldToScreenPoint(transform.position);
    }
}
