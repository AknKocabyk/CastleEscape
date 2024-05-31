using System.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private DynamicJoystick dynamicJoystick;
    [SerializeField] private Transform playerParentTransform;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float doorOpenSecond;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private float Playerhealth;

    private float _horizontal, _vertical;
    public bool playerDead;


    private void Start()
    {
    }

    private void Update()
    {
        MovementInput();

    }

    private void FixedUpdate()
    {
        Movement();
        SetRotation();
    }



    public void PlayerDead()
    {
        this.enabled = false;
        animationController.SetBool("Dead", true);
    }

    private void Movement()
    {
        playerRb.velocity = NewVelocity();
    }

    private Vector3 NewVelocity()
    {
        return new Vector3(_horizontal,playerRb.velocity.y,_vertical) * playerSpeed *Time.fixedDeltaTime;
    }

    private void SetRotation()
    {
        if(_horizontal != 0 || _vertical != 0 && this.gameObject.GetComponent<PlayerTrigger>().isAttacked==false)
        {
            playerParentTransform.rotation = Quaternion.LookRotation(NewVelocity());
            animationController.SetBool("Run", true);
            animationController.SetBool("Idle", false);
            animationController.SetBool("Attack", false);
        }
        else if(this.gameObject.GetComponent<PlayerTrigger>().isAttacked == true)
        {
            animationController.SetBool("Run", false);
            animationController.SetBool("Idle", false);
            animationController.SetBool("Attack", true);
        }
        else if(_horizontal == 0 || _vertical == 0)
        {
            animationController.SetBool("Run", false);
            animationController.SetBool("Idle", true);
            animationController.SetBool("Attack",false);
        }
    }

    private void MovementInput()
    {
        _horizontal = dynamicJoystick.Horizontal;
        _vertical = dynamicJoystick.Vertical;
    }
}
