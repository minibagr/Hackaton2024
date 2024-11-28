using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour {
    [SerializeField] private TMP_Text name, text;
    [SerializeField] private AnimationClip[] animations;
    [SerializeField] private float time;
    private static Dialog dialog;
    private Animation show;

    public void Update() {
        if (time > 0) {
            time -= Time.deltaTime;
            if (time <= 0) {
                show.clip = animations[1];
                show.Play();
            }
        }
    }

    // Example: Dialog.PlayDialog("Name:", "Interesting Text.", 5f);
    public static void PlayDialog(string name, string text, float waitTime) {
        if (dialog == null) {
            Dialog[] myItems = FindObjectsOfType(typeof(Dialog)) as Dialog[];

            List<Dialog> dialogs = new List<Dialog>();

            foreach (Dialog item in myItems) dialogs.Add(item);

            if (dialogs.Count == 1) {
                dialog = dialogs.ToArray()[0];
                dialog.Change(name, text, waitTime);
            }
            else if (dialogs.Count > 1) Debug.LogWarning("Multiple dialog systems found!");
            else Debug.LogWarning("No dialog system found!");
        } else {
            dialog.Change(name, text, waitTime);
        }
        
    }

    private void Start() {
        show = gameObject.GetComponent<Animation>();
    }

    private void Change(string name, string text, float waitTime) {
        this.name.text = name;
        this.text.text = text;

        show.clip = animations[0];
        show.Play();

        time = waitTime;
    }
}
