using UnityEngine;
using System.Collections;
using Ramses.Confactory;

public class EnemySpawnpoint : MonoBehaviour {

	public Path.PathTypes PathType { get { return pathType; } }

	[SerializeField]
	private Path.PathTypes pathType = Path.PathTypes.Ground;

	[SerializeField]
	private int pathIndex = 0;

	private ConEnemyTracker conEnemyTracker;
	private ConCurrentLevel comCurrentLevel;

	protected void Awake()
	{
		conEnemyTracker = ConfactoryFinder.Instance.Give<ConEnemyTracker>();
		comCurrentLevel = ConfactoryFinder.Instance.Give<ConCurrentLevel>();
	}

	public void SpawnUnit(string unitName)
	{
		Enemy prefab = conEnemyTracker.EnemyLibrary.GetEnemyPrefab(unitName);
        Enemy enemy = Instantiate<Enemy>(prefab);
        enemy.transform.position = transform.position;
		enemy.PathWalker.SetWalker(comCurrentLevel.CurrentLevel.LevelPaths[pathIndex], enemy.MovementSpeed);
    }
}
