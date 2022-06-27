using Days.Data.Infra;
using UnityEngine;

namespace Days.UI.ViewModel.Popup
{
    /// <summary>
    /// PopupManager에서 각기 다른 팝업들에게 공통적인 이벤트를 부여하기 위한 핸들러
    /// 해당 오브젝트 ViewModel에는 IPopupViewModel이 적용되어야한다
    /// </summary>
    public class PopupHandler : MonoBehaviour
    {
        /// <summary>
        /// 처음으로 게임 로드 시, 각 팝업 창에 현재 플레이어 데이타를 세팅해줌
        /// </summary>
        public void InitChildren(PlayerData playerData)
        {
            this.SendMessage("Init", playerData, SendMessageOptions.DontRequireReceiver);
        }

        /// <summary>
        /// Popup Controller를 통해 오브젝트를 활성화
        /// </summary>
        public void PopupActive(object obj)
        {
            this.gameObject.SetActive(true);
            this.SendMessage("ActiveGameObject", obj );
        }
    }
}
