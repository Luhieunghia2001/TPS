using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 15f;
    public float attackDistance = 2f;

    private NavMeshAgent agent;
    private Animator animator;
    private bool isDead = false; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (player == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        if (isDead) return;

        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetBool("isWalking", true);

            if (distance <= attackDistance)
            {
                agent.isStopped = true;
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("isWalking", false);
        }
    }

    public void SetDead()
    {
        isDead = true;
        agent.isStopped = true;
        animator.SetBool("isWalking", false);
        animator.enabled = false; 
    }
}
