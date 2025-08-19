using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public static EndGame Instance;

    public GameObject losePanel;
    public Button Restart;


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

    }

    void RestartGame()
    {
        SceneManager.LoadScene("Home");
    }
}
