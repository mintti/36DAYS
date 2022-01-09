using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestViewModel : MonoBehaviour
{
    public Text DayText;
    public Text ClockText;


    public void UpdateDayText(ushort day, string key)
    {
        DayText.text = $"Day      : {day}\n" +
                       $"Key Code : {key}";
    }
    
    public void UpdateClockText(int current, int total)
    {
        ClockText.text = $"{current} / {total}";
    }

}
