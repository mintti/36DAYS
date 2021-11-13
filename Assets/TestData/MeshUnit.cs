using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Days.Test
{
    public class MeshUnit : MonoBehaviour
    {
        private string _tag;
        private string _targetTag;
        // Start is called before the first frame update
        void Start()
        {
            Initialized();
        }

        private void Initialized()
        {
            _tag = this.gameObject.tag;

            switch(_tag)
            {
                case "Player" :
                    _targetTag = "Enemy";
                    return;
                case "Enemy" :
                    _targetTag = "Player";
                    return;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == _targetTag)
                this.gameObject.BroadcastMessage("Trace");
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == _targetTag)
                this.gameObject.BroadcastMessage("Patrol");
        }
    }
}