﻿using System;
using UnityEngine.UI; 
using UnityEngine;

namespace Assets.Scripts.App
{
	public abstract class LevelView : MonoBehaviour
	{
		public Button hintBtn;

		public abstract void ShowHint ();
		public abstract void EndGame();

		public void EnableHint(){
			hintBtn.image.color = Color.white;
			hintBtn.enabled = true;	
		}

		public void DisableHint(){
			Color color = Color.white;
			color.a = 0.9f;
			color.b = color.b - 0.4f;
			color.r = color.r - 0.4f;
			color.g = color.g - 0.4f;
			hintBtn.image.color = color;
			hintBtn.enabled = false;
		}

		public void OnClickMenuBtn(){
            PlaySoundClick();
            AppController.instance.ShowInGameMenu();
		}

		public void OnClickHintBtn(){
			if (hintBtn.enabled)
			{
                PlaySoundClick();
                ShowHint ();
			}
		}

        internal void PlaySoundClick()
        {
            SoundManager.instance.PlayClickSound();
        }

        internal void PlayRightSound()
        {
            SoundManager.instance.PlayRightAnswerSound();
        }

        internal void PlayWrongSound()
        {
            SoundManager.instance.PlayFailureSound();
        }


    }
}

