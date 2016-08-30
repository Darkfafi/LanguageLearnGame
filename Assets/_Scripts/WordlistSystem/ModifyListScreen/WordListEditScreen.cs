using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class WordListEditScreen : MonoBehaviour
{
	[SerializeField]
	private RectTransform contentHolder;

	[SerializeField]
	private WordTextTab wordTabPrefab;

	[SerializeField]
	private WordTextTabTranslation wordTabTranslationPrefab;

	[SerializeField]
	private LanguageTab languageTabPrefab;

	[SerializeField]
	private RectTransform languageTabsHolder;

	[SerializeField]
	private Button createLanguageTabButton;

	[SerializeField]
	private Button createLanugageWordTabButton;

	[SerializeField]
	private RectTransform textTabsHolder;

	[SerializeField]
	private Button saveButton;

	[SerializeField]
	private Button backButton;

	[SerializeField]
	private ScrollRect scrollRect;

	[SerializeField]
	private AskNamePopUp askNamePopUp;

	[SerializeField]
	private WordListModifierScreen wordlistModifier;

	// data
	private List<WordTextTab> allActiveTabs = new List<WordTextTab>();
	private List<LanguageTab> allLanguageTabs = new List<LanguageTab>();

	private FullWordListData currentData;
	private string currentLanguageSelected = FullWordListData.BASE_LANGUAGE;

	protected void OnEnable()
	{
		createLanugageWordTabButton.onClick.AddListener(() => AddWord());
		createLanguageTabButton.onClick.AddListener(() => CreateLanguage());
		saveButton.onClick.AddListener(() => SaveData());
		backButton.onClick.AddListener(() => BackButtonPressed());
	}

	protected void OnDisable()
	{
		createLanugageWordTabButton.onClick.RemoveAllListeners();
		saveButton.onClick.RemoveAllListeners();
		backButton.onClick.RemoveAllListeners();

		CleanAllLanguageTabs();
		CleanAllWordTabs();
	}

	public void ShowListForModification(FullWordListData data)
	{
		currentData = data;
		SelectLanguage(currentLanguageSelected);
	}

	private void SetLanguageTabs()
	{
		CleanAllLanguageTabs();
		int i = 0;
		float offset = 6;
        foreach (WordListSectionData sec in currentData.AllWordListDatas)
		{
			LanguageTab b = GameObject.Instantiate<LanguageTab>(languageTabPrefab);
			b.transform.SetParent(languageTabsHolder, false);
			b.SetLanguageTab(sec.Language, (sec.Language == currentLanguageSelected));
			allLanguageTabs.Add(b);
			b.TabClickedEvent += OnLanguageTabClickedEvent;
			b.DeleteClickedEvent += OnDeleteTabClickedEvent;
			float xpos = offset + (i * (((RectTransform)b.transform).sizeDelta.x + offset));
            b.transform.localPosition = new Vector2(xpos, -3.5f);
			languageTabsHolder.sizeDelta = new Vector2(xpos + ((RectTransform)b.transform).sizeDelta.x + offset, languageTabsHolder.sizeDelta.y);
			i++;
		}
	}

	private void OnLanguageTabClickedEvent(LanguageTab tab)
	{
		SelectLanguage(tab.Language);
	}

	private void OnDeleteTabClickedEvent(LanguageTab tab)
	{
		currentData.AllWordListDatas.Remove(currentData.GetSectionByLanguage(tab.Language));
		SelectLanguage(FullWordListData.BASE_LANGUAGE);
	}

	private void SetWordTabs()
	{
		WordListSectionData sData = currentData.GetSectionByLanguage(currentLanguageSelected);
        int listAmount = sData.Words.Count;
		float space = 10.0f;
		RectTransform rt = wordTabPrefab.transform as RectTransform;
		contentHolder.sizeDelta = new Vector2(contentHolder.sizeDelta.x, Mathf.Abs(textTabsHolder.localPosition.y) + (listAmount * space + (listAmount + 1) * rt.sizeDelta.y) + space);
		CleanAllWordTabs();
		for(int i = 0; i <= listAmount; i++)
		{
			if (i != listAmount)
			{
				WordTextTab w = null;

				if (currentLanguageSelected == FullWordListData.BASE_LANGUAGE)
				{
					w = GameObject.Instantiate<WordTextTab>(wordTabPrefab);
					w.DeleteClickedEvent += OnWordTabDeleteClickedEvent;
					w.SetChangeableWord(currentData.GetSectionByLanguage(FullWordListData.BASE_LANGUAGE).Words[i]);
				}
				else
				{
					WordTextTabTranslation wt = GameObject.Instantiate<WordTextTabTranslation>(wordTabTranslationPrefab);
					w = wt;
					wt.BasewordTextfield.text = currentData.GetSectionByLanguage(FullWordListData.BASE_LANGUAGE).Words[i];
					wt.SetChangeableWord(currentData.GetSectionByLanguage(currentLanguageSelected).Words[i]);
				}
				
				rt = w.transform as RectTransform;
				w.Id.text = (i + 1).ToString() + ":";
				w.TabTextModifiedEvent += OnTabTextModifiedEvent;

				allActiveTabs.Add(w);
			}
			else 
			{
				rt = createLanugageWordTabButton.transform as RectTransform;
				if (currentLanguageSelected == FullWordListData.BASE_LANGUAGE)
				{
					rt.gameObject.SetActive(true);
				}
				else
				{
					rt.gameObject.SetActive(false);
				}
			}

			rt.SetParent(textTabsHolder, false);
			rt.localPosition = new Vector3(rt.position.x, 0 - (((((RectTransform)wordTabPrefab.transform).sizeDelta.y) + space) * i), rt.position.z);
		}
	}

	private void CleanAllWordTabs()
	{
		if(allActiveTabs.Count > 0)
		{
			for (int i = 0; i < allActiveTabs.Count; i++)
			{
				allActiveTabs[i].DeleteClickedEvent -= OnWordTabDeleteClickedEvent;
				allActiveTabs[i].TabTextModifiedEvent -= OnTabTextModifiedEvent;
				Destroy(allActiveTabs[i].gameObject);
            }
		}
		allActiveTabs.Clear();
	}

	private void CleanAllLanguageTabs()
	{
		if (allLanguageTabs.Count > 0)
		{
			for (int i = 0; i < allLanguageTabs.Count; i++)
			{
				allLanguageTabs[i].TabClickedEvent -= OnLanguageTabClickedEvent;
				allLanguageTabs[i].DeleteClickedEvent -= OnDeleteTabClickedEvent;
                Destroy(allLanguageTabs[i].gameObject);
			}
		}
		allLanguageTabs.Clear();
	}

	private void OnTabTextModifiedEvent(WordTextTab tab)
	{
		SetWordTabsInData();
	}

	private void OnWordTabDeleteClickedEvent(WordTextTab tab)
	{
		string[] wordList = currentData.GetSectionByLanguage(currentLanguageSelected).Words.ToArray();
		int indexTab = wordList.GetIndexOf(tab.BaseWord);

		foreach(WordListSectionData section in currentData.AllWordListDatas)
		{
			section.Words.RemoveAt(indexTab);
		}
		SetWordTabs();
    }

	private void AddWord()
	{
		foreach(WordListSectionData dataSection in currentData.AllWordListDatas)
		{
			dataSection.Words.Add("");
		}
		SetWordTabs();
		scrollRect.verticalNormalizedPosition = 0f;
    }

	private void CreateLanguage()
	{
		askNamePopUp.gameObject.SetActive(true);
		askNamePopUp.SetTitleText("Creating new translation");
		askNamePopUp.SetPlaceholderInputText("Language name");

		askNamePopUp.PopupButtonPressedEvent -= OnPopupButtonPressedEvent;
		askNamePopUp.PopupButtonPressedEvent += OnPopupButtonPressedEvent;
    }

	private void OnPopupButtonPressedEvent(string givenName, AskNamePopUp.ButtonType buttonType, AskNamePopUp popUp)
	{
		askNamePopUp.PopupButtonPressedEvent -= OnPopupButtonPressedEvent;
		if (buttonType == AskNamePopUp.ButtonType.Create)
		{
			if (currentData.GetSectionByLanguage(givenName) != null)
			{
				popUp.SetWarningText("Language name '" + givenName + "' already in use!");
				CreateLanguage();
				return;
			}

			List<string> translationWordsList = new List<string>();
			foreach (string word in currentData.GetSectionByLanguage(FullWordListData.BASE_LANGUAGE).Words)
			{
				translationWordsList.Add("");
			}
			string languageName = givenName;
			WordListSectionData newLanguageSection = new WordListSectionData(languageName, translationWordsList.ToArray());
			currentData.AllWordListDatas.Add(newLanguageSection);
			SelectLanguage(languageName);
		}
		popUp.Clean();
	}

	private void SaveData()
	{
		SetWordTabsInData();
		SetLanguageTabsInData();

		WordListModifierScreen.SaveModifiedData();
		Debug.Log("Data Saved!");
	}

	private void SetWordTabsInData()
	{
		if (allActiveTabs.Count > 0)
		{
			for (int i = 0; i < allActiveTabs.Count; i++)
			{
				allActiveTabs[i].ImplementChangeableWordValue();
				currentData.GetSectionByLanguage(currentLanguageSelected).Words[i] = allActiveTabs[i].ChangeableWord.text;
			}
		}
	}

	private void SetLanguageTabsInData()
	{
		if (allLanguageTabs.Count > 0)
		{
			for (int i = 0; i < allLanguageTabs.Count; i++)
			{
				currentData.AllWordListDatas[i].Language = allLanguageTabs[i].Language;
			}
		}
	}

	private void BackButtonPressed()
	{
		wordlistModifier.Activate();
    }

	private void SelectLanguage(string language)
	{
		foreach(WordListSectionData sec in currentData.AllWordListDatas)
		{
			if(sec.Language == language)
			{
				currentLanguageSelected = language;
				SetLanguageTabs();
				SetWordTabs();
				return;
			}
		}
		Debug.LogError("Cannot switch to language: '" + language + "' because it does not exist");
	}
}
