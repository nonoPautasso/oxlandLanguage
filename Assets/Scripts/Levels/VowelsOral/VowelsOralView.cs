using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using System;
using UnityEngine.UI;

namespace Assets.Scripts.Levels.VowelsOral
{
    public class VowelsOralView : LevelView
    {

        public Button[] vowelButtons;

        private Image currentObjectImage;
        

        public override void EndGame()
        {
            throw new NotImplementedException();
        }

        public override void ShowHint()
        {
            throw new NotImplementedException();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}