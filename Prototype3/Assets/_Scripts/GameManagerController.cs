using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class GameManagerController : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;

    private ScoreManager scoreManager;
    private GameManager_Master gameManagerMaster;

    public int waves = 5;
    public float spawnInterval = 2.5f;
    public int enemiesPerWave = 10;
    public float timeBetweenWaves = 5f;

    private int enemyCount;

    public Text waveInfo;

    private void OnEnable()
    {
        Initialize();

        gameManagerMaster.OnGameOver += StopSpawningEnemies;

        StartCoroutine("SpawnEnemies");
    }

    private void Initialize()
    {
        scoreManager = GetComponent<ScoreManager>();
        gameManagerMaster = GetComponent<GameManager_Master>();
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < waves; i++)
        {
            StartCoroutine("ShowWaveInfo", i + 1);

            for (int j = 0; j < enemiesPerWave; j++)
            {
                Transform t = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];
                Instantiate(enemyPrefab, t.position, t.rotation);
                enemyCount++;

                yield return new WaitForSeconds(spawnInterval);
            }

            yield return new WaitUntil(AreTheyDead);
            enemiesPerWave += 5;
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private void StopSpawningEnemies()
    {
        StopCoroutine("SpawnEnemies");
    }

    private bool AreTheyDead()
    {
        return enemyCount == 0;
    }

    IEnumerator ShowWaveInfo(int wave)
    {
        waveInfo.text = "Wave" + wave;
        waveInfo.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        waveInfo.gameObject.SetActive(false);
    }

    public void DecrementEnemyCount()
    {
        enemyCount--;
    }

    public void UpdateScore(int value)
    {
        scoreManager.UpdateScoreBy(value);
    }

    private void OnDisable()
    {
        gameManagerMaster.OnGameOver -= StopSpawningEnemies;
    }
}
