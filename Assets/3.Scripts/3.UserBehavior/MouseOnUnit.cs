using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOnUnit : MonoBehaviour
{
    private void OnMouseEnter()
    {
        this.gameObject.BroadcastMessage("MouseOnEnterEvent");
    }

    private void OnMouseExit()
    {
        this.gameObject.BroadcastMessage("MouseOnExitEvent");
    }
}
