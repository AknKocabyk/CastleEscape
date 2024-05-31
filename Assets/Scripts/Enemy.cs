using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected int damage;
    [SerializeField] protected float cooldown;
    [SerializeField] protected float enemyRotationTime;
    [SerializeField] protected TMP_Text enemyLevelText;
    [SerializeField] protected Image enemyLevelImage;

    public  float enemyLevel;
    public bool dead;
    
    protected GameObject player;

    public Animator anim;
    protected Rigidbody rb;
    protected float distance;
    protected float timer;
   
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }



    private void FixedUpdate()
    {
        if (!dead)
        {
            Move();
        }
        else
        {
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            this.gameObject.GetComponent<Animator>().SetBool("Dead", true);
        }

    }

    public virtual void Attack()
    {

    }


    public virtual void Move()
    {
    }
}
