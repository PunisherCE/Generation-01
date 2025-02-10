using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign the prefab in the Inspector
    public int numberOfEnemies = 5; // Number of enemies to spawn
    public float spawnRadius = 22f; // Radius of the spawn circle

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector2 randomPoint = Random.insideUnitCircle.normalized * spawnRadius;
            Vector3 spawnPosition = new Vector3(randomPoint.x, 0f, randomPoint.y);
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Immediately point towards the center (0, 0, 0)
            Vector3 directionToCenter = (Vector3.zero - spawnPosition).normalized;
            enemy.transform.rotation = Quaternion.LookRotation(directionToCenter);
        }
    }
}
