using UnityEngine;
using System.Collections;

public class ContextScreen : MonoBehaviour {

	public bool IsOpened { get { return gameObject.activeSelf; } }
	public MenuContextHolder.ContextScreens ContextScreenId { get { return contextScreenId; } }

	protected MenuContextHolder menuContextHolder { get; private set; }

	[SerializeField]
	private MenuContextHolder.ContextScreens contextScreenId;

	public void SetContextScreen(MenuContextHolder holder)
	{
		menuContextHolder = holder;
    }

	public bool IsContextScreen(MenuContextHolder.ContextScreens id)
	{
		return contextScreenId == id;
    }

	public virtual void OpenScreen()
	{
		gameObject.SetActive(true);
	}

	public virtual void CloseScreen()
	{
		gameObject.SetActive(false);
	}
}
