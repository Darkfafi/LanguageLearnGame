using UnityEngine;
using System.Collections;
using Ramses.Confactory;
using System;

public class TypeControlls : MonoBehaviour, IConfactory
{
	public delegate void TypeControllsHandler(string typedString);
	public delegate void VoidHandler();

	public event VoidHandler TypeControllsDestroyedEvent;

	public event TypeControllsHandler CurrentStringModifiedEvent;
	public event TypeControllsHandler StringSubmittedEvent;

	private string currentString = "";

	protected void Update ()
	{
		foreach (char c in Input.inputString)
		{
			if (c == "\b"[0])
			{
				if(currentString.Length > 0)
				{
					currentString = currentString.Remove(currentString.Length - 1);
					if(CurrentStringModifiedEvent != null)
					{
						CurrentStringModifiedEvent(currentString);
                    }
				}
			}
			else if (c == "\n"[0] || c == "\r"[0])
			{
				string tempString = currentString;
				currentString = "";
				if(StringSubmittedEvent != null)
				{
					StringSubmittedEvent(tempString);
				}
                Debug.Log("Submitted: " + tempString);
				tempString = null;
			}
			else
			{
				currentString += c.ToString();
				if(CurrentStringModifiedEvent != null)
				{
					CurrentStringModifiedEvent(currentString);
                }
			}
		}
	}

	public void ConClear()
	{
		if(TypeControllsDestroyedEvent != null)
		{
			TypeControllsDestroyedEvent();
        }
    }

	public void ConStruct()
	{

	}

	public void OnSceneSwitch(int newSceneIndex)
	{

	}
}
