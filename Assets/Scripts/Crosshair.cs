using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    private float gunAccuracy;

    [SerializeField]
    private GameObject crosshairHUD;

    [SerializeField]
    private GunController gunController;
    
    public void WalkingAnimation(bool flag)
    {
        if (!GameManager.isWater)
        {
            WeaponManager.currentWeaponAnim.SetBool("Walk", flag);
            anim.SetBool("Walking", flag);
        }
    }
    
    public void RunningAnimation(bool flag)
    {
        if (!GameManager.isWater)
        {
            WeaponManager.currentWeaponAnim.SetBool("Run", flag);
            anim.SetBool("Running", flag);
        }
    }

    public void JumpingAnimation(bool flag)
    {
        if (!GameManager.isWater)
            anim.SetBool("Running", flag);
    }

    public void CrouchingAnimation(bool flag)
    {
        if (!GameManager.isWater)
            anim.SetBool("Crouching", flag);
    }

    public void FineSightAnimation(bool flag)
    {
        if (!GameManager.isWater)
            anim.SetBool("FineSight", flag);
    }

    public void FireAnimation()
    {
        if (!GameManager.isWater)
        {
            if (anim.GetBool("Walking") && !anim.GetBool("Crouching"))
                anim.SetTrigger("Walk_Fire");
            else if (anim.GetBool("Crouching"))
                anim.SetTrigger("Crouch_Fire");
            else
                anim.SetTrigger("Idle_Fire");
        }
    }

    public float GetAccuracy()
    {
        if (anim.GetBool("Walking") && !anim.GetBool("Crouching"))
            gunAccuracy = 0.06f;
        else if (anim.GetBool("Crouching"))
            gunAccuracy = 0.01f;
        else if (gunController.GetFineSightMode())
            gunAccuracy = 0.001f;
        else
            gunAccuracy = 0.03f;
        return gunAccuracy;
    }
}