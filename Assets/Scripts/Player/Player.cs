using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    [SerializeField] private Rigidbody rb;

    [Header("Camera Settings")]
    [SerializeField] private float clampAngle = 80.0f;
    [SerializeField] private float mouseSensitivity = 1000.0f;
    [SerializeField] private Transform playerCamera, cameraPoint;

    [Header("Player Stats")]
    [SerializeField] private bool invincibility = false;
    [SerializeField] private float health = 100f, maxHealth = 100f;
    [SerializeField] private float playerDamage = 100f;

    [Header("Movement Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float stamina = 100f, maxStamina = 100f;
    [SerializeField] private float normalSpeed = 5f, sprintSpeed = 5f, jumpHeigth = 5f, gravity = 9.14f;
    [SerializeField] private float staminaCooldownTimer = 0, staminaCooldownTime = 5f;
    [SerializeField] private float staminaRegenModifier = 5f, staminaUsageModifier = 5f;
    [SerializeField] private bool canSprint, isGrounded = true;

    [Header("Player UI")]
    [SerializeField] private Slider staminaUISlider;
    [SerializeField] private Slider healthUISlider;


    [Header("Debug Info")]
    [SerializeField] private Vector3 moveDir;
    [SerializeField] private float rotY, rotX;

    /* --- | Thing To Save | --- 
        Required:
            transform.position
            invincibility
            health
            mouse
            stamina
        Optional:
    */
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        if (Time.timeScale == 0) return;

        Camera();
        Movement();
        Crouch();
        UpdateUI();
    }

    /* --- | Camera | --- */

    private void Camera() {
        playerCamera.position = cameraPoint.position;

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
    }

    /* --- | Movement | --- */
    private void Movement() {
        if (!canSprint) {
            staminaCooldownTimer -= Time.deltaTime;

            if (staminaCooldownTimer <= 0) {
                staminaCooldownTimer = staminaCooldownTime;
                canSprint = true;
            }
        }

        if (canSprint && stamina > 0 && Input.GetKey(KeyCode.LeftShift) && moveDir.magnitude != 0) {
            stamina -= Time.deltaTime * staminaUsageModifier;

            if (stamina <= 0) canSprint = false;

            speed = sprintSpeed;

        } else if (stamina < maxStamina) {
            stamina += Time.deltaTime * staminaRegenModifier;
            if (stamina > maxStamina) stamina = maxStamina;

            speed = normalSpeed;

        } else speed = normalSpeed;

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

    /* --- | Crouch | --- */

    private void Crouch() {
        if (Input.GetKey(KeyCode.LeftControl)) transform.localScale = new Vector3(1, 0.5f, 1);
        else transform.localScale = Vector3.one;
    }

    /* --- | Update UI | --- */
    private void UpdateUI() {
        staminaUISlider.value = stamina;
        healthUISlider.value = health;
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

    /* --- | Saving And Loading | --- */

    public PlayerData SaveData() {
        return new PlayerData {
            playerPosition = transform.position,
            invincibility = invincibility,
            health = health,
            mouseSensitivity = mouseSensitivity,
            stamina = stamina,
            clampAngle = clampAngle,
            maxHealth = maxHealth,
            playerDamage = playerDamage,
            speed = speed,
            maxStamina = maxStamina,
            normalSpeed = normalSpeed, 
            sprintSpeed = sprintSpeed, 
            jumpHeigth = jumpHeigth, 
            gravity = gravity,
            staminaCooldownTimer = staminaCooldownTimer, 
            staminaCooldownTime = staminaCooldownTime,
            staminaRegenModifier = staminaRegenModifier, 
            staminaUsageModifier = staminaUsageModifier,
            canSprint = canSprint, 
            isGrounded = isGrounded,
        };
    }

    public void LoadData(PlayerData data) {
        transform.position = data.playerPosition;
        invincibility = data.invincibility;
        health = data.health;
        mouseSensitivity = data.mouseSensitivity;
        stamina = data.stamina;
        clampAngle = data.clampAngle;
        maxHealth = data.maxHealth;
        playerDamage = data.playerDamage;
        speed = data.speed;
        maxStamina = data.maxStamina;
        normalSpeed = data.normalSpeed;
        sprintSpeed = data.sprintSpeed;
        jumpHeigth = data.jumpHeigth;
        gravity = data.gravity;
        staminaCooldownTimer = data.staminaCooldownTimer;
        staminaCooldownTime = data.staminaCooldownTime;
        staminaRegenModifier = data.staminaRegenModifier; 
        staminaUsageModifier = data.staminaUsageModifier;
        canSprint = data.canSprint; 
        isGrounded = data.isGrounded;
    }
}