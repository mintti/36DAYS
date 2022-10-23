using System;
using System.Collections.Generic;
using System.Linq;
using Days.Game.Infra;
using UnityEngine;

namespace Days.Game.Script
{
    /// <summary>
    /// 게임 뷰에서 동작별로 UI(Canvas) 전환을 하기 위한 컨트롤러
    /// </summary>
    public class HierarchyController : MonoBehaviour
    {
        private GameManager _gameManager;
        private UIManager _uiManager;

        #region External Variable
        public List<GameObject> DefaultViewObjectList;
        public List<GameObject> CombatViewObjectList;
        #endregion

        public void Start()
        {
            _gameManager = FindObjectsOfType<GameManager>()?.First();

            if (_gameManager != null)
            {
                _uiManager = _gameManager.GetUIManager();
                    
                // UI Manager에 Hierarchy Controller 연결
                _gameManager.GetUIManager().ConnectHierarchyController(this);
            }
        }
        

        public void Init()
        {
            ChangeView(UIType.Default);    
        }
        
        public void ChangeView(UIType type)
        {
            switch (type)
            {
                case UIType.Default:
                    ExecuteSetActiveRepeat(DefaultViewObjectList, true);
                    ExecuteSetActiveRepeat(CombatViewObjectList, false);
                    break;
                case UIType.Combat:
                    ExecuteSetActiveRepeat(DefaultViewObjectList, false);
                    ExecuteSetActiveRepeat(CombatViewObjectList, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void ExecuteSetActiveRepeat(List<GameObject> list, bool value)
        {
            list.ForEach(obj => obj.SetActive(value));
        }
    }
}
