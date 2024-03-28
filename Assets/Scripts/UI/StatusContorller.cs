using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusContorller : MonoBehaviour
{
    [SerializeField]
    private int hp;
    private int currentHp;

    [SerializeField]
    private int sp;
    private int currentSp;

    [SerializeField]
    private int spIncreaseSpeed;

    [SerializeField]
    private int spRechargeTime;
    private int currentSpRechargeTime;

    private bool spUsed;

    [SerializeField]
    private int dp;
    private int currentDp;

    [SerializeField]
    private int hungry;
    private int currentHungry;

    [SerializeField]
    private int hungryDecreaseTime;
    private int currentHungryDecreaseTime;

    [SerializeField]
    private int thirsty;
    private int currentThirsty;

    [SerializeField]
    private int thirstyDecreaseTime;
    private int currentThirstyDecreaseTime;

    [SerializeField]
    private int satisfy;
    private int currentSatisfy;

    [SerializeField]
    private Image[] imagesGauge;

    private const int HP = 0, SP = 1, DP = 2, HUNGRY = 3, THIRSTY = 4, SATISFY = 5;

    private void Start()
    {
        currentHp = hp;
        currentSp = sp;
        currentDp = dp;
        currentHungry = hungry;
        currentThirsty = thirsty;
        currentSatisfy = satisfy;
    }

    private void Update()
    {
        Hungry();
        Thirsty();
        SPRechargeTime();
        SPRecover();
        GaugeUpdate();
    }

    private void Hungry()
    {
        if (currentHungry > 0)
        {
            if (currentHungryDecreaseTime <= hungryDecreaseTime)
                currentHungryDecreaseTime++;
            else
            {
                currentHungry--;
                currentHungryDecreaseTime = 0;
            }
        }
        else
            Debug.Log("배고픔 수치가 0이 되었습니다");
    }

    private void Thirsty()
    {
        if (currentThirsty > 0)
        {
            if (currentThirstyDecreaseTime <= thirstyDecreaseTime)
                currentThirstyDecreaseTime++;
            else
            {
                currentThirsty--;
                currentThirstyDecreaseTime = 0;
            }
        }
        else
            Debug.Log("목마름 수치가 0이 되었습니다");
    }

    private void GaugeUpdate()
    {
        imagesGauge[HP].fillAmount = (float) currentHp / hp;
        imagesGauge[SP].fillAmount = (float) currentSp / sp;
        imagesGauge[DP].fillAmount = (float) currentDp / dp;
        imagesGauge[HUNGRY].fillAmount = (float) currentHungry / hungry;
        imagesGauge[THIRSTY].fillAmount = (float) currentThirsty / thirsty;
        imagesGauge[SATISFY].fillAmount = (float) currentSatisfy / satisfy;
    }

    public void IncreaseHP(int count)
    {
        if (currentHp + count < hp)
            currentHp += count;
        else
            currentHp = hp;
    }
    
    public void DecreaseHP(int count)
    {
        if (currentDp > 0)
        {
            DecreaseDP(count);
            return;
        }
        currentHp -= count;

        if (currentHp <= 0)
            Debug.Log("HP가 0이 되었습니다");
    }

    public void IncreaseSP(int count)
    {
        if (currentSp + count < sp)
            currentSp += count;
        else
            currentSp = sp;
    }

    public void IncreaseDP(int count)
    {
        if (currentDp + count < dp)
            currentDp += count;
        else
            currentDp = dp;
    }

    public void DecreaseDP(int count)
    {
        currentDp -= count;

        if (currentDp <= 0)
            Debug.Log("방어력이 0이 되었습니다");
    }

    public void IncreaseHungry(int count)
    {
        if (currentHungry + count < hungry)
            currentHungry += count;
        else
            currentHungry = hungry;
    }

    public void DecreaseHungry(int count)
    {
        if (currentHungry - count < 0)
            currentHungry = 0;
        else
            currentHungry -= count;
    }

    public void IncreaseThirsty(int count)
    {
        if (currentThirsty + count < thirsty)
            currentThirsty += count;
        else
            currentThirsty = thirsty;
    }

    public void DecreaseThirsty(int count)
    {
        if (currentThirsty - count < 0)
            currentThirsty = 0;
        else
            currentThirsty -= count;
    }

    private void SPRechargeTime()
    {
        if (spUsed)
        {
            if (currentSpRechargeTime < spRechargeTime)
                currentSpRechargeTime++;
            else
                spUsed = false;
        }
    }

    private void SPRecover()
    {
        if (!spUsed && currentSp < sp)
            currentSp += spIncreaseSpeed;
    }

    public void DecreaseStamina(int count)
    {
        spUsed = true;
        currentSpRechargeTime = 0;

        if (currentSp - count > 0)
            currentSp -= count;
        else
            currentSp = 0;
    }

    public int GetCurrentSP()
    {
        return currentSp;
    }
}
