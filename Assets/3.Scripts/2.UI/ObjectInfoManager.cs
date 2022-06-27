/**

*@brief The object information display where Canvas.

*@details 오브젝트에 대한 정보를 캔버스에 표시합니다.
*         ex. Unit/Enemy/Building에 대한 이름
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Days.Infra.Interface;

namespace Days.GameSystem.UI
{
    public class ObjectInfoManager : MonoBehaviour
    {
        private List<IObject> objects;

        /// <summary>
        /// Object came in mouse area.
        /// </summary>
        public void MouseOnEnterEvent(IObject obj)
        {
           
        }

        /// <summary>
        /// Object went off mouse area.
        /// </summary>
        public void MouseOnExitEvent(IObject obj)
        {

        }

        public ObjectInfoManager()
        {
            this.Clear();
        }

        private void Clear()
        {
            objects = new List<IObject>(); ;
        }
    }
}
