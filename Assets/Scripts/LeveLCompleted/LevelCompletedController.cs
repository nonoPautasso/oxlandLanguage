using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.App;

namespace Assets.Scripts.LevelCompleted
{
    public class LevelCompletedController : MonoBehaviour
    {
        public static LevelCompletedController instance;       

        // Use this for initialization
        void Start()
        {
            if (instance == null)
                instance = this;       
            else if (instance != this)
                Destroy(gameObject);

            SoundManager.instance.PlayLevelCompleteSound();
        }

        internal void NextLvl(){
            AppController.instance.NextLvl();
        }

        internal void RetryLvl()
        {
            AppController.instance.RestartLvl();
        }

        internal void MainMenu()
        {
			ViewController.instance.LoadMainMenu();
        }

        internal void PlayClicSound()
        {
            SoundManager.instance.PlayClickSound();
        }
    }
}
