using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels
{
	public class SayTheNumberController : LevelController
    {

		public static SayTheNumberController instance;
		public SayTheNumberView view;
		public SayTheNumberModel model;

		public List<AudioClip> numberSounds;
		public List<AudioClip> spanishNumberSounds;
		public List<AudioClip> englishNumberSounds;

        

		public void Start(){
            if (instance == null){ instance = this;}
            else if (instance != this) Destroy(gameObject);
            MetricsManager.instance.GameStart();
            InitGame();
        }

		public override void InitGame(){
			model = new SayTheNumberModel();        
			switch(SettingsController.instance.GetLanguage ()) {
				case 0:
					numberSounds = spanishNumberSounds ;
					break;
				case 1:
					numberSounds = englishNumberSounds;
					break;
			}
            NextChallenge();
        }

		public override void NextChallenge(){
			view.EnableHintNumberButtons(model.disabledNumbers);
			view.NextChallenge ();
			model.NextChallenge();
			view.EnableHint();
			view.EnableNumberButtons();
            PlaySoundNumber();
        }

        public override void ShowHint(){
			LogHint ();
			model.RequestHint();
		}
			
        public void CheckAnswer(int number){			
			view.DisableNumberButtons ();         
			if (model.CheckAnswer (number)) {
				view.RightAnswer (number);
				LogAnswer(true);
				if (model.numbers.Count == 0) {
					view.EndGame ();
					EndGame (model.MinSeconds,model.PointsPerSecond,model.PointsPerError);  
				}
			} else {
				view.WrongAnswer ();
				LogAnswer (false);
				SoundManager.instance.PlayFailureSound ();
			}
			view.selectedNumber.enabled = true;                  
		}
       
		public void PlaySoundNumber(){
            view.PlaySoundAnimation(1);
            SoundManager.instance.PlayClip(numberSounds[model.currentNumber-1]);
		}      
    }
}


