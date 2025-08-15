using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZomebieHeal : MonoBehaviour
{
    [SerializeField] float _maxHeal;
    [SerializeField] float _currentHeal;

    ZombieRagdoll zombieRagdoll;
    ZombieAI zombieAI; 

    void Start()
    {
        _currentHeal = _maxHeal;
        zombieRagdoll = GetComponentInParent<ZombieRagdoll>();
        zombieAI = GetComponentInParent<ZombieAI>();
    }

    public void TakeDamage(float damage)
    {
        _currentHeal -= damage;

        if (_currentHeal <= 0)
        {
            Debug.Log("Zombie dead.");

            if (zombieAI != null)
            {
                zombieAI.SetDead();
            }

            if (zombieRagdoll != null)
            {
                zombieRagdoll.Die();
            }
        }
    }
}
