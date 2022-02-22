using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Days.System.Script;

namespace Days.Game.ViewModel
{
    public class StartSceneViewModel : MonoBehaviour
    {
        private MainManager _mainManager;

        private void Awake()
        {
            _mainManager = FindObjectsOfType<MainManager>().First();
            transform.Find("GameStart").GetComponent<Button>().onClick.AddListener(StartBtnClick);
        }
        

        /// <summary>
        /// 게임 시작 이벤트
        /// </summary>
        private void StartBtnClick()
        {
            if (!_mainManager.ExecuteGameStart())
            {
                // 게임 시작 실패 이벤트.. 메세지 박스든..
            }
        }
        
    }
}