using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 10.0f;
    private Camera camera;

    private float dist;
    private void Start()
    {
        camera = GetComponent<Camera>();
        dist = transform.position.z;
    }

    private void Update()
    {
        //¡‹¿Œ¡‹æ∆øÙ
        float scroll = Input.GetAxis("Mouse ScrollWheel") * speed;
        if (camera.fieldOfView <= 20.0f && scroll > 0)
        {
            camera.fieldOfView = 20.0f;
        }
        else if (camera.fieldOfView >= 80.0f && scroll < 0)
        {
            camera.fieldOfView = 80.0f;
        }
        else
        {
            camera.fieldOfView -= scroll;
        }

        //XY¿Ãµø
        if (Input.GetMouseButton(1))
        {
            transform.Translate(Vector3.right * -Input.GetAxis("Mouse X"));
            transform.Translate(transform.up * -Input.GetAxis("Mouse Y"), Space.World);
        }
    }
}
