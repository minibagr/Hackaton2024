using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Vector3 playerPosition;
    public bool invincibility;
    public float health;
    public float mouseSensitivity;
    public float stamina;
    public float clampAngle;
    public float maxHealth;
    public float playerDamage;
    public float speed;
    public float maxStamina;
    public float normalSpeed, sprintSpeed, jumpHeigth, gravity;
    public float staminaCooldownTimer, staminaCooldownTime;
    public float staminaRegenModifier, staminaUsageModifier;
    public bool canSprint, isGrounded;
}

[System.Serializable]
public class DoorData
{
    public bool isOpen;
    public float rotationAngle;
    public float rotationSpeed;
}

[System.Serializable]
public class SaveData
{
    public PlayerData playerData;
    public DoorData doorData;
}
