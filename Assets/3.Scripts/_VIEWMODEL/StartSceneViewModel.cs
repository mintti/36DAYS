using System;
using System.Linq;
using UnityEngine;
using Days.Common;

namespace Days.ViewModels
{
    public class StartSceneViewModel : MonoBehaviour
    {
        private MainManager _mainManager;

        private void Awake()
        {
            _mainManager = FindObjectsOfType<MainManager>().First();
        }

        /// <summary>
        /// 게임 시작 이벤트
        /// </summary>
        public void StartBtnClick()
        {
            if (!_mainManager.ExecuteGameStart())
            {
                // 게임 시작 실패 이벤트.. 메세지 박스든..
            }
        }
        
    }
}