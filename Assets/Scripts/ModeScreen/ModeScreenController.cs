﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using System;

namespace Assets.Scripts.ChooseScreen
{

    public class ModeScreenController : MonoBehaviour
    {

        public static ModeScreenController instance;
        public ModeScreenView modeScreenView;


        void Start()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        public void SetMode(int newMode)
        {
            SettingsController.instance.SwitchMode(newMode);
            ViewController.instance.LoadScene("MainMenu");
        }

        internal void GoBack()
        {
            ViewController.instance.LoadScene("NameScreen");
        }

        internal static void PlayClicSound()
        {
            SoundManager.instance.PlayClicSound();
        }
    }
}
