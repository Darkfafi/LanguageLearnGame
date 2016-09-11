using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyLibrary : ScriptableObject {

	[SerializeField]
	private EnemyLibraryData[] enemyData;

	public Enemy GetEnemyPrefab(string enemyName)
	{
		return GetEnemyData(enemyName).EnemyPrefab;
    }

	public Enemy[] GetEnemyPrefabs(Path.PathTypes enemyPathType)
	{
		List<Enemy> enemies = new List<Enemy>();

		for (int i = 0; i < enemyData.Length; i++)
		{
			if (enemyData[i].EnemyPathType == enemyPathType)
			{
				enemies.Add(enemyData[i].EnemyPrefab);
            }
		}

		return enemies.ToArray();
	}

	public Sprite GetEnemyPortrait(string enemyName)
	{
		return GetEnemyData(enemyName).EnemyPortrait;
    }

	private EnemyLibraryData GetEnemyData(string enemyName)
	{
		for (int i = 0; i < enemyData.Length; i++)
		{
			if (enemyData[i].EnemyName == enemyName)
			{
				return enemyData[i];
			}
		}
		Debug.LogError("Could not find enemy with name: '" + enemyName + "' in library.");
		return new EnemyLibraryData();
	}

	[Serializable]
	private struct EnemyLibraryData
	{
		public string EnemyName;
		public Enemy EnemyPrefab;
		public Path.PathTypes EnemyPathType;
		public Sprite EnemyPortrait;
	}
}
