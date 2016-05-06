using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.App;
using System;

namespace Assets.Scripts.Levels.Castlenumbers6
{

    public class CastleController : LevelController
    {
        public static CastleController instance;
        private CastleModel model;
        public CastleView view;

        private const int firstCastleGame = 6;
        private const int secondCastleGame = 7;
        private const int thirdCastleGame = 8;
        private const int fourthCastleGame = 9;
        private const int fifthCastleGame = 10;
        private const int sixthCastleGame = 11;

        private int streak;
        private int audioIndex;

        void Start(){
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
            InitGame();
        }
      
       

        internal void OnRightAnswerAnimationEnd(){
            streak++;
            if ((model.GetCurrentGame() == 5 && MetricsManager.instance.metricsModel.GetCurrentMetrics().rightAnswers >= 5) || (model.GetCurrentGame() == 4 && MetricsManager.instance.metricsModel.GetCurrentMetrics().rightAnswers >= 7) || (model.GetCurrentGame() == 3 && MetricsManager.instance.metricsModel.GetCurrentMetrics().rightAnswers >= 6)  || (model.GetCurrentGame() == 2 && MetricsManager.instance.metricsModel.GetCurrentMetrics().rightAnswers >= 11) || (model.GetCurrentGame() != 2 && MetricsManager.instance.metricsModel.GetCurrentMetrics().rightAnswers >= 10))
            {
                if (model.GetCurrentGame() == 5 || streak >= 4 || model.GetCurrentGame() == 3 || (model.GetCurrentGame() == 4 && streak >= 3)){
					EndGame (model.MinSeconds,model.PointsPerSecond,model.PointsPerError);  
                } else {
                    view.OnRightAnswerAnimationEnd();
                    NextChallenge();
                }
            }
            else{
                view.OnRightAnswerAnimationEnd();
                NextChallenge();
            }        
        }

        internal int GetCurrentGame()
        {
            return model.GetCurrentGame();
        }

        public override void InitGame() {
            MetricsManager.instance.GameStart();
            streak = 0;
            switch (AppController.instance.GetCurrentLevel())
            {
                case firstCastleGame:
                    model = new CastleModel(0);
                    break;
                case secondCastleGame:
                    model = new CastleModel(1);
                    break;
                case thirdCastleGame:
                    model = new CastleModel(2);
                    break;
                case fourthCastleGame:
                    model = new CastleModel(3);
                    break;
                case fifthCastleGame:
                    model = new CastleModel(4);
                    break;
                case sixthCastleGame:
                    model = new CastleModel(5);
                    break;
                default:
                    Debug.Log("Error in number castle game");
                    break;
            }
            
            NextChallenge();
            
        }

        internal void OnWrongAnswerAnimationEnd()
        {
            streak = 0;
            view.OnWrongAnswerAnimationEnd();
        }

        public override void NextChallenge()
        {
            model.NextChallenge();
            switch (model.GetCurrentGame())
            {
                case 0:
                    view.NextChallenge(model.GetCurrentNumber());
                    break;
                case 1:
                    view.NextChallenge(GenerateQuestion(model.GetCurrentNumber(), model.GetCurrentQuestion()), audioIndex, Math.Abs(model.GetCurrentQuestion()), model.GetCurrentNumber() + model.GetCurrentQuestion());
                    break;
                case 2:
                    view.NextChallenge(GenerateQuestionForThirdCastleGame(model.GetCurrentRow(), model.GetCurrentNumber()));
                    break;
                case 3:
                    view.NextChallenge(model.GetCurrentRow(), model.GetCurrentSolutions());
                    break;
                case 4:
                    view.NextChallenge(model.GetCurrentNumber(), model.GetCurrentSolutions());
                    break;
                case 5:
                    view.NextChallenge(model.GetCurrentNumber(), model.GetCurrentSolutions(), model.GetCurrentErrors());
                    break;
            }
        }

        private string GenerateQuestionForThirdCastleGame(int row, int answer)
        {
            if(row < 10) {
                return SettingsController.instance.GetLanguage() == 0 ? "SOY DE LA FAMILIA DEL " + (row * 10) + " Y TERMINO EN " + (answer%10) :
                       "I BELONG TO THE " + (row * 10) + " FAMILY AND END WITH A " + (answer % 10);
            } else if(row == 10)
            {
                
                return SettingsController.instance.GetLanguage() == 0 ? "SOY MAYOR QUE " + (answer - 1) +"  Y MENOR QUE " + (answer + 1) :
                    "I AM BIGGER THAN "+ (answer - 1)  + " AND SMALLER THAN " + (answer + 1);
            } else
            {
                if(UnityEngine.Random.Range(0.0f, 0.99f) < 0.5)
                {
                    return SettingsController.instance.GetLanguage() == 0 ? "SOY VECINO DEL " + (answer + 1) + " Y NO DEL " + (answer + 3) :
                    "I AM THE NEIGHBOUR OF " + (answer + 1) + " AND NOT OF " + (answer + 3);
                } else
                {
                    return SettingsController.instance.GetLanguage() == 0 ? "SOY VECINO DEL " + (answer - 1) + " Y NO DEL " + (answer - 3) :
                    "I AM THE NEIGHBOUR OF " + (answer - 1) + " AND NOT OF " + (answer - 3);
                }
            }
           
        }

