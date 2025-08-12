using System.Collections.Generic;
using UnityEngine;

public class ItemPanelOpener : MonoBehaviour
{
    public ItemManager itemManager;

    public void OpenItemSelectionPanel()
    {
        if (itemManager != null && itemManager.allAvailableItems.Count > 0)
        {
            List<Item> itemsToDisplay = GetRandomItems(3);
            itemManager.ShowCardSelection(itemsToDisplay);
        }
    }

    private List<Item> GetRandomItems(int count)
    {
        List<Item> availableItems = new List<Item>();
        foreach (Item item in itemManager.allAvailableItems)
        {
            if (item != null)
            {
                availableItems.Add(item);
            }
        }

        if (availableItems.Count == 0) return new List<Item>();

        List<Item> randomItems = new List<Item>();

        for (int i = 0; i < count && availableItems.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, availableItems.Count);
            Item selectedItem = availableItems[randomIndex];

            randomItems.Add(selectedItem);

            availableItems.RemoveAt(randomIndex);
        }

        return randomItems;
    }
}