using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.App;

namespace Assets.Scripts.ChooseScreen
{

    public class ModeScreenView : MonoBehaviour
    {
        public Button freeBtn, challengeBtn;
        public Text titleTxt, hoverTxt;
        public Button backBtn;

        // Use this for initialization
        void Start()
        {
            if (SettingsController.instance.GetLanguage() == 1)
            {
                titleTxt.text = "SELECT GAME MODE";
                freeBtn.GetComponentInChildren<Text>().text = "FREE";
                challengeBtn.GetComponentInChildren<Text>().text = "CHALLENGE";

            }
            else {
                titleTxt.text = "SELECCIONAR MODO DE JUEGO";
                freeBtn.GetComponentInChildren<Text>().text = "LIBRE";
                challengeBtn.GetComponentInChildren<Text>().text = "DESAFÍO";
            }

        }

        public void OnFreeClick()
        {
			SoundManager.instance.PlayClickSound();
            ModeScreenController.instance.SetMode(0);
           
        }

        public void OnChallengeClick()
        {
			SoundManager.instance.PlayClickSound();
            ModeScreenController.instance.SetMode(1);
          
        }

        public void OnHoverFreeBtn()
        {
            hoverTxt.text = (SettingsController.instance.GetLanguage() == 1) ? "Play each level as many times as you like. Choose the level that applies to what you're working on." :
                "Juega cada nivel las veces que quieras. Elige el nivel que más se adecue a lo que estés trabajando.";
        }

        public void OnHoverChallengeBtn()
        {
            hoverTxt.text = (SettingsController.instance.GetLanguage() == 1) ? "Unlock all the levels. Get all the stars!" :
                "Desbloquea los niveles para poder seguir avanzando. ¡Consigue todas las estrellas!";
        }

        public void OnExitHover()
        {
            hoverTxt.text = "";
        }

        public void OnClickBack()
        {
			SoundManager.instance.PlayClickSound();
            ModeScreenController.instance.GoBack();
        }

        

    }

}
