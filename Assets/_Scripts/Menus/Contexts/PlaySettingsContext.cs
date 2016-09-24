using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class PlaySettingsContext : ContextScreen {

	// Section 1

	[SerializeField]
	private Dropdown languageListDropdown;

	[SerializeField]
	private Dropdown difficultyDropdown;

	// Section 2

	[SerializeField]
	private Dropdown languageToTranslateDropdown;

	[SerializeField]
	private Dropdown translationLanguageDropdown;


	// Section 3

	[SerializeField]
	private Button nextButton;

	[SerializeField]
	private Button backButton;

	[SerializeField]
	private Toggle showWordToTranslateOnlyToggle;

	// Data

	private AllWordListsData loadedData;

	private string selectedLanguageList { get { return languageListDropdown.options[languageListDropdown.value].text; } }
	private string selectedLanguageToTranslate { get { return languageToTranslateDropdown.options[languageToTranslateDropdown.value].text; } }
	private string selectedTranslationLanguage { get { return translationLanguageDropdown.options[translationLanguageDropdown.value].text; } }

	public override void OpenScreen()
	{
		base.OpenScreen();
		menuContextHolder.SetGlobalTitleText("Play Settings");
	}

	protected void Awake()
	{
		loadedData = SaveLoadWordData.Load();
		LanguageListDropdownSet();
		ListLanguageOptionsDropdownsSet();
		DifficultyListDropdownSet();

		languageListDropdown.onValueChanged.AddListener(ListValueChanged);
		languageToTranslateDropdown.onValueChanged.AddListener(ToTranslateLanguageValueChanged);
		translationLanguageDropdown.onValueChanged.AddListener(TranslationLanguageValueChanged);

		nextButton.onClick.AddListener(() => OnNextButtonPressed());
		backButton.onClick.AddListener(() => OnBackButtonPressed());
	}

	protected void OnDestroy()
	{
		languageListDropdown.onValueChanged.RemoveAllListeners();
		languageToTranslateDropdown.onValueChanged.RemoveAllListeners();
		translationLanguageDropdown.onValueChanged.RemoveAllListeners();
		nextButton.onClick.RemoveAllListeners();
		backButton.onClick.RemoveAllListeners();
    }

	private void OnNextButtonPressed()
	{
		Ramses.Confactory.ConfactoryFinder.Instance.Give<ConSelectedLanguages>().SetLanguages(
			selectedLanguageList, selectedLanguageToTranslate, selectedTranslationLanguage);

		GameplaySettings settings = new GameplaySettings();

		settings.GameDifficulty = ((GameDifficulties) difficultyDropdown.value);
		settings.ShowWordToTranslateOnly = showWordToTranslateOnlyToggle.isOn;

		Debug.Log(settings.ShowWordToTranslateOnly);

		Ramses.Confactory.ConfactoryFinder.Instance.Give<ConCurrentGameplaySettings>().SetCurrentGameplaySettings(settings);
		menuContextHolder.SwitchSceneContext(MenuContextHolder.ContextScreens.PlayLevelSelectContext);
        Debug.Log("Next!");
	}

	private void OnBackButtonPressed()
	{
		menuContextHolder.SwitchSceneContext(MenuContextHolder.ContextScreens.MenuContext);
	}

	private void ListValueChanged(int newValue)
	{
		ListLanguageOptionsDropdownsSet();
	}

	private void ToTranslateLanguageValueChanged(int newValue)
	{
		if (translationLanguageDropdown.value == newValue)
		{
			translationLanguageDropdown.value = translationLanguageDropdown.options.GetLoopIndex(translationLanguageDropdown.value + 1);
		}
	}

	private void TranslationLanguageValueChanged(int newValue)
	{
		if (languageToTranslateDropdown.value == newValue)
		{
			languageToTranslateDropdown.value = languageToTranslateDropdown.options.GetLoopIndex(languageToTranslateDropdown.value + 1);
		}
	}

	private void LanguageListDropdownSet()
	{
		languageListDropdown.ClearOptions();
		List<string> listNames = new List<string>();
		
		foreach(FullWordListData data in loadedData.AllWordlists)
		{
			listNames.Add(data.ListName);
		}

		languageListDropdown.AddOptions(listNames);
		listNames.Clear();
		listNames = null;
	}

	private void ListLanguageOptionsDropdownsSet()
	{
		languageToTranslateDropdown.ClearOptions();
		translationLanguageDropdown.ClearOptions();
        List<string> languagesNames = new List<string>();

		foreach (WordListSectionData data in loadedData.GetFullWordListByListName(selectedLanguageList).AllWordListDatas)
		{
			if(data.Language != FullWordListData.BASE_LANGUAGE)
				languagesNames.Add(data.Language);
        }

		languageToTranslateDropdown.AddOptions(languagesNames);
		translationLanguageDropdown.AddOptions(languagesNames);
		translationLanguageDropdown.value = translationLanguageDropdown.options.GetLoopIndex(translationLanguageDropdown.value + 1);
        languagesNames.Clear();
		languagesNames = null;
	}

	private void DifficultyListDropdownSet()
	{
		difficultyDropdown.ClearOptions();
		List<string> difficulties = new List<string>(Enum.GetNames(typeof(GameDifficulties)));
		difficultyDropdown.AddOptions(difficulties);
		difficultyDropdown.value = 1;
    }
}
