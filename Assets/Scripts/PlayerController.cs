using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed;
    public float runningSpeed;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal");
        float curSpeedY = (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical");

        Vector3 move = transform.right * curSpeedX + transform.forward * curSpeedY;

        // Move the controller
        characterController.Move(move * Time.deltaTime);
    }
}
