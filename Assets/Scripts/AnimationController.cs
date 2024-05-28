using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetBool(string animation, bool value)
    {
        animator.SetBool(animation, value);
    }
}
