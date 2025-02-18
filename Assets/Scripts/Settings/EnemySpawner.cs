using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign the prefab in the Inspector
    public float spawnRadius = 22f; // Radius of the spawn circle
    public int numberOfEnemies = 2; // Number of enemies to spawn
    public int enemiesKilled = 0;


    public void SpawnEnemies(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 randomPoint = Random.insideUnitCircle.normalized * spawnRadius;
            Vector3 spawnPosition = new Vector3(randomPoint.x, 0f, randomPoint.y);
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.GetComponent<EnemyHealth>().spawner = this;

            // Immediately point towards the center (0, 0, 0)
            Vector3 directionToCenter = (Vector3.zero - spawnPosition).normalized;
            enemy.transform.rotation = Quaternion.LookRotation(directionToCenter);
        }
    }
}
