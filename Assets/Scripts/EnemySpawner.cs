using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private readonly Color gizmoCol = new Color(1.0f, 0.0f, 1.0f, 0.3f);
    private const float spawnDist = 100.0f;
    private const float spawnDistSq = spawnDist * spawnDist;

    public BaseChromable enemyPrefab;
    public float minSpawnTime = 5.0f;
    public float maxSpawnTime = 10.0f;
    public float spawnRadius = 5.0f;
    public int initialSpawnBurst = 1;

    float spawnTimer = 0.0f;
    bool hasInitialSpawned = false;

    void Start()
    {
        initialSpawnBurst = Mathf.Max(1, initialSpawnBurst);
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if ((transform.position - Movement.Instance.transform.position).sqrMagnitude < spawnDistSq)
        {
            if (!hasInitialSpawned)
            {
                for (int i = 0; i < initialSpawnBurst; ++i)
                {
                    Spawn();
                }
                hasInitialSpawned = true;
            }

            if (spawnTimer <= 0.0f)
            {
                Spawn();
                spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
            }
        }
    }

    void Spawn()
    {
        Instantiate(enemyPrefab, transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * Random.Range(-spawnRadius, spawnRadius), Quaternion.identity);
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = gizmoCol;
        Gizmos.DrawSphere(Vector3.zero, spawnRadius);
        Gizmos.DrawWireSphere(Vector3.zero, spawnRadius);
    }
}
