using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using UnityEngine.UI;
using Assets.Scripts.Levels.Vowels;
using Assets.Scripts.Levels.VowelsOral;

namespace Assets.Scripts.Levels.CompleteMissingVowel
{
	public class CompleteMissingVowelView : LevelView
	{

		public Toggle[] vowelButtons;
		public Button nextButton;
		public Button ticButton;

		public Image currentObjectImage;
		public AudioClip currentAudioClip;
		public Text rulesText;

		private DataTrio<string[], AudioClip[], Sprite[]> easyLetters;
		private DataTrio<string[], AudioClip[], Sprite[]> hardletters;

		private static Color32 SELECTED_COLOR = new Color32(53,198,227,255);
		private static Color32 WRONG_COLOR = new Color32(251,96,96,255);
		private static Color32 RIGHT_COLOR = new Color32(81,225,148,225);

		public override void ShowHint ()
		{
		}

		public override void EndGame ()
		{
			
		}

		public void ShowHints (DataPair<int, int> letterIndices)
		{
			DisableHint ();
			vowelButtons [letterIndices.Fst ()].interactable = false;
			vowelButtons [letterIndices.Snd ()].interactable = false;
		}

		public void Next (int letter)
		{
			currentAudioClip = easyLetters.Snd () [letter];
			currentObjectImage.sprite = easyLetters.Thd () [letter];
			rulesText.text = RemoveVowels (easyLetters.Fst () [letter].ToUpper ());
			ResetHint ();
		}

		public void NextHard (int letter)
		{
			currentAudioClip = hardletters.Snd () [letter];
			currentObjectImage.sprite = hardletters.Thd () [letter];
			rulesText.text = RemoveVowels (hardletters.Fst () [letter].ToUpper ());
			ResetHint ();
		}

		public void ResetHint ()
		{
			EnableHint ();
			for (int i = 0; i < vowelButtons.Length; i++) {
				vowelButtons [i].interactable = true;
			}
		}

		public void PlaySound ()
		{
			SoundManager.instance.PlayClip (currentAudioClip);
			SpeakerScript.instance.PlaySound (currentAudioClip.name.Length < 5 ? 1 : 2);
		}


		public void PlaySoundRight ()
		{
			PlayRightSound ();
		}

		public void PlaySoundWrong ()
		{
			PlayWrongSound ();
		}

		public void InitView ()
		{
		}

		public void SetResources (DataTrio<string[], AudioClip[], Sprite[]> resources, bool easy)
		{
			if (easy) {
				easyLetters = new DataTrio<string[], AudioClip[], Sprite[]> (resources.Fst (), resources.Snd (), resources.Thd ());
			} else {
				hardletters = new DataTrio<string[], AudioClip[], Sprite[]> (resources.Fst (), resources.Snd (), resources.Thd ());
			}
		}

		public void UpdateLetterSelections (int letter1, int letter2)
		{
			if (letter1 != -1)
				vowelButtons [letter1].GetComponent<Image> ().color = SELECTED_COLOR;
			if (letter2 != -1) {
				vowelButtons [letter2].GetComponent<Image> ().color = SELECTED_COLOR;
			}
			for (int i = 0; i < vowelButtons.Length; i++) {
				if (i != letter1 && i != letter2)
					vowelButtons [i].GetComponent<Image> ().color = Color.white;
			}
		}

		public void SelectLetter (int letter)
		{
			if (letter != -1)
				vowelButtons [letter].GetComponent<Image> ().color = SELECTED_COLOR;
		}

		public void IncorrectSelection (int letter1, int letter2)
		{
			if (letter1 != -1)
				vowelButtons [letter1].GetComponent<Image> ().color = WRONG_COLOR;
			if (letter2 != -1) {
				vowelButtons [letter2].GetComponent<Image> ().color = WRONG_COLOR;
			}
		}

		public void CorrectSelection (int letter1, int letter2)
		{
			if (letter1 != -1)
				vowelButtons [letter1].GetComponent<Image> ().color = RIGHT_COLOR;
			if (letter2 != -1) {
				vowelButtons [letter2].GetComponent<Image> ().color = RIGHT_COLOR;
			}
		}

		public void IncorrectSelection (int letter)
		{
				vowelButtons [letter].GetComponent<Image> ().color = WRONG_COLOR;
		}

		public void CorrectSelection (int letter)
		{
			vowelButtons [letter].GetComponent<Image> ().color = RIGHT_COLOR;
		}

		public void ResetSelection (int letter)
		{
			if (letter != -1)
				vowelButtons [letter].GetComponent<Image> ().color = Color.white;
		}


		public string RemoveVowels (string word)
		{
			word = word.Replace ("A", "_");
			word = word.Replace ("Á", "_");
			word = word.Replace ("E", "_");
			word = word.Replace ("É", "_");
			word = word.Replace ("I", "_");
			word = word.Replace ("Í", "_");
			word = word.Replace ("O", "_");
			word = word.Replace ("Ó", "_");
			word = word.Replace ("U", "_");
			word = word.Replace ("Ú", "_");
			return word;
		}

		public void CorrectAnswer (int letter)
		{
			nextButton.interactable = true;
			ticButton.interactable = false;
			CorrectSelection (letter);

		}

		public void ResetTickAndNext ()
		{
			nextButton.interactable = false;
			ticButton.interactable = true;
		}

		public void ShowWord(string s) {
			rulesText.text = s.ToUpper();
		}
	}
}