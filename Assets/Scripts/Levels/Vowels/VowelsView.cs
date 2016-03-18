using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using UnityEngine.UI;

namespace Assets.Scripts.Levels.Vowels
{
	public class VowelsView : LevelView
	{
		public Button[] lettersRevealed;
		public Button[] letterButtons;

		// Use this for initialization
		void Start ()
		{
	
		}

		public override void ShowHint ()
		{
			throw new System.NotImplementedException ();
		}

		public override void EndGame ()
		{
			throw new System.NotImplementedException ();
		}

		public void ShowLetters(int pos, char letter) 
		{
			lettersRevealed [pos].GetComponentsInChildren<Text>() = letter;
		}

		public void SetLetterButton(int pos, char letter)
		{
			letterButtons [pos].GetComponentsInChildren<Text> () = letter;
		}
	}
}