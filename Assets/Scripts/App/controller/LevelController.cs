using UnityEngine;
using System.Collections;

namespace Assets.Scripts.App{

	public abstract class LevelController : MonoBehaviour{

		public abstract void NextChallenge ();
		public abstract void ShowHint ();
		public abstract void InitGame ();

		public void LogAnswer(bool isCorrect){
			MetricsManager.instance.LogAnswer (isCorrect);
		}

		public void LogHint(){
			MetricsManager.instance.LogHint ();
		}

		public void LogMetrics(int lapsedSeconds,int minSeconds,int pointsPerSecond,int pointsPerError){
			MetricsManager.instance.GameFinished (minSeconds, pointsPerSecond, pointsPerError);
		}

		public void CheckIfLevelUp(){
			if (SettingsController.instance.GetMode () == 1) {
				int currentLevel = AppController.instance.GetCurrentLevel ();
				if (currentLevel==AppController.instance.GetMaxLevelPossible()&&currentLevel!=16) {
					AppController.instance.SetMaxLevelPossible(currentLevel + 1);
				}

			}
		}

		public void EndGame(int minSeconds,int pointsPerSecond,int  pointsPerError){
			CheckIfLevelUp();
            MetricsManager.instance.GameFinished(minSeconds, pointsPerSecond, pointsPerError);
            ViewController.instance.LoadScene("LevelCompleted");
		}

		public void UnloadLevel(){
			Destroy (gameObject);
		}
	}
}