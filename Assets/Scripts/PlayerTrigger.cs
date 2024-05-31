using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private Renderer blueDoorKey;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private PlayerMovementController playerMovementController;
    [SerializeField] private GameObject keyVfx, upgraderVfx,deadVfx;
    [SerializeField] private TMP_Text keyPriceText;
    [SerializeField] private TMP_Text playerLevelText;


    public int maxDoorLevelNumber;
    public bool isAttacked;
    public float playerLevel;
    private int keyPrice=0;
    private int levelDoorNumber=0;

    private void Update()
    {
        keyPriceText.text = keyPrice.ToString();
        playerLevelText.text = playerLevel.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BlueDoorKey"))
        {
            blueDoorKey.material.color = Color.green;
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
            GameObject vfx = Instantiate(keyVfx, other.transform.position, keyVfx.transform.rotation);
            Destroy(vfx, 3f);
            Destroy(other.gameObject);
            keyPrice++;
            levelDoorNumber++;
        }

        if (other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<Enemy>().enemyLevel < playerLevel)
        {
            other.gameObject.GetComponent<Enemy>().dead = true;
            Destroy(other.gameObject, 3f);
            isAttacked = true;
            GameObject vfx = Instantiate(deadVfx, other.gameObject.transform.position, deadVfx.transform.rotation);
            Destroy(vfx, 3f);
            Invoke("AttackFalse", 1f);
        }

        if (other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<Enemy>().enemyLevel > playerLevel)
        {
            other.gameObject.GetComponent<Enemy>().Attack();
            this.gameObject.GetComponent<PlayerMovementController>().PlayerDead();
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            GameObject vfx = Instantiate(deadVfx, other.gameObject.transform.position, deadVfx.transform.rotation);
            Destroy(vfx, 3f);
            Invoke("LevelManager", 3f);
            print("Game Over...");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
      

        if(collision.gameObject.CompareTag("NextLevel") && keyPrice>0)
        {
            Destroy(collision.gameObject);
            keyPrice--;

            if (levelDoorNumber == maxDoorLevelNumber)
            {
                LevelCompleted();
            }
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

    void LevelCompleted()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
