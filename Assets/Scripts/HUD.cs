using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private GunController gunController;
    private Gun currentGun;

    [SerializeField]
    private GameObject bulletHUD;

    [SerializeField]
    private TextMeshProUGUI[] bulletText;

    private void Update()
    {
        CheckBullet();
    }

    private void CheckBullet()
    {
        currentGun = gunController.GetGun();
        bulletText[0].text = currentGun.carryBulletCount.ToString();
        bulletText[1].text = currentGun.reloadBulletCount.ToString();
        bulletText[2].text = currentGun.currentBulletCount.ToString();
    }
}
