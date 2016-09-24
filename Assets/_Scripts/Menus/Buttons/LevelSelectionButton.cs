using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LevelSelectionButton : MonoBehaviour {

	public string LevelName { get { return nameText.text; } }
	public LevelLibrary.LevelDifficulty LevelDifficulty { get; private set; }

	public Vector2 Size { get { return ((RectTransform)button.transform).sizeDelta; } }

	[SerializeField]
	private Color selectedColor;

	[SerializeField]
	private Color notSelectedColor;

	[SerializeField]
	private Text difficultyText;

	[SerializeField]
	private Text nameText;

	[SerializeField]
	private Image previewImage;

	private Button button;

	private ConCurrentLevel conCurrentLevel;

	public void SetLevelButton(string name)
	{
		nameText.text = name;
		LevelDifficulty = conCurrentLevel.LevelLibrary.GetDifficultyByLevelName(LevelName);
		difficultyText.text = LevelDifficulty.ToString()[0].ToString();
    }

	protected void Awake ()
	{
		button = GetComponent<Button>();
		conCurrentLevel = Ramses.Confactory.ConfactoryFinder.Instance.Give<ConCurrentLevel>();

		button.onClick.AddListener(() => OnClicked());
    }

	protected void Update()
	{
		if(conCurrentLevel.CurrentLevelName == LevelName)
		{
			button.image.color = selectedColor;
		}
		else
		{
			button.image.color = notSelectedColor;
		}
	}

	protected void OnDestroy()
	{
		button.onClick.RemoveAllListeners();
	}

	private void OnClicked()
	{
		conCurrentLevel.SetCurrentLevel(LevelName);
	}

}
