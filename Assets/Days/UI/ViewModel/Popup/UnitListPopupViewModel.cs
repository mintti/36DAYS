using System;
using Days.Data.Infra;
using Days.UI.Infra;
using Days.UI.Prefab.ViewModel;
using Days.UI.Script;
using UnityEngine;
using util = Days.Util.Script.UtilityService;

namespace Days.UI.ViewModel.Popup
{
    public class UnitListPopupViewModel : MonoBehaviour, IPopupViewModel
    {
        private PlayerData _playerData;
        public GameObject unitListItem;
        
        /// <summary>
        /// 유닛 Scroll Viewer Content
        /// </summary>
        public GameObject content;

        #region IPopupViewModel
        /// <summary>
        /// 첫 게임 씬 로드 시, 플레이어 데이터 주입
        /// </summary>
        public void Init(PlayerData playerData)
        {
            _playerData = playerData;
            UpdateView();
        }
        
        public void ActiveGameObject(object obj)
        {
            
        }
        #endregion
        
        /// <summary>
        /// 플레이어 데이터가 설정 된 이후 팝업 창이 켜질 때마다 화면을 재구성해줌 
        /// </summary>
        public void OnEnable()
        {
            if (_playerData != null)
            {
                UpdateView();
            }
        }
        
        /// <summary>
        /// Content GameObject 위치에 유닛정보 오브젝트를 생성해줌.
        /// </summary>
        public bool UpdateView()
        {
            if (content != null)
            {
                // 초기화
                util.DestroyAllChildren(content);
                
                // 유닛 생성
                foreach (var unit in _playerData.UnitList)
                {
                    var unitObj = Instantiate(unitListItem, content.transform);
                    unitObj.GetComponent<UnitListItemViewModel>().SetItem(unit);
                }   
            }
            else
            {
                Debug.Log($"[{this.GetType().Name}] content is null");
            }

            return true;
        }
    }
}
