using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.Vowels
{
	public class VowelsController : LevelController
	{
		public VowelsView view;

		VowelsModel model;


		// Use this for initialization
		void Start ()
		{
			InitGame ();
		}

		public override void InitGame ()
		{
			model = new VowelsModel ();
			model.StartGame ();
			for (int i = 0; i < 5; i++) {
				view.ShowLetter (i, "");
			}
            for (int i = 0; i < 4; i++)
            {
                view.SetLetterButton(i, model.GenerateLetter());
            }
		}

		public override void NextChallenge ()
		{
			
		}

		public override void ShowHint ()
		{
            DataPair<string, int>[] letters = model.RequestHintInfo();
            for (int i = 0; i < letters.Length; i++)
            {
                view.RevealHint(letters[i].Snd(), letters[i].Fst());
            }
		}

		public void SubmitLetter (Text letter)
		{
            Debug.Log("Letter submitted: " + letter.text);
			int index = model.IndexOfRevealedVowel (letter.text);
            if (index != -1)
            {
                Debug.Log("Revealing letter at index: " + index);
                view.ShowLetter(index, letter.text);
                model.RevealLetter(index, letter.text);
                //LogAnswer(true);
            }
            //else LogAnswer(false);
            //if (model.GetNumRevealedLetters() == 5) EndGame(model.MinSeconds,model.PointsPerSecond, model.PointsPerError);
		}

		public void ResetBubble (Button bubble)
		{
			string letter = model.GenerateLetter ();
			model.ReturnToStack (bubble.GetComponentInChildren<Text> ().text);
			view.ResetAnimation (bubble, letter);
		}

	}
}