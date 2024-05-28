using System.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private FixedJoystick fixedJoystick;
    [SerializeField] private Transform playerParentTransform;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float doorOpenSecond;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private Renderer blueDoorKey;
    [SerializeField] private Animator doorAnimator;

    
    private float _horizontal;
    private float _vertical;

    private void Update()
    {
        MovementInput();
    }

    private void FixedUpdate()
    {
        Movement();
        SetRotation();
    }

    private void Movement()
    {
        playerRb.velocity = NewVelocity();
        animationController.SetBool("Run", _horizontal != 0 || _vertical != 0);
    }

    private Vector3 NewVelocity()
    {
        return new Vector3(_horizontal,playerRb.velocity.y,_vertical) * playerSpeed *Time.fixedDeltaTime;
    }

    private void SetRotation()
    {
        if(_horizontal != 0 || _vertical != 0)
        {
            playerParentTransform.rotation = Quaternion.LookRotation(NewVelocity());
            animationController.SetBool("Run", true);
        }
    }

    private void MovementInput()
    {
        _horizontal = fixedJoystick.Horizontal;
        _vertical = fixedJoystick.Vertical;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BlueDoorKey"))
        {
            print("Kapı Açıldı");
            blueDoorKey.material.color = Color.yellow;
            doorAnimator.enabled = true;
            
        }
    }

    
}
