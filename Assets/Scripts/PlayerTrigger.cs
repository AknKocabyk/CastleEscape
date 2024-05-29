using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private Renderer blueDoorKey;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private PlayerMovementController playerMovementController;
    public float playerLevel;


    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BlueDoorKey"))
        {
            print("Kapı Açıldı");
            blueDoorKey.material.color = Color.yellow;
            doorAnimator.enabled = true;
        }

        if (other.CompareTag("Upgrader"))
        {
            playerLevel+=10;
            Destroy(other.gameObject);
        }

      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.GetComponent<Enemy>().enemyLevel < playerLevel)
        {
            animationController.SetBool("Attack", true);
            Destroy(collision.gameObject);
            print("Düşman Öldürüldü...");
        }
        
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.GetComponent<Enemy>().enemyLevel > playerLevel)
        {
            collision.gameObject.GetComponent<Enemy>().anim.SetBool("Attack", true);
            this.gameObject.GetComponent<PlayerMovementController>().PlayerDead();
            print("Game Over...");
        }
    }
}
