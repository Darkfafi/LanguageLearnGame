using UnityEngine;
using System.Collections;
using Ramses.Confactory;
using System;

public class ConCurrentLevel :  IConfactory
{
	public LevelPlayground CurrentLevel { get; private set; }

	public LevelLibrary LevelLibrary { get; private set; }

	public void SetCurrentLevel(string levelName)
	{
		SetCurrentLevel(LevelLibrary.GetLevelByName(levelName));
    }

	public void SetCurrentLevel(LevelPlayground level)
	{
		CurrentLevel = level;
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
