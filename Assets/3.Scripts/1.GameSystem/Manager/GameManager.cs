using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Days.GameSystem.Controller;

using static Days.Infra.SystemConst;

namespace Days.GameSystem.Manager
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// 0 : GameStartManager
        /// 1 : GameProgressManager
        /// 2 : GameEndManager
        /// </summary>
        public GameObject[] _gameStateManagetObj = new GameObject[3];
        public GameUIController[] _gameStateManagers;

        #region Variable
        public State State { get; set; }
        #endregion

        private void Awake()
        {
            _gameStateManagers = new GameUIController[3];

            for(int index = 0; index < 3; index++)
            {
                _gameStateManagers[index] = _gameStateManagetObj[index].GetComponent<GameUIController>().Injection(this);
            }

        }

        private void Start()
        {
            LoadData();
            _gameStateManagers[(int)State].Active();
        }

        private void LoadData()
        {
            State = State.START;
        }

        /// <summary>
        /// Next State
        /// </summary>
        public void NextState()
        {
            // Ready next state
            int Idx = ((int)State + 1) % 3;
            State = (State)State.ToObject(typeof(State), Idx);
            _gameStateManagers[Idx].Active();
        }
    }
}