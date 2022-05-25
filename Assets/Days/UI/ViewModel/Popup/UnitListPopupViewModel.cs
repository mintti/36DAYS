using System;
using Days.Data.Infra;
using Days.UI.Infra;
using Days.UI.Prefab.ViewModel;
using Days.UI.Script;
using UnityEngine;
using util = Days.Util.Script.UtilityService;

namespace Days.UI.ViewModel
{
    public class UnitListPopupViewModel : MonoBehaviour, IPopupViewModel
    {
        private PlayerData _playerData;
        public GameObject unitListItem;
        
        /// <summary>
        /// 유닛 Scroll Viewer Content
        /// </summary>
        public GameObject content;
        
        public void Init(PlayerData playerData)
        {
            _playerData = playerData;
            UpdateView();
        }

        public void OnEnable()
        {
            if (_playerData != null)
            {
                UpdateView();
            }
        }

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
