using UnityEngine;

public class Gas : MonoBehaviour {
    [SerializeField] private float maxTime, time;

    private void OnTriggerEnter(Collider other) {
        if (other == null) return;

        if (other.tag == "Player") Dialog.PlayDialog("Programátor", "Nebolí to?", 5);
    }

    private void OnTriggerStay(Collider other) {
        if (other == null) return;

        if (other.tag == "Player") {
            if (time <= 0) {
                other.transform.parent.GetComponent<Player>().UpdateHealth(-5f);
                time = maxTime;
            } else {
                time -= Time.deltaTime;
            }
        }
    }
}
