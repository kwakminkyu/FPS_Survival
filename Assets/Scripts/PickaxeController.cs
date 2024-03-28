using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeController : CloseWeaponController
{
    public static bool isActivate = true;

    private void Start()
    {
        WeaponManager.currentWeapon = currentCloseWeapon.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentCloseWeapon.anim;
    }

    private void Update()
    {
        if (isActivate)
            TryAttack();
    }

    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                if (hitInfo.transform.CompareTag("Rock"))
                    hitInfo.transform.GetComponent<Rock>().Mining();
                else if (hitInfo.transform.CompareTag("NPC"))
                {
                    SoundManager.Instance.PlaySE("Animal_Hit");
                    hitInfo.transform.GetComponent<Pig>().Damage(1, transform.position);
                }
                isSwing = false;
            }
            yield return null;
        }
    }

    public override void CloseWeaponChange(CloseWeapon weapon)
    {
        base.CloseWeaponChange(weapon);
        isActivate = true;
    }
}
