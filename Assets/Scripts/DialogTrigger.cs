using UnityEngine;

public class DialogTrigger : MonoBehaviour {
    [SerializeField] private string name, text;
    [SerializeField] private float time;
    [SerializeField] private bool oneTime = true;
    [SerializeField] private bool activated = false;

    private void OnTriggerEnter(Collider other) {
        if (activated || other.gameObject.tag != "Player") return;

        if (oneTime) activated = true;
        
        Dialog.PlayDialog(name, text, time);
    }
}
