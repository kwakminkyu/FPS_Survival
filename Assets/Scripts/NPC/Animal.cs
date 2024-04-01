using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    [SerializeField]
    protected string animalName;
    [SerializeField]
    protected int hp;
    [SerializeField]
    protected float walkSpeed;
    [SerializeField]
    protected float walkTime;
    [SerializeField]
    protected float runSpeed;
    [SerializeField]
    protected float runTime;
    [SerializeField]
    protected float waitTime;

    protected Vector3 destination;
    protected float currentTime;

    protected bool isAction;
    protected bool isWalking;
    protected bool isRunning;
    protected bool isDead;

    protected Animator anim;
    protected AudioSource audioSource;
    protected Rigidbody rigid;
    protected BoxCollider boxCol;
    protected NavMeshAgent nav;

    [SerializeField]
    protected AudioClip[] nomalSound;
    [SerializeField]
    protected AudioClip hurtSound;
    [SerializeField]
    protected AudioClip deadSound;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
        currentTime = waitTime;
        isAction = true;
    }

    private void Update()
    {
        if (!isDead)
        {
            Move();
            ElapseTime();
        }
    }

    private void Move()
    {
        if (isWalking || isRunning)
        {
            nav.SetDestination(transform.position + destination * 5f);
        }
    }

    private void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                ResetAnimal(); // 랜덤 액션 개시
            }
        }
    }

    protected virtual void ResetAnimal()
    {
        isAction = false;
        isWalking = false;
        isRunning = false;
        nav.speed = walkSpeed;
        nav.ResetPath();
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        destination.Set(Random.Range(-0.2f, 0.2f), 0f, Random.Range(0.5f, 1f));
    }

    public virtual void Damage(int damage, Vector3 targetPos)
    {
        if (!isDead)
        {
            hp -= damage;

            if (hp <= 0)
            {
                Dead();
                return;
            }
            PlaySE(hurtSound);
            anim.SetTrigger("Hurt");
        }
    }

    protected void Dead()
    {
        PlaySE(deadSound);
        isWalking = false;
        isRunning = false;
        isDead = true;
        anim.SetTrigger("Dead");
    }

    protected void RandomSound()
    {
        int ran = Random.Range(0, 3);
        PlaySE(nomalSound[ran]);
    }

    protected void PlaySE(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
