using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    public GameObject cardSelectionPanel;
    public GameObject cardPrefab;
    public Transform cardSpawnPoint;
    public List<Item> allAvailableItems; 

    private List<ItemDisplay> activeCardDisplays = new List<ItemDisplay>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        cardSelectionPanel.SetActive(false);
    }

    public void ShowCardSelection(List<Item> itemsToShow)
    {
        cardSelectionPanel.SetActive(true);
        Time.timeScale = 0f;

        foreach (var card in activeCardDisplays)
        {
            Destroy(card.gameObject);
        }
        activeCardDisplays.Clear();

        foreach (Item item in itemsToShow)
        {
            if (cardPrefab != null)
            {
                GameObject newCardGO = Instantiate(cardPrefab, cardSpawnPoint);
                ItemDisplay cardDisplay = newCardGO.GetComponent<ItemDisplay>();
                if (cardDisplay != null)
                {
                    cardDisplay.DisplayItemInfo(item);
                    activeCardDisplays.Add(cardDisplay);
                }
            }
        }
    }

    public void OnCardSelected(Item selectedItem)
    {
        Debug.Log("Gọi OnCardSelected");

        if (PlayerStats.Instance != null)
        {
            if (selectedItem != null)
            {
                PlayerStats.Instance.ApplyBonusStats(selectedItem);
                Debug.Log($"Đã chọn item: {selectedItem.itemName}. Tăng máu: {selectedItem.bonusHeal}");

                AutomaticShooting automaticShooting = FindFirstObjectByType<AutomaticShooting>();
                if (automaticShooting != null)
                {
                    automaticShooting.ApplyItemBonus(selectedItem.bonusCooldown, selectedItem.bonusMagazineSize, selectedItem.bonusTotalAmmo);
                }
            }
        }

        if (PlayerStats.Instance != null)
        {
            if (selectedItem != null)
            {
                PlayerStats.Instance.ApplyBonusStats(selectedItem);
            }
        }

        cardSelectionPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}