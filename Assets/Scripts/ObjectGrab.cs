using UnityEngine;

public class ObjectGrab : MonoBehaviour {
    public float grabDistance = 10f;
    public float grabSmoothSpeed = 10f;
    public float launchForce = 1000f;
    public Transform holdPoint;

    private GameObject grabbedObject = null;
    private Rigidbody grabbedRigidbody = null;

    void Update() {
        if (grabbedObject != null) {
            HoldObject();

            if (Input.GetMouseButtonDown(0)) {
                LaunchObject();
            }
            else if (Input.GetMouseButtonDown(1)) {
                DropObject();
            }
        } else {
            if (Input.GetMouseButtonDown(0)) {
                TryGrabObject();
            }
        }
    }

    private void TryGrabObject() {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, grabDistance)) {
            if (hit.collider.GetComponent<Rigidbody>() != null) {
                grabbedObject = hit.collider.gameObject;
                grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();

                grabbedRigidbody.useGravity = false;
                grabbedRigidbody.linearVelocity = Vector3.zero;
                grabbedRigidbody.angularVelocity = Vector3.zero;
            }
        }
    }

    private void HoldObject() {
        Vector3 directionToHold = holdPoint.position - grabbedObject.transform.position;
        grabbedRigidbody.linearVelocity = directionToHold * grabSmoothSpeed;
    }

    private void DropObject() {
        if (grabbedObject != null) {
            grabbedRigidbody.useGravity = true;

            grabbedObject = null;
            grabbedRigidbody = null;
        }
    }

    private void LaunchObject() {
        if (grabbedObject != null) {
            grabbedRigidbody.useGravity = true;

            grabbedRigidbody.AddForce(transform.forward * launchForce, ForceMode.Impulse);

            grabbedObject = null;
            grabbedRigidbody = null;
        }
    }
}