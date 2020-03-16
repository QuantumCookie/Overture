using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_EnemyManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;

    private ScoreManager scoreManager;
    private GameManager_Master gameManagerMaster;

    public int waves = 5;
    public float spawnInterval = 2.5f;
    public int enemiesPerWave = 10;
    public float timeBetweenWaves = 5f;

    private List<GameObject> enemies = new List<GameObject>();

    public Text waveInfo;

    public AudioClip enemyExplosionClip;

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
                enemies.Add(Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length - 1)], t.position, t.rotation));

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
        return enemies.Count == 0;
    }

    IEnumerator ShowWaveInfo(int wave)
    {
        waveInfo.text = "Wave" + wave;
        waveInfo.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        waveInfo.gameObject.SetActive(false);
    }

    private void Update()
    {
        CheckEnemyStatus();
    }

    private void CheckEnemyStatus()
    {
        List<GameObject> dump = new List<GameObject>();

        foreach(GameObject g in enemies)
        {
            if(!g.activeSelf)
            {
                dump.Add(g);
                AudioSource.PlayClipAtPoint(enemyExplosionClip, g.transform.position);
                //Destroy(g, 2f);
                //UpdateScore(g.GetComponent<Enemy_Data>().scoreValue);
                Debug.Log("Enemy Dead");
            }
        }

        foreach(GameObject g in dump)
        {
            enemies.Remove(g);
            Destroy(g);
        }
    }

    /*private void UpdateScore(int value)
    {
        scoreManager.UpdateScoreBy(value);
    }*/
}