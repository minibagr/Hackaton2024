using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public GameObject keypad;
    public GameObject pauseMenu;
    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            InteractWithKeypad();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.GetComponent<PauseMenu>().Hide();
            }
            else
            {
                pauseMenu.GetComponent<PauseMenu>().Show();
            }
        }
    }

    void InteractWithKeypad()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("Keypad"))
            {
                gameObject.GetComponent<Player>().enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                keypad.SetActive(true);
            }
        }
    }
}