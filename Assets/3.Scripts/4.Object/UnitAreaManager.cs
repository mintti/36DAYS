using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * [UnitAreaManager]
 *   해당 클래스는 모든 UnitAreaController를 제어합니다.
 *   - 오브젝트에 대한 UnitAreaController 설정 및 제어
 *   - Controller 제어를 위한 리소스 제공
 */

namespace Days.Object.Controller
{
    public class UnitAreaManager : MonoBehaviour
    {
        protected Dictionary<string, Sprite> SpriteDict;
 

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            Initialized();
        }

        private void Initialized()
        {
            // set the sprite dictionary
            SpriteDict = new Dictionary<string, Sprite>();
            
            ReadData();
        }


        // 사용자의 데이타를 읽어 생성
        private void ReadData()
        {

        }
    }
}