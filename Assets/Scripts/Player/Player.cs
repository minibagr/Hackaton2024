using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private Rigidbody rb;

    [SerializeField] private bool isGrounded = true;

    [SerializeField] private Transform playerCamera, cameraPoint;
    [SerializeField] private float mouseSensitivity = 1000.0f, clampAngle = 80.0f, rotY, rotX;

    public float health = 100f, stamina = 100f;

    [SerializeField] private Vector3 moveDir;
    [SerializeField] private float speed = 10f, jumpHeigth = 5f, gravity = 9.14f;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        if (Time.timeScale == 0) return;

        playerCamera.position = cameraPoint.position;

        /* --- | Camera | --- */
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotY = rotY % 360f;
        rotX = rotX % 360f;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion CameraRotation = Quaternion.Euler(-rotX, rotY, 0);
        Quaternion BodyRotation = Quaternion.Euler(0, rotY, 0);

        playerCamera.rotation = CameraRotation;
        transform.rotation = BodyRotation;

        /* --- | Movement | --- */
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveDir = transform.forward * z + transform.right * x;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.AddForce(new Vector3(0, jumpHeigth, 0), ForceMode.Impulse);
            isGrounded = false;
        }

        if (moveDir.sqrMagnitude > 1f) moveDir.Normalize();

        Vector3 velocity = moveDir * speed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;
    }

    /* --- | Ground Checking | --- */

    void OnCollisionEnter(Collision collision) {
        if (collision == null) return;

        if (collision.transform.tag == "Ground") isGrounded = true;
    }

    void OnCollisionExit(Collision collision) {
        if (collision == null) return;

        if (collision.transform.tag == "Ground") isGrounded = false;
    }
}