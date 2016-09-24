using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MenuContextHolder : MonoBehaviour {

	public enum ContextScreens
	{
		MenuContext,
		PlaySettingsContext,
		PlayLevelSelectContext,
		StatsContext
	}

	[SerializeField]
	private Text globalTitle;

	[SerializeField]
	private ContextScreen[] allContextScreens;

	private ConSceneSwitcher sceneSwitcher;

	private ContextScreen preScreen = null;
	private ContextScreen screenSwitchingTo = null;

	protected void Awake()
	{
		sceneSwitcher = Ramses.Confactory.ConfactoryFinder.Instance.Give<ConSceneSwitcher>();
		sceneSwitcher.FullBlackEvent += OnBlackFullEvent;
        for (int i = 0; i < allContextScreens.Length; i++)
		{
			allContextScreens[i].SetContextScreen(this);
			if (allContextScreens[i].IsContextScreen(ContextScreens.MenuContext))
			{
				allContextScreens[i].OpenScreen();
			}
			else
			{
				allContextScreens[i].gameObject.SetActive(false);
            }
        }
	}

	public void SetGlobalTitleText(string text)
	{
		globalTitle.text = text;
	}

	public void SwitchSceneContext(ContextScreens sceneToShow)
	{
		ContextScreen currentScene = null;
        sceneSwitcher.FakeSwitchScreen();
        for (int i = 0; i < allContextScreens.Length; i++)
		{
			currentScene = allContextScreens[i];
            if (!currentScene.IsOpened && currentScene.IsContextScreen(sceneToShow))
			{
				screenSwitchingTo = currentScene;
            }

			if (currentScene.IsOpened && !currentScene.IsContextScreen(sceneToShow))
			{
				preScreen = currentScene;
			}
		}
	}

	private void OnBlackFullEvent()
	{
		if (preScreen != null)
		{
			preScreen.CloseScreen();
			preScreen = null;
		}

		if (screenSwitchingTo != null)
		{
			screenSwitchingTo.OpenScreen();
			screenSwitchingTo = null;
        }
	}
}
