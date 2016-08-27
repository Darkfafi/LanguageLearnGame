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
	private Button languageTabPrefab;

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

	// data
	private List<WordTextTab> allActiveTabs = new List<WordTextTab>();
	private List<Button> allLanguageTabs = new List<Button>();

	private FullWordListData currentData;
	private string currentLanguageSelected = FullWordListData.BASE_LANGUAGE;

	protected void Awake()
	{
		createLanugageWordTabButton.onClick.AddListener(() => AddWord());
		saveButton.onClick.AddListener(() => SaveData());
		backButton.onClick.AddListener(() => BackButtonPressed());
	}

	protected void Destroy()
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
        foreach (WordListSectionData sec in currentData.AllWordListDatas)
		{
			Button b = GameObject.Instantiate<Button>(languageTabPrefab);
			b.transform.SetParent(languageTabsHolder, false);
			b.GetComponentInChildren<Text>().text = sec.Language;
			allLanguageTabs.Add(b);
			b.onClick.AddListener(() => SelectLanguage(b.GetComponentInChildren<Text>().text));
			if(sec.Language == currentLanguageSelected)
			{
				b.GetComponent<Image>().color = Color.blue;
			}
		}
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
				WordTextTab w = GameObject.Instantiate<WordTextTab>(wordTabPrefab);
				rt = w.transform as RectTransform;
				w.Id.text = (i + 1).ToString() + ":";
				w.BaseWord.text = currentData.GetSectionByLanguage(FullWordListData.BASE_LANGUAGE).Words[i];
				w.Translation.text = sData.Words[i];
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
			rt.localPosition = new Vector3(rt.position.x, 0 - ((((RectTransform)wordTabPrefab.transform).sizeDelta.y + space) * i), rt.position.z);
		}
	}

	private void CleanAllWordTabs()
	{
		if(allActiveTabs.Count > 0)
		{
			for (int i = 0; i < allActiveTabs.Count; i++)
			{
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
				allLanguageTabs[i].onClick.RemoveAllListeners();
				Destroy(allLanguageTabs[i].gameObject);
			}
		}
		allLanguageTabs.Clear();
	}

	private void AddWord()
	{
		currentData.GetSectionByLanguage(currentLanguageSelected).Words.Add("");
		SetWordTabs();
		scrollRect.verticalNormalizedPosition = 0f;
    }

	private void SaveData()
	{
		WordListModifierScreen.SaveModifiedData();
		Debug.Log("Data Saved!");
	}

	private void BackButtonPressed()
	{
		Debug.Log("Back!");
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
