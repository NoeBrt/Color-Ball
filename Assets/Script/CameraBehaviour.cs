using System.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;

    private Vector3 initialPosition;
    private bool isResetting = false;
    private Camera mainCamera;

    void Start()
    {
        // Store the initial position of the camera
        initialPosition = transform.position;
        mainCamera = Camera.main;
    }

    void OnEnable()
    {
        // Subscribe to the Death action
        PlayerActions.Death += OnPlayerDeath;
    }

    void OnDisable()
    {
        // Unsubscribe from the Death action
        PlayerActions.Death -= OnPlayerDeath;
    }

    void LateUpdate()
    {
        if (isResetting)
        {
            // Stop resetting if the player clicks

            ResetPosition();

            // Check if the player is within the camera bounds and enable the player controller
            if (IsPlayerWithinCameraBounds())
            {
                player.GetComponent<playerController>().enabled = true;
                if (Input.GetMouseButtonDown(0))
                {
                    isResetting = false;
                }
            }
        }
        else if (player.position.y > transform.position.y)
        {
            Vector3 desiredPosition = new Vector3(
                transform.position.x,
                player.position.y,
                transform.position.z
            );
            Vector3 smoothedPosition = Vector3.Lerp(
                transform.position,
                desiredPosition,
                smoothSpeed * Time.deltaTime
            );
            transform.position = smoothedPosition;
        }
    }

    private void ResetPosition()
    {
        // Smoothly move the camera back to its initial position
        transform.position = Vector3.Lerp(
            transform.position,
            initialPosition,
            smoothSpeed * Time.deltaTime
        );

        // Check if the camera is close enough to the initial position to stop resetting
        if (Vector3.Distance(transform.position, initialPosition) < 0.01f)
        {
            transform.position = initialPosition; // Snap to the exact position
            isResetting = false; // Stop resetting
        }
    }

    private IEnumerator WaitAndReset()
    {
        // Disable player controller during reset
        player.GetComponent<playerController>().enabled = false;

        // Wait for 1.5 seconds
        yield return new WaitForSeconds(1.5f);

        // Start resetting the camera position
        isResetting = true;
    }

    private void OnPlayerDeath()
    {
        // Start the coroutine to wait before resetting
        StartCoroutine(WaitAndReset());
    }

    private bool IsPlayerWithinCameraBounds()
    {
        // Convert player's position to viewport point
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(player.position);

        // Check if the player is within the viewport bounds (0,0) to (1,1)
        return viewportPoint.x >= 0
            && viewportPoint.x <= 1
            && viewportPoint.y >= 0
            && viewportPoint.y <= 1;
    }
}
