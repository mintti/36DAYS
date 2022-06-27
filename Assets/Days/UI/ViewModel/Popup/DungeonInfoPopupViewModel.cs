using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using Days.Data.Infra;
using Days.Game.Script;
using Days.Resource.Model;
using Days.UI.Infra;
using Days.Resource;
using Days.UI.Prefab.ViewModel;
using util = Days.Util.Script.UtilityService;

namespace Days.UI.ViewModel.Popup
{
    public class DungeonInfoPopupViewModel : MonoBehaviour, IPopupViewModel
    {
        public Text dungeonInfoText;
        public GameObject content;
        public Slider slider;
        public GameObject dungeonAdvanceUnitItem;
            
        private PlayerData _playerData;     // 
        private GameManager _gameManager;   // 
        private Dungeon _dungeon;           // 팝업에 표시할 던전 정보
        private List<DungeonAdvanceUnitItemViewModel> _advanceUnitList;
        
        public void Start()
        {
            _gameManager = FindObjectsOfType<GameManager>()?.First();
        }

        #region IPopupViewModel
        public void Init(PlayerData playerData)
        {
            // 초기화
            _playerData ??= playerData;
            
            UpdateView();
        }

        public void ActiveGameObject(object obj)
        {
            var dungeonIndex = (int)obj;

            _dungeon = ResourceManager.DungeonList[dungeonIndex];
            dungeonInfoText.text = _dungeon.Name;
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
                _advanceUnitList = new List<DungeonAdvanceUnitItemViewModel>();
                
                // 유닛 생성
                foreach (var unit in _playerData.UnitList)
                {
                    var unitObj = Instantiate(dungeonAdvanceUnitItem, content.transform);
                    _advanceUnitList.Add(unitObj.GetComponent<DungeonAdvanceUnitItemViewModel>());
                    _advanceUnitList.Last().SetItem(unit);
                }   
            }
            else
            {
                Debug.Log($"[{this.GetType().Name}] content is null");
            }

            return true;
        }
        
        /// <summary>
        /// 선택한 유닛들을 파티로 만들어 던전에 보냅니다.
        /// </summary>  
        public void SendPartyDungeon()
        {
            var list = new List<byte>();

            foreach (var advanUnit in _advanceUnitList)
            {
                if (advanUnit.selector.IsSelected)
                {
                    list.Add(advanUnit.GetUnitIndex());
                }
            }

            if (_advanceUnitList.Count > 0)
            {
                _gameManager.SendPartyDungeon(list, _dungeon.Index, GetLenght());   
            }
        }

        /// <summary>
        /// 슬라이더의 값을 Byte 값으로 변환 후 반환
        /// </summary>
        private ushort GetLenght()
        {
            return (ushort)slider.value;
        }

    }
}
