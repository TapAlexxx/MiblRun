using Scripts.StaticClasses;
using UnityEngine;

namespace Scripts.Logic.Hud
{

    public class TimeConverter 
    {
        public string ConvertToText(float currentBestTime)
        {
            int roundedSeconds = Mathf.RoundToInt(currentBestTime);
            int minutes = roundedSeconds / Constants.SecondsInMinute;
            int seconds = roundedSeconds - minutes * Constants.SecondsInMinute;
            string additionalMinutesZero = minutes < 10 ? "0" : "";
            string additionalSecondsZero = seconds < 10 ? "0" : "";
            return $"{additionalMinutesZero}{minutes}:{additionalSecondsZero}{seconds}";
        }
    }

}