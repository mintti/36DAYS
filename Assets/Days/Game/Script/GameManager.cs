using System.Collections;
using System.Collections.Generic;
using Days.Data.Infra;
using Days.Game.OS.Script;
using Days.System.Script;
using UnityEngine;
using util = Days.Util.Script.UtilityService;


namespace Days.Game.Sciprt
{
    public class GameManager : MonoBehaviour
    {
        private MainManager _mainManager;
        private GameService _gameService;
        private OsManager _osManager;

        // 복제된 사용자 정보
        private PlayerData _playerData;
        
        public bool Init(MainManager mainManager)
        {
            _mainManager = mainManager;
            _gameService = GetComponentInChildren<GameService>();
            _osManager = GetComponentInChildren<OsManager>();

            _playerData = _mainManager.GetDataManager().GetPlayerData();

            // 키 및 유저 데이타 기반으로 게임 세팅
            if (!_gameService.ExecuteGameSetting())
            {
                util.PrintErrorLog("[GAME] Failed to execute game setting during the game initialized process.");
            }
            
            Debug.Log("Game Setting Complete.");
            return true;
        }

        public async void Run()
        {
            Debug.Log("Run.");
            
            
            
            Debug.Log("Stop.");
            NextDay();
        }


        /// <summary>
        /// 하루가 지남
        /// </summary>
        private void NextDay()
        {
            // 데이타 생성
            _playerData = _gameService.PreAutomation(_playerData);
            
            // 데이타 저장
            _mainManager.GetDataManager().Save(_playerData);
            
            //재개
            Invoke(nameof(Run), 5);
        }
    }
}