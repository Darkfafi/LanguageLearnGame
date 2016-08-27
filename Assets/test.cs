using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{

	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			AllWordListsData d = SaveLoadWordData.Load();
			//Debug.Log(d.Language + "<l & w>" + d.Words[1]);
			Debug.Log(d.GetFullWordListByListName("GList"));
		}
		else if (Input.GetKeyDown(KeyCode.G))
		{
			SaveLoadWordData.Save(new AllWordListsData(new FullWordListData[] {
				new FullWordListData("GList",new WordListSectionData[] { new WordListSectionData(FullWordListData.BASE_LANGUAGE, new string[] { "Base1", "Base12" }), new WordListSectionData("G", new string[] { "G1", "G2" }) })
			}));
		}
		else if (Input.GetKeyDown(KeyCode.F))
		{
			SaveLoadWordData.Save(new AllWordListsData(new FullWordListData[] {
				new FullWordListData("GList",new WordListSectionData[] { new WordListSectionData(FullWordListData.BASE_LANGUAGE, new string[] { "Base1", "Base12" }), new WordListSectionData("F", new string[] { "F1", "F2" }) })
			}));
		}
		else if (Input.GetKeyDown(KeyCode.D))
		{
			SaveLoadWordData.Remove();
		}
		else if (Input.GetKeyDown(KeyCode.P))
		{
			WordListModifierScreen.SaveModifiedData();
		}
    }
}
