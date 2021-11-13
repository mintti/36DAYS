using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Days.Test
{
    /// <summary>
    /// This use if to limit player move area.
    /// </summary>
    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }

    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rb;

        public Transform _transform;
        public float speed = 10;
        public Boundary boundary;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            _transform = this.gameObject.GetComponent<Transform>();

        }
        void Update()
        {
            //Default Mover
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb.velocity = movement * speed;
            rb.position = new Vector2(rb.position.x, rb.position.y);


            //// under code used to limit player moving.
            //rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            //    0.0f,
            //    Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
        }
    }
}