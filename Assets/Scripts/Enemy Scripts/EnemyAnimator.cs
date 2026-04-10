using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;
    public bool IsAttacking { get; set; }
    public bool IsRunning { get; set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        animator.SetBool("IsAttacking", IsAttacking);
        animator.SetBool("IsRunning", IsRunning);
    }
}
