using UnityEngine;

public class ZombieRagdoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Rigidbody[] allRigidbodies;

    private bool isDead = false;

    void Awake()
    {
        allRigidbodies = GetComponentsInChildren<Rigidbody>();
        SetRagdoll(false);
    }

    public void SetRagdoll(bool active)
    {
        animator.enabled = !active;
        foreach (var rb in allRigidbodies)
        {
            rb.isKinematic = !active;
        }
    }

    public void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;

        SetRagdoll(true);


        Destroy(gameObject, 3f);

        PlayerStats.Instance.AddEXP();


    }
}