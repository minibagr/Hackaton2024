using UnityEngine;

public class ObjectGrab : MonoBehaviour {
    [SerializeField] private float grabDistance = 5f;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private GameObject grabbedObject;

    public void Update() {
        if (grabbedObject != null) {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ReleaseObject();
            }
            grabbedObject.transform.position = holdPoint.position;
            grabbedObject.transform.rotation = holdPoint.rotation;

        } else if (Input.GetKeyDown(KeyCode.E)) TryGrabObject();
    }

    void TryGrabObject() {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, grabDistance)) {
            if (hit.collider.GetComponent<Rigidbody>() != null) {
                grabbedObject = hit.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    void ReleaseObject() {
        if (grabbedObject != null) {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject = null;
        }
    }
}