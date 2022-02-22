using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Days.Data.Infra;
using Days.Game.Sciprt;
using UnityEngine;
using UnityEngine.UI;

public class TestViewModel : MonoBehaviour
{
    private UIManager _uiManager;
    
    public Text DayText;
    public Text ClockText;


    public void Start()
    {
        _uiManager = FindObjectsOfType<UIManager>().First();
        
        _uiManager.AddGameDataEvent(UpdateClockText);
        _uiManager.AddPlayerDataEvent(UpdateDayText);
    }

    public void UpdateDayText(PlayerData playerData)
    {
        DayText.text = $"Day      : {playerData.Day}\n" +
                       $"Key Code : {playerData.KeyCode}";
    }
    
    public void UpdateClockText(GameData gameData)
    {
        ClockText.text = $"{gameData.Time} / {gameData.TimeCycle}";
    }

}
