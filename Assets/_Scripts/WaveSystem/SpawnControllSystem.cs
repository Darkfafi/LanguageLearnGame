using UnityEngine;
using System.Collections;
using Ramses.Confactory;

public class SpawnControllSystem : MonoBehaviour
{
	[SerializeField]
	private EnemySpawnpoint[] AllSpawnpointsInLevel;

	private ConEnemyTracker conEnemyTracker;
	private ConSelectedLanguages conSelectedLanguage;

	private void Start ()
	{
		conEnemyTracker = ConfactoryFinder.Instance.Give<ConEnemyTracker>();
		conSelectedLanguage = ConfactoryFinder.Instance.Give<ConSelectedLanguages>();
	}
}
