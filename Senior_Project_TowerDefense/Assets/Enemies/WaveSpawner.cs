using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5.5f;
    private float countdown = 2f;
    private bool waveEnd = false;
    public Text waveText;

    //public Text waveCountdownText;

    private int waveIndex = 0;

    void Start()
    {
        //healthSystem = healthObject.GetComponent<Health>();
    }

    void Update()
    {
        if (countdown <= 0f && waveIndex < 21)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            waveEnd = false;
        }
        if (spawnPoint.childCount == 0)
        {
            if (!waveEnd)
            {
                MoneySystem.addCash(10);
            }
            waveEnd = true;
            countdown -= Time.deltaTime;
        }
        if (waveIndex >= 21 && spawnPoint.childCount == 0 && waveEnd)
        {
            gameProgress.gameState = false;
        }
        if (waveText != null)
            waveText.text = "Wave " + waveIndex.ToString();
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1.2f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint);
    }
}
