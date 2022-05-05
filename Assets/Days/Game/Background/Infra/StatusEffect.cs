﻿using System;
using Days.Game.Object.Infra;

namespace Days.Game.Background.Infra
{
    public enum Target
    {
        All,
        Us,
        Enemy
    }
    public class StatusEffect
    {
        /// <summary>
        /// 효과의 이름(Key)
        /// </summary>
        public string Name { get; set; } 
        
        /// <summary>
        /// 대상
        /// 효과의 대상을 선택한다. 
        /// </summary>
        public Target Target { get; set; }
        public Type Type { get; set; }          // 미정
        public int Interval { get; set; }       // 주기
        public int Count { get; set; }          // 횟수
        public int Time { get; set; }           // 지속 시간
        public int Probability { get; set; }    // 확률
        
        // Sample
        public CurrentState CurrentState { get; set; }
        
        // Function

        /// <summary>
        /// 동일한 주기를 가진 경우, 효과를 중첩하여 적용 시킨다.
        /// </summary>
        /// <param name="statusEffect"></param>
        public void Merge(StatusEffect statusEffect)
        {
            // Merge Status
            CurrentState.ExecuteStatusEffect(statusEffect.CurrentState);
            
        }

        /// <summary>
        /// 효과를 해제 하는 경우 감산한다.
        /// </summary>
        /// <param name="statusEffect"></param>
        public void Disconnect(StatusEffect statusEffect)
        {
            CurrentState.Hp -= statusEffect.CurrentState.Hp;
            CurrentState.Mp -= statusEffect.CurrentState.Mp;
            CurrentState.Stress -= statusEffect.CurrentState.Stress;
            CurrentState.Gauge -= statusEffect.CurrentState.Gauge;
            CurrentState.Hunger -= statusEffect.CurrentState.Hunger;
        }
    }
}