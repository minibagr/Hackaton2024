using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Show()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Hide()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
