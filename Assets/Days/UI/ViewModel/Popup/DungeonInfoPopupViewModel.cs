using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

using Days.Data.Infra;
using Days.Game.Object.Infra.Const;
using Days.Game.Object.Infra.Model;
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
        #region View Variable
        public Text dungeonInfoText;
        
        // Dungeon Info View
        public GameObject dungeonInfoView;
        public GameObject content;                  // 유닛 리스트를 표시할 오브젝트
        public GameObject dungeonAdvanceUnitItem;   // content에 생성할 유닛 오브젝트
        public Slider slider;
        
        // Party Info View
        public GameObject partyInfoView;
        public GameObject eventButtonObject;
        public Text dungeonEventInfoText;
        #endregion
            
        private PlayerData _playerData;     // 정보 접근
        private GameManager _gameManager;   // 정보 접근
        private Dungeon _dungeon;           // 팝업에 표시할 던전 정보
        private PartyHandler _currentParty;     // 표시된 던전의 파티 상태
        
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

            _dungeon = ResourceManager.GetDungeon(dungeonIndex);
            dungeonInfoText.text = _dungeon.Name;

            _currentParty = _playerData.PartyList.Find(x => x.DungeonIndex == dungeonIndex);
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
        /// 해당 던전에 투입된 파티 여부에 따른 뷰를 제공
        /// </summary>
        public bool UpdateView()
        {
            // 초기화
            dungeonInfoView.SetActive(false);
            partyInfoView.SetActive(false);
            
            int partyIndex = _playerData.PartyList.FindIndex(x => x.DungeonIndex == _dungeon.Index);
            
            if (partyIndex == -1)
            {
                if (!SetViewDungeonInfo())
                {
                    Debug.Log("던전 정보를 출력하는데 실패했습니다.");
                    return false;   
                }   
            }
            else
            {
                var party = _playerData.PartyList[partyIndex];
                if (!SetViewPartyInfoInDungeon(party.State))
                {
                    Debug.Log("던전에 출전 중인 파티 정보를 출력하는데 실패했습니다.");
                    return false;   
                }
            }

            return true;
        }

        #region 던전 정보가 제공되는 뷰
        
        /// <summary>
        /// 탐색 중이 아닌 던전
        /// - 던정 정보 표시
        /// - 파티 투입을 위한 UI 제공
        /// 
        /// Content GameObject 위치에 유닛정보 오브젝트를 생성해줌.
        /// </summary>
        private bool SetViewDungeonInfo()
        {
            if (content != null)
            {
                // 화면 초기화
                dungeonInfoView.SetActive(true);
                content.SetActive(true);
                
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

        #endregion

        #region 던전 내 파티 정보 및 이벤트 발생 시 제공되는 뷰

        private bool SetViewPartyInfoInDungeon(PartyState partyState)
        {
            // 화면 초기화
            partyInfoView.SetActive(true);
            dungeonEventInfoText.text = default;

            if (partyState == PartyState.Fight || partyState == PartyState.EventWait)
            {
                eventButtonObject.SetActive(true);
                dungeonEventInfoText.text = "던전 이벤트가 발생했습니다.";
            }
            else
            {
                eventButtonObject.SetActive(false);
            }

            return true;
        }

        /// <summary>
        /// 사용자가 이벤트 수행을 위한 버튼 선택
        /// </summary>
        public void SelectEventButton()
        {
            var partyState = _currentParty.State; 
            
            if (partyState == PartyState.Fight)
            {
                _gameManager.ExecuteCombat(_currentParty);
            }
            else if (partyState == PartyState.EventWait)
            {
                
            }
        }
        
        /// <summary>
        /// 귀혼 버튼 선택
        /// </summary>
        public void SelectReturnButton()
        {
            // 이후 확인 메세지창 띄우기
            if (true)
            {
                _currentParty.UpdateState(PartyState.Retreat);
            }
            _gameManager.ReturnPartyDungeon(_currentParty);
        }
        #endregion
    }
}
