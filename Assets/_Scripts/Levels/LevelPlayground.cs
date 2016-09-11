using UnityEngine;
using System;
using System.Collections;
using Ramses.Confactory;

public class LevelPlayground : MonoBehaviour {

	public Path[] LevelPaths { get { return levelPaths; } }

	[SerializeField]
	private Path[] levelPaths;

	[SerializeField]
	private string backgroundMusicName = "BattleMusic";

	private void Awake()
	{
		ConfactoryFinder.Instance.Give<ConAudioManager>().PlaySoloAudio(backgroundMusicName, ConAudioManager.MUSIC_STATION);
		ConfactoryFinder.Instance.Give<ConAudioManager>().SetAudioStationPitch(ConAudioManager.MUSIC_STATION, 0.87f);
    }
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
	public float SpeedModifier { get { return speedModifier; } }
	public Transform[] PathWaypoints { get { return pathWaypoints; } }
	
	[SerializeField]
	private string pathName;

	[SerializeField]
	private Transform[] pathWaypoints;

	[SerializeField]
	private PathTypes pathType;

	[SerializeField]
	private int speedModifier = 1;

}
