using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandContorller : MonoBehaviour
{
    public static bool isActivate = false;

    [SerializeField]
    private Hand currentHand;

    private bool isAttack = false;
    private bool isSwing = false;

    private RaycastHit hitInfo;

    private void Update()
    {
        if (isActivate)
            TryAttack();
    }

    private void TryAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!isAttack)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    private IEnumerator AttackCoroutine()
    {
        isAttack = true;
        currentHand.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentHand.attackDealyA);
        isSwing = true;
        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(currentHand.attackDealyB);
        isSwing = false;

        yield return new WaitForSeconds(currentHand.attackDealy - currentHand.attackDealyA - currentHand.attackDealyB);
        isAttack = false;
    }
    
    private IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                isSwing = false;
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }

    private bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, currentHand.range))
        {
            return true;
        }
        return false;
    }

    public void HandChange(Hand hand)
    {
        if (WeaponManager.currentWeapon != null)
            WeaponManager.currentWeapon.gameObject.SetActive(false);

        currentHand = hand;
        WeaponManager.currentWeapon = currentHand.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentHand.anim;

        currentHand.transform.localPosition = Vector3.zero;
        currentHand.gameObject.SetActive(true);
        isActivate = true;
    }
}
