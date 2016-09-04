using UnityEngine;
using System;
using System.Collections;

public class LevelPlayground : MonoBehaviour {

	public Path[] LevelPaths { get { return levelPaths; } }

	[SerializeField]
	private Path[] levelPaths;
}

[Serializable]
public class Path
{
	public enum PathTypes
    {
		Ground,
		Air
	}

	public string PathName { get { return pathName; } }
	public PathTypes PathType { get { return pathType; } }

	public Transform[] PathWaypoints { get { return pathWaypoints; } }
	
	[SerializeField]
	private string pathName;

	[SerializeField]
	private Transform[] pathWaypoints;

	[SerializeField]
	private PathTypes pathType;

}
