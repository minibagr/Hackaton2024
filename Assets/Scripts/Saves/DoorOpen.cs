using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Transform doorTransform; // Assign the door's transform in the Inspector
    public float rotationAngle = 90f; // Angle to rotate (e.g., 90 degrees for open)
    public float rotationSpeed = 2f; // Speed of rotation
    public bool isOpen = false; // Track the door's state

    private Quaternion closedRotation; // Initial rotation
    private Quaternion openRotation; // Target rotation

    void Start()
    {
        // Set up initial and target rotations
        closedRotation = doorTransform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0, rotationAngle, 0);
    }

    void Update()
    {
        // Smoothly interpolate between closed and open rotations
        doorTransform.rotation = Quaternion.Slerp(doorTransform.rotation,
            isOpen ? openRotation : closedRotation,
            Time.deltaTime * rotationSpeed);
    }

    // Call this method to toggle the door state
    public void ToggleDoor()
    {
        Dialog.PlayDialog("Door", "You successfully opened the door.", 5f);
        isOpen = !isOpen;
    }

    public DoorData SaveData()
    {
        return new DoorData
        {
            isOpen = isOpen,
            rotationAngle = rotationAngle,
            rotationSpeed = rotationSpeed
        };
    }

    public void LoadData(DoorData doorData)
    {
        isOpen = doorData.isOpen;
        rotationAngle = doorData.rotationAngle;
        rotationSpeed = doorData.rotationSpeed;
    }
}
