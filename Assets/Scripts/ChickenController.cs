using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : BaseChromable
{
    Animator animator;

    float animOffset;

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        switch (Random.Range(0, 3))
        {
            default:
            case 0:
                R = 1.0f;
                G = 1.0f;
                B = 0.0f;
                break;
            case 1:
                R = 1.0f;
                G = 0.0f;
                B = 1.0f;
                break;
            case 2:
                R = 0.0f;
                G = 1.0f;
                B = 1.0f;
                break;
        }

        animOffset = Random.Range(0.0f, 5.0f);
    }

    protected override void Update()
    {
        animator.SetFloat("Speed", Mathf.Sin(Time.time + animOffset));
        if ((Time.time + animOffset) % 3.0f < 0.1f)
            animator.SetTrigger("IsAttacking");
        base.Update();
    }
}
