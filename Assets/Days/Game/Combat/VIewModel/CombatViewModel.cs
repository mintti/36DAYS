using System;
using System.Collections.Generic;
using Days.Game.Combat.Script;
using Days.Game.Object.Infra.Model;
using Days.Resource.Model;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Days.Game.Combat.ViewModel
{
    /// <summary>
    /// 현재 턴에 따른 뷰 제공을 위한 뷰모델
    /// 뷰를 통해 인풋을 처리(타 컨트롤러에 전달 등)
    /// </summary>
    public class CombatViewModel : MonoBehaviour
    {
        #region External Varaibles
        public GameObject UnitActionObj;
        public Transform EntityStatusListTransform;
        public GameObject EntityStatusObjectPrefab;
        #endregion
        
        private int currentEntityIndex;
        private CombatController _combatController;
        private FieldController _fieldController;
        
        // Entity 상태 표시 변수
        private Dictionary<int, List<SkillModel>> _skillBufferDict; // 스킬 버퍼
        private Dictionary<int, UIEntityStatus> _ettSttVMDict;   // 엔티티별 스탯
        
        // 선택 모드 UI
        public GameObject SelectModePanelObj;
        public Text selectCounterText;
        public int selectedCnt;
        public int totalSelectCnt;
        
        /// <summary>
        /// 초기화
        /// </summary>
        public void Init(CombatController combatController, List<CombatEntityHandler> list)
        {
            _combatController = combatController;
            _fieldController = _combatController.FieldController;
            
            // 초기화
            _skillBufferDict ??= new Dictionary<int, List<SkillModel>>();
            _ettSttVMDict ??= new Dictionary<int, UIEntityStatus>();
            
            // 화면 초기화
            SelectModePanelObj.SetActive(false);
            
            // 엔티티 초기화
            foreach (var handler in list)
            {
                int index = handler.GetIndex();
                
                // 스킬 셋 버퍼에 저장
                _skillBufferDict.Add(index, handler.GetCombatInfo().GetSkillList());
                
                // 기본 스테이터스 정보 표시
                var obj = Instantiate(EntityStatusObjectPrefab, EntityStatusListTransform);
                _ettSttVMDict.Add(index, obj.GetComponent<UIEntityStatus>());
                _ettSttVMDict[index].Init(handler);
            }
        }

        #region 매턴 호출되는 뷰 설정 항목
        /// <summary>
        /// 매턴 입력된 정보를 기반으로 화면을 구성
        /// </summary>
        public void SetUnitView(int index)
        {
            currentEntityIndex = index; 
            var test = _skillBufferDict[index];
            UpdateEntityStatus(index);  // 테스트

            // 해당 값으로 뷰 설정
        }

        public void SetEnemyView(int index)
        {
            
        }
        
        public void ClearView()
        {
            _combatController = null;
            _skillBufferDict.Clear();
            
            // 객체 제거 후 초기화
            foreach (var key in _ettSttVMDict.Keys)
            {
                _ettSttVMDict[key].Destroy();
            }
            _ettSttVMDict.Clear();
        }
        #endregion

        #region 스킬 선택 관련 뷰 업데이트
        /// <summary>
        /// 사용자가 타겟의 행동을 지정했을 때 호출
        /// </summary>
        public void SelectedActionEvent(int skillIdx)
        {
            var skill = _skillBufferDict[currentEntityIndex][skillIdx];
            _fieldController.SelectCombatAction(skill);
            
            // 선택 시 필드 영역 초기화
            _fieldController.ClearField();
        }
        
        /// <summary>
        /// 스킬 위에 커서를 올리면 스킬 정보를 출력 및 사용 가능 필드 표시
        /// </summary>
        public void MouseEnterActionEvent(int skillIndex)
        {
            var targetIndex = _combatController.GetCurrentEntity().GetIndex();
            var skill = _skillBufferDict[targetIndex][skillIndex];
            
            _fieldController.UpdateField(skill);
        }

        
        /// <summary>
        /// 스킬 위에 커서를 올리면 스킬 정보를 출력
        /// </summary>
        public void MouseExitActionEvent()
        {
            _fieldController.ClearField();
        }

        /// <summary>
        /// 매개 변수 값에 따른 스킬 대상 선택 모드 화면 전환
        /// </summary>
        public void ChangeSelectMode(bool isSelectMode)
        {
            UnitActionObj.SetActive(!isSelectMode);
            SelectModePanelObj.SetActive(isSelectMode);

            if (isSelectMode)
            {
                // 선택 가능한 유닛들만 활성화 되도록 업데이트
                
                // 화면 업데이트
                UpdateSelectCount();   
            }
            else
            {
                _fieldController.StopSelectMode();
                
                // 초기화
                selectCounterText.text = string.Empty;
                selectedCnt = 0;
                totalSelectCnt = 0;
            }
        }
        
        public void UpdateSelectCount()
        {
            selectCounterText.text = $"{selectedCnt}/{totalSelectCnt}";
        }
        #endregion

        #region 전투 중 스테이터스 정보 표시
        public void UpdateEntityStatus(int index)
        {
            _ettSttVMDict[index].UpdateStatus();
        }
        #endregion
    }

}
