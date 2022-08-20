using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditManager : MonoBehaviour
{
    public UiController UiController;
    private int _credit;
    public int credit
    {
        get { return _credit; }  
        set {
            _credit = value;
            OnUpdateCredit();
        }
    }

    
    [Header("초당 크레딧")]
    public int creditPerSec;
    
    [Header("클릭당 크레딧")]
    public readonly int creditPerTouch = 5;
    
    

    private void OnUpdateCredit()
    {
        UiController.UpdateCredit($"{credit:#,0}");
    }
    public void SetCreditPerSec(CreditType creditType)
    {
        creditPerSec = (int) creditType;
    }

    public bool GetIsEnable(CreditType value)
    {
        int _value = (int)value;
        return credit - _value >= 0;
    }
    
    
    public void ConsumeCredit(CreditType value)
    {
        int _value = (int)value;
        if (credit - _value >= 0)
        {
            credit -= _value;   
        }
    }
    
    public void EarnCredit(CreditType value)
    {
        int _value = (int) value;
        credit += _value;
    }

    public void EarnCreditPerSec()
    {
        credit += creditPerSec;
    }
    
    public void EarnCreditPerTouch()
    {
        credit += creditPerTouch;
    }
}
