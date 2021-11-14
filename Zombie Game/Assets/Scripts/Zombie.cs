using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Player player;
    public float moveSpeed;
    float MS;
    public int health;
    public int damage;
    public float atackrange;
    public float atackspeed;
    public AudioSource deathSound;
    public ParticleSystem particleSystemKill;
    public ParticleSystem particleSystemDamage;

    CapsuleCollider capsuleCollider;
    Animator animator;
    MovementAnimator movementAnimator;
    bool dead;
    float stuned;
    ScoreCounter scoreCounter;

    // Start is called before the first frame update
    void Start()
    {
        MS = moveSpeed;
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
        navMeshAgent.updateRotation = false;

        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponentInChildren<Animator>();
        movementAnimator = GetComponent<MovementAnimator>();
        scoreCounter = FindObjectOfType<ScoreCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused) return;
        if (dead) return;

        if (health <= 0)
        {
            Kill();
            return;
        }

        if (stuned > 0.0f)
            stuned -= 1;
        else MS = moveSpeed;

        var dist = player.transform.position - transform.position;
        if (dist.magnitude <= atackrange && stuned == 0.0f)
        {
            player.Damage(damage);
            animator.SetTrigger("atack");
            stuned = atackspeed * 25;
            MS = 0.1f;
        }
        navMeshAgent.SetDestination(player.transform.position);
        navMeshAgent.speed = MS;
        Vector3 arr = navMeshAgent.velocity.normalized;
        transform.rotation = Quaternion.LookRotation(new Vector3(arr[0], 0, arr[2])); 
    }


    public void Damage()
    {
        if (!dead)
        {
            health -= 1;
            particleSystemDamage.Play();

        }
    }

    private void Kill()
    {
        if (!dead)
        {
            dead = true;
            deathSound.Play();
            scoreCounter.AddScore(damage*2);
            Destroy(capsuleCollider);
            Destroy(movementAnimator);
            Destroy(navMeshAgent);
            Destroy(gameObject, 5.0f);
            animator.SetTrigger("died");
            particleSystemKill.Play();
        }
    }
}
