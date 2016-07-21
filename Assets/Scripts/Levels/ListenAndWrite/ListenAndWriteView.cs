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
		private InputField inputText;
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
			// Sets the MyValidate method to invoke after the input field's default input validation invoke (default validation happens every time a character is entered into the text field.)
			inputText.onValidateInput += delegate(string input, int charIndex, char addedChar) { return MyValidate( addedChar ); };

        }


		private char MyValidate(char charToValidate)
		{
			
			return charToValidate.ToString().ToUpper().ToCharArray()[0];
		}



        internal void NextChallenge()
        {	
            ticBtn.interactable = false;
			deleteButton.interactable = false;
            inputText.text = "";

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
			SoundManager.instance.PlayClickSound ();
          
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
			SoundManager.instance.PlayClickSound ();
            ListenAndWriteController.GetController().CheckAnswer(inputText.text);
        }

        public void OnClickLetter(string letter)
        {
			SoundManager.instance.PlayClickSound ();
            if (tildeOn && (letter[0] == 'a' || letter[0] == 'e' || letter[0] == 'i' || letter[0] == 'o' || letter[0] == 'u') )
            {
                switch (letter[0])
                {
                    case 'a':
                        inputText.text += "Á" ;
                        break;
                    case 'e':
                        inputText.text += "É" ;
                        break;
                    case 'i':
                        inputText.text +=  "Í";
                        break;
                    case 'o':
                        inputText.text +=  "Ó" ;
                        break;
                    case 'u':
                        inputText.text +=  "Ú" ;
                        break;
                }
            } else if (diaresisOn && letter[0] == 'u')
            {
                inputText.text +=  "Ü";
            }
            else
            {
                inputText.text += letter.ToUpper();
            }
            tildeOn = false;
            diaresisOn = false;
        }

        public void OnClickTilde()
        {
			SoundManager.instance.PlayClickSound ();
            tildeOn = true;
            diaresisOn = false;
        }

        public void OnClickDiaresis()
        {
			SoundManager.instance.PlayClickSound ();
            diaresisOn = true;
            tildeOn = false;
        }

        public void OnClickShift()
        {
			SoundManager.instance.PlayClickSound ();
            UpdateLettersOnKeyboard();
        }

		public void OnClickDelete()
		{
			SoundManager.instance.PlayClickSound ();
			string newString = inputText.text.Substring (0, inputText.text.Length - 1);
			inputText.text = newString;
		}

		public void UpdateLowerCaseText(){
//			input.text = input.text.ToUpper ();
			string newText =  inputText.text.ToUpper ();
			inputText.text = newText;
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
			SpeakerScript.instance.PlaySound (2);
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
            ticBtn.interactable = inputText.text.Length > 0;
			deleteButton.interactable = inputText.text.Length > 0;

        }

		public override void EndGame ()
		{
			throw new NotImplementedException ();
		}

       
    }
}