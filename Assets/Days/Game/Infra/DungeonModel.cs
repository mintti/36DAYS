using Days.Data.Infra;
using UnityEngine;

namespace Days.Game.Infra
{
    /// <summary>
    /// [DB] Dungeon data to be stored in Player data.
    /// </summary>
    public class DungeonModel : IViewObject
    {
        /// <summary>
        /// Resource에서 던전 정보를 읽기위해 사용되는 던전의 Index.
        /// </summary>
        public int Index { get; set; }

        #region Advanced Variable
        /// <summary>
        /// [탐색] 현재 진행된 탐색 거리
        /// </summary>
        public int CurrentLength { get; set; }
        
        /// <summary>
        /// [탐색] 설정한 탐색 목표 거리
        /// </summary>
        public ushort TotalLength { get; set; }
        
        /// <summary>
        /// [탐색] 생성된 던전 키
        /// </summary>
        public byte DungeonKey { get; set; }
        #endregion
        
        
        #region IViewObject

        public Vector2 Vector2 { get; set; }

        public void SetPosition(Vector2 vector2)
        {
            Vector2 = vector2;
        }
        #endregion
        public void UpdateData()
        {
            
        }

        public void AppendReward()
        {
            
        }
    }
}