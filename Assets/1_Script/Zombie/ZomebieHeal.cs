using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZomebieHeal : MonoBehaviour
{
    [SerializeField] float _maxHeal;
    [SerializeField] float _currentHeal;

    [SerializeField] private Image healBar;

    [SerializeField] private GameObject panel;

    ZombieRagdoll zombieRagdoll;
    ZombieAI zombieAI; 

    void Start()
    {
        _currentHeal = _maxHeal;
        zombieRagdoll = GetComponentInParent<ZombieRagdoll>();
        zombieAI = GetComponentInParent<ZombieAI>();
        panel.SetActive(false);
    }



    public void TakeDamage(float damage)
    {
        _currentHeal -= damage;

        panel.SetActive(true);


        UpdateUI();

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

    private void UpdateUI()
    {
        float healbar = _currentHeal/_maxHeal;
        healBar.fillAmount = healbar;
    }
}
