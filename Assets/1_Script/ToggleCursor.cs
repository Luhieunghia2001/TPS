using UnityEngine;

public class ToggleCursor : MonoBehaviour
{
    private bool cursorLocked = true;

    void Start()
    {
        LockCursor(true); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote)) 
        {
            LockCursor(!cursorLocked);
        }


    }

    void LockCursor(bool isLocked)
    {
        cursorLocked = isLocked;

        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
