using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropUnit : MonoBehaviour
{
    private Vector3 positionGap;
    private Vector3 mousePosition;
    private Vector3 worldPosition;
    
    private void OnMouseDown()
    {
        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        worldPosition =  Camera.main.ScreenToWorldPoint(mousePosition);

        positionGap = worldPosition - this.transform.position;
    }

    private void OnMouseDrag() {
        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        worldPosition =  Camera.main.ScreenToWorldPoint(mousePosition);
        
        this.transform.position = worldPosition - positionGap;
    }
}
