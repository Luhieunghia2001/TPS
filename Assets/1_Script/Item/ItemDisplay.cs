using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public Image itemIconImage;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemMotaText;
    public TextMeshProUGUI bonusStatsText;

    private Item currentItem;

    public void DisplayItemInfo(Item itemDisplay)
    {
        if (itemDisplay != null)
        {
            currentItem = itemDisplay;

            if (itemIconImage != null && itemDisplay.itemIcon != null)
            {
                itemIconImage.sprite = itemDisplay.itemIcon;
            }

            if (itemNameText != null)
            {
                itemNameText.text = itemDisplay.itemName;
            }

            if (itemMotaText != null)
            {
                itemMotaText.text = itemDisplay.itemMota;
            }

            if (bonusStatsText != null)
            {
                System.Text.StringBuilder statsBuilder = new System.Text.StringBuilder();

                //bonus Stats
                if(itemDisplay.bounusDamage > 0)
                {
                    statsBuilder.AppendLine($"Tăng sát thương: +{itemDisplay.bounusDamage}");

                }
                if (itemDisplay.bonusHeal > 0)
                {
                    statsBuilder.AppendLine($"Tăng máu: +{itemDisplay.bonusHeal}");
                }

                if (itemDisplay.bonusAmor > 0)
                {
                    statsBuilder.AppendLine($"Tăng giáp: +{itemDisplay.bonusAmor}");
                }

                if (itemDisplay.bonusSpeed > 0)
                {
                    statsBuilder.AppendLine($"Tăng tốc độ: +{itemDisplay.bonusSpeed}");
                }


                //bounus Gun
                if (itemDisplay.bonusCooldown > 0)
                {
                    statsBuilder.AppendLine($"Tăng tốc độ bắn: +{itemDisplay.bonusCooldown}");
                }

                if (itemDisplay.bonusMagazineSize > 0)
                {
                    statsBuilder.AppendLine($"Tăng cỡ băng đạn: +{itemDisplay.bonusMagazineSize}");
                }

                if (itemDisplay.bonusTotalAmmo > 0)
                {
                    statsBuilder.AppendLine($"Tăng tổng đạn: +{itemDisplay.bonusTotalAmmo}");
                }

                bonusStatsText.text = statsBuilder.ToString();
            }
        }
    }

    public void ApplyItem()
    {
        ItemManager.Instance.OnCardSelected(currentItem);

        ToggleCursor.Instance.ToggleCursorLock();
    }
}