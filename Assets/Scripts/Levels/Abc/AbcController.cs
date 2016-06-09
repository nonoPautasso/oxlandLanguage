using System;
using Assets.Scripts.App;
using Assets.Scripts.Common;
using UnityEngine;
using I18N;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.Abc {
	public class AbcController : LevelController {
		private AbcModel model;
		public AbcView view;

		public override void NextChallenge () { }

		public override void ShowHint () {
			LogHint ();
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new AbcModel();
			view.Controller (this);
			model.StartGame();
			SetCurrentPage ();
		}

		void SetCurrentPage () {
			view.SetCurrentPage (model.GetCurrentLetter(), model.GetCurrentPageNumber (), model.GetCurrentPage ());
		}

		public void BackButton () {
			model.BackButton ();
			SetCurrentPage ();
		}

		public void NextButton () {
			model.NextButton ();
			SetCurrentPage ();
		}

		public void ObjectClick (int index) {
			bool correct = model.ObjectClick (index);
			LogAnswer (correct);
			view.Answer(model.GetWord (index), index, correct);

			if(model.HasEnded()){
				EndGame(model.MinSeconds, model.PointsPerSecond, model.PointsPerError);
			}
		}

		public bool IsCorrect (int index) {
			return model.IsCorrect (index);
		}

		public void SubmarineClick () {
			string letter = model.GetLetters()[model.GetCurrentPageNumber()];
			AudioClip clip = Resources.Load<AudioClip>("Audio/" + I18n.Msg ("words.locale") + "/Letters/" + letter);
			SoundManager.instance.PlayClip(clip);
		}

		public void SoundButtonClick (int index) {
			Word word = model.GetWord (index);
			word.PlayWord();
			view.WordPlayed (word.AudioLength ());
		}

		public List<Tuple<Word, bool>> GetCurrentPage () {
			return model.GetCurrentPage ();
		}

		public List<string> GetLetters () {
			return model.GetLetters ();
		}

		public override void RestartGame () {
			Start();
		}

		public void Start() { InitGame (); }
	}
}