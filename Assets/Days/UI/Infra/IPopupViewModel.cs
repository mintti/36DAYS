using Days.Data.Infra;

namespace Days.UI.Infra
{
    /// <summary>
    /// PopupViewModel에 해당하는 객체의 컨트롤러를 제어하기 위한 인터페이스
    /// </summary>
    public interface IPopupViewModel
    {
        /// <summary>
        /// Initialize object.
        /// </summary>
        /// <param name="playerData"></param>
        void Init(PlayerData playerData);

        /// <summary>
        /// 게임 오브젝트 활성화 
        /// </summary>
        /// <param name="obj"></param>
        void ActiveGameObject(object obj);
    }
}