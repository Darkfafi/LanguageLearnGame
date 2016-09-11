using UnityEngine;
using System.Collections;
using Ramses.Confactory;
using System;

public class ConSelectedLanguages : IConfactory
{

	public FullWordListData		SelectedWordList		{ get; private set; }
	public WordListSectionData	LanguageToTranslate		{ get; private set; }
	public WordListSectionData	TranslationLanguage		{ get; private set; }


	public void SetLanguages(string wordlistName, string languageToTranslate, string translationLanguage)
	{
		SelectedWordList = SaveLoadWordData.Load().GetFullWordListByListName(wordlistName);
		LanguageToTranslate = SelectedWordList.GetSectionByLanguage(languageToTranslate);
		TranslationLanguage = SelectedWordList.GetSectionByLanguage(translationLanguage);
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
