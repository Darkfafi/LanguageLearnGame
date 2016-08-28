using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WordTextTab : MonoBehaviour
{
	public Text Id { get { return id; } }
	public InputField ChangeableWord { get { return changeableWord; } }
	public string BaseWord { get { return baseWord; } }

	[SerializeField]
	private Text id;

	[SerializeField]
	private InputField changeableWord;

	protected string baseWord;

	public virtual void SetChangeableWord(string word)
	{
		ChangeableWord.text = baseWord = word;
	}

	public virtual void ImplementChangeableWordValue()
	{
		baseWord = ChangeableWord.text;
    }

}
