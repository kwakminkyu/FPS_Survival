using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private int hp;

    [SerializeField]
    private float destroyTime;

    [SerializeField]
    private SphereCollider col;

    [SerializeField]
    private GameObject rockObj;

    [SerializeField]
    private GameObject debrisObj;

    [SerializeField]
    private GameObject itemObj;

    [SerializeField]
    private int itemCount;

    [SerializeField]
    private GameObject effectPrefab;

    [SerializeField]
    private string strikeSound;
    [SerializeField]
    private string destroySound;

    public void Mining()
    {
        SoundManager.Instance.PlaySE(strikeSound);
        
        GameObject clone = Instantiate(effectPrefab, col.bounds.center, Quaternion.identity);
        Destroy(clone, destroyTime);

        hp--;
        if (hp <= 0)
            Destruction();
    }

    private void Destruction()
    {
        SoundManager.Instance.PlaySE(destroySound);

        col.enabled = false;
        for (int i = 0; i < itemCount; i++)
        {
            Instantiate(itemObj, rockObj.transform.position, Quaternion.identity);
        }
        Destroy(rockObj);

        debrisObj.SetActive(true);
        Destroy(debrisObj, destroyTime);
    }
}
