using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SoldierEnemy : Enemy
{
    [SerializeField] float speed;
    [SerializeField] float detectionDistance;
    protected float patrolTimer;
    private float playerLevel;

    private void Update()
    {
        enemyLevelText.text = enemyLevel.ToString();
        playerLevel= player.GetComponent<PlayerTrigger>().playerLevel;

        if (enemyLevel < playerLevel)
        {
            enemyLevelImage.color = Color.green;
        }
    }
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

    public override void Attack()
    {
        this.gameObject.GetComponent<Animator>().SetBool("Attack", true);
        Invoke("ThisEnabledFalse", 0.3f);
    }

    public void ThisEnabledFalse()
    {
        this.enabled = false;
    }

}
