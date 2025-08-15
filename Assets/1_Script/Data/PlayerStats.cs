using UnityEngine;

[System.Serializable]
public class DataPlayer
{
    public int Level;
    public int expTotal;
    public int expCurrent;
    public float maxHeal;
    public float currentHeal;
    public float damage;
    public float Amor;
    public float Speed;
}


public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public DataPlayer PlayerData;

    private ItemPanelOpener itemShop;

    private Animator ani;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        itemShop = FindFirstObjectByType<ItemPanelOpener>();

        ani = GetComponent<Animator>();

        PlayerData = SaveLoadManager.LoadData();


        if (PlayerData == null)
        {
            PlayerData = new DataPlayer();
            PlayerData.Level = 1;
            PlayerData.expTotal = 100;
            PlayerData.expCurrent = 0;
            PlayerData.maxHeal = 1000;
            PlayerData.currentHeal = PlayerData.maxHeal;
            PlayerData.damage = 10;
            PlayerData.Amor = 10;
            PlayerData.Speed = 5f;

            SaveLoadManager.SaveData(PlayerData);
        }

        PlayerData.currentHeal = PlayerData.maxHeal;


    }

    public void AddEXP()
    {
        Debug.Log("Exp +");
        PlayerData.expCurrent += 10;
        LevelUP();
    }

    void LevelUP()
    {
        if(PlayerData.expCurrent >= PlayerData.expTotal)
        {
            PlayerData.Level += 1;
            PlayerData.expCurrent = 0;
            PlayerData.expTotal += 50;

            itemShop.OpenItemSelectionPanel();

            ToggleCursor.Instance.ToggleCursorLock();


        }

    }

    public void ApplyBonusStats(Item item)
    {
        if (item == null) return;
        PlayerData.damage += item.bounusDamage;
        PlayerData.maxHeal += item.bonusHeal;
        PlayerData.Amor += item.bonusAmor;
        PlayerData.Speed += item.bonusSpeed;

        if (item.bonusHeal > 0)
        {
            PlayerData.currentHeal += item.bonusHeal;
        }

        SavaData();
    }

    public void TakeDamage(float damage)
    {
        float actualDamage = damage - PlayerData.Amor;

        if (actualDamage < 1)
        {
            actualDamage = 1;
        }

        PlayerData.currentHeal -= actualDamage;
        if (PlayerData.currentHeal <= 0)
        {
            ani.Play("Die");
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject, 4f);
    }

    public void SavaData()
    {
        SaveLoadManager.SaveData(PlayerData);
    }

    public void DeleteData()
    {
        SaveLoadManager.DeleteData();
    }
}
