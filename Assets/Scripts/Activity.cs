using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActivityStruct
{
    public ActivityType activityType;
    public CreditType credit;
}

public class Activity : MonoBehaviour
{
    public StageManager stageManager;
    
    public static Activity instance;

    internal List<ActivityStruct> activitys = new List<ActivityStruct>(3);
    
    void Awake()
    {
        instance = this;

        activitys.Add(new ActivityStruct
        {
            activityType = ActivityType.EXERCISE,
            credit = CreditType.EIGHTTHOUNSAND
        });
        activitys.Add(new ActivityStruct
        {
            activityType = ActivityType.TRAVEL,
            credit = CreditType.TWELVETHOUSAND
        });
        activitys.Add(new ActivityStruct
        {
            activityType = ActivityType.PARTTIMEJOB,
            credit = CreditType.TENTHOUSAND
        });
    }

    public void Activate(ActivityType activityType)
    {
        
        stageManager.OnActivate(activityType);
    }
    public ActivityStruct GetActivity(ActivityType activityType)
    {
        var activity = activitys.Find(item =>item.activityType == activityType);
        return activity;
    }
}