using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class PlayLevelSelectContext : ContextScreen {

	[SerializeField]
	private ScrollRect scrollRect;

	[SerializeField]
	private RectTransform buttonHolder;

	[SerializeField]
	private Dropdown difficultyFilterDropdown;

	[SerializeField]
	private LevelSelectionButton buttonPrefab;

	[SerializeField]
	private Button backButton;

	[SerializeField]
	private Button playButton;

	private List<LevelSelectionButton> levelButtons = new List<LevelSelectionButton>();

	private ConCurrentLevel conCurrentLevel;

	private bool difficultyFilterSet = false;
	private LevelLibrary.LevelDifficulty difficultyFilter = LevelLibrary.LevelDifficulty.Easy;

	public override void OpenScreen()
	{
		base.OpenScreen();
        menuContextHolder.SetGlobalTitleText("Level Select");
		SetDifficultyDropdown();
		SetScrollView();
		AddAllListeners();
	}

	public override void CloseScreen()
	{
		base.CloseScreen();
		RemoveScrollViewSet();
		RemoveAllListeners();
	}

	protected void Awake()
	{
		conCurrentLevel = Ramses.Confactory.ConfactoryFinder.Instance.Give<ConCurrentLevel>();

		backButton.onClick.AddListener(() => OnBackButtonPressed());
		playButton.onClick.AddListener(() => OnPlayButtonPressed());
		conCurrentLevel.SetCurrentLevel("");
    }

	protected void Update()
	{
		playButton.interactable = (conCurrentLevel.CurrentLevelName != "");
	}

	protected void OnDestroy()
	{
		backButton.onClick.RemoveAllListeners();
        playButton.onClick.RemoveAllListeners();
    }

	private void OnBackButtonPressed()
	{
		menuContextHolder.SwitchSceneContext(MenuContextHolder.ContextScreens.PlaySettingsContext);
	}

	private void OnPlayButtonPressed()
	{
		Debug.Log("Play!");
		Ramses.Confactory.ConfactoryFinder.Instance.Give<ConSceneSwitcher>().SwitchScreen("Game");
	}

	private void SetScrollView() 
	{
		RemoveScrollViewSet();
        scrollRect.verticalNormalizedPosition = 0;
		string[] levelNames = conCurrentLevel.LevelLibrary.GetAllLevelNames();
		if (difficultyFilterSet)
			levelNames = conCurrentLevel.LevelLibrary.GetAllLevelNames(difficultyFilter);

		Vector2 lastPos = new Vector2();
		float buttonYSize = 0;
		float offset = 10;

		for (int i = 0; i < levelNames.Length; i++)
		{
			string levelName = levelNames[i];
			LevelSelectionButton b = Instantiate(buttonPrefab);
			b.SetLevelButton(levelName);
			b.transform.SetParent(buttonHolder, false);
			levelButtons.Add(b);
			float realOffset = offset;
			if(buttonYSize == 0)
			{
				buttonYSize = b.Size.y;
            }
			if(i != 0)
			{
				realOffset += b.Size.y;
            }
			lastPos.y -= realOffset;
			((RectTransform)b.transform).anchoredPosition = lastPos;
			lastPos = ((RectTransform)b.transform).anchoredPosition;
        }
		buttonHolder.sizeDelta = new Vector2(buttonHolder.sizeDelta.x , levelNames.Length * (buttonYSize + offset) + offset);
    }

	private void RemoveScrollViewSet()
	{
		for(int i = levelButtons.Count - 1; i >= 0 ; i--)
		{
			Destroy(levelButtons[i].gameObject);
        }
		levelButtons.Clear();
    }

	private void SetDifficultyDropdown()
	{
		difficultyFilterDropdown.ClearOptions();
		List<string> options = new List<string>();
		options.Add("All");
		foreach (string difficulty in Enum.GetNames(typeof(LevelLibrary.LevelDifficulty)))
		{
			options.Add(difficulty);
        }
		difficultyFilterDropdown.AddOptions(options);
		difficultyFilterDropdown.value = 0;
    }

	private void OnDifficultyFilterChanged(int value)
	{
		if (value == 0)
		{
			difficultyFilterSet = false;
		}
		else
		{
			difficultyFilterSet = true;
			difficultyFilter = ((LevelLibrary.LevelDifficulty)(value - 1));
		}
		SetScrollView();
    }

	private void AddAllListeners()
	{
		difficultyFilterDropdown.onValueChanged.AddListener(OnDifficultyFilterChanged);
	}

	private void RemoveAllListeners()
	{
		difficultyFilterDropdown.onValueChanged.RemoveAllListeners();
	}
}
