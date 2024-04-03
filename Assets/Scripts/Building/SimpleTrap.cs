using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTrap : MonoBehaviour
{
    private Rigidbody[] rigides;

    [SerializeField]
    private GameObject meatObj;

    [SerializeField]
    private int damage;

    private bool isActivated = false;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip soundActivate;

    [SerializeField]
    private StatusContorller playerStatus;

    private void Awake()
    {
        rigides = GetComponentsInChildren<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (!isActivated)
        {
            if (other.gameObject.tag != "Untagged")
            {
                audioSource.clip = soundActivate;
                isActivated = true;
                audioSource.Play();

                Destroy(meatObj);

                for (int i = 0; i < rigides.Length; i++)
                {
                    rigides[i].useGravity = true;
                    rigides[i].isKinematic = false;
                }

                if (other.gameObject.name == "Player")
                {
                    playerStatus.DecreaseHP(damage);
                }
            }
        }
    }
}
