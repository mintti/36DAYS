using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPlayerStateViewer : MonoBehaviour
{

    public void Trace()
    {
        Debug.Log("Trace");
        this.GetComponent<SpriteRenderer>().color = Color.blue;
    }

    public void Patrol()
    {
        Debug.Log("Patrol");
        this.GetComponent<SpriteRenderer>().color = new Color(0,0,0);
    }

    public void MouseOnEnterEvent()
    {
    }

    public void MouseOnExitEvent()
    {
    }
}
