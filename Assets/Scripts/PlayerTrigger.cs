using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private Renderer blueDoorKey;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private PlayerMovementController playerMovementController;
    [SerializeField] private GameObject keyVfx;
    [SerializeField] private GameObject upgraderVfx;

    public bool isAttacked;
    public float playerLevel;
    private bool key;

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
            GameObject vfx = Instantiate(upgraderVfx, other.transform.position, upgraderVfx.transform.rotation);
            Destroy(vfx, 3f);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Key"))
        {
            key = true;
            GameObject vfx = Instantiate(keyVfx, other.transform.position, keyVfx.transform.rotation);
            Destroy(vfx, 3f);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.GetComponent<Enemy>().enemyLevel < playerLevel)
        {
            collision.gameObject.GetComponent<Enemy>().dead=true;
            Destroy(collision.gameObject, 3f);
            isAttacked = true;
            Invoke("AttackFalse", 1f);
            print("Düşman Öldürüldü...");
        }
        
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.GetComponent<Enemy>().enemyLevel > playerLevel)
        {
            this.gameObject.GetComponent<PlayerMovementController>().PlayerDead();
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            Invoke("LevelManager", 3f);
            print("Game Over...");
        }

        if(collision.gameObject.CompareTag("NextLevel") && key)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            print("Yeni Sahne Yüklendi");
        }
    }

    void AttackFalse()
    {
        isAttacked = false;
    }

    void LevelManager()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
