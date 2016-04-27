using UnityEngine;
using System.Collections;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels.CompleteMissingVowel
{
	public class CompleteMissingVowelController : LevelController
	{

		public CompleteMissingVowelView view;
		private CompleteMissingVowelModel model;

		public string letter1;
		public string letter2;


		public override void NextChallenge ()
		{
			if (model.easyMode) {
				view.Next (model.NextEasy ());
			} else {
				if (model.wordCount < 10)
					view.NextHard (model.NextHard ());
				else
					EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);
			}
			view.UpdateLetterSelections (-1, -1);
			view.ResetTicAndNext ();
			letter1 = "";
			letter2 = "";
		}

		public override void ShowHint ()
		{
			view.PlaySoundClic ();
			view.ShowHints (model.RequestHintInfo ());
			view.ResetSelection (model.GetLetterPos (letter1));
			view.ResetSelection (model.GetLetterPos (letter2));
			letter1 = "";
			letter2 = "";
			LogHint ();
		}

		public override void InitGame ()
		{
			letter1 = "";
			letter2 = "";
			MetricsManager.instance.GameStart ();
			model = new CompleteMissingVowelModel ();
			model.InitModel (SettingsController.instance.GetLanguage ());
			view.InitView ();
			LoadResources ();
			view.Next (model.NextEasy ());
		}

		public void LoadResources ()
		{
			view.SetResources (model.LoadResources (true), true);
			view.SetResources (model.LoadResources (false), false);

		}

		public override void RestartGame ()
		{
			InitGame ();
		}

		// Use this for initialization
		void Start ()
		{
			InitGame ();
		}

		public void SelectLetter (string letter)
		{
			view.PlaySoundClic ();
			if (!letter1.Equals ("") && !letter2.Equals ("")) {
				if (letter1.Equals (letter)) {
					view.ResetSelection (model.GetLetterPos (letter1));
					letter1 = "";
				} else if (letter2.Equals (letter)) {
					view.ResetSelection (model.GetLetterPos (letter2));
					letter2 = "";
				} else {
					return;
				}
				return;
			}
				
			if (model.easyMode) {
				if (letter1.Equals (letter)) {
					view.ResetSelection (model.GetLetterPos (letter1));
					letter1 = "";
				}
				view.ResetSelection (model.GetLetterPos (letter1));
				letter1 = letter;
				letter2 = "";
				view.SelectLetter (model.GetLetterPos (letter1));
			} else {
				if (letter1.Equals (letter)) {
					view.ResetSelection (model.GetLetterPos (letter1));
					letter1 = "";
					return;
				} else if (letter2.Equals (letter)) {
					view.ResetSelection (model.GetLetterPos (letter2));
					letter2 = "";
					return;
				}
					
				
				if (letter1.Equals ("")) {
					view.ResetSelection (model.GetLetterPos (letter1));
					letter1 = letter;
					view.SelectLetter (model.GetLetterPos (letter1));

				} else {
					if (letter2.Equals ("")) {
						view.ResetSelection (model.GetLetterPos (letter2));
						letter2 = letter;
						view.SelectLetter (model.GetLetterPos (letter2));
					} else {
						view.ResetSelection (model.GetLetterPos (letter1));
						letter1 = letter;
						view.SelectLetter (model.GetLetterPos (letter1));
					}
				}
			}
		}

		public void SubmitLetters ()
		{
			bool correct;
			if (model.easyMode) {
				if (letter1.Equals ("")) {
					view.PlaySoundClic ();
					return;
				} else {
					correct = model.CheckEasyLetter (letter1);
					if (correct) {
						if (!(model.wordCount < 5))
							model.easyMode = false;
						view.PlayRightSound ();
						view.CorrectAnswer ();
					} else {
						view.IncorrectSelection (model.GetLetterPos (letter1), -1);
						view.PlayWrongSound ();
					}
					LogAnswer (correct);
				}
			} else {
				if (letter1.Equals ("") || letter2.Equals ("")) {
					view.PlaySoundClic ();
					if (letter1.Equals ("")) {
						view.IncorrectSelection (model.GetLetterPos (letter2));
					} else {
						view.IncorrectSelection (model.GetLetterPos (letter1));
					}
					return;
				} else {
					correct = model.CheckHardLetter (letter1) && model.CheckHardLetter (letter2);
					LogAnswer (correct);
					if (!correct) {
						if (!model.CheckHardLetter (letter1))
							view.IncorrectSelection (model.GetLetterPos (letter1));
						if (!model.CheckHardLetter (letter2))
							view.IncorrectSelection (model.GetLetterPos (letter2));
						view.PlayWrongSound ();
					} else {
						view.SelectLetter (model.GetLetterPos (letter1));
						view.SelectLetter (model.GetLetterPos (letter2));
						view.PlayRightSound ();
						view.CorrectAnswer ();
						
					}
				}
			}
			if (correct) {
				view.ShowWord (model.GetCurrentWord ());
			}
		}
	}
}