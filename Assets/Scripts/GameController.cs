using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    enum WeekDay { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday }

    public int gameMinutesElapsed = 0;
    public int GAME_MINUTE_DURATION = 1; // In seconds. Used for bulk update of gameMinutes.
    public float updateGameMinutesRate = 1; // Update the game minutes every {updateGameMinutesRate} seconds.
    private float timeToUpdateGameMinutes;

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
        updateClockText(gameMinutesElapsed);
    }

    private static int HOUR_DURATION = 60; // In game minutes.
    private static int DAY_DURATION = HOUR_DURATION * 24;

    public Boolean twelveHourClock = true;

    void updateClockText(int gameMinutesElapsed){
        int gameMins = gameMinutesElapsed;
        WeekDay weekDay = WeekDay.Monday;
        int hours = 0;
        while(gameMins >= DAY_DURATION){
            gameMins -= DAY_DURATION;
            weekDay = weekDay.Next();
        }
        while(gameMins >= HOUR_DURATION){
            gameMins -= HOUR_DURATION;
            hours++;
        }

        string amPm = "";
        if(twelveHourClock){
            amPm = hours < 12 ? "AM" : "PM";
            hours = hours <= 12 ? hours : hours - 12;
            hours = hours == 0 ? 12 : hours;
        }

        clockText.text = $"{weekDay}, {hours}:{gameMins} {amPm} ({gameMinutesElapsed})";
    }

    void goToSleep(){
        eventText.text = "I overslept!";
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