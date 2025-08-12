using UnityEngine;

public class ToggleCursor : MonoBehaviour
{
    public static ToggleCursor Instance;

    private bool cursorLocked = true;

    public GameObject cameraLook;



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

    void Start()
    {
        LockCursor(true);
        cameraLook.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote)) 
        {
            ToggleCursorLock();

        }
    }

    public void ToggleCursorLock()
    {
        LockCursor(!cursorLocked);
    }

    public void LockCursor(bool isLocked)
    {
        cursorLocked = isLocked;

        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (cameraLook != null)
            {
                cameraLook.SetActive(true);
            }
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            if (cameraLook != null)
            {
                cameraLook.SetActive(false);
            }
            Cursor.visible = true;
        }
    }
}
