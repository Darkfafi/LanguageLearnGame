using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WordListButton : MonoBehaviour {

	public delegate void WordListButtonHandler(WordListButton button);
	public event WordListButtonHandler AccessButtonPressedEvent;
	public event WordListButtonHandler DeleteButtonPressedEvent;

	[SerializeField]
	private Button accessButton;

	[SerializeField]
	private Button deleteButton;

	public void HideDeleteButton()
	{
		deleteButton.gameObject.SetActive(false);
	}

	public string GetText()
	{
		return accessButton.GetComponentInChildren<Text>().text;
    }

	public void SetText(string text)
	{
		accessButton.GetComponentInChildren<Text>().text = text;
	}

	private void Awake()
	{
		accessButton.onClick.AddListener(() => AccessClicked());
		deleteButton.onClick.AddListener(() => DeleteClicked());
	}

	private void OnDestroy()
	{
		accessButton.onClick.RemoveAllListeners();
		deleteButton.onClick.RemoveAllListeners();
	}

	private void AccessClicked()
	{
		if(AccessButtonPressedEvent != null)
		{
			AccessButtonPressedEvent(this);
        }
	}

	private void DeleteClicked()
	{
		if(DeleteButtonPressedEvent != null)
		{
			DeleteButtonPressedEvent(this);
        }
	}
}
