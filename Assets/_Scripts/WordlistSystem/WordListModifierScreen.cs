using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WordListModifierScreen : MonoBehaviour
{
	// Serializefields
	[SerializeField]
	private Text title;

	[SerializeField]
	private RectTransform listHolder;

	[SerializeField]
	private Button wordListButtonPrefab;

	[SerializeField]
	private WordListEditScreen wordListEditScreen;

	// Data
	private static AllWordListsData allData = SaveLoadWordData.Load();
	private List<Button> allCurrentButtons = new List<Button>();

	void Awake ()
	{
		wordListEditScreen.gameObject.SetActive(false);
		GenerateListButtons();
    }

	private void GenerateListButtons()
	{
		allCurrentButtons.Clear();
        float space = 10.0f;
		int listAmounts = allData.AllWordlists.Length;
		Button b = GameObject.Instantiate<Button>(wordListButtonPrefab);
		RectTransform rt = b.transform as RectTransform;
		listHolder.sizeDelta = new Vector2(listHolder.sizeDelta.x, (listAmounts * space + (listAmounts + 1) * rt.sizeDelta.y) + space);
        for (int i = 0; i <= listAmounts; i++)
		{
			if (i != 0)
			{
				b = GameObject.Instantiate<Button>(wordListButtonPrefab);
			}

			Text text = b.GetComponentInChildren<Text>();
			if(i == listAmounts)
			{
				text.text = "<Create new list>";
			}
			else
			{
				text.text = allData.AllWordlists[i].ListName;
			}
			rt = b.transform as RectTransform;
			rt.SetParent(listHolder, false);
			rt.localPosition = new Vector3(b.transform.position.x, b.transform.position.y - (rt.sizeDelta.y / 1.5f) - ((rt.sizeDelta.y + space) * i), b.transform.position.z);
			allCurrentButtons.Add(b);
            b.onClick.AddListener(() => WordListButtonPressed(b, text));
		}
	}

	private void WordListButtonPressed(Button button, Text text)
	{
		if(text.text == "<Create new list>")
		{
			CreateNewList();
		}
		else
		{
			OpenModifyScreen(text.text);
		}
	}

	private void CreateNewList()
	{
		
	}

	private void OpenModifyScreen(string nameWordList)
	{
		foreach(Button b in allCurrentButtons)
		{
			b.onClick.RemoveAllListeners();
			Destroy(b.gameObject);
		}
		allCurrentButtons.Clear();

		wordListEditScreen.gameObject.SetActive(true);
		wordListEditScreen.ShowListForModification(allData.GetFullWordListByListName(nameWordList));
    }

	public static void SaveModifiedData()
	{
		SaveLoadWordData.Save(allData);
    }
}
