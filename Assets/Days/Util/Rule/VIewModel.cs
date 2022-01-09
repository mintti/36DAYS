namespace Days.Util.Rule
{
    /* =========================
     *          R U L E
     * =========================
     * Name         : ViewModel
     * Description  : UI에 표시하는 Property 및 User Behavior에 대한 이벤트를 입력 받으며
     *                해당하는 ViewModel Event를 실행시킵니다. 
     * 
     */
    public interface ViewModel
    {
        #region Variable

        /// <summary>
        /// 전역 변수로써 사용될 변수
        /// </summary>
        /* public */ int GlobalProperty { get; set; }
        
        /// <summary>
        /// 지역 변수로써 사용될 변수
        /// </summary>
        /* private */ int Property { get; set; }


        #endregion

        #region Custom Method

        /// <summary>
        /// 해당 메소드는 로직을 수행시키는 상위 메소드 입니다.
        /// 해당 메소드는 Method 명명이 가르키는 기능을 수행하기 위한 프로세스로 이루어져 있습니다.
        /// 해당 메소드 내에선 직접적인 코드 사용보단 아래처럼 사용하는 것을 권장합니다.
        ///     - Update a variable within the ViewModel.
        ///     - Function calls to execute detailed procedures.
        /// 
        /// [ Execute{Method} ]  Method = Summary of the logic to be executed.
        /// </summary>
        void ExecuteMethod();
        
        #endregion

        #region MyRegion

        /// <summary>
        /// Used method that to send a Broadcast Message or a Normal Message
        ///  
        /// [ Send/Receive_MSGType_{Method}]  Method = What to execute 
        /// </summary>
        void Send_BroadcastMSG_Method();
        void Receive_BroadcastMSG_Method();
        void Send_MSG_Method();
        void Receive_MSG_Method();

        #endregion
    }
}