using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Days.Data.Infra;
using Days.Game.Combat.Infra;
using Days.Game.Combat.Skill;
using Days.Game.Combat.ViewModel;
using Days.Resource.Model;
using Days.Util.Infra;
using Days.Util.Script;
using UnityEngine;
using Grid = Days.Util.Infra.Grid;

namespace Days.Game.Combat.Script
{
    public class FieldController : MonoBehaviour
    {
        private CombatController _combatController;
        private bool SelectMode { get; set; }
        public enum SelectFieldType : int
        {
            None,
            Grid,
            Target
        }

        #region const  
        private const int Width = 9;
        private const int Height = 3;
        private readonly FieldViewModel [,] _fieldInfo = new FieldViewModel[Width, Height];
        #endregion
        
        private Stack<Grid> _changedFieldStack;
        private byte _selectedCount;
        private List<Grid> _selectedGridList;
        
        private bool _canSelectNullTargetFalg;
        public void Init(CombatController combatController, byte dungeonIndex)
        {
            _combatController = combatController;
            _changedFieldStack = new Stack<Grid>();
            
            var fields = GetComponentsInChildren<FieldViewModel>();

            var index = 0;
            for (byte w = 0, widthCnt = 9; w < widthCnt; w++)
            {
                for (byte h = 0, heightCnt = 3; h < heightCnt; h++)
                {
                    fields[index].Init(this, w, -h);
                    _fieldInfo[w, h] = fields[index];
                    index++;
                }
            }
        }

        #region Feild - Entity

        public void ConnectEntityToField(ICombatTarget entity)
        {
            Vector2 vec = entity.GetViewModel().GetPosition();
            _fieldInfo[(int) vec.x, (int) vec.y].OnEntity = entity;
        }


        public void DisconnectEntityToField(ICombatTarget entity)
        {
            Vector2 vec = entity.GetViewModel().GetPosition();

            if (_fieldInfo.IsValidArray(vec.x, vec.y))
            {
                _fieldInfo[(int) vec.x, (int) vec.y].OnEntity = null;
            }
        }

        #endregion

        #region 스킬 사용

        private SkillModel _skilltemp;
        public void CancelCombatAction()
        {
            if (_skilltemp != null)
            {
                StopCoroutine(WaitSelectCombatAction(_skilltemp));    
            }
        }
        
        public void SelectCombatAction(SkillModel skill)
        {
            _skilltemp = skill;
            StartCoroutine(WaitSelectCombatAction(_skilltemp));
        }
        
        /// <summary>
        /// 스킬 정보를 읽어, 대상 값 수집
        /// </summary>
        private IEnumerator WaitSelectCombatAction(SkillModel skill)
        {
            ICombatTarget caster = _combatController.GetCurrentEntity();
            List<ICombatTarget> targets = new List<ICombatTarget>();
            
            if (skill.SelectType == SelectType.Target)                      // 범위 내에서 대상을 선택해야하는 스킬
            {
                // 초기화 및 대상 선택 모드로 변경
                _selectedGridList = new List<Grid>();
                _selectedCount = 0; 
                SelectMode = true;
                _combatController.CombatViewModel.ChangeSelectMode(SelectMode);
                
                // 선택 가능한 대상을 화면에 표시
                foreach (var grid in _changedFieldStack)
                {
                    ICombatTarget target = _fieldInfo[grid.x, grid.y].OnEntity;
                    target.ChangeSelectMode(true);
                }
                
                // 지정된 카운트만큼 대기
                var waitUntil = new WaitUntil(() => (_selectedCount < skill.TargetCount));
                     
                // 스킬 사용
                caster.GetBehavior().Execute(skill, targets);
            }
            else if (skill.SelectType == SelectType.TargetWithinGrid)       // 범위 내 포함되는 전체 대상을 하는 스킬
            {
                // 스킬 사용할 대상 화면에 표시
                _selectedGridList = _changedFieldStack.ToList();
                
                // 영역 내 모든 엔티티의 정보를 전달
                foreach (var grid in _selectedGridList)
                {
                    ICombatTarget target = _fieldInfo[grid.x, grid.y].OnEntity;
                    if(target != null)
                        targets.Add(target);
                };
                
                // 스킬 사용
                caster.GetBehavior().Execute(skill, targets);
            }
            
            // 표시한 사용 가능 대상 제거

            yield return null;
        }

        private void SendSelectData(object[] objs)
        {
            
        }
        
