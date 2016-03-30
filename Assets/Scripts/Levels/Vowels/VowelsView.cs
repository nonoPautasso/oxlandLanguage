using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using UnityEngine.UI;

namespace Assets.Scripts.Levels.Vowels
{
	public class VowelsView : LevelView
	{
		public Button[] revealedLetters;
		public Button[] bubbleLetters;

		public override void ShowHint ()
		{
			throw new System.NotImplementedException ();
		}

        public void RevealHint(int pos, string letter)
        {
            (revealedLetters[pos].GetComponentInChildren<Text> () as Text).text = letter;
            (revealedLetters[pos].GetComponentInChildren<Text>() as Text).color = new Color(0, 0, 0, 0.5f);
        }

		public override void EndGame ()
		{
			throw new System.NotImplementedException ();
		}

		public void ShowLetter (int pos, string letter)
		{
			(revealedLetters [pos].GetComponentInChildren<Text> () as Text).text = letter;
            (revealedLetters[pos].GetComponentInChildren<Text>() as Text).color = new Color(0, 0, 0, 1f);
        }

		public void SetLetterButton (int pos, string letter)
		{
			(bubbleLetters [pos].GetComponentInChildren<Text> () as Text).text = letter;
		}

		public void ResetAnimation (Button bubble, string letter)
		{
			//	bubble.GetComponent<Animation> ().GetComponent<AnimationState>().time = 0;
			bubble.GetComponentInChildren<Text> ().text = letter;
		}
	}
}