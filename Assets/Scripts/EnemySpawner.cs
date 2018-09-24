using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool looping = false;

	// Use this for initialization
	IEnumerator Start () {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
        
	}

    private IEnumerator SpawnAllWaves()
    {
        ShuffleWaves();
        for(int i = 0; i < waveConfigs.Count; i++)
        {
            WaveConfig currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {

        for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
        {

            var newEnemy = Instantiate(
            waveConfig.GetEnemyPrefab(),
            waveConfig.GetWaypoints()[0].transform.position,
            Quaternion.identity
            );

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns()); 
        }
    }

    private void ShuffleWaves()
    {
        for(int i = 0; i < waveConfigs.Count; i++)
        {
            WaveConfig temp = waveConfigs[i];
            int randomIndex = Random.Range(i, waveConfigs.Count);
            waveConfigs[i] = waveConfigs[randomIndex];
            waveConfigs[randomIndex] = temp;
        }
    }
}
 