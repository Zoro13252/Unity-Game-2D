using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartAnimation()
    {
        animator.SetBool("startAnim", true);
    } 
}
