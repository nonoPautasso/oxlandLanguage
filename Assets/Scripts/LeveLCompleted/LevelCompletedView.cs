using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.App;
using System;

namespace Assets.Scripts.LevelCompleted
{
    public class LevelCompletedView : MonoBehaviour
    {
        public Text title;
        public Text score;
        public Button nextLvlBtn;
        public List<Image> stars;
        public GameObject correct;
        public GameObject incorrect;
        public GameObject hints;


        // Use this for initialization
        void Start() {
            UpdateLanguage(SettingsController.instance.GetLanguage());
            List<List<GameMetrics>> metrics = MetricsManager.instance.metricsModel.metrics;
            SetValues(MetricsManager.instance.metricsModel.GetCurrentMetrics());
            UpdateNextLvlBtn(!AppController.instance.IsMaxLevelPossible()); 
        }

        private void UpdateNextLvlBtn(bool update)
        {
            nextLvlBtn.interactable = update;
            nextLvlBtn.enabled = update;
            Color color = nextLvlBtn.image.color;
            if (update) { color.a = 1;  }
            else { color.a = 0.7f; }
            nextLvlBtn.image.color = color;
        }

        private void UpdateLanguage(int language)
        {
            switch (language)
            {
                case 0:
                    title.text = "¡NIVEL COMPLETO!";
                    correct.GetComponentInChildren<Text>().text = "Correctas:";
                    incorrect.GetComponentInChildren<Text>().text = "Incorrectas:";
                    hints.GetComponentInChildren<Text>().text = "Pistas:";
                    break;
                case 1:
                    title.text = "LEVEL COMPLETED!";
                    correct.transform.Find("label").GetComponent<Text>().text = "Correct:";
                    incorrect.transform.Find("label").GetComponent<Text>().text = "Inorrect:";
                    hints.transform.Find("label").GetComponent<Text>().text = "Hints:";
                    break;
            }
        }

        public void SetValues(GameMetrics gameMetrics)
        {
            for(int i = 0; i < 3; i++)
            {
                stars[i].gameObject.SetActive(i < gameMetrics.stars);
            }

            score.text = "" + gameMetrics.score;
            correct.transform.Find("quantity").GetComponent<Text>().text = "" + gameMetrics.rightAnswers;
            incorrect.transform.Find("quantity").GetComponent<Text>().text = "" + gameMetrics.wrongAnswers;
            hints.transform.Find("quantity").GetComponent<Text>().text = "" + gameMetrics.hints;
        }

        public void OnClickNextLvl()
        {
            PlayClicSound();
            LevelCompletedController.instance.NextLvl();
        }

        public void OnClickRetry()
        {
            PlayClicSound();
            LevelCompletedController.instance.RetryLvl();
        }

        public void OnClicMainMenu()
        {
            PlayClicSound();
            LevelCompletedController.instance.MainMenu();
        }

        private void PlayClicSound()
        {
            LevelCompletedController.instance.PlayClicSound();
        }
    }
}
