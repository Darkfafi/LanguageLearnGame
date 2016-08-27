using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WordTextTab : MonoBehaviour
{
	public Text Id { get { return id; } }
	public Text BaseWord { get { return baseWord; } }
	public Text Translation { get { return translation; } }


	[SerializeField]
	private Text id;

	[SerializeField]
	private Text baseWord;

	[SerializeField]
	private Text translation;

}
