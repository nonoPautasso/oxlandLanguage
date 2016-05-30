using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using System;
using UnityEngine.UI;

namespace Assets.Scripts.Levels.VowelsOral
{
	public class VowelsOralController : LevelController
	{

		public VowelsOralView view;

		private VowelsOralModel model;

		public override void InitGame ()
		{
			MetricsManager.instance.GameStart ();
			model = new VowelsOralModel ();
			model.StartGame ();
			view.InitView ();
			LoadResources ();
			view.Next (model.Next ());

		}

		public override void NextChallenge ()
		{
			view.Next (model.Next ());
		}

		public override void RestartGame ()
		{
			throw new NotImplementedException ();
		}

		public override void ShowHint ()
		{
			LogHint ();
			view.ShowHints (model.RequestHintInfo ());
		}

		public void LoadResources ()
		{
			int language = SettingsController.instance.GetLanguage ();
			view.SetResources (model.LoadResources ("A", language));
			view.SetResources (model.LoadResources ("E", language));
			view.SetResources (model.LoadResources ("I", language));
			view.SetResources (model.LoadResources ("O", language));
			view.SetResources (model.LoadResources ("U", language));
		}

		// Use this for initialization
		void Start ()
		{
			InitGame ();
		}

		public void SelectLetter(Toggle toggle){
			
			SoundManager.instance.PlayClickSound ();
			if (model.GetCurrentSelectedLetter () == "") {
				view.EnableCheckButton ();
			}

			view.SetSelectedToggle (toggle);
			model.SetCurrentSelectedLetter (toggle.GetComponentInChildren<Text> ().text);
		}

		public void SubmitLetter ()
		{
			bool correct = model.CheckSubmittedLetter ();
			if (correct) {
				view.PlayRightSound ();
				LogAnswer (true);
				view.PrepareForNext ();	 

					
			} else {
				view.PlayWrongSound ();
				LogAnswer (false);
				model.SetCurrentSelectedLetter ("");
				view.DisableCheckButton ();
				view.PaintWrongToggle ();
			}
		}

		public void onClickNextButton(){
			int nextLetterIndex = model.Next ();

			if (nextLetterIndex == -1) {
				EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);
			} else {
				SoundManager.instance.PlayClickSound ();
				view.EnableAllButtons ();
				view.Next (nextLetterIndex);  // Tells view to show the letter at appropriate index
			}
		}

	}
}