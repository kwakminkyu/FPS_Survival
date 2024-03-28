using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pig : MonoBehaviour
{
    [SerializeField]
    private string animalName;
    [SerializeField]
    private int hp;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float walkTime;
    [SerializeField]
    private float waitTime;

    private Vector3 direction;
    private float currentTime;

    private bool isAction;
    private bool isWalking;

    private Animator anim;
    private Rigidbody rigid;
    private BoxCollider boxCol;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
        currentTime = waitTime;
        isAction = true;
    }

    private void Update()
    {
        Move();
        Rotation();
        ElapseTime();
    }

    private void Move()
    {
        if (isWalking)
        {
            rigid.MovePosition(transform.position + (transform.forward * walkSpeed * Time.deltaTime));
        }
    }

    private void Rotation()
    {
        if (isWalking)
        {
            Vector3 rotation = Vector3.Lerp(transform.eulerAngles, direction, 0.01f);
            rigid.MoveRotation(Quaternion.Euler(rotation));
        }
    }

    private void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                ResetPig(); // ·£´ý ¾×¼Ç °³½Ã
            }
        }
    }

    private void ResetPig()
    {
        isWalking = false;
        isAction = false;
        anim.SetBool("Walking", isWalking);
        direction.Set(0, Random.Range(0f, 360f), 0);
        RandomAction();
    }

    private void RandomAction()
    {
        isAction = true;

        int ran = Random.Range(0, 4);

        if (ran == 0)
            Wait();
        else if (ran == 1)
            Eat();
        else if (ran == 2)
            Peek();
        else if (ran == 3)
            TryWalk();
    }

    private void Wait()
    {
        currentTime = waitTime;
        Debug.Log("´ë±â");
    }

    private void Eat()
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");
        Debug.Log("Ç®¶â±â");
    }

    private void Peek()
    {
        currentTime = waitTime;
        anim.SetTrigger("Peek");
        Debug.Log("µÎ¸®¹ø");
    }

    private void TryWalk()
    {
        isWalking = true;
        currentTime = walkTime;
        anim.SetBool("Walking", true);
        Debug.Log("°È±â");
    }
}
