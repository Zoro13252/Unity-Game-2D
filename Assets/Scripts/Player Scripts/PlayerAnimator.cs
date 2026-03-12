using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    public bool IsMoving { private get; set; }
    public bool IsJumping { private get; set; }
    public bool IsJumpingToFall { private get; set; }
    public bool Attack { private get; set; }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        animator.SetBool("IsMoving", IsMoving);
        animator.SetBool("IsJumping", IsJumping);
        animator.SetBool("IsJumpingToFall", IsJumpingToFall);
        animator.SetBool("Attack", Attack);
    }
}
