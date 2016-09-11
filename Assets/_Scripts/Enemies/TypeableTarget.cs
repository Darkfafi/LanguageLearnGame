using UnityEngine;
using System.Collections;
using Ramses.Confactory;
public class TypeableTarget : MonoBehaviour {

	public int WeaknessWordId { get { return weaknessWordId; } }
	private int weaknessWordId = 0;

	private ConSelectedLanguages conSelectedLanguages;

	protected virtual void Awake()
	{
		conSelectedLanguages = ConfactoryFinder.Instance.Give<ConSelectedLanguages>();
    }

	public void SetWeaknessWord(int idWeaknessWord)
	{
		weaknessWordId = idWeaknessWord;
    }

	public bool CheckWeaknessWordToTranslate(string wordToTranslate)
	{
		return conSelectedLanguages.LanguageToTranslate.Words[weaknessWordId] == wordToTranslate;
	}

	public bool CheckWeaknessWordTranslation(string translationWord)
	{
		return conSelectedLanguages.TranslationLanguage.Words[weaknessWordId] == translationWord;
	}
}
