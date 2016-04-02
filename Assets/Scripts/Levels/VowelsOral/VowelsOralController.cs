using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using System;

namespace Assets.Scripts.Levels.VowelsOral
{
    public class VowelsOralController : LevelController
    {

        public VowelsOralView view;

        private VowelsOralModel model;

        public override void InitGame()
        {
            model.InitModel();
            LoadResources();
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
    }
}