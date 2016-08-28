using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LanguageTab : MonoBehaviour {

	public delegate void LanguageTabHandler(LanguageTab tab);
	public event LanguageTabHandler DeleteClickedEvent;
	public event LanguageTabHandler TabClickedEvent;

	[SerializeField]
	private InputField modificationNameTab;

	[SerializeField]
	private Text textLanguageTab;

	[SerializeField]
	private Button deleteButton;

	[SerializeField]
	private Button tabButton;

	[SerializeField]
	private Image buttonImage;

	public string Language { get; private set; }

	public bool IsBaseLanguage { get { return textLanguageTab.text == FullWordListData.BASE_LANGUAGE; } } 

	public void SetLanguageTab(string language, bool currentTab)
	{
		modificationNameTab.text = textLanguageTab.text = Language = language;
		bool editAble = (currentTab && !IsBaseLanguage);

		deleteButton.gameObject.SetActive(editAble);
		modificationNameTab.gameObject.SetActive(editAble);
		textLanguageTab.gameObject.SetActive(!editAble);
		if (currentTab)
			buttonImage.color = Color.green;
    }

	private void Awake()
	{
		deleteButton.onClick.AddListener(() => OnDeleteClicked());
		tabButton.onClick.AddListener(() => OnTabClicked());
		modificationNameTab.onValueChanged.AddListener(UpdateLanguage);
    }

	private void UpdateLanguage(string language)
	{
		Language = language;
	}

	private void OnDeleteClicked()
	{
		if(DeleteClickedEvent != null)
		{
			DeleteClickedEvent(this);
		}
	}

	private void OnTabClicked()
	{
		if (TabClickedEvent != null)
		{
			TabClickedEvent(this);
		}
	}

	private void OnDestroy()
	{
		tabButton.onClick.RemoveAllListeners();
		deleteButton.onClick.RemoveAllListeners();
		modificationNameTab.onValueChanged.RemoveAllListeners();
	}
}
