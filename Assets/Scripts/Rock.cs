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
    private GameObject effectPrefab;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip effectClip;
    [SerializeField]
    private AudioClip debrisClip;

    public void Mining()
    {
        audioSource.clip = effectClip;
        audioSource.Play();
        GameObject clone = Instantiate(effectPrefab, col.bounds.center, Quaternion.identity);
        Destroy(clone, destroyTime);

        hp--;
        if (hp <= 0)
            Destruction();
    }

    private void Destruction()
    {
        audioSource.clip = debrisClip;
        audioSource.Play();
        col.enabled = false;
        Destroy(rockObj);

        debrisObj.SetActive(true);
        Destroy(debrisObj, destroyTime);
    }
}
