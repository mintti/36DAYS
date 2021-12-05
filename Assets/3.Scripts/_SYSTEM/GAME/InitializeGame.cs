using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util = Days.Service.UtilityService;

namespace Days.Game
{
    /*
     * @ Class          : Initialize Game
     * @ Description    : 해당 클래스는 게임에서 매일 호출되어, 데이타 생성 및 해당 데이터를 기반으로 설정을 합니다.
     *                    게임 데이타를 불러온 경우, 데이타 생성 과정을 생략한 나머지 과정을 수행합니다.
     * 
     */

    public class InitializeGame : MonoBehaviour
    {
        public bool Initialize()
        {
            if (!PreAutomation())
            {
                util.PrintErrorLog("[SYSTEM] Failed to Pre-Automation data during the game initialized process.");
                return false;
            }
            
            return true;
        }
    
    
        /// <summary>
        /// 게임에서 사용자가 다음 날로 넘어가는 경우, 아래의 내용을 설정하고 저장합니다.
        /// - 게임 상 다음 날로 넘어가는 경우 특정한 설정이 필요한 경우
        /// - 랜덤 키 값을 생성합니다. 해당 키 값을 기반으로 각 이벤트들을 설정합니다.
        /// </summary>
        private bool PreAutomation()
        {
            // Set Game Data About Next Day
            
            // Create Random Key
            
            // Data Save
            return true;
        }
        
        
        /// <summary>
        /// 앞서 PreAutomation 중 생성된 키 값을 기반으로 게임 내부 데이타를 설정합니다.
        /// </summary>
        private bool ExecuteGameSetting()
        {
            // Create Event
            
            // Execute Stage Setting
            return true;
        }
    }
}
