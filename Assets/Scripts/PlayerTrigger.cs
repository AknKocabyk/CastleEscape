using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private Renderer blueDoorKey;
    [SerializeField] private Animator doorAnimator;

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
