﻿using System;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform hoursTransform;
    public Transform minutesTransform;
    public Transform secondsTransform;
    const float degreesPerHour = 30.0f;
    const float degreesPerMinute = 6.0f;
    const float degreesPerSecond = 6.0f;
    public bool continous;
    void Update()
    {
        if(continous)
        {
            UpdateContinous();
        }
        else
        {
            UpdateDiscrete();
        }
    }
    void UpdateContinous()
    {
        TimeSpan time = DateTime.Now.TimeOfDay;
        //Debug.Log(DateTime.Now);
        hoursTransform.localRotation = Quaternion.Euler(0f, (float)time.TotalHours*degreesPerHour, 0f);
        minutesTransform.localRotation = Quaternion.Euler(0f, (float)time.TotalMinutes * degreesPerMinute, 0f);
        secondsTransform.localRotation = Quaternion.Euler(0f, (float)time.TotalSeconds * degreesPerSecond, 0f);
    }
    void UpdateDiscrete()
    {
        DateTime time = DateTime.Now;
        //Debug.Log(DateTime.Now);
        hoursTransform.localRotation = Quaternion.Euler(0f, time.Hour * degreesPerHour, 0f);
        minutesTransform.localRotation = Quaternion.Euler(0f, time.Minute * degreesPerMinute, 0f);
        secondsTransform.localRotation = Quaternion.Euler(0f, time.Second * degreesPerSecond, 0f);
    }
}
