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

        public override void InitGame()
        {
            model = new VowelsOralModel();
            model.InitModel();
            view.InitView();
            LoadResources();
            view.Next(model.Next());
        }

        public override void NextChallenge()
        {
            view.Next(model.Next());
        }

        public override void RestartGame()
        {
            throw new NotImplementedException();
        }

        public override void ShowHint()
        {
            view.ShowHints(model.RequestHintInfo());
        }

        public void LoadResources()
        {
            view.SetResources(model.LoadResources("A"));
            view.SetResources(model.LoadResources("E"));
            view.SetResources(model.LoadResources("I"));
            view.SetResources(model.LoadResources("O"));
            view.SetResources(model.LoadResources("U"));
        }

        // Use this for initialization
        void Start()
        {
            InitGame();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SubmitLetter(Button button)
        {
            bool correct = model.CheckSubmittedLetter(button.GetComponentInChildren<Text>().text);
            if (correct)
            {
                view.PlayRightSound();
                //  LogAnswer(true);
                int nextLetterIndex = model.Next();
                if (nextLetterIndex == -1) EndGame(model.MinSeconds, model.PointsPerSecond, model.PointsPerError);
                view.Next(nextLetterIndex);    // Tells view to show the letter at appropriate index
            }
            else
            {
                view.PlayWrongSound();
            //    LogAnswer(false);
            }
        }

    }
}