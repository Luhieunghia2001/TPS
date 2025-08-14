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

    public void ApplyBonusStats(Item item)
    {
        if (item == null) return;

        PlayerData.maxHeal += item.bonusHeal;
        PlayerData.Amor += item.bonusAmor;
        PlayerData.Speed += item.bonusSpeed;

        if (item.bonusHeal > 0)
        {
            PlayerData.currentHeal += item.bonusHeal;
        }

        SavaData();
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
