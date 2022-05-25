using Days.Data.Infra;

namespace Days.UI.Infra
{
    /// <summary>
    /// always 
    /// </summary>
    public interface IPopupViewModel
    {
        /// <summary>
        /// Initialize object.
        /// </summary>
        /// <param name="playerData"></param>
        void Init(PlayerData playerData);
    }
}