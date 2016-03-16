using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.App;
using System;

namespace Assets.Scripts.Metrics
{
    public class ResultsView : MonoBehaviour
    {
        private const int MAX_ROWS = 6;

        public GameObject resultsView;

        public Button nextPageBtn;
        public Button prevPageBtn;
        public List<GameObject> viewRaws;
        public Text title;
        private List<MetricsRaw> raws;
        private int currentPage;
        private List<int> currentRawsToViewDetails;

        public GameObject detailsView;
        public Button goBackBtn;

        // Use this for initialization
        void Start(){
            currentPage = 0;
            title.text = SettingsController.instance.GetLanguage() == 0 ? "RESULTADOS" : "RESULTS";
            raws = new List<MetricsRaw>(viewRaws.Count);
            for (int i = 0; i < viewRaws.Count; i++){
                raws.Add(new MetricsRaw(viewRaws[i]));
            }

            currentRawsToViewDetails = new List<int>(6);
            updateArrows();
            updateMetricRows();
        }

        public void OnClicNextPageBtn(){
            PlaySoundClick();
            currentPage++;
            updateMetricRows();
            updateArrows();
        }

        public void OnClicPrevPageBtn(){
            PlaySoundClick();
            currentPage--;
            updateMetricRows();
            updateArrows();
        }

        public void OnClickCrossBtn(){
            PlaySoundClick();
            ViewController.instance.LoadScene("MainMenu");
        }

        public void OnClickViewDetailsCrossBtn()
        {
            PlaySoundClick();
            detailsView.SetActive(false);
            resultsView.SetActive(true);
        }

        private void PlaySoundClick()
        {
            SoundManager.instance.PlayClicSound();
        }

        private void updateMetricRows(){
            currentRawsToViewDetails.RemoveAll(e => true);
            List<int> activities = MetricsManager.instance.metricsModel.GetLevelIndexes(currentPage, MAX_ROWS);
            currentRawsToViewDetails.AddRange(activities);
            int i = 0;
            for (; i < MAX_ROWS && i < activities.Count; i++){
                updateRow(MetricsManager.instance.metricsModel.GetBestMetric(activities[i]), i, activities[i]);
                raws[i].Show();
            }
            for(; i < MAX_ROWS; i++){
                raws[i].Hide();
            }        
        }

        private void updateRow(GameMetrics gameMetrics, int rowIndex, int activity){
            raws[rowIndex].setActivity(AppController.instance.appModel.GetTitleFromIndex(activity));

            if (gameMetrics != null){
                raws[rowIndex].setScore(gameMetrics.score);
                raws[rowIndex].setStars(gameMetrics.stars);
                raws[rowIndex].getViewDetailsBtn().enabled = true;
            } else{
                raws[rowIndex].setScore(0);
                raws[rowIndex].setStars(0);
                raws[rowIndex].getViewDetailsBtn().enabled = false;
            }          
        }

        public void OnClickViewDetails(int index){
            Debug.Log(index +","+ currentRawsToViewDetails.Count);
            PlaySoundClick();
            ViewDetails(currentRawsToViewDetails[index]);
        }

        private void ViewDetails(int v){
            Debug.Log("View details of " + v);
            resultsView.SetActive(false);
            detailsView.SetActive(true);
            string activity = SettingsController.instance.GetLanguage() == 0 ? AppController.instance.appModel.GameTitlesSpanish[AppController.instance.GetCurrentLevel() - 1] : AppController.instance.appModel.GameTitlesEnglish[AppController.instance.GetCurrentLevel() - 1];
            ((DetailsView)detailsView.GetComponent<DetailsView>()).showDetailsOf(activity, SettingsController.instance.GetUsername(), MetricsManager.instance.GetMetricsByLevel(AppController.instance.GetCurrentLevel() - 1));
        }

        private void updateArrows(){
            nextPageBtn.gameObject.SetActive((MetricsManager.instance.metricsModel.GetLevelIndexes(currentPage + 1, MAX_ROWS).Count > 0));
            prevPageBtn.gameObject.SetActive((MetricsManager.instance.metricsModel.GetLevelIndexes(currentPage - 1, MAX_ROWS).Count > 0));

        }
    }
}