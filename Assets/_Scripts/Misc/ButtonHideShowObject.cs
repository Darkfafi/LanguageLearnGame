using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonHideShowObject : MonoBehaviour {

	[SerializeField]
	private Button unityButton;

	[SerializeField]
	private GameObject objectOne;

	[SerializeField]
	private GameObject objectTwo;

	private void Awake()
	{
		unityButton.onClick.AddListener(() => Clicked());
    }
	
	private void Clicked()
	{
		objectOne.SetActive(objectOne.activeSelf ? false : true);
		objectTwo.SetActive(objectTwo.activeSelf ? false : true);
	}

	private void OnDestroy()
	{
		unityButton.onClick.RemoveAllListeners();
	}
}
