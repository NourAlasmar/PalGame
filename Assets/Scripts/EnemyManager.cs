using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public Enemy[] enemies;
    void Start()
    {
        InvokeRepeating(nameof(SpawnNewEnemy), 1f, 1f);

    }


    void SpawnNewEnemy()
    {
        var randomIndex = Random.Range(0, enemies.Length);
        var enemyToSpawn = enemies[randomIndex];
        var randomX = Random.Range(enemyToSpawn.minX, enemyToSpawn.maxX);
        var pos = new Vector3(randomX, 0, 0);
        Instantiate(enemyToSpawn, pos + transform.position, Quaternion.identity, transform);

    }
}
