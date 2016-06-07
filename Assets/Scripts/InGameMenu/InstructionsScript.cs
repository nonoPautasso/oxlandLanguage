﻿using UnityEngine;
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
			currentLevel = AppController.instance.GetCurrentLevel ();
			switch (SettingsController.instance.GetLanguage ()) {
			case 0:
				instructionText = AppController.instance.appModel.InstructionsSpanish [currentLevel];
				instructionAudio = (AudioClip) Resources.Load ("AudiosConsignas/Castellano/" + (currentLevel+1));
				break;
			case 1:
				instructionText = AppController.instance.appModel.InstructionsEnglish [currentLevel];
				instructionAudio =  (AudioClip) Resources.Load ("AudiosConsignas/English/" + (currentLevel+1)+"e");
				break;
			}

			this.GetComponentsInChildren<Text> () [0].text = instructionText;

			SoundManager.instance.PlayClip (instructionAudio);

		}

		public void OnClickPanel(){
			SoundManager.instance.StopSound();
			SoundManager.instance.PlayClickSound ();
			ViewController.instance.HideInstructions ();
			TimerImpl.instance.Resume();
		}
	}
}

