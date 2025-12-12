using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner :MonoBehaviour
{
    [Header("Limits")]
    [SerializeField] private Transform Upper;
    [SerializeField] private Transform Lower;

    [Header("Objects")]
    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemyData[] enemyTypes;
    
    [SerializeField] private Collectable collectable;
    [SerializeField] private CollectableData[] collectableTypes;

    [Header("Difficulty Settings")]
    [SerializeField] private float difficultyInterval = 20f; 
    [SerializeField] private float difficultyMultiplier = 0.95f;  

    private int difficultyLevel = 0;

    private void Start()
    {
        StartCoroutine(DifficultyRoutine());

        foreach (var enemy in enemyTypes)
            StartCoroutine(SpawnEnemyRoutine(enemy));

        foreach (var col in collectableTypes)
            StartCoroutine(SpawnCollectableRoutine(col));
    }

    private IEnumerator DifficultyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(difficultyInterval);

            difficultyLevel++;
        }
    }

    private IEnumerator SpawnEnemyRoutine(EnemyData enemyData)
    {
        while (true)
        {
            float modifiedTime = enemyData.SpawnTime * Mathf.Pow(difficultyMultiplier, difficultyLevel);

            yield return new WaitForSeconds(modifiedTime);

            Enemy newEnemy = Instantiate(enemy, GetRandomYPosition(), Quaternion.identity);
            newEnemy.Init(enemyData);
        }
    }

    private IEnumerator SpawnCollectableRoutine(CollectableData col)
    {
        while (true)
        {
            float modifiedTime = col.SpawnTime * Mathf.Pow(difficultyMultiplier, difficultyLevel);

            yield return new WaitForSeconds(modifiedTime);

            Collectable newCollectable = Instantiate(collectable, GetRandomYPosition(), Quaternion.identity);
            newCollectable.Init(col);
        }
    }

    private Vector3 GetRandomYPosition()
    {
        float y = Random.Range(Lower.position.y, Upper.position.y);
        float x = Upper.position.x;
        return new Vector3(x, y, 0);
    }
}
