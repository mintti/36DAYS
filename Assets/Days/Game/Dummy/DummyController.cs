using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Days.Game.Sciprt;
using Days.Game.Object.Infra;
using Days.Data.Infra;

namespace Days.Game.Dummy
{
    public class DummyController : MonoBehaviour
    {
        private GameManager _gameManager { get; set; }
        // Start is called before the first frame update
        void Start()
        {
            _gameManager = FindObjectsOfType<GameManager>().First();
        }

        public void Proto01_01()
        {
            // UI 생성
            for (int i = 0; i < 4; i++)
            {
                _gameManager.CreateUnit(new ObjectInfo()
                {
                    Index = (byte)i,
                    Name = $"Unit{i}",
                    State = new State()
                    {
                        Speed = (ushort)(10 + i) 
                    }
                });   
            }

            List<byte> units = new List<byte>() {0, 1, 2, 3};
            _gameManager.SendPartyDungeon(units, 0, 100);
            
        }
    }
}
