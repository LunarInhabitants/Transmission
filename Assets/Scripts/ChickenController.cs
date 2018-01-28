using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : BaseChromable
{
    private const float deathDist = 100.0f;
    private const float deathDistSq = deathDist * deathDist;

    public float chickenSpeed = 15.0f;

    float chickenRadius = 1f;

    public float attackDist = 1.5f;
    float attackSq;

    public float damageToCauseOnAttack = 5.0f;
    public ParticleSystem deathParticles;

    Animator animator;
    new Rigidbody rigidbody;

    bool isOnDeathRow = false;
    bool isAttacking = false;
    
    AudioSource audioSource;

    [SerializeField]
    AudioClip attackReady, attackMade;


    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();

        float attackPlusChickenRad = attackDist + chickenRadius;
        attackSq = attackPlusChickenRad * attackPlusChickenRad;

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
    }

    protected override void Update()
    {
        if ((transform.position - Player.Instance.transform.position).sqrMagnitude > deathDistSq)
        {
            Destroy(gameObject);
            return;
        }

        if (!isOnDeathRow)
        {
            Vector3 toPlayer = Player.Instance.transform.position - transform.position;

            TryAttack(toPlayer);
            

            if (R + G + B > 2.9f)
            {
                isOnDeathRow = true;
                StartCoroutine(DeathAnim());
            }
        }

        base.Update();
    }

    private void FixedUpdate()
    {
        if (!isOnDeathRow)
        {
            Movement(Player.Instance.transform.position);
        }
    }

    void Movement(Vector3 Target)
    {
        Vector3 toPlayer = Target - transform.position;
        rigidbody.AddForce(toPlayer.normalized * (chickenSpeed * 100.0f) * Time.deltaTime, ForceMode.Acceleration);
        Vector3 lookDir = rigidbody.velocity.normalized;
        if (lookDir.sqrMagnitude <= 0.001f)
            lookDir = Vector3.forward;
        rigidbody.rotation = Quaternion.Lerp(rigidbody.rotation, Quaternion.LookRotation(lookDir), 0.2f);
        animator.SetFloat("Speed", rigidbody.velocity.sqrMagnitude);
    }
    
    void TryAttack(Vector3 toPlayer)
    {
        if (isAttacking == false)
        {
            if (toPlayer.sqrMagnitude <= attackSq)
            {
                isAttacking = true;
                StartCoroutine(DoAttack());
            }
        }
    }


    IEnumerator DoAttack()
    {        
        animator.SetTrigger("IsAttacking");
        audioSource.PlayOneShot(attackReady);
        yield return new WaitForSeconds(1);

        Vector3 toPlayer = Player.Instance.transform.position - transform.position;

        if (toPlayer.sqrMagnitude <= attackSq)
        {
            audioSource.PlayOneShot(attackMade);
            Player.Instance.Hurt(damageToCauseOnAttack);
        }

        yield return new WaitForSeconds(0.5f);

        isAttacking = false;
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
