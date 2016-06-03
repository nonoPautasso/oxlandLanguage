using UnityEngine;
using Assets.Scripts.Timer;
using UnityEngine.UI;
using Assets.Scripts.App;

namespace Assets.Scripts.InGameMenu
{

	public class InGameMenuView : MonoBehaviour
	{
		public Button mainMenuBtn, instructionsBtn, restartGameBtn, backToGameBtn;
		public Text  hoverTxt;


		public void OnMainMenuClick()
		{
			PlayClickSound();
			AppController.instance.MainMenu();
			MetricsManager.instance.DiscardCurrentMetrics();
			ViewController.instance.HideInGameMenu ();
		}

		public void OnInstructionsClick(){
			PlayClickSound();
			ViewController.instance.ShowInstructions();
			ViewController.instance.HideInGameMenu ();
		}

		public void OnClickRestartGame(){
			PlayClickSound();
			AppController.instance.RestartLvl();
			ViewController.instance.HideInGameMenu ();
			MetricsManager.instance.DiscardCurrentMetrics();
		}

		public void OnClickBackToGame(){
			PlayClickSound();
			ViewController.instance.HideInGameMenu ();
			TimerImpl.instance.Resume();
		}

		public void OnHoverMainMenuBtn()
		{
			hoverTxt.text = (SettingsController.instance.GetLanguage() == 1) ? "MAIN MENU" :
				"MENÚ PRINCIPAL";
		}

		public void OnHoverInstructionsBtn()
		{
			hoverTxt.text = (SettingsController.instance.GetLanguage() == 1) ? "INSTRUCTIONS" :
				"CONSIGNA";
		}

		public void OnHoverBackToGameBtn()
		{
			hoverTxt.text = (SettingsController.instance.GetLanguage() == 1) ? "BACK TO GAME" :
				"VOLVER AL JUEGO";
		}

		public void OnHoveRestartGameBtn()
		{
			hoverTxt.text = (SettingsController.instance.GetLanguage() == 1) ? "RESTART GAME" :
				"EMPEZAR DE NUEVO";
		}

		public void OnExitHover()
		{
			hoverTxt.text = "";
		}



		private void PlayClickSound()
		{
			SoundManager.instance.PlayClickSound();
		}

	}

}
