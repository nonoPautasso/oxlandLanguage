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

		private int[] extraTextLevels;
		private string[] extraText;
	


		void Start(){
			extraTextLevels = new int[]{5,7,8,14,15,16};


			currentLevel = AppController.instance.GetCurrentLevel ();
			switch (SettingsController.instance.GetLanguage ()) {
			case 0:
				extraText = new string[]{"MISMO TAMAÑO","UNO MÁS GRANDE QUE 89","SOY DE LA FAMILIA DEL 20 Y TERMINO EN 6","YA PAGÓ","YA PAGÓ","TENGO"};
				instructionText = AppController.instance.appModel.InstructionsSpanish [currentLevel-1];
				instructionAudio = (AudioClip) Resources.Load ("AudiosConsignas/Castellano/oxland" + currentLevel);
				break;
			case 1:
				extraText = new string[]{"SAME SIZE","ONE BIGGER THAN 89","I BELONG TO THE 20 FAMILY AND END WITH A 6","YOU'VE ALREADY PAID","YOU'VE ALREADY PAID","YOU'VE GOT"};
				instructionText = AppController.instance.appModel.InstructionsEnglish [currentLevel-1];
				instructionAudio =  (AudioClip) Resources.Load ("AudiosConsignas/English/oxland" + currentLevel+"e");
				break;
			}

			int extraTextIndex = Array.IndexOf (extraTextLevels, currentLevel);
			if ( extraTextIndex!= -1) {
				this.GetComponentsInChildren<Text> () [1].text = extraText[extraTextIndex];
			}
			this.GetComponentsInChildren<Text> () [0].text = instructionText;
			SoundManager.instance.PlayClip (instructionAudio);

		}

		public void OnClickPanel(){
			SoundManager.instance.StopSound();
			SoundManager.instance.PlayClicSound ();
			AppController.instance.HideInstructions ();
			TimerImpl.instance.Resume();
		}
	}
}

