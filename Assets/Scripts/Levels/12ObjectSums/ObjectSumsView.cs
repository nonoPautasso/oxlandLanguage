using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.App;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
namespace Assets.Scripts.Levels.ObjectSums
{
    public class ObjectSumsView : LevelView
    {
        public Button nextBtn;
        public Button refreshBtn;
        public List<Button> numberBtns;
        public Button ticBtn;
        public Button clearAnswerBtn;
        public Text firstNumberTxt;
        public Text secondNumberTxt;
        public Text answerTxt;
        public List<Image> currentImages;
        public List<Sprite> objectSprites;
        private List<int> spriteIndexes;  // Indexes of sprites to select one and remove its index without removing it.
        public List<Image> hintLeftImages;
        public List<Image> hintRightImages;

		private bool answerSubmitted;
		private bool isCorrectAnswer;

		public EventSystem myEventSystem;

		void Start(){
			answerSubmitted = false;
			isCorrectAnswer = false;
		}

        public override void EndGame()
        {
            throw new NotImplementedException();
        }
        // This method shows and assignes the sprites when the hint button is clicked.
        public override void ShowHint()
        {
            DisableHint();

            for (int i = 0; i < Int32.Parse(firstNumberTxt.text); i++)
            {
                hintLeftImages[i].sprite = currentImages[0].sprite;
                hintLeftImages[i].enabled = true;
            }
            for (int i = 0; i < Int32.Parse(secondNumberTxt.text); i++)
            {
                hintRightImages[i].sprite = currentImages[0].sprite;
                hintRightImages[i].enabled = true;
            }
            ObjectSumsController.instance.ShowHint();
        }
        // This method disables the sprites of the hint in the screen.
        public void HideHint()
        {
            for (int i = 0; i < 10; i++)
            {
                hintLeftImages[i].enabled = false;
                hintRightImages[i].enabled = false;
            }
        }

        // Update is called once per frame
        // This method makes it posible to use the keyboard to play the game, the user can write the numbers with the keyboard and 
        // clear the answer with the backspace. It can also call the menu with the escape or the p (pause) keys.
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
				myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);

				OnClickMenuBtn(); 
			}
			else if (Input.GetKeyDown(KeyCode.Return)) { OnReturnButtonKeyPressed(); }
            else if (refreshBtn.isActiveAndEnabled || nextBtn.isActiveAndEnabled) { }
            else if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0)){ OnClickNumberBtn(0);}
            else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) { OnClickNumberBtn(1); }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) { OnClickNumberBtn(2); }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) { OnClickNumberBtn(3); }
            else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) { OnClickNumberBtn(4); }
            else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) { OnClickNumberBtn(5); }
            else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)) { OnClickNumberBtn(6); }
            else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7)) { OnClickNumberBtn(7); }
            else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8)) { OnClickNumberBtn(8); }
            else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9)) { OnClickNumberBtn(9); }
            else if (Input.GetKeyDown(KeyCode.Backspace)) { OnClickClearBtn(); }

        }

		void OnReturnButtonKeyPressed(){

		

			if(!answerSubmitted){
				OnClickTicBtn();
			}else if(answerSubmitted && !isCorrectAnswer){
				OnClickRefreshBtn();
			}else if(answerSubmitted && isCorrectAnswer){
				OnClickNextBtn();
			}

		}
        // Plays the right sound, enables the nextBtn and disables the hintBtn.
        internal void CorrectAnswer()
        {
			isCorrectAnswer = true;
            PlayRightSound();
            nextBtn.gameObject.SetActive(true);
            DisableHint();
        }
        // Plays the wrong sound and enables the refreshBtn.
        internal void WrongAnswer()
        {
            PlayWrongSound();
            refreshBtn.gameObject.SetActive(true);
        }
        // This method is used every time an answer is correct and the tic button is pressed (except for the last) 
        // and prepares the next challenge
        // Changes the first and second numbers, disables the next and refresh buttons, clears the answer and enables  
        // the numbers, tic and hint buttons. It also selects another object to be added.
        internal void NextChallenge(int numberOne, int numberTwo)
        {
            nextBtn.gameObject.SetActive(false);
            refreshBtn.gameObject.SetActive(false);
            EnableHint();
            firstNumberTxt.text = "" + numberOne;
            secondNumberTxt.text = "" + numberTwo;
            answerTxt.text = "";
            SetGameBtnsEnabled(true);
            HideHint();
            SelectImage();
        }
        // Selects an image from a list of sprites (objectSprites) using a list of integers(spriteIndexes) 
        // to save the sprite list. If the spiteIndexes list is empty, the list is filled.
        private void SelectImage()
        {
            if (spriteIndexes == null || spriteIndexes.Count == 0) { FillSpriteIndexes(); }
            int index = UnityEngine.Random.Range(0, spriteIndexes.Count);
            int currentSpriteIndex = spriteIndexes[index];
            spriteIndexes.RemoveAt(index);
            for (int i = 0; i < currentImages.Count; i++)
            {
                currentImages[i].sprite = objectSprites[currentSpriteIndex];
            }
            
         
        }
        // This metod fills the spriteIndexes list used in the previous method.
        private void FillSpriteIndexes()
        {
            if (spriteIndexes == null) { spriteIndexes = new List<int>(objectSprites.Count); }

            for (int i = 0; i < objectSprites.Count; i++)
            {
                spriteIndexes.Add(i);
            }  
        }
        // This method concatenates the number in the answerTxt, if the amount of characters in the latter is less than 2.
        public void OnClickNumberBtn(int number)
        {
			myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            if (answerTxt.text.Length < 2) {
                PlaySoundClic();
                answerTxt.text += number;
            }
        }
        // Only when the answerTxt is not empty, this metod disables the numbers, tic and clear buttons
        // And calls a method from the controller to check the answer.
        public void OnClickTicBtn()
        {
			myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
			if (answerTxt.text != "") {
				PlaySoundClic();
				SetGameBtnsEnabled(false);
				int answer = answerTxt.text == "" ? 0 : Int32.Parse(answerTxt.text);
				ObjectSumsController.instance.CheckAnswer(answer);
				answerSubmitted = true;
			} 

        }
        // This method disables or enables the numbers, tic and clear buttons.
        private void SetGameBtnsEnabled(bool isEnabled)
        {
            ticBtn.enabled = isEnabled;
            clearAnswerBtn.enabled = isEnabled;
            for (int i = 0; i < numberBtns.Count; i++)
            {
                numberBtns[i].enabled = isEnabled;
            }
        }
        // Clears the answerTxt.
        public void OnClickClearBtn()
        {
			myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            answerTxt.text = "";
        }
        // Asks the controller for the NextChallenge method.
        public void OnClickNextBtn()
        {
			myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            ObjectSumsController.instance.NextChallenge();
			answerSubmitted = false;
			isCorrectAnswer = false;
        }
        // Disables the refresh button, enables the numbers, tic and clear buttons
        // And clears the answer.
        public void OnClickRefreshBtn()
        {
            refreshBtn.gameObject.SetActive(false);
            SetGameBtnsEnabled(true);
            answerTxt.text = "";
			answerSubmitted = false;
        }

    }

}