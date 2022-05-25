using Days.Data.Infra;
using UnityEngine;

namespace Days.UI.ViewModel
{
    public class PopupHandler : MonoBehaviour
    {
        public void InitChildren(PlayerData playerData)
        {
            this.SendMessage("Init", playerData, SendMessageOptions.DontRequireReceiver);
        }
    }
}
