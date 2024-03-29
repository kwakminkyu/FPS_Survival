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
    private float runSpeed;
    private float applySpeed;
    [SerializeField]
    private float runTime;
    [SerializeField]
    private float waitTime;

    private Vector3 direction;
    private float currentTime;

    private bool isAction;
    private bool isWalking;
    private bool isRunning;
    private bool isDead;

    private Animator anim;
    private AudioSource audioSource;
    private Rigidbody rigid;
    private BoxCollider boxCol;

    [SerializeField]
    private AudioClip[] pigNomalSound;
    [SerializeField]
    private AudioClip pigHurt;
    [SerializeField]
    private AudioClip pigDead;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
        currentTime = waitTime;
        isAction = true;
    }

    private void Update()
    {
        if (!isDead)
        {
            Move();
            Rotation();
            ElapseTime();
        }
    }

    private void Move()
    {
        if (isWalking || isRunning)
        {
            rigid.MovePosition(transform.position + (transform.forward * applySpeed * Time.deltaTime));
            Debug.Log(applySpeed);
        }
    }

    private void Rotation()
    {
        if (isWalking || isRunning)
        {
            Vector3 rotation = Vector3.Lerp(transform.eulerAngles, new Vector3(0, direction.y, 0), 0.01f);
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
                ResetPig(); // 랜덤 액션 개시
            }
        }
    }

    private void ResetPig()
    {
        isAction = false;
        isWalking = false;
        isRunning = false;
        applySpeed = walkSpeed;
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        direction.Set(0, Random.Range(0f, 360f), 0);
        RandomAction();
    }

    private void RandomAction()
    {
        isAction = true;
        RandomSound();

        int ran = Random.Range(0, 4);

        if (ran == 0)
            Wait();
        else if (ran == 1)
            Eat();
        else if (ran == 2)
            Peek();
        else if (ran == 3)
            Walk();
    }

    private void Wait()
    {
        currentTime = waitTime;
        Debug.Log("대기");
    }

    private void Eat()
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");
        Debug.Log("풀뜯기");
    }

    private void Peek()
    {
        currentTime = waitTime;
        anim.SetTrigger("Peek");
        Debug.Log("두리번");
    }

    private void Walk()
    {
        isWalking = true;
        isRunning = false;
        currentTime = walkTime;
        applySpeed = walkSpeed;
        anim.SetBool("Walking", true);
        Debug.Log("걷기");
    }

    public void Run(Vector3 targetPos)
    {
        direction = Quaternion.LookRotation(transform.position - targetPos).eulerAngles;

        isWalking = false;
        isRunning = true;
        currentTime = runTime;
        applySpeed = runSpeed;
        anim.SetBool("Running", true);
        Debug.Log("달리기");
    }

    public void Damage(int damage, Vector3 targetPos)
    {
        if (!isDead)
        {
            hp -= damage;

            if (hp <= 0)
            {
                Dead();
                return;
            }
            PlaySE(pigHurt);
            anim.SetTrigger("Hurt");
            Run(targetPos);
        }
    }

    private void Dead()
    {
        PlaySE(pigDead);
        isWalking = false;
        isRunning = false;
        isDead = true;
        anim.SetTrigger("Dead");
    }

    private void RandomSound()
    {
        int ran = Random.Range(0, 3);
        PlaySE(pigNomalSound[ran]);
    }

    private void PlaySE(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
