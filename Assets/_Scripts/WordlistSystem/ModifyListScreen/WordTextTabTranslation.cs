using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WordTextTabTranslation : WordTextTab {

	public Text BasewordTextfield { get { return basewordTextfield; } }

	[SerializeField]
	private Text basewordTextfield;

	protected string translationWord;

	public override void ImplementChangeableWordValue()
	{
		translationWord = ChangeableWord.text;
    }

	public override void SetChangeableWord(string word)
	{
		ChangeableWord.text = translationWord = word;
    }
}
