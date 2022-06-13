using System.Collections.Generic;
using System.Linq;
using Days.Data.Infra;
using Days.Game.Script;
using Days.UI.Infra;
using Days.UI.ViewModel.Map;
using UnityEditor;
using UnityEngine;

namespace Days.UI.Script
{
    public class MapController : MonoBehaviour
    {
        private GameManager _gameManager;

        #region Dungeon Object Variable
        /// <summary>
        /// Dungeon Object Prefab
        /// </summary>
        public GameObject DungeonObjectViewPrefab;
    
        /// <summary>
        /// Dungeon Object들이 생성 될 위치
        /// </summary>
        public Transform DungeonListTransform;
    
        /// <summary>
        /// Map 관련 오브젝트 관리
        /// </summary>
        private List<DungeonViewModel> _dungeonViewModels;
    

        #endregion
    
        void Start()
        {
            _gameManager = FindObjectsOfType<GameManager>()?.First();

            if (_gameManager != null)
            {
                // UI Manager에 Map Controller 연결
                _gameManager.GetUIManager().ConnectMapController(this);
                
                // 관련 오브젝트 수집
                
                // 설정 완료
            }
        }

        #region Map 구성 관련 함수

        /// <summary>
        /// Player Data를 읽어 Map을 구성
        /// </summary>
        public void InitMap(PlayerData playerData)
        {
            var dungeonViewModels = new List<DungeonViewModel>();
            var dungeonList = playerData.DungeonList;
            
            // Main ..(Castle)
            
            // Random Dungeon n개
            foreach (var dungeon in dungeonList)
            {
                var obj = Instantiate(DungeonObjectViewPrefab, DungeonListTransform);
                dungeonViewModels.Add(obj.GetComponent<DungeonViewModel>());
                dungeonViewModels.Last().Init();
            }
            
            // 첫날인 경우 맵 정보 새로 생성 필요
            if (playerData.Day == 0)
            {
                dungeonViewModels.ForEach(dvm => dvm.LayTheGroundwork());
                
                DungeonListTransform.gameObject.BroadcastMessage("ActiveSimulated");

                Invoke(null, 1);
                
                dungeonViewModels.ForEach(dvm => dvm.UpdateDistance());
                
                // castle에 가까운 순으로 순서 매김 
                dungeonViewModels = dungeonViewModels.OrderByDescending(x=>x.Distance).ToList();
                
                // 위 순서대로 던전 난이도 지정 및 좌표 저장
                for (var index = 0; index < dungeonList.Count; index++)
                {
                    dungeonViewModels[index].SetDungeon(dungeonList[index]);
                    
                    var vector2 = dungeonViewModels[index].GetTransform();
                    _gameManager.GetPlayerData().DungeonList[index].SetPosition(vector2);;
                }
            }
            else
            {
                // 첫날이 아닌 경우, 기존 데이터대로 화면 구성
                for (var index = 0; index < dungeonList.Count; index++)
                {
                    dungeonViewModels[index].SetDungeon(dungeonList[index], dungeonList[index].Vector2);
                }
            }
            
            // 생성된 Map Obj의 viewmodel 전달
            _dungeonViewModels = dungeonViewModels;
        }
        #endregion
    }
}