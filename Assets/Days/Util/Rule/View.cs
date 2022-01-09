namespace Days.Util.Rule
{
    /* =========================
     *          R U L E
     * =========================
     * Name         : View
     * Description  : UI에 표시하는 Property 및 User Behavior에 대한 이벤트를 입력 받으며
     *                해당하는 ViewModel Event를 실행시킵니다. 
     * 
     */
    public interface View
    {
        /// <summary>
        /// UI에서 표시되는 Property는 다음 형식과 같이 선언됩니다.
        /// </summary>
        int Property { get; set; }

        /// <summary>
        /// Property를 가르키는 값이 ViewModel 내에서 변경 되었을 경우, 해당 함수를 호출하여 업데이트 합니다.
        /// </summary>
        void OnPropertyUpdate();

        /// <summary>
        /// UI에서 발생한 이벤트를 전달받습니다.
        /// </summary>
        void OnPropertyClick();
        void OnPropertyEvent();
    }
    
}