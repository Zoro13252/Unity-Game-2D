using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    public bool IsMoving { get; set; }
    public bool IsJumping { get; set; }


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        animator.SetBool("IsMoving", IsMoving);
        animator.SetBool("IsJumping", IsJumping);
    }


    public void PlayAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void PlayDeath()
    {
        animator.SetTrigger("IsDeathing");
    }


    // public bool IsAttacking()
    // {
    //     return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
    // }

    // public bool IsInState(string stateName)
    // {
    //     return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    // }

}