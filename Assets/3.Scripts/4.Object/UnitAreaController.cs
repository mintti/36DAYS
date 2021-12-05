using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 해당 클래스는 오브젝트가 가지는 기능(스킬)에 대한 Collider를 제어합니다.
 * - 한 가지 기능에 대한 제어
 * - Active by user : 유저가 Active()를 호출하여 Collider On
 * - Active by interval : (auto) 일정 시간마다 Active하는 경우, Run하여 실행
 */

namespace Days.Object.Controller
{
    public class UnitAreaController : MonoBehaviour
    {
        
        #region Variables
        
        /// <summary>
        /// 
        ///</summary>
        private string _activeType { get; set; }

        // 해당 오브젝트가 Interval Object 일 때,
        // 오브젝트 가동 여부
        private bool _activeObject { get; set; } 

        #endregion

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            
        }


        /// <summary>
        /// Called the method
        /// </summary>
        public bool CreateCollider(string activeType, string colliderType, float size, string polygonType = null)
        {
            _activeType = activeType;

            // 2. Generation by collider type.
            switch(colliderType.ToLower())
            {
                case "box" :
                    // CreateBoxCollider();
                    break;

                case "circle" :
                    // CreateCircleCollider();
                    break;

                case "polygon" :
                    // A sprite with polygons must exist.
                    // CreatePolygonCollider(polygonType);
                    
                    //if(false)   // not found polygon Type. 
                        return false; 
                    break;

                default :
                    Debug.Log("Fail to create collider.");
                    break;
            }

            // 3. Setting other things.


            // 4. Setting about activeType..
            if( _activeType.ToLower().Equals( "user" ) )
            {
                
            }
            else
            {
                _activeObject = true;
                StartCoroutine("Coroutine");
            }

            // Success to create the collider.
            return true;
        }


        IEnumerator Coroutine(float interval)
        {
            while( _activeObject || true) // 후에 Game System Active 변수로... 
            {
                BroadcastMessage("ActiveObject");

                yield return new WaitForSeconds(interval * 1);  // 후에 1은 System Timer의 double speed로 대체될 수 있다. 
            }
        }
    }
}