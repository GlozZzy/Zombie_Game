using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public float moveSpeed;
    public int health;
    public int ammo;
    Cursor cursor;
    Shot shot;
    public Transform gunBarrelOut;
    public Transform gunBarrelIn;
    public AudioSource shotSound;
    public AudioSource deathSound;
    public AudioSource FootstepSound;

    CapsuleCollider capsuleCollider;
    Animator animator;
    MovementAnimator movementAnimator;
    float dead;

    // Start is called before the first frame update
    void Start()
    {
        dead = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();
        cursor = FindObjectOfType<Cursor>();
        navMeshAgent.updateRotation = false;
        shot = FindObjectOfType<Shot>();

        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponentInChildren<Animator>();
        movementAnimator = GetComponent<MovementAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused) return;

        if (dead > 0)
        {
            health = 0;
            dead -= Time.deltaTime;
            if (dead <= 1)
            {
                Destroy(gameObject);
            }
            return;
        }

        if (health <= 0)
        {
            health = 0;
            Kill();
        }

        Vector3 dir = Vector3.zero;
        bool bW = (Input.GetKey(KeyCode.W) ? true : false);
        bool bS = (Input.GetKey(KeyCode.S) ? true : false);
        bool bA = (Input.GetKey(KeyCode.A) ? true : false);
        bool bD = (Input.GetKey(KeyCode.D) ? true : false);
        if (bW)
        {
            dir.z = -1.0f;
            dir.x = 1.0f;
        }
        if (bS)
        {
            dir.z = 1.0f;
            dir.x = -1.0f;
        }
        if (bA)
        {
            dir.z = 1.0f;
            dir.x = 1.0f;
        }
        if (bD)
        {
            dir.z = -1.0f;
            dir.x = -1.0f;
        }

        if (bW && bA)
        {
            dir.z = 0.0f;
            dir.x = 1.0f;
        }
        if (bA && bS)
        {
            dir.z = 1.0f;
            dir.x = 0.0f;
        }
        if (bS && bD)
        {
            dir.z = 0.0f;
            dir.x = -1.0f;
        }
        if (bW && bD)
        {
            dir.z = -1.0f;
            dir.x = 0.0f;
        }
        navMeshAgent.velocity = dir.normalized * moveSpeed;
        if (dir.magnitude > 0) 
            if (!FootstepSound.isPlaying) FootstepSound.Play();


        Vector3 forward = cursor.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(forward.x, 0, forward.z));

        if (Input.GetMouseButtonDown(0) && (ammo > 0))
        {
            var from = gunBarrelIn.position;
            var to = gunBarrelOut.position;
            var direction = (to - from).normalized;


            RaycastHit hit;
            if (Physics.Raycast(from, to - from, out hit, 200))
            {
                to = new Vector3(hit.point.x, from.y, hit.point.z);
                if (hit.transform != null)
                {
                    var zombie = hit.transform.GetComponent<Zombie>();
                    if (zombie != null)
                        zombie.Damage();
                }
            }
            else
                to = from + direction * 100;
            shotSound.Play();
            shot.Show(from, to);
            ammo--;
        }
    }

    private void Kill()
    {
        deathSound.Play();
        dead = 4.0f;
        Destroy(capsuleCollider);
        Destroy(movementAnimator);
        Destroy(navMeshAgent);
        animator.SetTrigger("died");
    }
}
