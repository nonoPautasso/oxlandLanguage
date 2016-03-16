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
        

        public void OnMainMenuClic()
        {
            PlayClicSound();
            AppController.instance.MainMenu();
            Destroy(gameObject);
            MetricsManager.instance.DiscardCurrentMetrics();
        }

        public void OnInstructionsClic(){
            PlayClicSound();
            AppController.instance.ShowInstructions();
            Destroy(gameObject);
        }

        public void OnCicRestartGame(){
            PlayClicSound();
            AppController.instance.RestartLvl();
            Destroy(gameObject);
            MetricsManager.instance.DiscardCurrentMetrics();
        }

        public void OnClicBackToGame(){
            PlayClicSound();
            Destroy(gameObject);
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

      

        private void PlayClicSound()
        {
            SoundManager.instance.PlayClicSound();
        }

    }

}
