using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        originDrag = 0;
        originColor = RenderSettings.fogColor;
        originFogDensity = RenderSettings.fogDensity;
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
