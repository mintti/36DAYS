using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Days.Game.Background.Infra;
using Days.Game.Object.Infra;
using Days.Util.Infra;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UIElements;

namespace Days.Game.Background.Script
{
    public delegate void EffectDel(StatusEffect effect);

    public class StatusEffectManager
    {
        private event EffectDel EffectDelList;
        private BackgroundManager _backgroundManager;

        public void Init(BackgroundManager backgroundManager)
        {
            _backgroundManager = backgroundManager;

            EffectDelList += ExecuteCountEffect;

            _constantStatusEffectDict = new Dictionary<int, List<StatusEffect>>();
            _activeEffectList = new List<string>();

        }

        public void ExecuteEffect(StatusEffect effect)
        {
            EffectDelList?.Invoke(effect);
        }

        // TODO : 추후 함수 명 변경하기 (실행하여 존재 여부로 Active/Inactive 시킴)
        public void ActiveEffect(string name)
        {
            var effect = GetResource_Test(name);
            if (_activeEffectList.Contains(name))
            {
                RemoveConstantEffect(effect);
            }
            else
            {
                AddConstantEffect(effect);
            }

        }

        // 추후 리소스 쪽에서 이펙트 리스트 생성 후, 리스트에서 값 호출하여 사용하도록 수정 할 것
        private StatusEffect GetResource_Test(string name)
        {
            StatusEffect effect = new StatusEffect();
            switch (name)
            {
                case "test":
                    effect.Name = "test";
                    effect.Target = Target.Us;
                    effect.Interval = 1;
                    effect.Probability = 30;
                    effect.CurrentState = new CurrentState()
                    {
                        Hp = 5
                    };
                    break;
                default:
                    break;
            }

            return effect;
        }

        #region Time Control

        private int _tick;

        /// <summary>
        /// 초 증가 함수
        /// </summary>
        public void Increase()
        {
            _tick++;

            ExecuteConstantEffect();

        }

        public void Clear()
        {
            _tick = 0;
        }

        #endregion

        #region 주기적인 이벤트를 발생 시킴 (Target : All Object)

        private Dictionary<int, List<StatusEffect>> _constantStatusEffectDict;
        private List<string> _activeEffectList;

        /// <summary>
        /// 세로운 주기적 이벤트를 추가한다.
        /// </summary>
        /// <param name="statusEffect"> 추가될 고정 효과 </param>
        private void AddConstantEffect(StatusEffect statusEffect)
        {
            var interval = statusEffect.Interval;
            var target = statusEffect.Target;

            if (_constantStatusEffectDict.ContainsKey(interval) == false)
            {
                _constantStatusEffectDict.Add(interval, new List<StatusEffect>());
            }

#if false // 주기별로 구분하여 모든 효과를 저장하는 Dict
            ConstantStatusEffectDict[Interval].Add(statusEffect);

#else // 주기 + 타겟별로 구분하여 저장하는 Dict
            var index = _constantStatusEffectDict[interval].FindIndex(x => x.Target.Equals(target));
            if (index == -1)
            {
                // 새로운 타겟에 대한 효과
                _constantStatusEffectDict[interval].Add(statusEffect);
            }
            else
            {
                // 기존 타겟에 대한 효과 
                _constantStatusEffectDict[interval][index].Merge(statusEffect);
            }

            _activeEffectList.Add(statusEffect.Name);
#endif
        }

        private void RemoveConstantEffect(StatusEffect statusEffect)
        {
            var interval = statusEffect.Interval;
            var target = statusEffect.Target;

            var targetIndex = _constantStatusEffectDict[interval].FindIndex(x => x.Target.Equals(target));

            // 제거 
            if (_constantStatusEffectDict[interval][targetIndex].Count == 1)
            {
                // 해당 타겟에 대한 효과가 아예 존재하지 않는다면, 타겟 자체를 제거
                _constantStatusEffectDict[interval].RemoveAt(targetIndex);
            }
            else
            {
                // 효과 해제
                _constantStatusEffectDict[interval][targetIndex].Disconnect(statusEffect);
            }

            _activeEffectList.Remove(statusEffect.Name);
        }

        /// <summary>
        /// 일치하는 주기의 Constant Effect 적용
        /// </summary>
        private void ExecuteConstantEffect()
        {
            foreach (var pair in _constantStatusEffectDict)
            {
                if (_tick % pair.Key != 0) continue;
                foreach (var statusEffect in _constantStatusEffectDict[pair.Key])
                {
                    switch (statusEffect.Target)
                    {
                        case Target.Us:
                            // 동작 기록
                            _backgroundManager.GetGameManager().GetPlayerData().UnitList?
                                .ForEach(unit=>unit.ExecuteStatusEffect(statusEffect));

                            break;
                        case Target.Enemy:

                            break;
                        case Target.All:

                            break;
                        default:
                            break;
                    }
                }
            }
        }


        #endregion
        
        #region Pixed Type
        private void ExecuteCountEffect(StatusEffect effect)
        {
            // 횟수를 부영하는 이벤트를 발생 시킴
        }
        
        private void ExecuteFixedEffect(StatusEffect statusEffect)
        {
            
        }
        
        private void ExecuteParticularEffect(StatusEffect statusEffect)
        {
            // 경우에 따라 발생. 별도의 함수들을 실행 시키는 컨트롤 함수
        }

        #endregion
        
        #region Other Effects

        

        #endregion
    }
}