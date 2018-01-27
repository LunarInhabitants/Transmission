using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : BaseChromable
{
    public float chickenSpeed = 15.0f;
    public float attackDist = 2.0f;
    public ParticleSystem deathParticles;

    Animator animator;
    new Rigidbody rigidbody;

    float animOffset;
    float attackDistSq;
    bool isOnDeathRow = false;

    protected override void Start()
    {
        base.Start();

        attackDistSq = attackDist * attackDist;

        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();

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
        if (!isOnDeathRow)
        {
            Vector3 toPlayer = Movement.Instance.transform.position - transform.position;

            rigidbody.AddForce(toPlayer.normalized * (chickenSpeed * 100.0f) * Time.deltaTime, ForceMode.Acceleration);
            rigidbody.rotation = Quaternion.Lerp(rigidbody.rotation, Quaternion.LookRotation(rigidbody.velocity.normalized), 0.2f);
            animator.SetFloat("Speed", rigidbody.velocity.sqrMagnitude);

            if (toPlayer.sqrMagnitude <= attackDistSq)
            {
                animator.SetTrigger("IsAttacking");
            }

            if (R + G + B > 2.9f)
            {
                isOnDeathRow = true;
                StartCoroutine(DeathAnim());
            }
        }

        base.Update();
    }

    IEnumerator DeathAnim()
    {
        animator.SetFloat("Speed", 0.0f);

        for (float i = 0; i < 1.0f; i += Time.deltaTime * 2.0f)
        {
            brightnessMult = 1.0f + i * 5.0f;
            transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(0.01f, 0.01f, 0.01f), i);
            yield return null;
        }

        deathParticles.transform.SetParent(null, true);
        deathParticles.transform.localScale = Vector3.one;
        deathParticles.Play();
        Destroy(deathParticles, 8.0f);
        Destroy(gameObject);
    }
}
