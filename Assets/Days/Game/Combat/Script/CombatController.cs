using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Days.Game.Combat.Behavior;
using Days.Game.Combat.Enemy;
using Days.Game.Combat.Infra;
using Days.Game.Combat.ViewModel;
using Days.Game.Object.Infra;
using Days.Game.Object.Infra.Model;
using Days.Game.Script;
using Days.Resource;
using Days.Resource.Model;
using Days.Util.Infra;
using UnityEngine;
using UnityEngine.UI;
using Grid = Days.Util.Infra.Grid;

namespace Days.Game.Combat.Script
{
    public partial class CombatController : MonoBehaviour
    {
        private GameManager _gameManager;
        
        #region External Variable

        public Camera BattleCamera;
        public FieldController FieldController;
        public CombatViewModel CombatViewModel;
        
        public GameObject CombatObjectsGameObject;
        #endregion 

        #region Variable

        private List<CombatEntityHandler> _entityList;
        private Stack<CombatEntityHandler> _sequenceStack;
        
        private bool _inputAction = true;    // Active after executed action to object.
        private bool _updateSpeedState;      // Active when changed speed state. 

        #endregion

        private void Start()
        {
            _gameManager = FindObjectsOfType<GameManager>()?.First();

            if (_gameManager != null)
            {
                _gameManager.SetCombatController(this);   
            }
            
        }

        /// <summary>
        /// execute combat process
        /// </summary>
        public bool ExecuteCombat(CombatInfo combatInfo)
        {
            StartCoroutine(nameof(Combat), combatInfo);
            return true;
        }

