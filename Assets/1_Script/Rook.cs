using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Rook : MonoBehaviour
{
    [SerializeField] private float _maxHeal;
    [SerializeField] private float _currentHeal;

    [Header("UI")]
    public TextMeshProUGUI healText;
    public Image healBar;

    private void Start()
    {
        _currentHeal = _maxHeal;
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        healText.text = $"{ _currentHeal}/{ _maxHeal}";
        healBar.fillAmount = _currentHeal / _maxHeal;
    }

    public void TakeDamage(float damage)
    {
        _currentHeal -= damage;

        if (_currentHeal < 0)
        {
            _currentHeal = 0;
        }

        Debug.Log("Rook received " + damage + " damage. Current heal: " + _currentHeal);

        UpdateUI(); 
        GameOver(); 
    }

    void GameOver()
    {
        if(_currentHeal<=0)
        {
            Debug.Log("You lose");
        }
    }

}
