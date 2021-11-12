using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject powerupPrefab;
    private float spawnRange = 9.5f;
    private int countEnemy;
    private int waveEnemy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        countEnemy = FindObjectsOfType<Enemy>().Length;

        if(countEnemy == 0)
        {
            waveEnemy++;
            SpawnEnemy(waveEnemy);
            Instantiate(powerupPrefab, GenSpawnPos(), powerupPrefab.transform.rotation);
        }
    }

    private void SpawnEnemy(int enemysToSpawn)
    {
        for (int i = 0; i < enemysToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenSpawnPos(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 spawnPos = new Vector3(spawnPosX, 0f, spawnPosZ);

        return spawnPos;
    }
}