        /// <summary>
        /// 실질적인 전투 절차
        /// </summary>
        private IEnumerator Combat(CombatInfo combatInfo)
        {
            bool combatEnd = false;
            // 초기화  
            var waitUntil = new WaitUntil(() => _inputAction);
            
            // 화면 세팅
            
            Init(combatInfo);
            InitField();

            
            // 전투 전 설정 
            ExecutePreCombatEvent();
            
            while(true)
            {
                // 턴 시작 초기화 및 이벤트 (Sequence Stack)
                ExecuteInitTurn();
                while(true)
                {
                    _updateSpeedState = false;
                    _inputAction = false;
                    
                    // 현재 턴 대상의 가능한 액션을 표시
                    var target =  _sequenceStack.First();
                    CombatViewModel.SetUnitView(target.GetIndex());
                    target.Execute();
                    
                    // 액션이 입력될 때까지 대기
                    yield return waitUntil; 
                    
                    _sequenceStack.Pop();
                    
                    // 만약, 사용자 액션으로 인해 옵젝트가 사망한 경우, 아래 조건문을 실행하기 전에 sequence stack에서 정리가 필요하다. 
                    if (_sequenceStack.Count > 0)
                    {
                        // [테스트] 턴 종료 후 몬스터가 존재하는지 체크,
                        // 추후 함수를 사용하지 않고, 리스트 변화에 따라 필드 값에 변화를 주고 필드 값 변화에 따라 수행하도록 변경 필요
                        if (!(_entityList.Any(handler => handler.GetEntityType() == EntityType.Enemy)))
                        {
                            combatEnd = true;
                            break;
                        }
                        
                        // if, speed is changed then update sequence.
                        if(_updateSpeedState)
                        {
                            UpdateSequenceStack();
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (combatEnd)
                {
                    break;
                }
            }
            
            // 종료
            _gameManager.EndCombat("test");
            yield return null;
        }
        
        
        #region 전체적인 전투 세팅

        // 전체적인 전투 세팅 
        private void Init(CombatInfo combatInfo)
        {
            // 던전 정보 세팅
            FieldController.Init(this, combatInfo.DungeonIndex);
            
            // 유닛 세팅
            _entityList = CombatObjectsGameObject.GetComponentsInChildren<CombatEntityHandler>().ToList();
            
            var party = combatInfo.PartyInfo;
            var partyCnt = party.UnitsIndex.Count;
            
            
            for (var index = 0 ; index < 4; index ++)
            {
                if (index < partyCnt)
                {
                    var unitData = _gameManager.GetPlayerData().UnitList[index];
                    var unit = _entityList[index];
                    
                    unit.Init(this, index, EntityType.Unit, unitData, new UnitBehavior(), TurnEndAction);
                    unit.GetBehavior().Init(unit);
                    SetEntityPosition(unit, index, 0);  
                }
                else
                {
                    _entityList.RemoveAt(index);
                }
            }

            var enemies = combatInfo.EnemyList;
            var enemiesCnt = enemies.Count;
            
            for (byte index = 0; index < 4; index++)
            {
                var curIdx = index + partyCnt;
                if (index < enemiesCnt)
                {
                    var enemyData = new EnemyInfo(); 
                    enemyData.Init(ResourceManager.GetEnemy(enemies[index]));
                    
                    var enemy = _entityList[curIdx];
                    enemy.Init(this, curIdx, EntityType.Enemy, enemyData, new EnemyTestBehavior(), TurnEndAction);
                    enemy.GetBehavior().Init(enemy,
                                             EnemyTargetSearchCloseBy,
                                             EnemyTargetTrace,
                                             CheckToUseSkill);
                    SetEntityPosition(enemy, curIdx, 0); 
                }
                else
                {
                    _entityList.RemoveAt(curIdx);
                }
            }
            
            // 전투 UI 설정
            CombatViewModel.Init(this, _entityList);
            
        }
        
        /// <summary>
        /// 전투 시작 전 필드에 오브젝트들의 위치를 설정합니다.
        /// </summary>
        private void InitField()
        {
            
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void ExecutePreCombatEvent()
        {
            // 유닛/아티펙트에 의한 고정적인 전체 전투 효과 적용   
        }

        private void ExecutePostCombatEvent()
        {
            
        }


        private void SetEntityPosition(ICombatTarget entity, int x, int y)
        {
            FieldController.DisconnectEntityToField(entity); 
            
            entity.GetViewModel().SetPosition(new Vector2(x, y)); 
            
            FieldController.ConnectEntityToField(entity);
        }
        #endregion

        #region combat object control about player input 
        /// <summary>
        /// 오브젝트에서 선택 이벤트가 발생 했을 때 실행
        /// </summary>
        /// <param name="objectInfo"></param>
        public void SelectedObject(CombatEntityHandler objectInfo)
        {
        }
        
        /// <summary>
        /// 현재 턴을 가지는 오브젝트의 좌표 값을 반환
        /// </summary>
        /// <returns></returns>
        public Vector2 GetCurrentUnitPos()
        {
            return _sequenceStack.First().GetViewModel().GetPosition();
        }
        
        // /// <summary>
        // /// 스킬 및 좌표 선택
        // /// </summary>
        // /// <param name="pos"></param>
        // public void SelectedField(Vector3 pos)
        // {
        //     _sequenceStack.First().GetViewModel().SetPosition(pos);
        //     CompleteAction();
        // }
        
        /// <summary>
        /// 입력된 액션에 대해 수행이 완료된 후 실행
        /// </summary>
        private void TurnEndAction()
        {
            _inputAction = true;
        }
        #endregion
        
        
        #region 전투 진행 중 수행 함수
        /// <summary>
        /// 매 턴 시작 시 동작하는 함수
        /// </summary>
        private void ExecuteInitTurn()
        {
            foreach (var unit in _entityList)
            {
                unit.State.Push(CombatState.Wait);
            }

            UpdateSequenceStack();
        }

        /// <summary>
        /// 현재 액션 가능한 오브젝트들의 순서를 속도순으로 정렬
        /// </summary>
        private void UpdateSequenceStack()
        {
            // 턴이 종료되지 않은 Ready 상태인...
            // 추후 죽어있는지 살아있는지에 대한 상태도 확인이 필요하다.
            _sequenceStack = new Stack<CombatEntityHandler>(
                _entityList.Where( x=> x.State.First() == CombatState.Wait)
                                   .OrderByDescending(x=>x.GetSpeed())
                                   .Select(x=>x)
                                   .ToList());
        }

        
        public ICombatTarget GetCurrentEntity() => _sequenceStack.First();
        #endregion
    }
}