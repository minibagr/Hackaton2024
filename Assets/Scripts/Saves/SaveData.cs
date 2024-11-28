using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Vector3 playerPosition;
    public bool invincibility;
    public float health;
    public float mouseSensitivity;
    public float stamina;
}

[System.Serializable]
public class DoorData
{
    public bool isOpen;
    public float rotationAngle = 90f;
    public float rotationSpeed = 2f;
}

[System.Serializable]
public class SaveData
{
    public PlayerData playerData;
    public DoorData doorData;
}
