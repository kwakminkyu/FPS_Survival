using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeekAnimal : Animal
{
    public void Run(Vector3 targetPos)
    {
        destination = new Vector3(transform.position.x - targetPos.x, 0, transform.position.z - targetPos.z).normalized ;

        isWalking = false;
        isRunning = true;
        currentTime = runTime;
        nav.speed = runSpeed;
        anim.SetBool("Running", true);
        Debug.Log("´Þ¸®±â");
    }

    public override void Damage(int damage, Vector3 targetPos)
    {
        base.Damage(damage, targetPos);
        if (!isDead)
            Run(targetPos);
    }
}
