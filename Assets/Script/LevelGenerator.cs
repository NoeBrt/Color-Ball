using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject FinishLine;

    [SerializeField]
    private List<GameObject> obstacles;

    [SerializeField]
    private List<GameObject> collectibles;

    [SerializeField]
    int levelSize = 10;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Vector3 playerStartPosition = new Vector3(0, 0, 0);

    [SerializeField]
    private Vector2 distanceRangeBetweenObstacles = new Vector2(2.0f, 5.0f);

    [SerializeField]
    private Vector3 startPosition;

    void Awake()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        Vector3 currentPosition = startPosition;
        int obstacleCooldown = Mathf.Max(1, obstacles.Count / 2); // Adjust the cooldown to your preference

        List<int> recentlyUsedObstacles = new List<int>();

        for (int i = 0; i < levelSize; i++)
        {
            // Place an obstacle
            if (obstacles.Count > 0)
            {
                int obstacleIndex;
                do
                {
                    obstacleIndex = Random.Range(0, obstacles.Count);
                } while (recentlyUsedObstacles.Contains(obstacleIndex));

                GameObject obstacle = obstacles[obstacleIndex];
                Instantiate(obstacle, currentPosition, Quaternion.identity);

                // Update the recently used obstacles list
                recentlyUsedObstacles.Add(obstacleIndex);
                if (recentlyUsedObstacles.Count > obstacleCooldown)
                {
                    recentlyUsedObstacles.RemoveAt(0);
                }
            }

            // Move to the next position within the distance range, vertically
            float distance = Random.Range(
                distanceRangeBetweenObstacles.x,
                distanceRangeBetweenObstacles.y
            );
            currentPosition += new Vector3(0, distance, 0);

            // Randomly decide if a collectible should be placed between obstacles (30% chance)
            if (collectibles.Count > 0 && Random.value < 0.4f)
            {
                GameObject collectible = collectibles[Random.Range(0, collectibles.Count)];
                Vector3 collectiblePosition = currentPosition - new Vector3(0, distance / 1.5f, 0); // Place it between the current and the previous obstacle
                GameObject col = Instantiate(collectible, collectiblePosition, Quaternion.identity);
                //set active the collectible
                col.SetActive(true);
            }
        }

        // Add finish line at the end and name it "Finish"
        GameObject finishline = Instantiate(
            FinishLine,
            currentPosition + Vector3.right * -0.07f,
            Quaternion.identity
        );
        finishline.name = "Finish";
    }

    void PositionPlayer()
    {
        if (player != null)
        {
            player.transform.position = playerStartPosition;
        }
    }
}
