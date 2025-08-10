using TMPro;
using UnityEngine;

public class CanvasPlayerManager : MonoBehaviour
{
    public TextMeshProUGUI Ammo;

    public void UpdateUI(int currentAmmo, int totalAmmo)
    {
        Ammo.text = $"{currentAmmo} / {totalAmmo}";
    }


}
