using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPlayerManager : MonoBehaviour
{
    [Header("Heal")]
    public Image HealBar;
    public TextMeshProUGUI healText;

    [Header("Level")]
    public TextMeshProUGUI LevelText;
    public Image ExpBar;

    private PlayerStats playerStats;

    public TextMeshProUGUI Ammo;


    private void Start()
    {
        playerStats = PlayerStats.Instance;
        UpdateAllUI();
    }

    private void Update()
    {
        UpdateAllUI();
    }

    void UpdateAllUI()
    {
        Update_HealUI();
        Update_Level_Exp();
    }


    void Update_HealUI()
    {
        float fillAmount = playerStats.PlayerData.currentHeal / playerStats.PlayerData.maxHeal;
        HealBar.fillAmount = fillAmount;

        healText.text = $"{playerStats.PlayerData.currentHeal}/{playerStats.PlayerData.maxHeal}";
    }

    void Update_Level_Exp()
    {
        LevelText.text = $"{playerStats.PlayerData.Level}";

        float expAmount = (float)playerStats.PlayerData.expCurrent/playerStats.PlayerData.expTotal;
        ExpBar.fillAmount = expAmount;

    }




    public void UpdateUI(int currentAmmo, int totalAmmo)
    {
        Ammo.text = $"{currentAmmo} / {totalAmmo}";
    }


}
