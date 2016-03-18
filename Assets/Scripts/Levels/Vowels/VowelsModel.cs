using UnityEngine;
using System.Collections;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels.Vowels
{
	public class VowelsModel : LevelModel
	{
		char[] bubbleLetters;
		bool[] revealedLetters;

		// Use this for initialization
		void Start ()
		{
			bubbleLetters = new char[4];
			revealedLetters = new bool[5];
		}

		public override void StartGame ()
		{
			throw new System.NotImplementedException ();
		}

		public override void NextChallenge ()
		{
			bubbleLetters = new char[4];
			revealedLetters = new bool[5];
		}

		public override void RequestHint ()
		{
			throw new System.NotImplementedException ();
		}
	}
}