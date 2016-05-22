using System;
using Assets.Scripts.App;
using Assets.Scripts.Common;
using UnityEngine;
using I18N;

namespace Assets.Scripts.Levels.Abc {
	public class AbcController : LevelController {
		public AbcView view;
		private AbcModel model;

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
			view.SetCurrentPage (model.GetCurrentLetter (), model.GetCurrentPage ());
		}

		public void ObjectClick (int index) {
			bool correct = model.ObjectClick (index);
			LogAnswer (correct);
			view.Answer(model.GetWord (index), index, correct);

			if(model.PageEnded()){
				view.PageEnded ();
			}
		}

		public void NextClick(){
			if(model.HasEnded()){
				EndGame(model.MinSeconds, model.PointsPerSecond, model.PointsPerError);
			}

			model.NextPage ();
			SetCurrentPage ();
		}

		public bool IsCorrect (int index) {
			return model.IsCorrect (index);
		}

		public void LetterClick () {
			string letter = model.GetCurrentLetter ();
			AudioClip clip = Resources.Load<AudioClip>("Audio/" + I18n.Msg ("words.locale") + "/Letters/" + letter);
			SoundManager.instance.PlayClip(clip);
		}

		public void SoundButtonClick (int index) {
			model.GetWord(index).PlayWord();
		}

		public override void RestartGame () {
			Start();
		}

		public void Start() { InitGame (); }
	}
}