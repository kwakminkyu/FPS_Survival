using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Water : MonoBehaviour
{
    [SerializeField]
    private float waterDrag;
    private float originDrag;

    [SerializeField]
    private Color waterColor;
    [SerializeField]
    private float waterFogDensity;

    [SerializeField]
    private Color waterNightColor;
    [SerializeField]
    private float waterNightFogDensity;

    private Color originColor;
    private float originFogDensity;

    [SerializeField]
    private Color originNightColor;
    [SerializeField]
    private float originNightFogDensity;

    [SerializeField]
    private string soundWaterOut;
    [SerializeField]
    private string soundWaterIn;
    [SerializeField]
    private string soundWaterBreath;

    [SerializeField]
    private float breathTime;
    private float currentBreathTime;

    [SerializeField]
    private float totalOxygen;
    private float currentOxygen;
    private float temp;
    [SerializeField]
    private GameObject baseUI;
    [SerializeField]
    private TextMeshProUGUI totalOxygenText;
    [SerializeField]
    private TextMeshProUGUI currentOxygenText;
    [SerializeField]
    private Image gaugeImage;

    [SerializeField]
    private StatusContorller playerStat;


    private void Awake()
    {
        originDrag = 0;
        originColor = RenderSettings.fogColor;
        originFogDensity = RenderSettings.fogDensity;
        totalOxygenText.text = totalOxygen.ToString();
        currentOxygenText.text = totalOxygen.ToString();
        currentOxygen = totalOxygen;
    }

    private void Update()
    {
        if (GameManager.isWater)
        {
            currentBreathTime += Time.deltaTime;
            if (currentBreathTime >= breathTime)
            {
                SoundManager.Instance.PlaySE(soundWaterBreath);
                currentBreathTime = 0f;
            }
            DecreaseOxygen();
        }
    }

    private void DecreaseOxygen()
    {
        currentOxygen -= Time.deltaTime;
        currentOxygenText.text = Mathf.RoundToInt(currentOxygen).ToString();
        gaugeImage.fillAmount = currentOxygen / totalOxygen;
        
        if (currentOxygen <= 0)
        {
            temp += Time.deltaTime;
            if (temp >= 1)
            {
                playerStat.DecreaseHP(1);
                temp = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
            GetWater(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
            GetOutWater(other);
    }

    private void GetWater(Collider player)
    {
        baseUI.SetActive(true);

        SoundManager.Instance.PlaySE(soundWaterIn);
        GameManager.isWater = true;
        player.transform.GetComponent<Rigidbody>().drag = waterDrag;

        if (!GameManager.isNight)
        {
            RenderSettings.fogColor = waterColor;
            RenderSettings.fogDensity = waterFogDensity;
        }
        else
        {
            RenderSettings.fogColor = waterNightColor;
            RenderSettings.fogDensity = waterNightFogDensity;
        }
    }

    private void GetOutWater(Collider player)
    {
        if (GameManager.isWater)
        {
            baseUI.SetActive(false);

            currentOxygen = totalOxygen;
            SoundManager.Instance.PlaySE(soundWaterOut);
            GameManager.isWater = false;
            player.transform.GetComponent<Rigidbody>().drag = originDrag;

            if (!GameManager.isNight)
            {
                RenderSettings.fogColor = originColor;
                RenderSettings.fogDensity = originFogDensity;
            }
            else
            {
                RenderSettings.fogColor = originNightColor;
                RenderSettings.fogDensity = originNightFogDensity;
            }
        }
    }
}