        /// <summary>
        /// 스킬을 사용할 영역을 선택 
        /// </summary>
        public void InputField(Vector3 fieldPos)
        {
            // position info send to combat controller.
            //_combatController.SelectedField(fieldPos);
        }

        /// <summary>
        /// 대상 선택
        /// </summary>
        public void SelectedEvent(FieldViewModel field)
        {
            if (SelectMode)
            {
                var position = field.transform.localPosition;
                _selectedGridList.Add(new Grid(position.x, position.y));
                _selectedCount++;
                //_combatController.SelectedField();
            }
        }
        

        public void Deselect()
        {
            if (SelectMode)
            {
                ClearField();
            }
        }

        /// <summary>
        /// 스킬 대상 설정 중 선택 취소하여 스킬 사용이 중단됨
        /// </summary>
        public void StopSelectMode()
        {
            if (SelectMode)
            {
                StopCoroutine(WaitSelectCombatAction(_skilltemp));
                // 선택 가능한 대상을 화면에 표시
                foreach (var grid in _changedFieldStack)
                {
                    ICombatTarget target = _fieldInfo[grid.x, grid.y].OnEntity;
                    target.ChangeSelectMode(false);
                }
            }
        }
        #endregion

        #region mouse on event
        
        /// <summary>
        /// 현재 턴을 진행하는 오브젝트의 로컬 좌표를 배열 인덱스로 변환하여 반환
        /// </summary>
        private Vector2 GetCurrentUnitPos()
        {
            var targetPos = _combatController.GetCurrentUnitPos();
            targetPos.y *= -1;

            return targetPos;
        }
        
        /// <summary>
        /// 입력된 스킬을 받아 스킬 사용가능한 필드의 위치를 출력
        /// </summary>
        public void UpdateField(SkillModel skill)
        {
            Vector2 crtEntPos = _combatController.GetCurrentUnitPos();
            
            // 스킬타겟에 해당하는 위치 설정
            var list = new List<Grid>();

            switch (skill.TargetType)
            {
                case TargetType.SingleTargetSkill:
                case TargetType.MultiTargetSkill:
                case TargetType.AreaNonTargetSkill:
                    list = GetPosByArea(crtEntPos, skill.Area);
                    break;
                case TargetType.AbsolutelyNonTargetSkill:
                    list = skill.Coordinate;
                    break;
                case TargetType.RelativeNonTargetSkill:
                    list = GetPosByRelative(crtEntPos, skill.Coordinate);
                    break;
            }
            
            foreach (var grid in list)
            {
                _fieldInfo[grid.x, grid.y].ChangeFieldType(FieldType.Select);
                _changedFieldStack.Push(grid);
            }
        }
        
        /// <summary>
        /// 개체의 위치를 기반으로 Area에 해당하는 전방향의 좌료를 반환
        /// </summary>
        private List<Grid> GetPosByArea(Vector2 pos, int area)
        {
            var list = new List<Grid>(); 
            for (var x = -area; x <= area; x++)
            {
                if (CanUsePosX(pos, x))
                {
                    for (var y = -area; y <= area; y++)
                    {
                        if (CanUsePosY(pos, y))
                        {
                            list.Add(new Grid((int)pos.x + x, (int)pos.y + y));
                        }
                    }   
                }
            }

            return list;
        }

        /// <summary>
        /// 개체의 위치를 기반으로하는 상대 경로 스킬의 범위 값 반환
        /// </summary>
        private List<Grid> GetPosByRelative(Vector2 pos, List<Grid> targets)
        {
            var list = new List<Grid>();

            foreach (var grid in targets)
            {
                if (CanUsePosX(pos, grid.x) && CanUsePosY(pos, grid.y))
                {
                    list.Add(new Grid((int)pos.x + grid.x, (int)pos.y + grid.y));
                }
            }
            return list;
        }


        private bool CanUsePosX(Vector2 pos, int x)
        {
            return (pos.x + x < 0 || pos.x + x >= Width) == false;
        }
        
        private bool CanUsePosY(Vector2 pos, int y)
        {
            return (pos.y + y < 0 || pos.y + y >= Height) == false;
        }
        
        public void ClearField()
        {
            while (_changedFieldStack.Count > 0)
            {
                var item = _changedFieldStack.Pop();
                _fieldInfo[item.x, item.y].ChangeFieldType(FieldType.None);
            };
        }
        #endregion

    }
}
