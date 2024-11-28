using System;
using TMPro;
using UnityEngine;

public class CheckForCode : MonoBehaviour
{
    public String code = "1234";
    public DoorOpen doorOpen;
    public GameObject player;
    private TMP_Text text;
    private String invalid = "Invalid code";
    private String right = "Solved";
    private bool canType = true;
    private bool solved;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (solved)
        {
            text.text = right;
            canType = false;
        }
        else if (text.text.Length == 4)
        {
            if (text.text == code)
            {
                doorOpen.ToggleDoor();
                Hide();
                solved = true;
            }
            else
            {
                canType = false;
                Invoke(nameof(ResetText), 2f);
                text.text = invalid;
            }
        }
    }

    private void ResetText()
    {
        canType = true;
        text.text = "";
    }

    public void AddText(String t)
    {
        if (canType)
            text.text += t;
    }

    public void Hide()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<Player>().enabled = true;
        transform.parent.transform.parent.gameObject.SetActive(false);
    }
}
