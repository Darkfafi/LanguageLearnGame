using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Ramses.Confactory;

public class MainMenuContext : ContextScreen {


	// Buttons
	[SerializeField]
	private Button playButton;

	[SerializeField]
	private Button wordlistButton;

	[SerializeField]
	private Button statsButton;

	[SerializeField]
	private Button exitButton;

	private ConSceneSwitcher conSceneSwitcher;

	protected void Awake()
	{
		conSceneSwitcher = ConfactoryFinder.Instance.Give<ConSceneSwitcher>();
		AddButtonEventListeners();
	}

	protected void OnDestroy()
	{
		RemoveButtonEventListeners();
    }

	public override void OpenScreen()
	{
		base.OpenScreen();
		menuContextHolder.SetGlobalTitleText("Main Menu");
	}

	private void AddButtonEventListeners()
	{
		playButton.onClick.AddListener(() => OnPlayButtonPressed());
		wordlistButton.onClick.AddListener(() => OnWordListButtonPressed());
		statsButton.onClick.AddListener(() => OnStatsButtonPressed());
		exitButton.onClick.AddListener(() => OnExitButtonPressed());
    }

	private void RemoveButtonEventListeners()
	{
		playButton.onClick.RemoveAllListeners();
		wordlistButton.onClick.RemoveAllListeners();
		statsButton.onClick.RemoveAllListeners();
		exitButton.onClick.RemoveAllListeners();
    }

	private void OnPlayButtonPressed()
	{
		menuContextHolder.SwitchSceneContext(MenuContextHolder.ContextScreens.PlaySettingsContext);
		Debug.Log("Play pressed");
	}

	private void OnWordListButtonPressed()
	{
		conSceneSwitcher.SwitchScreen("CreateWordlistScene");
	}

	private void OnStatsButtonPressed()
	{
		menuContextHolder.SwitchSceneContext(MenuContextHolder.ContextScreens.StatsContext);
		Debug.Log("Stats pressed");
	}

	private void OnExitButtonPressed()
	{
		Application.Quit();
		Debug.Log("Exit pressed");
	}
}
