using Days.Data.Script;
using Days.Game.Background.Infra;
using UnityEngine;

namespace Days.Game.Object.Infra
{
    public class ObjectInfo
    {
        public ushort Index { get; set; }
        public string Name { get; set; }
        public State State { get; set; }
        public Character Character { get; set; }
        public Weapon Weapon { get; set; }

        public CurrentState CurrentState { get; set; }
        
        public ObjectState ObjectState { get; set; }

        public void ComputeState()
        {
            
        }
        
        public void Attack()
        {
            
        }

        public void Hit()
        {
            
        }

        public void ExecuteStatusEffect(StatusEffect statusEffect)
        {
            CurrentState.ExecuteStatusEffect(statusEffect.CurrentState);
        }
        
          
    }
}