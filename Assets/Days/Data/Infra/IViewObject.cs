using UnityEngine;

namespace Days.Data.Infra
{
    /// <summary>
    /// DB에 저장될 View Object로써 가지는 정보.
    /// </summary>
    public interface IViewObject
    {
        #region Transform
        Vector2 Vector2 { get; set; }
        
        #endregion
    }
}