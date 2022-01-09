using System;
using System.Collections;
using System.Collections.Generic;
using Days.Data.Infra;
using UnityEngine;

using Days.Game.Infra;
using Random = System.Random;
using util = Days.Util.Script.UtilityService;

namespace Days.Game.Sciprt
{
    public class GameService : MonoBehaviour
    {
        /// <summary>
        /// 게임에서 사용자가 다음 날로 넘어가는 경우, 아래의 내용을 설정하고 저장합니다.
        /// - 게임 상 다음 날로 넘어가는 경우 특정한 설정이 필요한 경우
        /// - 랜덤 키 값을 생성합니다. 해당 키 값을 기반으로 각 이벤트들을 설정합니다.
        /// </summary>
        public PlayerData PreAutomation(PlayerData playerData)
        {
            try
            {
                // Set Game Data About Next Day
                playerData.Day++;
            
                // Create Random Key
                Random random = new Random();
                playerData.KeyCode = random.Next().ToString();
            }
            catch (Exception e)
            {
                // 추후 조치 필요
                util.PrintErrorLog("[GAME] Failed to Pre-Automation data during the game initialized process.");
                throw;
            }
            
            return playerData;
        }
        
        
        /// <summary>
        /// 앞서 PreAutomation 중 생성된 키 값을 기반으로 게임 내부 데이타를 설정합니다.
        /// </summary>
        public bool ExecuteGameSetting()
        {
            // Create Event
            
            // Execute Stage Setting
            
            return true;
        }
        
    }
}
