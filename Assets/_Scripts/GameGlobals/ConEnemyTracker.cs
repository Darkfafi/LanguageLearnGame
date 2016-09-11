using UnityEngine;
using System.Collections.Generic;
using Ramses.Confactory;
using System;

public class ConEnemyTracker : IConfactory
{
	public delegate void EnemyTrackerHandler(Enemy enemy);

	public event EnemyTrackerHandler EnemyAddedEvent;
	public event EnemyTrackerHandler EnemyDiedEvent;
	public event EnemyTrackerHandler EnemyRemovedEvent;

	private List<Enemy> currentEnemiesTracking = new List<Enemy>();

	public EnemyLibrary EnemyLibrary { get; private set; }

	public void RegisterEnemy(Enemy enemy)
	{
		if (!currentEnemiesTracking.Contains(enemy))
		{
			currentEnemiesTracking.Add(enemy);
			if (EnemyAddedEvent != null)
			{
				EnemyAddedEvent(enemy);
			}
		}
    }

	public void UnregisterEnemy(Enemy enemy)
	{
		if (currentEnemiesTracking.Contains(enemy))
		{
			currentEnemiesTracking.Remove(enemy);
			if (EnemyRemovedEvent != null)
			{
				EnemyRemovedEvent(enemy);
			}
		}
	}

	public void RegisteredEnemyDied(Enemy enemy)
	{
		if (currentEnemiesTracking.Contains(enemy))
		{
			if (EnemyDiedEvent != null)
			{
				EnemyDiedEvent(enemy);
			}
		}
	}

	public void ConClear()
	{
		
	}

	public void ConStruct()
	{
		EnemyLibrary = Resources.Load<EnemyLibrary>("Libraries/EnemyLibrary");
    }

	public void OnSceneSwitch(int newSceneIndex)
	{

	}
}
