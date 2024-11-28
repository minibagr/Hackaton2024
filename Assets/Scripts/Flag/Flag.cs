using UnityEngine;

public class Flag : MonoBehaviour {
    [SerializeField] private bool canTake;

    private void OnTriggerEnter(Collider other) {
        if (other == null || !canTake) return;

        if (other.tag == "Player") Debug.Log("Do Something");
    }
}
