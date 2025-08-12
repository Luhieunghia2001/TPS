using UnityEngine;

[CreateAssetMenu(fileName ="new item", menuName = "Item/Base Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemMota;
    public Sprite itemIcon;

    [Header("Stat bonus")]
    public float bonusHeal;
    public float bonusAmor;
    public float bonusSpeed;

    [Header("Gun bounus")]
    public float bonusCooldown;
    public int bonusMagazineSize;
    public int bonusTotalAmmo;
}
