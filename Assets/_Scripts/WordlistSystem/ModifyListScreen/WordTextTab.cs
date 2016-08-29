using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WordTextTab : MonoBehaviour
{
	public delegate void TextTabHandler(WordTextTab tab);
	public event TextTabHandler DeleteClickedEvent;
	public event TextTabHandler TabTextModifiedEvent;

	public Text Id { get { return id; } }
	public InputField ChangeableWord { get { return changeableWord; } }
	public string BaseWord { get { return baseWord; } }

	[SerializeField]
	private Text id;

	[SerializeField]
	private InputField changeableWord;

	[SerializeField]
	private Button deleteButton;

	protected string baseWord;

	protected void Awake()
	{
		changeableWord.onEndEdit.AddListener(ChangeableWordEdited);
        if (deleteButton != null)
		{
			deleteButton.onClick.AddListener(() => DeleteClicked());
        }
	}

	private void ChangeableWordEdited(string newWord)
	{
		ImplementChangeableWordValue();
		if(TabTextModifiedEvent != null)
		{
			TabTextModifiedEvent(this);
        }
	}

	protected void OnDestroy()
	{
		changeableWord.onEndEdit.RemoveAllListeners();
		if (deleteButton != null)
		{
			deleteButton.onClick.RemoveAllListeners();
		}
	}

	public virtual void SetChangeableWord(string word)
	{
		ChangeableWord.text = baseWord = word;
	}

	public virtual void ImplementChangeableWordValue()
	{
		baseWord = ChangeableWord.text;
    }

	private void DeleteClicked()
	{
		if(DeleteClickedEvent != null)
		{
			DeleteClickedEvent(this);
		}
	}
}
