using UnityEngine;
using System.Collections;
using Ramses.Confactory;
using System;

public class ConCurrentLevel :  IConfactory
{

	public LevelPlayground CurrentLevelObject { get { return spawnedLevel; } }
	public string CurrentLevelName { get; private set; }

	public LevelLibrary LevelLibrary { get; private set; }

	private LevelPlayground spawnedLevel;

	public void SetCurrentLevel(string levelName)
	{
		CurrentLevelName = levelName;
	}

	public LevelPlayground CreateCurrentLevelObject() 
	{
		RemoveCurrentLevelObject();
		spawnedLevel = GameObject.Instantiate<LevelPlayground>(LevelLibrary.GetLevelByName(CurrentLevelName));
		return spawnedLevel;
	}

	public void RemoveCurrentLevelObject() 
	{
		if (spawnedLevel != null) 
		{
			GameObject.Destroy(spawnedLevel);
			spawnedLevel = null;
        }
	}

	public void ConClear()
	{

    }

	public void ConStruct()
	{
		LevelLibrary = Resources.Load<LevelLibrary>("Libraries/LevelLibrary");
    }

	public void OnSceneSwitch(int newSceneIndex)
	{

	}
}
