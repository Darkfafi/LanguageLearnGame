using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AskNamePopUp : MonoBehaviour {

	public enum CloseMethod
	{
		Destroy,
		Hide
	}

	public enum ButtonType
	{
		Create,
		Close
	}

	public delegate void PopUpHandler(AskNamePopUp popup);
	public delegate void PopUpStringHandler(string stringInInputfield, ButtonType buttonType, AskNamePopUp popup);

    public event PopUpStringHandler PopupButtonPressedEvent;

	public CloseMethod PopupCloseMethod = CloseMethod.Hide;

	public bool CloseOnCreateButtonPressed = true;
	public bool CloseOnCancelButtonPressed = true;

	[SerializeField]
	private InputField inputfield;

	[SerializeField]
	private Button CancelButton;

	[SerializeField]
	private Button CreateButton;

	[SerializeField]
	private Text titleText;

	[SerializeField]
	private Text placeholderInputText;

	[SerializeField]
	private Text warningText;

	protected void Awake()
	{
		CreateButton.onClick.AddListener(() => OnCreateClicked());
		CancelButton.onClick.AddListener(() => OnCancelClicked());
	}

	protected void OnDestroy()
	{
		CreateButton.onClick.RemoveAllListeners();
		CancelButton.onClick.RemoveAllListeners();
	}

	private void OnCreateClicked()
	{
		if (CloseOnCreateButtonPressed)
			CloseMethodExecution();

		if (PopupButtonPressedEvent != null)
		{
			PopupButtonPressedEvent(inputfield.text, ButtonType.Create, this);
		}
	}

	private void OnCancelClicked()
	{
		if (CloseOnCancelButtonPressed)
			CloseMethodExecution();

		if (PopupButtonPressedEvent != null)
		{
			PopupButtonPressedEvent(inputfield.text, ButtonType.Close, this);
		}
	}

	private void CloseMethodExecution()
	{
		switch(PopupCloseMethod)
		{
			case CloseMethod.Destroy:
				GameObject.Destroy(this.gameObject);
				break;
			case CloseMethod.Hide:
				this.gameObject.SetActive(false);
				break;
		}
	}

	public void SetWarningText(string text)
	{
		if(text == "")
		{
			HideWarningText();
			return;
		}
		warningText.gameObject.SetActive(true);
		warningText.text = "Warning:" + "\n";
		warningText.text = text;
	}

	public void HideWarningText()
	{
		warningText.gameObject.SetActive(false);
    }

	public void Clean()
	{
		inputfield.text = "";
		HideWarningText();
	}
	
	public void SetTitleText(string text)
	{
		titleText.text = text;
	}

	public void SetPlaceholderInputText(string text)
	{
		placeholderInputText.text = text;
    }
}
