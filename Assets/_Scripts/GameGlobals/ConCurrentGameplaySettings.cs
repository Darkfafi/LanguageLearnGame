﻿using UnityEngine;
using System.Collections;
using Ramses.Confactory;
using System;

public enum GameDifficulties
{
	Easy = 0,
	Medium = 1,
	Hard = 2
}

public class ConCurrentGameplaySettings : IConfactory
{

	public GameplaySettings CurrentGameplaySettings { get; private set; }


	public void SetCurrentGameplaySettings(GameplaySettings settings)
	{
		CurrentGameplaySettings = settings;
	}

	public void ConClear()
	{

	}

	public void ConStruct()
	{

	}

	public void OnSceneSwitch(int newSceneIndex)
	{

	}
}


public struct GameplaySettings
{
	public bool ShowWordToTranslateOnly;
	public GameDifficulties GameDifficulty;
}