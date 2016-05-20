using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels.ListenAndWrite
{

    public class ListenAndWriteView : LevelView
    {

        [SerializeField]
        private Text sentence;
        [SerializeField]
        private InputField input;
        [SerializeField]
        private List<Button> keyboardButtons;
        [SerializeField]
       // private Toggle shiftToggle;

        private bool tildeOn;
        private bool diaresisOn;

        [SerializeField]
        private Button soundButton;
        [SerializeField]
        private Button ticBtn;
		[SerializeField]
		private Button deleteButton;
       
       

        private Color highlitedColor = new Color32(242, 255, 21, 255);

        // Use this for initialization
        void Start()
        {

        }

//        internal void SetSentence(string sentence)
//        {
//            this.sentence.text = sentence;
//        }

        internal void NextChallenge()
        {
            ticBtn.interactable = false;
			deleteButton.interactable = false;
            input.text = "";

            hintBtn.interactable = true;
            UnpaintAllKeyboard();
            OnClickSoundBtn();
        }

        private void UnpaintAllKeyboard()
        {
            for (int i = 0; i < keyboardButtons.Count; i++)
            {
                keyboardButtons[i].GetComponent<Image>().color = Color.white;
            }

//            for (int i = 0; i < shiftToggle.GetComponentsInChildren<Image>().Length; i++)
//            {
//                shiftToggle.GetComponentsInChildren<Image>()[i].color = Color.white;
//            }
        }


        public override void ShowHint()
        {
			SoundManager.instance.PlayClicSound ();
          
			ListenAndWriteController.GetController().ShowHint();
            hintBtn.interactable = false;
        }

        public void PaintKeyboardButton(int index)
        {
//            if (index == 27) {
//                for (int i = 0; i < shiftToggle.GetComponentsInChildren<Image>().Length; i++)
//                {
//                    shiftToggle.GetComponentsInChildren<Image>()[i].color = highlitedColor;
//                }
//                
//            }
//            else keyboardButtons[index].GetComponent<Image>().color = highlitedColor;
			keyboardButtons[index].GetComponent<Image>().color = highlitedColor;
        }


        public void OnClickTic()
        {
			SoundManager.instance.PlayClicSound ();
            ListenAndWriteController.GetController().CheckAnswer(input.text);
        }

        public void OnClickLetter(string letter)
        {
			SoundManager.instance.PlayClicSound ();
            if (tildeOn && (letter[0] == 'a' || letter[0] == 'e' || letter[0] == 'i' || letter[0] == 'o' || letter[0] == 'u') )
            {
                switch (letter[0])
                {
                    case 'a':
                        input.text += "Á" ;
                        break;
                    case 'e':
                        input.text += "É" ;
                        break;
                    case 'i':
                        input.text +=  "Í";
                        break;
                    case 'o':
                        input.text +=  "Ó" ;
                        break;
                    case 'u':
                        input.text +=  "Ú" ;
                        break;
                }
            } else if (diaresisOn && letter[0] == 'u')
            {
                input.text +=  "Ü";
            }
            else
            {
                input.text += letter.ToUpper();
            }
            tildeOn = false;
            diaresisOn = false;
        }

        public void OnClickTilde()
        {
			SoundManager.instance.PlayClicSound ();
            tildeOn = true;
            diaresisOn = false;
        }

        public void OnClickDiaresis()
        {
			SoundManager.instance.PlayClicSound ();
            diaresisOn = true;
            tildeOn = false;
        }

        public void OnClickShift()
        {
			SoundManager.instance.PlayClicSound ();
            UpdateLettersOnKeyboard();
        }

		public void OnClickDelete()
		{
			SoundManager.instance.PlayClicSound ();
			string newString = input.text.Substring (0, input.text.Length - 1);
			input.text = newString;
		}

		public void UpdateLowerCaseText(){
			input.text = input.text.ToUpper ();
		}

        private void UpdateLettersOnKeyboard()
        {
            for (int i = 0; i < 27; i++)
            {
                
                 keyboardButtons[i].GetComponentInChildren<Text>().text = keyboardButtons[i].GetComponentInChildren<Text>().text.ToUpper();
               
            }
        }

        public void OnClickSoundBtn()
        {
            ListenAndWriteController.GetController().PlayCurrentWord();
//          soundButton.GetComponent<Animator>().SetBool("play", true);
            soundButton.enabled = false;
            Invoke("EnableSoundButton", 1.5f);
        }

        private void EnableSoundButton()
        {
//            soundButton.GetComponent<Animator>().SetBool("play", false);
            soundButton.enabled = true;
        }

        public void UpdateButtons()
        {
            ticBtn.interactable = input.text.Length > 0;
			deleteButton.interactable = input.text.Length > 0;

        }

		public override void EndGame ()
		{
			throw new NotImplementedException ();
		}

       
    }
}