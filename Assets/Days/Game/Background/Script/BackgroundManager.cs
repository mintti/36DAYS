using System.Collections.Generic;
using Days.Game.Script;
using UnityEngine;

namespace Days.Game.Background.Script
{
    /// <summary>
    ///
    /// Artifact, NPC, Player 효과에 대한 이벤트를 동작시킨다.`
    /// </summary>
    public class BackgroundManager : MonoBehaviour
    {
        private GameManager _gameManager;

        public GameManager GetGameManager() => _gameManager;

        #region Variable
        private StatusEffectManager _npcEffect { get; set; }
        private StatusEffectManager _artifactEffect { get; set; }
        #endregion

        
        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
            
            _artifactEffect = new StatusEffectManager();
            _artifactEffect.Init(this);
        }
        
        /// <summary>
        /// 1Tick 증가될 때 Scheduler 에서 실행
        /// </summary>
        public void Increase()
        {
            _artifactEffect.Increase();
        }

        public void ActiveEffect(string arg)
        {
            var arr = arg.Split('/');
            switch (arr[0].ToLower())
            {
                case "artifact":
                    _artifactEffect.ActiveEffect(arr[1]);
                    break;
                default:
                    break;
            }
        }
        
        public void ExecutePreEvent()
        {
            
        }

        public void ExecutePostEvent()
        {
            _artifactEffect.Clear();
        }
        
    }
}