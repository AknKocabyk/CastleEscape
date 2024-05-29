using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierEnemy : Enemy
{
    [SerializeField] float speed;
    [SerializeField] float detectionDistance;
    protected float patrolTimer;


    public override void Move()
    {
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        patrolTimer += Time.deltaTime;
        if (patrolTimer > enemyRotationTime)
        {
            transform.Rotate(new Vector3(0, 90, 0));
            patrolTimer = 0;
        }
    }

}
