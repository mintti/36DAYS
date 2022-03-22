using Days.Data.Infra;
using UnityEngine;
using Days.Util.Infra;

using Days.Game.ViewModel;

namespace Days.Game.Script
{
    public delegate void GameDel(GameData data);
    public delegate void PlayerDel(PlayerData data);
    public class UIManager : MonoBehaviour
    {
        private GameManager _gameManager;
        
        private event GameDel GamaDataEvent;
        private event PlayerDel PlayerDataEvent;
        public bool ExecuteManager(GameManager gameManager)
        {
            _gameManager = gameManager;
            
            return true;
        }


        public void AddGameDataEvent(GameDel func) =>  GamaDataEvent += func;
        public void AddPlayerDataEvent(PlayerDel func) => PlayerDataEvent += func;
        
        public void UpdateGameDataView(GameData gameData)
        {
            GamaDataEvent?.Invoke(gameData);
        }

        public void UpdatePlayerDataView(PlayerData playerData)
        {
            PlayerDataEvent?.Invoke(playerData);
        }
    }
}