using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LevelLibrary : ScriptableObject {

	public enum LevelDifficulty
	{
		Easy = 0,
		Medium = 1,
		Hard = 2
	}

	public string[] GetAllLevelNames()
	{
		List<string> names = new List<string>(GetAllLevelNames(LevelDifficulty.Easy));
		names.AddRange(GetAllLevelNames(LevelDifficulty.Medium));
		names.AddRange(GetAllLevelNames(LevelDifficulty.Hard));
		return names.ToArray();
	}

	public string[] GetAllLevelNames(LevelDifficulty difficultyFilter)
	{
		List<string> names = new List<string>();
		for (int i = 0; i < levels.Length; i++)
		{
			if (levels[i].LevelDifficulty == difficultyFilter)
			{
				names.Add(levels[i].LevelName);
			}
		}
		return names.ToArray();
	}

	public LevelPlayground GetLevelByName(string name)
	{
		for(int i = 0; i < levels.Length; i++)
		{
			if(levels[i].LevelName == name)
			{
				return levels[i].LevelPrefab;
			}
		}
		Debug.LogError("Level with name '" + name + "' could not be found. Please check the LevelLibrary");
		return null;
	}

	public LevelPlayground[] GetLevelsByDifficulty(LevelDifficulty difficulty)
	{
		List<LevelPlayground> allLevelsOfDifficulty = new List<LevelPlayground>();
		for (int i = 0; i < levels.Length; i++)
		{
			if (levels[i].LevelDifficulty == difficulty)
			{
				allLevelsOfDifficulty.Add(levels[i].LevelPrefab);
            }
		}

		Debug.LogError("Level with name '" + name + "' could not be found. Please check the LevelLibrary");
		return allLevelsOfDifficulty.ToArray(); ;
	}


	[SerializeField]
	private LevelLibItem[] levels;

	[Serializable]
	private struct LevelLibItem
	{
		public string LevelName;
		public LevelLibrary.LevelDifficulty LevelDifficulty;
		public LevelPlayground LevelPrefab;
	}
}
