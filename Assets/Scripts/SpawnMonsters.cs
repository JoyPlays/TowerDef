using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnMonsters : MonoBehaviour
{
	public enum SpawnState
	{
		SPAWNING,
		WAITING,
		COUNTING
	}
	
	[System.Serializable]
	public class Wave
	{
		public string name;
		public GameObject enemy;
		public int count;
		public float rate;
	}

	[Header("Statistics")]
	[SerializeField]
	private int enemyCount = 0;

	[Header("Wave Configuration")]
	public Wave[] waves;
	private int nextWave = 0;
	private int currentWave;

	[Header("Settings")]
	public float timeBetweenWaves = 5f;
	public float waveCountdown = 0f;

	[Header("UI")]
	public Text monsterCountText;
	public Text currentWaveText;

	private float searchCountdown = 1f;

	private SpawnState state = SpawnState.COUNTING;

	private void Start()
	{
		waveCountdown = timeBetweenWaves;
	}

	void Update()
    {
		currentWave = nextWave + 1;
		currentWaveText.text = "WAVE " + currentWave.ToString();
		if (state == SpawnState.WAITING)
		{
			// Check if enemies are still alive
			if (!EnemyIsAlive())
			{
				// Begin a new round;
				WaveCompleted();
			}
			else
			{
				return;
			}
		}

		if (waveCountdown <= 0)
		{
			if (state != SpawnState.SPAWNING)
			{
				// Start spawning the wave.
				StartCoroutine(SpawnWave(waves[nextWave]));
			}
		}
		else
		{
			waveCountdown -= Time.deltaTime;
		}
    }

	bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if (GameObject.FindGameObjectWithTag("Enemy") == null)
			{
				return false;
			}
		}
		return true;

	}

	IEnumerator SpawnWave(Wave _wave)
	{
		Debug.Log("Spawning Wave: " + _wave.name);
		state = SpawnState.SPAWNING;

		// Spawn
		for (int i = 0; i < _wave.count; i++)
		{
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds(1f/_wave.rate);
		}

		// Waiting
		state = SpawnState.WAITING;
		yield break;
	}

	void SpawnEnemy(GameObject _enemy)
	{
		
		// Spawn Enemy
		Debug.Log("Spawning Enemy: " + _enemy.name);
		Instantiate(_enemy, transform.position, transform.rotation);
		enemyCount++;
		monsterCountText.text = Mathf.Floor(enemyCount).ToString();
	}

	void WaveCompleted()
	{
		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;
		Debug.Log("Wave Completed!");

		if (nextWave + 1 > waves.Length - 1)
		{
			// THIS IS THE PLACE OF GAME FINISH SCREEN.


			nextWave = 0;
			Debug.Log("ALL WAVES COMPELTE!");
		}
		else
		{
			nextWave++;
		}
	}
}
