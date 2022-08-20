using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class StageManager : MonoBehaviour
{
    public UiController uiController;
    public CreditManager creditManager;
    private readonly short endDate = 16;

    private short _date;

    private float curSec;
    private readonly int daySec = 10;
    
    private int exercise = 0;
    private int travel = 0;
    private int partTimeJob = 0;

    public End end;

    public Sprite[] images;


    public Animator animator;
    private short date
    {
        get { return _date;}
        set
        {
            uiController.calanderText.text = value.ToString();
            _date = value;
        }
    }

    public bool isPlaying;

    private bool wasExercised;

    void Start()
    {
        OnPlayStart();
    }

    private void OnPlayStart()
    {
        date = 0;
        isPlaying = false;
        Debug.Log("비활성화");
        wasExercised = false;
        OnDayStart();
    }

    private void OnDayStart()
    {
        date += 1;
        curSec = 0;
        if (endDate < date)
        {
            Debug.LogError("엔딩 날짜를 지났습니다.");
        }

        isPlaying = true;
        Debug.Log("활성화");
        creditManager.SetCreditPerSec(wasExercised ? CreditType.ONE : CreditType.TWO);
        wasExercised = false;
        StartCoroutine(EarnMoneyPerSec());
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (curSec <= daySec)
        {
            curSec += Time.deltaTime;
            uiController.UpdateSlide( 1 - (curSec / daySec));
            yield return null;
        }

        curSec = daySec;
        EndDate();
    }

    public void OnActivate(ActivityType activityType)
    {
        if (!isPlaying) return;
        var activity = Activity.instance.GetActivity(activityType);
        Debug.Log("액티브");
        if (activityType == ActivityType.PARTTIMEJOB)
        {
            creditManager.EarnCredit(activity.credit);
            partTimeJob++;
        }
        else
        {
            if (!creditManager.GetIsEnable(activity.credit))
            {
                isPlaying = true;
                return;
            }
            creditManager.ConsumeCredit(activity.credit);
            if (activityType == ActivityType.EXERCISE)
            {
                wasExercised = true;
                exercise++;
            }
            else
            {
                travel++;
            }
        }
        isPlaying = false;
        //실행
        // 실행, 실행 끝나면 다음 날
        StopAllCoroutines();
        StartCoroutine(Activate(activityType));
    }

    private IEnumerator Activate(ActivityType activityType)
    {
        
        animator.SetTrigger(activityType.ToString());
        float sec = 0f;
        int targetSec = 1;
        while (targetSec >= sec)
        {
            sec += Time.deltaTime;
            yield return null;
        }
        animator.SetTrigger(ActivityType.IDLE.ToString());
        EndDate();
    }

    public void EndDate()
    {
        StopAllCoroutines();
        isPlaying = false;
        if (date >= 15)
        {
            // 엔딩
            Ending();
        }
        else
        {
            // 머시기 한 다음
            OnDayStart();
        }
    }

    private void Ending()
    {
        if (travel == 5 && partTimeJob == 5 && exercise == 5)
        {
            end.PlayEnd(images[1], "엔딩 2. 황금 밸런스", "이번 방학은 정말 알차게 보냈다! 이것저것 경험하며 배우고 얻은 게 많았어! 정말 뿌듯해~");
        }
        else if (travel == 15 || partTimeJob == 15 || exercise == 15)
        {
            string most;
            if (travel == 15)
            {
                most = "여행";
            }else if (partTimeJob == 15)
            {
                most = "알바";
            }
            else
            {
                most = "운동";
            }
            end.PlayEnd(images[0], "엔딩 1. 한 우물만 파기", "이번 방학엔 "+ most + "만 했다.. 보람은 있지만, 다른 걸 못해본게 아쉬워. 다음 방학엔 더 다양한 활동을 해야겠어!");   
        }
        else
        {
            end.PlayEnd(images[2], "엔딩 3. 선택과 집중", "이번 방학엔 몇 가지가 기억에 남아. 얻은 게 많았지만, 다음 방학엔 다른 것도 미리 열심히 해봐야겠어!");
        }
    }
    
    private IEnumerator EarnMoneyPerSec()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        
        while (isPlaying)
        {
            creditManager.EarnCreditPerSec();
            yield return waitForSeconds;
        }
    }
    
    public bool GetIsPlaying()
    {
        return isPlaying;
    }
}
