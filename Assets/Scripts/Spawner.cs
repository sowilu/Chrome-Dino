using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static bool stop;

    public List<GameObject> obstacles;

    public float spawnRate = 3;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 0, spawnRate);
    }

    void SpawnObstacle()
    {
        if (stop) return;

        var index = Random.Range(0, obstacles.Count);
        var prefab = obstacles[index];

        Instantiate(prefab, transform.position, transform.rotation);
    }
}
