using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.Levels;
using Assets.Scripts.App;
using System;

namespace Assets.Scripts.Levels.Castlenumbers6
{

    public class CastleView : LevelView
    {
        private Color ORANGE = new Color32(255, 123, 63, 255);
        private Color EMPTY = new Color32(0, 0, 0, 0);
        private Color UNFOCUSED = new Color32(218, 190, 163, 255);
        private Color GOLD = new Color32(187, 176, 223, 255);
        private Color Disabled = new Color32(180, 132, 61, 112);

        public GameObject mainPanel;

        public Text title;
        public Button soundBtn;
        public Button ticBtn;

        public List<Button> gridButtons;
        public GameObject lateralNumberButtonsPanel;

        public List<Button> lateralNumbers;

        private List<int> currentInputFields;
        private List<int> disabledLateralNumbers;
        private int focus;

        private int referenceNumber;
        private int audioIndex;
        private int firstNumberReference;
        public List<AudioClip> spanishAudioTexts;
        public List<AudioClip> englishAudioTexts;

        public UnityEngine.EventSystems.EventSystem myEventSystem;

        void Start()
        {
            lateralNumberButtonsPanel.SetActive(false);
        
            switch (CastleController.instance.GetCurrentGame())
            {
                case 0:
                    lateralNumberButtonsPanel.SetActive(false);
                    soundBtn.gameObject.SetActive(true);
                    title.text = SettingsController.instance.GetLanguage() == 0 ? "¿DÓNDE ESTOY?" : "WHERE AM I?";
                    ticBtn.gameObject.SetActive(false);
                    break;
                case 1:
                    lateralNumberButtonsPanel.SetActive(false);
                    soundBtn.gameObject.SetActive(true);
                    ticBtn.gameObject.SetActive(false);
                    break;
                case 2:
                    lateralNumberButtonsPanel.SetActive(false);
                    soundBtn.gameObject.SetActive(false);
                    ticBtn.gameObject.SetActive(false);
                    break;
                case 3:
                    title.text = SettingsController.instance.GetLanguage() == 0 ? "COMPLETA LOS NÚMEROS FALTANTES" : "FILL THE GRID WITH THE MISSING NUMBERS";
                    lateralNumberButtonsPanel.SetActive(true);
                    soundBtn.gameObject.SetActive(false);
                    ticBtn.gameObject.SetActive(true);
                    break;
                case 4:
                    title.text = SettingsController.instance.GetLanguage() == 0 ? "COMPLETA LOS NÚMEROS FALTANTES" : "FILL THE GRID WITH THE MISSING NUMBERS";
                    lateralNumberButtonsPanel.SetActive(true);
                    soundBtn.gameObject.SetActive(false);
                    ticBtn.gameObject.SetActive(true);
                    break;
                case 5:
                    title.text = SettingsController.instance.GetLanguage() == 0 ? "ENCUENTRA EL INTRUSO EN LA GRILLA" : "FIND THE INTRUDER IN THE GRID";
                    lateralNumberButtonsPanel.SetActive(false);
                    soundBtn.gameObject.SetActive(false);
                    ticBtn.gameObject.SetActive(true);
                    break;
            }            
        }        

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) { OnClickMenuBtn(); }
            else if(CastleController.instance.GetCurrentGame() != 3 && CastleController.instance.GetCurrentGame() != 4) { }
            else if (Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Tab)) { NextFocus(); }
            else if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0)) { OnClickLateralNumber(0); }
            else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) { OnClickLateralNumber(1); }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) { OnClickLateralNumber(2); }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) { OnClickLateralNumber(3); }
            else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) { OnClickLateralNumber(4); }
            else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) { OnClickLateralNumber(5); }
            else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)) { OnClickLateralNumber(6); }
            else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7)) { OnClickLateralNumber(7); }
            else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8)) { OnClickLateralNumber(8); }
            else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9)) { OnClickLateralNumber(9); }
            else if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Delete)) { ClearText(); }

        }

        internal void NextChallenge(int currentNumber, List<int> currentSolutions, List<int> currentErrors)
        {
            for (int i = 0; i < 17; i++){
                Unpaint(gridButtons[currentNumber + i]);
                InteractableButton(gridButtons[currentNumber + i], currentNumber + i);
            }

            DoModifications(currentSolutions, currentErrors);           
            if (currentInputFields == null) currentInputFields = new List<int>();
            currentInputFields.RemoveAll(e => true);
            for (int i = 0; i < 17; i++) {currentInputFields.Add(currentNumber + i); }
            for (int i = 0; i < currentNumber; i++){HideButton(gridButtons[i]);}
            for (int i = currentNumber + 17; i < 100; i++) {HideButton(gridButtons[i]);}        
            EnableHint();
            UpdateTicBtn();
        }

        private void DoModifications(List<int> currentSolutions, List<int> currentErrors)
        {
            for (int i = 0; i < currentSolutions.Count; i++) {
                gridButtons[currentSolutions[i]].GetComponentInChildren<Text>().text = "" + currentErrors[i];
            }
        }

        private void NextFocus()
        {            
            int index = currentInputFields.IndexOf(focus);
            if (index == currentInputFields.Count - 1) { focus = currentInputFields[0]; }
            else { focus = currentInputFields[index + 1]; }
            SetInputFocus(false);              
        }

        private void ClearText()
        {
            DeselectAll();
            gridButtons[focus].GetComponentInChildren<Text>().text = "";
            UpdateLateralNumbers();
            UpdateTicBtn();
        }

        public override void ShowHint()
        {
            DeselectAll();
            CastleController.instance.ShowHint();
            DisableHint();
        }

        public override void EndGame()
        {
            DisableAllButons();
        }

        internal void NextChallenge(int number)
        {
            hintBtn.enabled = false;
            PlayNumberSound(number);
            EnableAllButtons();
            Invoke("EnableHint", 0.5f);
        }

        private void EnableAllButtons(){
            for (int i = 0; i < gridButtons.Count; i++) {
                EnableNumberButton(i);
            }      
            soundBtn.enabled = soundBtn.IsActive();
            
        }    

        private void DeselectAll() {
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(mainPanel);
        }

        public void OnClicNumber(int number){           
            PlaySoundClic();
            DeselectAll();
            switch (CastleController.instance.GetCurrentGame())
            {
                case 3:
                    focus = number;
                    SetInputFocus(true);
                    break;
                case 4:
                    focus = number;
                    SetInputFocus(true);
                    break;
                case 5:
                    if (IsPainted(gridButtons[number])) { Unpaint(gridButtons[number]); }
                    else { PaintButton(gridButtons[number]); }
                    UpdateTicBtn();
                    break;
                default:
                    DisableAllButons();
                    hintBtn.enabled = false;
                    if (CastleController.instance.CheckAnswer(number)) { CorrectAnswer(); }
                    else { WrongAnswer(); }
                    break;
            }           
        }

        private bool IsPainted(Button button)
        {
            return button.image.color != EMPTY;
        }

        private void Unpaint(Button button)
        {
            button.image.color = EMPTY;
        }

        private void PaintButton(Button button)
        {
            button.image.color = ORANGE;
        }

        public void OnClickLateralNumber(int number){          
            DeselectAll();
            if (gridButtons[focus].GetComponentInChildren<Text>().text.Length < 2)
            {
                PlaySoundClic();
                gridButtons[focus].GetComponentInChildren<Text>().text += number;
                UpdateLateralNumbers();
                UpdateTicBtn();
            }        
        }

        private void UpdateTicBtn()
        {
            if(CastleController.instance.GetCurrentGame() == 5)
            {
                ticBtn.interactable = AtLeastOnePainted();
            } else
            {
                ticBtn.interactable = allFieldsCompleted();
            }
            
        }

        private bool AtLeastOnePainted()
        {
            for (int i = 0; i < currentInputFields.Count; i++)
            {
                if (IsPainted(gridButtons[currentInputFields[i]])) { return true; }
            }

            return false;
        }

        private bool allFieldsCompleted()
        {
            for (int i = 0; i < currentInputFields.Count; i++)
            {
                if (gridButtons[currentInputFields[i]].GetComponentInChildren<Text>().text == "") return false;
            }

            return true;
        }

        internal void ShowButtonsAsOk(List<int> hintNumbers)
        {
            for (int i = 0; i < hintNumbers.Count; i++)
            {
                PaintButton(gridButtons[hintNumbers[i]], Disabled);
                gridButtons[hintNumbers[i]].GetComponentInChildren<Text>().text = "" + hintNumbers[i];
                gridButtons[hintNumbers[i]].interactable = false;
                gridButtons[hintNumbers[i]].enabled = false;
            }
        }

        private void PaintButton(Button button, Color color){
            button.image.color = color;
        }

        internal void EnableButtonsWithHint(){
            if(CastleController.instance.GetCurrentGame() >= 3) // dejar solo en 4 si el lvl 3 no funciona
            {
                for (int i = 0; i < currentInputFields.Count; i++)
                {
                    gridButtons[currentInputFields[i]].enabled = true;
                    gridButtons[currentInputFields[i]].interactable = true;
                }
                hintBtn.enabled = hintBtn.image.color.a == 1;
            }
            else
            {
                for (int i = 0; i < gridButtons.Count; i++)
                {
                    gridButtons[i].enabled = gridButtons[i].image.color.a == 1;
                }
                hintBtn.enabled = hintBtn.image.color.a == 1;
                soundBtn.enabled = true;
            }
            
        }

        internal void NextChallenge(string v)
        {
            title.text = v;
            EnableHint();
            EnableAllButtons();       
        }

        internal void NextChallenge(string v, int audioIndex, int numberWithQuestion, int currentNumber)
        {
            DisableAllButons();
            for (int i = 0; i < gridButtons.Count; i++)
            {
                PaintButton(gridButtons[i]);
            }
            title.text = v;
            PlayTextAsSound(numberWithQuestion, audioIndex, currentNumber);     
        }

        private void PlayTextAsSound(int numberWithQuestion, int audioIndex, int currentNumber)
        {
            firstNumberReference = numberWithQuestion;
            referenceNumber = currentNumber;
            this.audioIndex = audioIndex;
            SayQuestion();
        }

        private void SayQuestion()
        {
            Debug.Log("first numner: " + firstNumberReference);
            soundBtn.enabled = false;
            if (audioIndex != 2 && audioIndex != 3){
                PlayNumberSound(firstNumberReference);
                Invoke("PlayText", 1f);
            } else { PlayText(); }
            
        }

        private void PlayText()
        {
            soundBtn.enabled = false;
            Debug.Log("text: " + audioIndex);
            SpeakerScript.instance.PlaySound(2);
            SoundManager.instance.PlayClip(SettingsController.instance.GetLanguage() == 0 ? spanishAudioTexts[audioIndex] : englishAudioTexts[audioIndex]);
            Invoke("DisableSoundBtn", 0.4f);
            Invoke("PlayCurrentNumber", 1.4f);
        }

        private void DisableSoundBtn()
        {
            soundBtn.enabled = false;
        }

        private void PlayCurrentNumber()
        {
            soundBtn.enabled = false;
            Debug.Log("Sec numner: " + referenceNumber);
            PlayNumberSound(referenceNumber);
            Invoke("EnableAllBtnsAndHintBtn", 1f);
            
        }

        private void EnableAllBtnsAndHintBtn()
        {
            EnableHint();
            EnableAllButtons();
        }

        public void OnClicSoundBtn()
        {
            DeselectAll();
            if (CastleController.instance.GetCurrentGame() == 1) { DisableAllButons(); SayQuestion(); }
            else PlayNumberSound(CastleController.instance.GetCurrentNumber());
        }

        internal void DisableNumbers(List<int> disabledRows)
        {
            for(int i = 0; i < disabledRows.Count; i++)
            {
                for(int j = disabledRows[i] * 10; j < (disabledRows[i] * 10 + 10); j++) {
                    DisableNumberButton(j);
                }
            }
        }

        internal void DisableLateralNumbers(List<int> disabledLateralNumbers)
        {
            this.disabledLateralNumbers = disabledLateralNumbers;
            for (int i = 0; i < disabledLateralNumbers.Count; i++)
            {
                lateralNumbers[disabledLateralNumbers[i]].interactable = false;
            }
        }      

        private void WrongAnswer()
        {
            AnswerAnimation.instance.PlayAnimation(false);
        }

        internal void NextChallenge(int currentRow, List<int> list)
        {
            if(CastleController.instance.GetCurrentGame() != 5)
            {
                focus = -1;
                list.Sort();
                if (disabledLateralNumbers == null) disabledLateralNumbers = new List<int>(10);
                disabledLateralNumbers.RemoveAll(e => true);
                for (int i = 0; i < gridButtons.Count; i++)
                {
                    bool isActive = gridButtonIsActive(i, currentRow, CastleController.instance.GetCurrentGame());
                    if (isActive) { gridButtons[i].gameObject.SetActive(isActive); }
                    else { gridButtons[i].GetComponentInChildren<Text>().text = ""; }
                    Color color = gridButtons[i].image.color;
                    color.a = 0;
                    gridButtons[i].image.color = color;
                    gridButtons[i].interactable = false;
                    gridButtons[i].enabled = false;
                }
                if (CastleController.instance.GetCurrentGame() == 3)
                {
                    for (int i = currentRow * 10; i < (currentRow + 2) * 10; i++)
                    {
                        bool isSolution = list.Contains(i);
                        gridButtons[i].GetComponentInChildren<Text>().text = isSolution ? "" : "" + i;
                        gridButtons[i].interactable = isSolution;
                        gridButtons[i].enabled = isSolution;
                        if (isSolution)
                        {
                            if (focus == -1) focus = i;
                            gridButtons[i].image.color = new Color(0.8f, 0.49f, 0);
                        }
                    }
                }
                else if (CastleController.instance.GetCurrentGame() == 4)
                { // current row is current number in this case
                    for (int i = 0; i < list.Count; i++)
                    {
                        gridButtons[list[i]].GetComponentInChildren<Text>().text = "";
                        gridButtons[list[i]].interactable = true;
                        gridButtons[list[i]].enabled = true;
                        if (focus == -1) focus = list[i];
                        gridButtons[list[i]].image.color = UNFOCUSED;
                    }
                    gridButtons[currentRow].interactable = false;
                    gridButtons[currentRow].enabled = false;
                    gridButtons[currentRow].image.color = GOLD;
                }

                currentInputFields = list;
                SetInputFocus(true);
                ticBtn.interactable = false;
            } 
            EnableHint();
        }

        private void HideButton(Button button)
        {
            button.image.color = EMPTY;
            button.GetComponentInChildren<Text>().text = "";
            button.enabled = false;
            button.interactable = false;
        }

        private void InteractableButton(Button button, int number)
        {
            button.GetComponentInChildren<Text>().text = "" + number;
            button.interactable = true;
            button.enabled = true;
        }

        private bool gridButtonIsActive(int i, int currentRow, int level){
            if (level == 3) return (currentRow * 10) <= i && i < ((currentRow + 2) * 10);
            else if (level == 4) return true;
            else{
                Debug.Log("ERROR EN GRID BUTTON IS ACTIVE");
                return true;
            }
        }

        private void SetInputFocus(bool clear)
        {
            for (int i = 0; i < currentInputFields.Count; i++)
            {
                if(currentInputFields[i] == focus) {
                    gridButtons[currentInputFields[i]].image.color = new Color(1, 0.6f, 0);
                    if(clear) gridButtons[currentInputFields[i]].GetComponentInChildren<Text>().text = "";
                } else{
                    gridButtons[currentInputFields[i]].image.color = new Color(0.8f, 0.49f, 0);
                }
            }

            UpdateLateralNumbers();
            UpdateTicBtn();
        }

        private void UpdateLateralNumbers()
        {
            if (gridButtons[focus].GetComponentInChildren<Text>().text.Length == 2) {DisableLateralNumbers();}
            else{ EnableLateralNumbers(); }
            DisableLateralNumbers(disabledLateralNumbers);
        }

        private void EnableLateralNumbers()
        {
            for (int i = 0; i < lateralNumbers.Count; i++){
                lateralNumbers[i].interactable = !disabledLateralNumbers.Contains(i);
            }
        }

        private void DisableLateralNumbers()
        {
            for (int i = 0; i < lateralNumbers.Count; i++)
            {
                lateralNumbers[i].interactable = false;
            }
        }

        internal void OnWrongAnswerAnimationEnd()
        {
            EnableButtonsWithHint();
            if (CastleController.instance.GetCurrentGame() >= 3){ ticBtn.interactable = true; }         
        }

        private void CorrectAnswer()
        {
            AnswerAnimation.instance.PlayAnimation(true);
        }

        internal void OnRightAnswerAnimationEnd()
        {
            EnableButtonsWithHint();
            if (CastleController.instance.GetCurrentGame() >= 3) { ticBtn.interactable = true; }          
        }

        private void DisableAllButons(){
            for (int i = 0; i < gridButtons.Count; i++) {
                gridButtons[i].enabled = false;               
            }
            soundBtn.enabled = false;
            hintBtn.enabled = false;     
        }

        private void DisableNumberButton(int i)
        {
            gridButtons[i].enabled = false;            
            gridButtons[i].image.color = Disabled;
        }

        private void EnableNumberButton(int i)
        {
            gridButtons[i].enabled = true;         
            gridButtons[i].image.color = ORANGE;
        }

        public void OnClickTicBtn()
        {
            DeselectAll();
            ticBtn.interactable = false;
            List<int> answers = new List<int>(currentInputFields.Count);
            if(CastleController.instance.GetCurrentGame() == 5)
            {
                for (int i = 0; i < currentInputFields.Count; i++)
                {
                    if (IsPainted(gridButtons[currentInputFields[i]])) { answers.Add(currentInputFields[i]); }
                }
               
            }else
            {
                for (int i = 0; i < currentInputFields.Count; i++)
                {
                    string text = gridButtons[currentInputFields[i]].GetComponentInChildren<Text>().text;
                    int currentAnswer = text == "" ? 0 : int.Parse(text);
                    answers.Add(currentAnswer);
                }
            }
           
            DisableAllButons();
            hintBtn.enabled = false;
            if (CastleController.instance.CheckAnswer(answers)) { CorrectAnswer(); }
            else { WrongAnswer(); }
            
        }

        private void PlayNumberSound(int number)
        {         
			AudioClip numberAudio; 
			soundBtn.enabled = false;
            SpeakerScript.instance.PlaySound(number < 10 ? 1 : 2);
            if (SettingsController.instance.GetLanguage() == 0)
            {            
				numberAudio = (AudioClip) Resources.Load ("Castle/Castellano/" + number);
				SoundManager.instance.PlayClip(numberAudio);
            } else
            {
				numberAudio = (AudioClip) Resources.Load ("Castle/English/" + number+"e");
                SoundManager.instance.PlayClip(numberAudio);
            }
            Invoke("EnabledSoundBtn", 1.3f);
        }

        private void EnabledSoundBtn()
        {
            soundBtn.enabled = true;
        }
    }
}