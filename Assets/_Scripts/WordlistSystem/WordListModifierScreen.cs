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
	private WordListButton wordListButtonPrefab;

	[SerializeField]
	private WordListEditScreen wordListEditScreen;

	[SerializeField]
	private AskNamePopUp namePopUp;

	[SerializeField]
	private Button backButton;

	[SerializeField]
	private Button saveButton;

	// Data
	private static AllWordListsData allData = SaveLoadWordData.Load();
	private List<WordListButton> allCurrentButtons = new List<WordListButton>();

	private void Awake()
	{
		Activate();
	}

	private void OnDestroy()
	{
		saveButton.onClick.RemoveAllListeners();
		backButton.onClick.RemoveAllListeners();
		DeleteCurrentButtons();
	}

	public void Activate ()
	{
		allData = SaveLoadWordData.Load();
		DeleteCurrentButtons();
		namePopUp.gameObject.SetActive(false);
		wordListEditScreen.gameObject.SetActive(false);
		GenerateListButtons();

		saveButton.onClick.RemoveAllListeners();
		backButton.onClick.RemoveAllListeners();

		saveButton.onClick.AddListener(() => OnSaveClicked());
		backButton.onClick.AddListener(() => OnBackClicked());

		title.text = "Word lists";
    }

	private void OnSaveClicked()
	{
		SaveModifiedData();
	}

	private void OnBackClicked()
	{
		Debug.Log("Back! (List View)");
	}

	private void GenerateListButtons()
	{
		DeleteCurrentButtons();
        float space = 10.0f;
		int listAmounts = allData.AllWordlists.Count;
		WordListButton b = GameObject.Instantiate<WordListButton>(wordListButtonPrefab);
		RectTransform rt = b.transform as RectTransform;
		listHolder.sizeDelta = new Vector2(listHolder.sizeDelta.x, (listAmounts * space + (listAmounts + 1) * rt.sizeDelta.y) + space);
        for (int i = 0; i <= listAmounts; i++)
		{
			if (i != 0)
			{
				b = GameObject.Instantiate<WordListButton>(wordListButtonPrefab);
			}

			if(i == listAmounts)
			{
				b.SetText("<Create new list>");
				b.HideDeleteButton();
            }
			else
			{
				b.SetText(allData.AllWordlists[i].ListName);
			}
			rt = b.transform as RectTransform;
			rt.SetParent(listHolder, false);
			rt.localPosition = new Vector3(b.transform.position.x, b.transform.position.y - (rt.sizeDelta.y / 1.5f) - ((rt.sizeDelta.y + space) * i), b.transform.position.z);
			allCurrentButtons.Add(b);
			b.AccessButtonPressedEvent -= WordListButtonPressed;
			b.AccessButtonPressedEvent += WordListButtonPressed;

			b.DeleteButtonPressedEvent -= DeleteWordButtonPressed;
			b.DeleteButtonPressedEvent += DeleteWordButtonPressed;
        }
	}

	private void WordListButtonPressed(WordListButton button)
	{
		if(button.GetText() == "<Create new list>")
		{
			CreateNewList();
		}
		else
		{
			OpenModifyScreen(button.GetText());
		}
	}

	private void DeleteWordButtonPressed(WordListButton button)
	{
		allData.AllWordlists.Remove(allData.GetFullWordListByListName(button.GetText()));
		SaveModifiedData();
		Activate();
	}

	private void CreateNewList()
	{
		namePopUp.gameObject.SetActive(true);
		namePopUp.SetTitleText("Name your word list:");
		namePopUp.SetPlaceholderInputText("Word list name....");

		namePopUp.PopupButtonPressedEvent -= OnPopupButtonPressedEvent;
		namePopUp.PopupButtonPressedEvent += OnPopupButtonPressedEvent;
    }

	private void OnPopupButtonPressedEvent(string givenString, AskNamePopUp.ButtonType buttonType, AskNamePopUp popup)
	{
		namePopUp.PopupButtonPressedEvent -= OnPopupButtonPressedEvent;
		if (buttonType == AskNamePopUp.ButtonType.Create)
		{
			if (allData.GetFullWordListByListName(givenString) != null)
			{
				popup.SetWarningText("Name '" + givenString + "' already in use!");
				CreateNewList();
				return;
			}

			allData.AllWordlists.Add(new FullWordListData(givenString, new WordListSectionData[] { new WordListSectionData(FullWordListData.BASE_LANGUAGE, new string[] { }) }));
			SaveModifiedData();
			OpenModifyScreen(givenString);
		}

		popup.Clean();
	}

	private void OpenModifyScreen(string nameWordList)
	{
		saveButton.onClick.RemoveAllListeners();
		backButton.onClick.RemoveAllListeners();

		DeleteCurrentButtons();
        wordListEditScreen.gameObject.SetActive(true);
		wordListEditScreen.ShowListForModification(allData.GetFullWordListByListName(nameWordList));
	}

	private void DeleteCurrentButtons()
	{
		if (allCurrentButtons.Count > 0)
		{
			foreach (WordListButton b in allCurrentButtons)
			{
				b.AccessButtonPressedEvent -= WordListButtonPressed;
				b.DeleteButtonPressedEvent -= DeleteWordButtonPressed;
				Destroy(b.gameObject);
			}
			allCurrentButtons.Clear();
		}
	}

	public static void SaveModifiedData()
	{
		SaveLoadWordData.Save(allData);
    }
}
