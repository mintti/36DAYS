using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Days.GameSystem.Manager;

namespace Days.GameSystem.Controller
{
    public class GameUIController : MonoBehaviour
    {
        GameManager _gameManager;

        public void Active()
        {

            this.gameObject.SetActive(true);
        }


        /// <summary>
        /// This method is called at the button about process end.
        /// </summary>
        public void End()
        {
            /**
             
             **/
            Inactive();
            _gameManager.NextState();
        }

        // End methods
        private void Inactive()
        {
            this.gameObject.SetActive(false);
        }


        //Initialization
        public GameUIController Injection(GameManager gameManager)
        {
            _gameManager = gameManager;
            this.gameObject.SetActive(false);

            return this;
        }
    }
}