using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TextMeshProUGUI timeText;

    private float elapsedTime;
    private bool isCounting = false;

    private void Awake()
    {
        if(Instance == null)
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
        StartTimer();
    }

    private void Update()
    {
        if (!isCounting) return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timeText.text = $"Thời gian phòng thủ: {minutes:00}:{seconds:00}";
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        isCounting = true;
    }

    public void StopTimer()
    {
        isCounting = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        timeText.text = "00:00";
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

}
