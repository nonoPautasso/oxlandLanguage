using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.App;
using System;

namespace Assets.Scripts.Metrics
{
    public class DetailsView : MonoBehaviour
    {
        public int MAX_COLUMNS = 10;

        public Text activity;
        public Text username;
        public Text date;
        public Text score;
        public Text time;
        public Text correctQuantity;
        public Text incorrectQuantity;
        public Text hintsQuantity;
        public Text note;

        public GameObject lineRenderer;

		public List<Toggle> points;

        private List<GameMetrics> metricsPoints;             

       

        private void PlayClicSound()
        {
            SoundManager.instance.PlayClickSound();
        }

        internal void ShowDetailsOf(string activity, string username, List<GameMetrics> metrics)
        {
            metricsPoints = new List<GameMetrics>();
            this.activity.text = activity;
            this.username.text = username.ToUpper();
            groupGames(metrics);
            makeChart();
            //joinPoints(points, metricsPoints.Count);
        }

      
//        private void joinPoints(List<Image> points, int count)
//        {
//            for(int i = 0; i < count && i - 1 < points.Count - 1 ; i++)
//            {
//                points[i].GetComponent<LineRenderer>().SetVertexCount(2);
//                Vector3 pos = points[i].transform.position;
//                pos.z = -10;
//
//                Vector3 pos1 = points[i+1].transform.position;
//                pos1.z = -10;
//
//
//                points[i].GetComponent<LineRenderer>().SetPosition(0, pos);
//                points[i].GetComponent<LineRenderer>().SetPosition(1, pos1);
//            }
//        }    


		public void ResetChart(){
			for (int j = 0; j < points.Count; j++){
				points[j].GetComponent<Toggle>().isOn = false;
			}
		}

        private void makeChart()
        {

            float MAX_Y = points[1].transform.position.y;
            float MIN_Y = points[0].transform.position.y;
			Debug.Log ("min" + MIN_Y + "max" + MAX_Y);

            for (int i = 0; i < metricsPoints.Count; i++){
				points[i].gameObject.SetActive(true);
				Vector3 pos = points[i].transform.position;
                pos.y = calculateY(metricsPoints[i].score, MAX_Y, MIN_Y);
                points[i].transform.position = pos;
              
            }
			Debug.Log ("INDEX:"+(metricsPoints.Count-1));
			Debug.Log ("ARRAY COMP:"+points[metricsPoints.Count-1]);
            points[metricsPoints.Count - 1].GetComponent<Toggle>().isOn = true;

            for(int i = metricsPoints.Count; i < MAX_COLUMNS; i++){
                points[i].gameObject.SetActive(false);
            }
        }

        private float calculateY(int score, float MAX_Y, float MIN_Y)
        {
            float SCORE_MAX = 10000;
            float SCORE_MIN = 1000;
            float t = MIN_Y + (((score - SCORE_MIN) * (MAX_Y - MIN_Y)) / (SCORE_MAX - SCORE_MIN));
            return t;
        }               

        private void groupGames(List<GameMetrics> metrics){        
            int gruopSize = (int)Math.Ceiling((metrics.Count + 0.0f) / (MAX_COLUMNS + 0.0f));
            this.note.text = gruopSize + (SettingsController.instance.GetLanguage() == 0 ? " en " : " in ") + "1";

            for(int i = 0; i < Math.Ceiling((metrics.Count + 0.0f) / (gruopSize + 0.0f)); i++)
            {
                List<GameMetrics> currentGroup = metrics.GetRange(i, i + gruopSize < metrics.Count ? gruopSize : metrics.Count - metricsPoints.Count);
                GameMetrics currentMetric = new GameMetrics(-1, -1);
                currentMetric.date = currentGroup[0].date;
                for(int j = 0; j < currentGroup.Count; j++)
                {
                    currentMetric.score += currentGroup[j].score;
                    currentMetric.lapsedTime += currentGroup[j].lapsedTime;
                    currentMetric.hints += currentGroup[j].hints;
                    currentMetric.rightAnswers += currentGroup[j].rightAnswers;
                    currentMetric.wrongAnswers += currentGroup[j].wrongAnswers;
                }
                currentMetric.score = Average(currentMetric.score, currentGroup.Count);
                currentMetric.lapsedTime = Average(currentMetric.lapsedTime, currentGroup.Count);
                currentMetric.hints = Average(currentMetric.hints, currentGroup.Count);
                currentMetric.rightAnswers = Average(currentMetric.rightAnswers, currentGroup.Count);
                currentMetric.wrongAnswers = Average(currentMetric.wrongAnswers, currentGroup.Count);

                metricsPoints.Add(currentMetric);
            }
        }

        private int Average(int number, int count)
        {
            return (int)Math.Round((number + 0f) / (count + 0f));
        }

        public void ShowInfoOf(int pointNumber)
        {
			if (pointNumber < metricsPoints.Count) {
				GameMetrics currentMetric = metricsPoints[pointNumber];
				date.text = currentMetric.date;
				score.text = "" +currentMetric.score;
				time.text = "" + currentMetric.lapsedTime + " s";
				correctQuantity.text = "" + currentMetric.rightAnswers;
				incorrectQuantity.text = "" + currentMetric.wrongAnswers;
				hintsQuantity.text = "" + currentMetric.hints;
			}

        }
    }
}