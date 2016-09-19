using UnityEngine;
using System.Collections;

public class MenuContextHolder : MonoBehaviour {

	public enum ContextScreens
	{
		MenuContext,
		PlayContext,
		StatsContext
	}

	[SerializeField]
	private ContextScreen[] allContextScreens;

	private ConSceneSwitcher sceneSwitcher;

	protected void Awake()
	{
		sceneSwitcher = Ramses.Confactory.ConfactoryFinder.Instance.Give<ConSceneSwitcher>();

		for (int i = 0; i < allContextScreens.Length; i++)
		{
			allContextScreens[i].SetContextScreen(this);
			allContextScreens[i].gameObject.SetActive(allContextScreens[i].IsContextScreen(ContextScreens.MenuContext));
        }
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
				currentScene.OpenScreen();
			}

			if (currentScene.IsOpened && !currentScene.IsContextScreen(sceneToShow))
			{
				currentScene.CloseScreen();
			}
		}
	}
}
