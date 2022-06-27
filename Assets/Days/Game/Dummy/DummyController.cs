using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Days.Game.Script;
using Days.Game.Object.Infra;
using Days.Data.Infra;
using Days.Data.Script;

namespace Days.Game.Dummy
{
    public class DummyController : MonoBehaviour
    {
        private GameManager _gameManager { get; set; }
        // Start is called before the first frame update
        void Start()
        {
            _gameManager = FindObjectsOfType<GameManager>()?.First();
        }

        public void Proto01_01()
        {
            // UI 생성
            // for (int i = 0; i < 4; i++)
            // {
            //     _gameManager.CreateUnit(new ObjectInfo()
            //     {
            //         Index = (byte)i,
            //         Name = $"Unit{i}",
            //         State = new State()
            //         {
            //             Speed = (ushort)(10 + i) 
            //         }
            //     });   
            // }

            List<byte> units = new List<byte>() {0, 1, 2, 3};
            _gameManager.SendPartyDungeon(units, 0, 100);
            
        }

        /// <summary>
        /// PROTO02. 유닛의 고용
        /// - 1. 임시 데이터를 통해 생성이 가능하다.
        /// </summary>
        public void Proto02_01()
        {
            // 고용
            for (byte i = 0; i < 4; i++)
            {
                _gameManager.CreateUnit(new ObjectModel()
                {
                    Index = i,
                    Name = $"Unit{i}",
                    Job = i,
                    CurrentState = new CurrentState()
                    {
                        Hp = 1,
                        Mp = 0,
                        Gauge = 0,
                        Hunger = 0,
                        Stress = 0
                    }
                });   
            }
        }


        private bool _proto03Flag = false;
        public void Proto03_01()
        {
            Debug.Log( _proto03Flag ? "Inactive" : "Active" + "Test Effect");
            _gameManager.GetBackgroundManager().ActiveEffect("artifact/test");
            _proto03Flag = !_proto03Flag;
        }

        private bool _proto04Flag = false;
        /// <summary>
        /// 랜덤 맵 생성 및 제거
        /// </summary>
        public void Proto04_01()
        {
            if (_proto04Flag)
            {
                
                
            }
            else
            {
                
                
            }

            _proto04Flag = !_proto04Flag;
        }
    }
}
