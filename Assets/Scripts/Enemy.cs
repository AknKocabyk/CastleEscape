using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected int damage;
    [SerializeField] protected float cooldown;
    [SerializeField] protected float enemyRotationTime;
    
    protected GameObject player;

    protected Animator anim;
    protected Rigidbody rb;
    protected float distance;
    protected float timer;
   
    public bool dead = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        Attack();
    }

    private void FixedUpdate()
    {
        Move();
    }


    public void ChangeHealth(int count)
    {
        health -= count;
        if (health <= 0)
        {
            dead = true;
            Destroy(this.gameObject);
        }
    }


    public virtual void Move()
    {
    }
    public virtual void Attack()
    {
    }
}
