using System;
using System.Collections;
using System.Collections.Generic;
using Days.Game.Object.Infra;
using Days.Resource;
using UnityEngine;

using util = Days.Util.Script.UtilityService;

namespace Days.System.Script
{
   public class SystemService : MonoBehaviour
    {
        #region Initialize
        public bool Init()
        {
            if (!ReadResource())
            {
                util.PrintErrorLog("[SYSTEM] Failed to read resource data during the system initialized process.");
                return false;
                
            }
            
            return true;
        }

        // system resource
        private bool ReadResource()
        {
            return true;
        }

        #endregion

        #region related Resource

        

        
        public bool InitResourceManager()
        {
            // Data
            InitCharacter();

            // Image
            
            return true;
        }
        
        private void InitCharacter()
        {
            ResourceManager.CharacterList = new List<Character>()
            {
                new Character(10, 10, 10 ),    // 모험가
                new Character(10, 10, 10 ),    // 전사
                new Character(10, 10, 10 ),    // 마법사
                new Character(10, 10, 10 ),    // 성직자
                new Character(10, 10, 10 ),    // 궁수
                new Character(10, 10, 10 ),
            };
        }
        #endregion
    }
}