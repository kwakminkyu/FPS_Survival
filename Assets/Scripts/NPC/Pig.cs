using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pig : WeekAnimal
{
    protected override void ResetAnimal()
    {
        base.ResetAnimal();
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

    private void Walk()
    {
        isWalking = true;
        isRunning = false;
        currentTime = walkTime;
        nav.speed = walkSpeed;
        anim.SetBool("Walking", true);
        Debug.Log("°È±â");
    }
}
