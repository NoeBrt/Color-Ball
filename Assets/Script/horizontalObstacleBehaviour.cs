using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacleBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private float distance;

    [SerializeField]
    private float offset;

    [SerializeField]
    private float angle;

    [SerializeField]
    private GameObject elementPrefab;

    [SerializeField]
    private float scale = 0.2f;

    private List<GameObject> elements;
    private Camera mainCamera;
    private float screenRightBoundary;

    void Start()
    {
        elements = new List<GameObject>();
        mainCamera = Camera.main;

        // Calculate screen right boundary
        float cameraWidth = 2f * mainCamera.orthographicSize * mainCamera.aspect;
        screenRightBoundary = mainCamera.transform.position.x + cameraWidth / 2 + 5;

        for (int i = 0; i < 4; i++)
        {
            Vector3 position = new Vector3(
                transform.position.x + offset - i * distance,
                transform.position.y,
                transform.position.z
            );
            GameObject newElement = Instantiate(elementPrefab, position, Quaternion.identity);
            newElement.transform.localScale = new Vector3(scale, scale, 1f);
            newElement.transform.parent = transform;

            elements.Add(newElement);
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        // Move
        transform.position += speed * Time.deltaTime * transform.right;

        for (int i = 0; i < elements.Count; i++)
        {
            GameObject element = elements[i];

            if (element.transform.position.x > screenRightBoundary)
            {
                elements.RemoveAt(i);

                GameObject lastElement = elements[elements.Count - 1];

                element.transform.localPosition = new Vector3(
                    lastElement.transform.localPosition.x - distance * 3.33f,
                    element.transform.localPosition.y,
                    element.transform.localPosition.z
                );
                elements.Add(element);
            }
        }
    }
}