        private string GenerateQuestion(int answerNumber, int question)
        {
            switch (Math.Abs(question))
            {            
                case 1:
                    switch(UnityEngine.Random.Range(0,3))
                            {
                        case 0:
                            audioIndex = question > 0 ? 0 : 1;
                            if (SettingsController.instance.GetLanguage() == 0) {
                                return "UNO MÁS " + (question > 0 ? "CHICO" : "GRANDE") +" QUE " + (answerNumber + question);
                            } else{
                                return "ONE " + (question > 0 ? "SMALLER" : "BIGGER") + " THAN " + (answerNumber + question);
                            }
                        case 1:
                            audioIndex = question > 0 ? 4 : 5;
                            if (SettingsController.instance.GetLanguage() == 0)
                            {
                                return "UNO " + (question > 0 ? "MENOS" : "MÁS") + " QUE " + (answerNumber + question);
                            }
                            else {
                                return "ONE " + (question > 0 ? "LESS" : "MORE") + " THAN " + (answerNumber + question);
                            }
                        case 2:
                            audioIndex = question > 0 ? 2 : 3;
                            if (SettingsController.instance.GetLanguage() == 0){
                                return "EL " + (question > 0 ? "ANTERIOR" : "QUE LE SIGUE") + " A " + (answerNumber + question);
                            }
                            else {
                                return "THE ONE " + (question > 0 ? "BEFORE " : "AFTER ") + (answerNumber + question);
                            }
                            }
                    break;
                default:
                    audioIndex = question > 0 ? 0 : 1;
                    if (SettingsController.instance.GetLanguage() == 0){
                        return NumberToString(Math.Abs(question)) + " MÁS " + (question > 0 ? "CHICO" : "GRANDE") + " QUE " + (answerNumber + question);
                    }
                    else {
                        return NumberToString(Math.Abs(question)) +" " + (question > 0 ? "SMALLER" : "BIGGER") + " THAN " + (answerNumber + question);
                    }
         
            }
            Debug.Log("question : " + question);
            Debug.Log("ans num : " + answerNumber);
            return "ERROR AL GENERAR TEXT";
        }

        private string NumberToString(int v){
            switch (v) {
                case 1:
                    return SettingsController.instance.GetLanguage() == 0 ? "UNO" : "ONE";
                case 2:
                    return SettingsController.instance.GetLanguage() == 0 ? "DOS" : "TWO";
                case 3:
                    return SettingsController.instance.GetLanguage() == 0 ? "TRES" : "THREE";
                case 4:
                    return SettingsController.instance.GetLanguage() == 0 ? "CUATRO" : "FOUR";
                case 5:
                    return SettingsController.instance.GetLanguage() == 0 ? "CINCO" : "FIVE";
                default:
                    return "ERROR";
            }
        }

        public override void ShowHint()
        {
            LogHint();
            model.RequestHint();
            switch (model.GetCurrentGame())
            {
                case 3:
                    view.DisableLateralNumbers(model.GetDisabledLateralNumbers());
                    break;
                case 4:
                    view.DisableLateralNumbers(model.GetDisabledLateralNumbers());
                    break;
                case 5:
                    view.ShowButtonsAsOk(model.GetNumbersToShow());
                    break;
                default:
                    view.DisableNumbers(model.GetDisabledRows());
                    break;
            }
        }

        internal bool CheckAnswer(List<int> currentSolutions)
        {
            bool answer = model.CheckAnswer(currentSolutions);
            DoActionsPostCheckAnswer(answer);
            return answer;
        }

        private void DoActionsPostCheckAnswer(bool answer)
        {
            LogAnswer(answer);
            Debug.Log(MetricsManager.instance.metricsModel.GetCurrentMetrics().rightAnswers);
            if(model.GetCurrentGame() >= 3){
                int rightAnswers = MetricsManager.instance.metricsModel.GetCurrentMetrics().rightAnswers;
                if (model.GetCurrentSegment() == 0 && rightAnswers == 2) model.NextSegment();
                else if (model.GetCurrentSegment() == 1 && rightAnswers == 4) model.NextSegment();
                Debug.Log("Current segment: " + model.GetCurrentSegment());
            }
        }

        internal bool CheckAnswer(int number)
        {
            bool answer = model.CheckAnswer(number);
            DoActionsPostCheckAnswer(answer);
            return answer;
        }

        internal int GetCurrentNumber()
        {
            return model.GetCurrentNumber();
        }
    }

}