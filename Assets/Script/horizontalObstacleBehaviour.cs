using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horizontalObstacleBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float distance = 20.5f;
    [SerializeField] private float angle;
    [SerializeField] private GameObject element;
    [SerializeField] private float scale = 0.2f;
    [SerializeField] private List<GameObject> elements;

    private Camera mainCamera;
    private float screenRightBoundary;

    void Start()
    {
        elements = new List<GameObject>();

        // Get the main camera directly
        mainCamera = Camera.main;

        // Calculate the screen boundaries in world coordinates using the camera's orthographic size
        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        screenRightBoundary = mainCamera.transform.position.x + cameraWidth;

        for (int i = 0; i < 8; i++)
        {
            GameObject newElement = Instantiate(element, new Vector3(transform.position.x + scale + i * distance, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, i % 2 == 0 ? 90 : -90));
            newElement.transform.localScale = new Vector3(scale, scale, 1f);
            newElement.transform.parent = transform;

            int colorIndex = i % GameColors.Colors.Count;
            newElement.GetComponent<SpriteRenderer>().color = GameColors.Colors[colorIndex];
            elements.Add(newElement);
        }
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);

        for (int i = 0; i < elements.Count; i++)
        {
            GameObject element = elements[i];
            if (element.transform.position.x > screenRightBoundary)
            {
                // Teleport to just before the first element in the list
                GameObject firstElement = elements[0];
                element.transform.position = new Vector3(firstElement.transform.position.x - distance, element.transform.position.y, element.transform.position.z);

                // Avoid having the same color as the first element
                int firstElementColorIndex = GameColors.Colors.IndexOf(firstElement.GetComponent<SpriteRenderer>().color);
                int newColorIndex = (firstElementColorIndex + 1) % GameColors.Colors.Count;
                element.GetComponent<SpriteRenderer>().color = GameColors.Colors[newColorIndex];

                // Move the teleported element to the start of the list to maintain order
                elements.RemoveAt(i);
                elements.Insert(0, element);
            }
        }
    }
}
