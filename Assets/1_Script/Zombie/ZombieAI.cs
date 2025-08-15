using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ZombieAI : MonoBehaviour
{
    public float chaseDistance = 15f;
    public float attackDistance = 2f;
    public float attackDamage = 10f;
    public float targetUpdateInterval = 0.5f;

    private NavMeshAgent agent;
    private Animator animator;
    private bool isDead = false;

    private Transform target;
    private Rook rookScript;
    private PlayerStats playerStats;

    private IEnumerator findTargetCoroutine;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        findTargetCoroutine = FindTargetRoutine();
        StartCoroutine(findTargetCoroutine);
    }

    void Update()
    {
        if (isDead) return;

        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance <= chaseDistance)
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
                animator.SetBool("isWalking", true);

                if (distance <= attackDistance)
                {
                    agent.isStopped = true;
                    animator.SetTrigger("Attack");
                    Attack();
                }
            }
            else
            {
                agent.isStopped = true;
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("isWalking", false);
        }
    }

    private IEnumerator FindTargetRoutine()
    {
        while (!isDead)
        {
            FindClosestTarget();
            yield return new WaitForSeconds(targetUpdateInterval);
        }
    }

    private void FindClosestTarget()
    {
        GameObject[] rooks = GameObject.FindGameObjectsWithTag("Rook");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        GameObject closestTargetObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject rook in rooks)
        {
            float distance = Vector3.Distance(transform.position, rook.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTargetObject = rook;
            }
        }

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTargetObject = player;
            }
        }

        if (closestTargetObject != null)
        {
            target = closestTargetObject.transform;
            if (closestTargetObject.CompareTag("Rook"))
            {
                rookScript = closestTargetObject.GetComponent<Rook>();
                playerStats = null;
            }
            else if (closestTargetObject.CompareTag("Player"))
            {
                playerStats = closestTargetObject.GetComponent<PlayerStats>();
                rookScript = null;
            }
            else
            {
                rookScript = null;
                playerStats = null;
            }
        }
        else
        {
            target = null;
            rookScript = null;
            playerStats = null;
        }
    }

    public void SetDead()
    {
        isDead = true;
        agent.isStopped = true;
        animator.SetBool("isWalking", false);
        animator.enabled = false;
        StopCoroutine(findTargetCoroutine);
    }

    public void Attack()
    {
        if (rookScript != null)
        {
            rookScript.TakeDamage(attackDamage);
        }
        else if (playerStats != null)
        {
            playerStats.TakeDamage(attackDamage);
        }
    }
}
