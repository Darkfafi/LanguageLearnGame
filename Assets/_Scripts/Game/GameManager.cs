using UnityEngine;
using System.Collections;
using Ramses.Confactory;

public class GameManager : MonoBehaviour {

	private ConCurrentLevel conCurrentLevel;

	protected void Awake ()
	{
		Initialize();
	}

	protected void Start()
	{
		StartGame();
	}

	private void Initialize()
	{
		conCurrentLevel = ConfactoryFinder.Instance.Give<ConCurrentLevel>();
    }

	private void StartGame()
	{
		conCurrentLevel.CreateCurrentLevelObject();
    }
}
