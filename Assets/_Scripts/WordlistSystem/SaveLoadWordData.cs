using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadWordData
{
	public static string path = Application.persistentDataPath + "/SavedWordlists";
	public static string fileName = "Wordlist.dat";
	public static string fullPathToFile = path + "/" + fileName;


	public static void Save(AllWordListsData data)
	{
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(fullPathToFile);
		bf.Serialize(file, data);
		file.Close();
	}

	public static AllWordListsData Load()
	{
		if (File.Exists(fullPathToFile))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(fullPathToFile, FileMode.Open);
			AllWordListsData data = (AllWordListsData)bf.Deserialize(file);
			file.Close();
			return data;
		}
		return new AllWordListsData(null);
	}

	public static void Remove()
	{
		if (Directory.Exists(path))
		{
			if(File.Exists(fullPathToFile))
			{
				File.Delete(fullPathToFile);
			}
			Directory.Delete(path);
		}
	}
}

[Serializable]
public class AllWordListsData
{
	public FullWordListData[] AllWordlists { get; private set; }

	public AllWordListsData(FullWordListData[] allLists)
	{
		AllWordlists = allLists != null ? allLists : new FullWordListData[] { };
	}

	public FullWordListData GetFullWordListByListName(string name)
	{
		for (int i = 0; i < AllWordlists.Length; i++)
		{
			if (AllWordlists[i].ListName == name)
			{
				return AllWordlists[i];
			}
		}
		return null;
	}

	public FullWordListData GetFullWordListByLanguage(string language)
	{
		for(int i = 0; i < AllWordlists.Length; i++)
		{
			if(AllWordlists[i].GetSectionByLanguage(language) != null)
			{
				return AllWordlists[i];
			}
		}
		return null;
	}
}

[Serializable]
public class FullWordListData
{
	public const string BASE_LANGUAGE = "Base List";

	public string ListName { get; private set; }
	public List<WordListSectionData> AllWordListDatas { get; private set; }

	public FullWordListData(string name, WordListSectionData[] wordListSections)
	{
		ListName = name;
        AllWordListDatas = new List<WordListSectionData>(wordListSections);
	}

	public WordListSectionData GetSectionByLanguage(string language)
	{
		for (int i = 0; i < AllWordListDatas.Count; i++)
		{
			if (AllWordListDatas[i].Language == language)
			{
				return AllWordListDatas[i];
			}
        }
		return null;
	}
}

[Serializable]
public class WordListSectionData
{
	public string Language;
	public List<string> Words;

	public WordListSectionData(string l, string[] w)
	{
		Language = l;
		Words = new List<string>(w);
	}
}