using UnityEngine;
using System.Collections;
using System;

public class Enemy : TypeableTarget, IHealthUser
{
	public Health UserHealth
	{
		get
		{
			return health;
        }
	}
	[SerializeField]
	private Health health = new Health();

	public float MovementSpeed
	{
		get { return movementSpeed; }
	}
	[SerializeField]
	private float movementSpeed = 3;

	public PathWalker PathWalker { get; private set; }

	protected override void Awake()
	{
		base.Awake();
		PathWalker = gameObject.AddComponent<PathWalker>();
		PathWalker.StartPathWalking();
        health.HealhDamagedEvent += OnHealthDamagedEvent;
		Ramses.Confactory.ConfactoryFinder.Instance.Give<ConEnemyTracker>().RegisterEnemy(this);
	}

	protected void OnDestroy()
	{
		health.HealhDamagedEvent -= OnHealthDamagedEvent;
		Ramses.Confactory.ConfactoryFinder.Instance.Give<ConEnemyTracker>().UnregisterEnemy(this);
	}

	private void OnHealthDamagedEvent(Health health, int previousHealth)
	{
		if(!health.IsAlive)
		{
			Die();
		}
	}

	private void Die()
	{
		Ramses.Confactory.ConfactoryFinder.Instance.Give<ConEnemyTracker>().RegisteredEnemyDied(this);
		Debug.Log("Die!");
		Destroy(gameObject);
	}
}
