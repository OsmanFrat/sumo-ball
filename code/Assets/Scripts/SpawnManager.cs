using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject strongEnemy;
    public GameObject powerUpPrefab;
    public GameObject gunPowerUp;
    public GameObject smashPowerUp;
    public GameObject boss;
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;
    public int powerUpNumber = 1;
    void Start()
    {
        float repeatRate = Random.Range(5f,10f);
        
        SpawnEnemyWave(waveNumber);
        SpawnPowerUp(powerUpNumber);
        InvokeRepeating("SpawnStrongEnemy", 5f, 15f);
        InvokeRepeating("SpawnSmashPowerUp", 5f, repeatRate);
        InvokeRepeating("SpawnGunPowerUp", 3f, repeatRate);
        Invoke("SpawnBoss", 25f);
    }


    void SpawnEnemyWave(int enemyToSpawn)
    {
        
        for(int i = 0; i < enemyToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    void SpawnStrongEnemy()
    {
        Instantiate(strongEnemy, GenerateSpawnPosition(), strongEnemy.transform.rotation);
    }

    void SpawnGunPowerUp()
    {

        Instantiate(gunPowerUp, GenerateSpawnPosition(), gunPowerUp.transform.rotation);
        
    }

    void SpawnSmashPowerUp()
    {
        Instantiate(smashPowerUp, GenerateSpawnPosition(), smashPowerUp.transform.rotation);
         
    }

    void SpawnPowerUp(int powerUpToSpawn)
    {
        for(int i = 0; i < powerUpToSpawn; i++)
        {
            Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
              

        }
    }
    
    void SpawnBoss()
    {

        Instantiate(boss, GenerateSpawnPosition(), boss.transform.rotation);
        
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if(enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            SpawnPowerUp(powerUpNumber);
            SpawnGunPowerUp();
            SpawnSmashPowerUp();
        }

        
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }

    /*
    private Vector3 GenerateSpawnPositionForGunPowerUp()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }
    */
}
