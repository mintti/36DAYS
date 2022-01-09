using UnityEngine;
using Days.Data.Infra;

namespace Days.Data.Script
{
    public class DataService : MonoBehaviour
    {
        // 임시 로컬 데이타 변수
        private PlayerData _file;
        
        public PlayerData ReadPlayerData()
        {
            if (true) // 기존 플레이어 데이터가 존재하지 않는 경우, 기본 데이타 설정
            {
                Debug.Log("Crate player data.");
                return new PlayerData
                {
                    Day = 0,
                    KeyCode = "...."
                };
            }
            Debug.Log("Read player data.");

            PlayerData playerData = _file;


            return playerData;
        }

        public bool WritePlayerData(PlayerData playerData)
        {

            _file = playerData;
            return true;
        }
    }
}