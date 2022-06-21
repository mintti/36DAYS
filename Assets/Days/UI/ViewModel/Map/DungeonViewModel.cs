using System;
using Days.Data.Infra;
using Days.Game.Infra;
using Days.Infra.Interface;
using Days.Resource.Model;
using Days.UI.Infra;
using Days.UI.Script;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Days.UI.ViewModel.Map
{
    public class DungeonViewModel : MonoBehaviour
    {
        private MapController _mapController;

        #region Variable
        private CircleCollider2D _collider;
        private Rigidbody2D _rigidbody;
        
        public int DungeonIndex;
        public float Distance { get; set; }

        #endregion        
        public void Init(MapController mapController)
        {
            _mapController = mapController;
            
            _collider = GetComponent<CircleCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// 던전 오브젝트에 해당하는 던정 정보 및 위치 정보를 받아 설정
        /// </summary>
        public void SetDungeon(DungeonModel dungeon, Vector2 vector2 = default)
        {
            if (vector2 != default)
            {
                transform.position = vector2;
            }

            DungeonIndex = dungeon.Index;
        }

        #region Create Map
        /// <summary>
        /// 랜덤 맵 생성을 위한 랜덤 값 부여
        /// </summary>
        public void LayTheGroundwork()
        {
            _rigidbody.simulated = false;

            var vector = new Vector2(Random.Range(-0.5f, +0.5f), Random.Range(-0.5f, +0.5f));
            _collider.offset = vector;
            _collider.radius = Random.Range(1.5f, 8.0f);
        }

        public void ActiveSimulated()
        {
            _rigidbody.simulated = true;
        }
        

        /// <summary>
        /// 성과의 거리를 측정
        /// </summary>
        public void UpdateDistance()
        {
            var position = transform.position;
            Distance = Math.Abs(position.x) + Math.Abs(position.y);
        }

        /// <summary>
        /// Collider 설정 기본 값으로 변경
        /// </summary>
        public void SetDefaultState()
        {
            var vector = new Vector2(0, 0);
            _collider.offset = default;
            _collider.radius = 1;
        }
        
        
        /// <summary>
        /// 설정된 위치 정보를 반환
        /// 첫 생성 때 사용
        /// </summary>
        public Vector2 GetTransform()
        {
            return transform.position;
        }
        
        #endregion

        #region User Behavior

        public void OnMouseUp()
        {
            _mapController.SelectedDungeon(DungeonIndex);
        }

        #endregion
    }
}