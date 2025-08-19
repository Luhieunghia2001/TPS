using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public static EndGame Instance;

    [SerializeField] private GameObject losePanel;
    [SerializeField] private TextMeshProUGUI Time;
    [SerializeField] private Button Restart;


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
        losePanel.SetActive(false);
        Restart.onClick.AddListener(RestartGame);
    }

    public void OnLosePanel()
    {
        losePanel.SetActive(true);
        GameManager.Instance.StopTimer();

        float elapsed = GameManager.Instance.GetElapsedTime();
        int minutes = Mathf.FloorToInt(elapsed / 60f);
        int seconds = Mathf.FloorToInt(elapsed % 60f);

        Time.text = $"Thời gian phòng thủ: {minutes:00}:{seconds:00}";

    }

    void RestartGame()
    {
        SceneManager.LoadScene("Home");
    }
}
