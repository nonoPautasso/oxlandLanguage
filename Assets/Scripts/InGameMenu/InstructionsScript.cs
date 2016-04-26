using UnityEngine;
using Assets.Scripts.Timer;
using UnityEngine.UI;
using Assets.Scripts.App;
using System;

namespace Assets.Scripts.InGameMenu
{
	public class InstructionsScript : MonoBehaviour
	{

		private int currentLevel;
		private string instructionText;
		private AudioClip instructionAudio;
	


		void Start(){

			switch (SettingsController.instance.GetLanguage ()) {
			case 0:
				instructionText = AppController.instance.appModel.InstructionsSpanish [currentLevel-1];
				instructionAudio = (AudioClip) Resources.Load ("AudiosConsignas/Castellano/oxland" + currentLevel);
				break;
			case 1:
				instructionText = AppController.instance.appModel.InstructionsEnglish [currentLevel-1];
				instructionAudio =  (AudioClip) Resources.Load ("AudiosConsignas/English/oxland" + currentLevel+"e");
				break;
			}

			this.GetComponentsInChildren<Text> () [0].text = instructionText;
			SoundManager.instance.PlayClip (instructionAudio);

		}

		public void OnClickPanel(){
			SoundManager.instance.StopSound();
			SoundManager.instance.PlayClicSound ();
			ViewController.instance.HideInstructions ();
			TimerImpl.instance.Resume();
		}
	}
}

