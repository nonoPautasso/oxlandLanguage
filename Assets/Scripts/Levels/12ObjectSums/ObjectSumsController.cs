using UnityEngine;
using System.Collections;
using Assets.Scripts.App;
using System;

namespace Assets.Scripts.Levels.ObjectSums
{

    public class ObjectSumsController : LevelController
        
    {
        private const int MIN_STREAK = 4;
        private const int MIN_CORRECT_ANSWERS = 10;

        public static ObjectSumsController instance;
        private ObjectSumsModel model;
        public ObjectSumsView view;
        private int streak;

        void Start(){
            streak = 0;
            if (instance == null){ instance = this; }
            else if (instance != this){ Destroy(this); }
            InitGame();
        }
   
        public override void InitGame()
        {
            model = new ObjectSumsModel();
            model.StartGame();
            MetricsManager.instance.GameStart();
            NextChallenge();
        }
        // This method asks the view and the model for NextChallange method.
        public override void NextChallenge(){
            model.NextChallenge();
            view.NextChallenge(model.GetFirstNumber(), model.GetSecondNumber());          
        }

        public override void ShowHint(){
            LogHint();
        }
        // This method is used to check if the answer is correct, and to count the streak of correct answers.
        // To win the game the player needs to have 10 corrects answers and at least the last 4 have to be correct.
        internal void CheckAnswer(int answer){
            bool isCorrectAnswer = model.CheckAnswer(answer);
            LogAnswer(isCorrectAnswer);
            if (isCorrectAnswer){
                streak++;

                if (MetricsManager.instance.metricsModel.GetCurrentMetrics().rightAnswers >= MIN_CORRECT_ANSWERS && streak>=MIN_STREAK) {
					EndGame (model.MinSeconds,model.PointsPerSecond,model.PointsPerError);  
                } else{ view.CorrectAnswer(); }
            }
            else{
                view.WrongAnswer();
                streak = 0;
            }
        }
    }
}
