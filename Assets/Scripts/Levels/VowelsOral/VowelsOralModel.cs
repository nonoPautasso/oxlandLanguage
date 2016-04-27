using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using System;
using Assets.Scripts.Levels.Vowels;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.VowelsOral
{
	public class VowelsOralModel : LevelModel
	{

		private string currentCorrectLetter;

		int[] letterAmounts;
		int langNum;

		public void InitModel ()
		{
			letterAmounts = new int[5];
		}

		public DataTrio<Sprite[], AudioClip[], int> LoadResources (string letter, int language)
		{
			langNum = language;
			string lang;
			string englishText;
			switch (language) {
			case 0:
				lang = "Spanish";
				englishText = "";
				break;
			case 1:
				lang = "English";
				englishText = "English";
				break;
			default:
				lang = "Spanish";
				englishText = "";
				break;
			}
			int intValue;
			switch (letter) {
			case "A":
				intValue = 0;
				break;
			case "E":
				intValue = 1;
				break;
			case "I":
				intValue = 2;
				break;
			case "O":
				intValue = 3;
				break;
			case "U":
				intValue = 4;
				break;
			default:
				intValue = -1;
				break;
			}
			DataTrio<Sprite[], AudioClip[], int> toReturn;
			AudioClip[] clips = Resources.LoadAll<AudioClip> ("Audio/" + lang + "/" + letter + "Words");
			Sprite[] images = Resources.LoadAll<Sprite> ("Sprites/" + lang + "/ObjectsVowels" + englishText);
			toReturn = new DataTrio<Sprite[], AudioClip[], int> (new Sprite[clips.Length], new AudioClip[clips.Length], intValue);
			for (int i = 0, j = GetLetterStartingIndex (letter); j < GetLetterStartingIndex (letter)
			+ GetLetterSize (letter); i++, j++) {
				toReturn.Fst () [i] = images [j];
				toReturn.Snd () [i] = clips [i];
			}
			letterAmounts [intValue] = 2;
			Sprite[] fst = toReturn.Fst ();
			AudioClip[] snd = toReturn.Snd ();

			DataPair<Sprite[], AudioClip[]> randomized = RandomizeArrays<Sprite, AudioClip> (toReturn.Fst (), toReturn.Snd ());
			toReturn.SetFst (randomized.Fst ());
			toReturn.SetSnd (randomized.Snd ());
			return toReturn;
		}

		public override void NextChallenge ()
		{
			throw new NotImplementedException ();
		}

		public override void RequestHint ()
		{
			throw new NotImplementedException ();
		}

		public DataPair<int, int> RequestHintInfo ()
		{
			DataPair<int, int> toReturn;
			switch (currentCorrectLetter) {
			case "A":
				toReturn = new DataPair<int, int> (2, 4);
				break;
			case "E":
				toReturn = new DataPair<int, int> (2, 4);
				break;
			case "I":
				toReturn = ChooseTwoRandomOpenVowels ();
				break;
			case "O":
				toReturn = new DataPair<int, int> (2, 4);
				break;
			case "U":
				toReturn = ChooseTwoRandomOpenVowels ();
				break;
			default:
				toReturn = null;
				break;
			}
			return toReturn;
		}

		public DataPair<int, int> ChooseTwoRandomOpenVowels ()
		{
			DataPair<int, int> toReturn;
			int fst = 0, snd = 0;
			while (fst == snd) {
				fst = UnityEngine.Random.Range (0, 3);
				snd = UnityEngine.Random.Range (0, 3);
			}
			if (fst == 2)
				fst = 3;
			if (snd == 2)
				snd = 3;
			toReturn = new DataPair<int, int> (fst, snd);
			return toReturn;
		}

		public override void StartGame ()
		{
			throw new NotImplementedException ();
		}

		static DataPair<T[], V[]> RandomizeArrays<T, V> (T[] arr1, V[] arr2)
		{
			for (var i = arr1.Length - 1; i > 0; i--) {
				var r = UnityEngine.Random.Range (0, i);
				var tmp1 = arr1 [i];
				arr1 [i] = arr1 [r];
				arr1 [r] = tmp1;

				var tmp2 = arr2 [i];
				arr2 [i] = arr2 [r];
				arr2 [r] = tmp2;
			}
			return new DataPair<T[], V[]> (arr1, arr2);
		}

		public int Next ()
		{
			if (letterAmounts [0] == 0 && letterAmounts [1] == 0 && letterAmounts [2] == 0
			    && letterAmounts [3] == 0 && letterAmounts [4] == 0)
				return -1;
			int value = UnityEngine.Random.Range (0, 5);
			if (letterAmounts [value] == 0)
				return Next ();
			else
				letterAmounts [value]--;
			switch (value) {
			case 0:
				currentCorrectLetter = "A";
				break;
			case 1:
				currentCorrectLetter = "E";
				break;
			case 2:
				currentCorrectLetter = "I";
				break;
			case 3:
				currentCorrectLetter = "O";
				break;
			case 4:
				currentCorrectLetter = "U";
				break;
			default:
				currentCorrectLetter = null;
				break;
			}
			Debug.Log ("Value returned: " + value);
			return value;
		}

		public bool CheckSubmittedLetter (string letter)
		{
			return letter.Equals (currentCorrectLetter);
		}

		int GetLetterStartingIndex (string letter)
		{
			int toReturn;
			switch (letter) {
			case "A":
				toReturn = 0;
				break;
			case "E":
				if (langNum == 0)
					toReturn = 13;
				else
					toReturn = 6;
				break;
			case "I":
				if (langNum == 0)
					toReturn = 24;
				else
					toReturn = 13;
				break;
			case "O":
				if (langNum == 0)
					toReturn = 29;
				else
					toReturn = 19;
				break;
			case "U":
				if (langNum == 0)
					toReturn = 36;
				else
					toReturn = 27;
				break;
			default:
				toReturn = -1;
				break;
			}
			return toReturn;
		}

		int GetLetterSize (string letter)
		{
			int toReturn;
			switch (letter) {
			case "A":
				if (langNum == 0)
					toReturn = 13;
				else
					toReturn = 6;
				break;
			case "E":
				if (langNum == 0)
					toReturn = 11;
				else
					toReturn = 7;
				break;
			case "I":
				if (langNum == 0)
					toReturn = 5;
				else
					toReturn = 6;
				break;
			case "O":
				if (langNum == 0)
					toReturn = 7;
				else
					toReturn = 8;
				break;
			case "U":
				if (langNum == 0)
					toReturn = 5;
				else
					toReturn = 5;
				break;
			default:
				toReturn = -1;
				break;
			}
			return toReturn;
		}
	}
}