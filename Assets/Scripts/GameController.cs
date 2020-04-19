using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public int gameMinutesElapsed = 0;
    public int GAME_MINUTE_DURATION = 1; // In seconds. Used for bulk update of gameMinutes.
    public float updateGameMinutesRate = 1; // Update the game minutes every {updateGameMinutesRate} seconds.
    private float timeToUpdateGameMinutes;

    public Image overlayPanel;

    public TextMeshProUGUI clockText;
    public TextMeshProUGUI eventText;

    public Flower[] flowers;

    // Start is called before the first frame update
    void Start()
    {
        timeToUpdateGameMinutes = updateGameMinutesRate;
    }

    // Update is called once per frame
    void Update()
    {
        timeToUpdateGameMinutes -= updateGameMinutesRate;
        if(timeToUpdateGameMinutes < 0){
            timeToUpdateGameMinutes += updateGameMinutesRate;
            updateGameMinutes((int)(updateGameMinutesRate / GAME_MINUTE_DURATION));
        }
    }

    void updateGameMinutes(int gameMinutes){
        gameMinutesElapsed += gameMinutes;
        foreach(Flower f in flowers){
            f.updateGameMinutes(gameMinutes);
        }
        GameTimeStamp gTimeStamp = ClockTextController.gameMinutesElapsedToGameTimeStamp(gameMinutesElapsed);
        ClockTextController.updateClockText(clockText, gTimeStamp);
        DayTimeController.updateDayTime(overlayPanel, gTimeStamp);
    }


    void goToSleep(){
        eventText.text = "I overslept!";
    }
}

public class InventoryController {

}

public class GameTimeStamp
{
    internal WeekDay weekDay = WeekDay.Monday;
    internal int hours = 0; // in 24-hour format (and in game-hours).
    internal int minutes = 0; // in game-minutes.
}

internal enum WeekDay { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday }

public class ClockTextController {
    private static int HOUR_DURATION = 60; // In game minutes.
    private static int DAY_DURATION = HOUR_DURATION * 24;

    public static Boolean twelveHourClock = true;

    public static GameTimeStamp gameMinutesElapsedToGameTimeStamp(int gameMinutesElapsed){
        int gameMins = gameMinutesElapsed; 
        GameTimeStamp gts = new GameTimeStamp();
        while (gameMins >= DAY_DURATION)
        {
            gameMins -= DAY_DURATION;
            gts.weekDay = gts.weekDay.Next();
        }
        while (gameMins >= HOUR_DURATION)
        {
            gameMins -= HOUR_DURATION;
            gts.hours++;
        }
        return gts;
    }

    public static void updateClockText(TextMeshProUGUI clockText, GameTimeStamp gTimeStamp)
    {
        int hours = gTimeStamp.hours;
        string amPm = "";
        if (twelveHourClock)
        {
            amPm = gTimeStamp.hours < 12 ? "AM" : "PM";
            hours %= 12;
            hours = hours == 0 ? 12 : hours;
        }

        clockText.text = $"{gTimeStamp.weekDay}, {hours}:{gTimeStamp.minutes} {amPm}";
    }
}

public class DayTimeController {
    enum DayTime { MIDNIGHT, MORNING, AFTERNOON, NIGHT }

    private static Color NIGHT = new Color(0, 0, 0, 0);
    private static Color MIDNIGHT = new Color(0, 0, 0, 0); // very dark so player goes to bed.
    private static Color SUNRISE = new Color(1, 1, 1, 0);
    private static Color DAY = new Color(1, 1, 1, 0);
    /* 
    private Dictionary<DayTime, Color> dayTimeColors = new Dictionary<DayTime, Color>{
        {DayTime.MIDNIGHT, BLACK},
        {DayTime.MORNING, WHITE},
        {DayTime.AFTERNOON, WHITE},
        {DayTime.NIGHT, BLACK}
    }; */

    public static void updateDayTime(Image overlayPanel, GameTimeStamp gTimeStamp)
    {
        switch(gTimeStamp.hours){
            case int h when (h >= 0 && h <= 5):
                // MIDNIGHT
                break;
            case int h when (h == 6):
                // SUNRISE
                break;
            case int h when (h >= 7 && h <= 17):
                // DAY
                break;
            case int h when (h == 18):
                // SUNSET
                break;
            case int h when (h >= 19 && h <= 23):
                // NIGHT
            default:
                // SOMETHING WENT WRONG
                break;
        }
    }
}

// Thanks to husayt: https://stackoverflow.com/a/643438/3399416
public static class Extensions
{

    public static T Next<T>(this T src) where T : struct
    {
        if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

        T[] Arr = (T[])Enum.GetValues(src.GetType());
        int j = Array.IndexOf<T>(Arr, src) + 1;
        return (Arr.Length == j) ? Arr[0] : Arr[j];
    }
}