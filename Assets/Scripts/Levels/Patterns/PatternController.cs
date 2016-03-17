using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts.App;

public class PatternController : LevelController {

	public static PatternController instance;

	private PatternModel patternModel;
	public PatternView patternView;

	private Figure[] currentFigures;
	private Figure[] currentAnswers;

	private int currentLanguage;

	private Stage currentStage;

	public enum Stage{sameSize,sameColor,sameForm};


	// Use this for initialization



	private bool showingHints;

	private int[] stagesOrder;
	private int stageCounter;

	public void Start(){
		if (instance == null)           
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		MetricsManager.instance.GameStart ();
		InitGame ();
	}

	public override void InitGame() {
		
		patternModel = new PatternModel();

		currentFigures = new Figure[9];
		currentAnswers = new Figure[6];



		stageCounter = 0;
		stagesOrder = new int[]{0,1,2};
		ShuffleArray(stagesOrder);

		currentLanguage = SettingsController.instance.GetLanguage ();

		NextChallenge();

	}
	

	public override void NextChallenge ()
	{
		showingHints = false;
		GetNextStage();
		EmptyAnswers();
		showingHints = false;
		ShowRandomFigures();
		patternModel.changeCurrentStage(currentStage);

		patternView.EnableHint ();
		switch(currentStage){
		case Stage.sameSize:
			switch (currentLanguage) {
			case 0:
				patternView.ChangeObjectiveName ("MISMO TAMAÑO");
				break;
			case 1:
				patternView.ChangeObjectiveName ("SAME SIZE");
				break;

			}
				break;
			case Stage.sameColor:
			switch (currentLanguage) {
			case 0:
				patternView.ChangeObjectiveName("MISMO COLOR");
				break;
			case 1:
				patternView.ChangeObjectiveName ("SAME COLOR");
				break;

			}
				
				break;
			case Stage.sameForm:
			switch (currentLanguage) {
			case 0:
				patternView.ChangeObjectiveName("MISMA FORMA");
				break;
			case 1:
				patternView.ChangeObjectiveName ("SAME SHAPE");
				break;

			}

				
				break;
			default:
				break;
		}
	}



	public void ShowRandomFigures(){
		for(int i = 0;i<9;i++){
			Figure randomFigure = ChooseRandomFigure(i);
			patternView.ShowSprite(i,randomFigure.Sprite);
		}
	}
	

	public void ChooseFigure(int figureIndex){
		int firstEmptyPosition = GetFirstEmptyAnswerPosition();

		if(firstEmptyPosition!=-1){
			patternView.ShowAnswerSprite(firstEmptyPosition, currentFigures[figureIndex].Sprite);

			currentAnswers[firstEmptyPosition] = currentFigures[figureIndex];
			
			Figure randomFigure = ChooseRandomFigure(figureIndex);
			patternView.ShowSprite(figureIndex,randomFigure.Sprite);
		}

	}

	private Figure ChooseRandomFigure(int index){
		Figure[] figures = patternModel.Figures;
		Figure[] hintFigures = patternModel.HintFigures;

		Figure newRandomFigure = showingHints ? hintFigures[Random.Range(0,hintFigures.Length)] : figures[Random.Range(0,figures.Length)];
		currentFigures[index] = newRandomFigure;
		return newRandomFigure;
	}

	public void RemoveFigure(int figureIndex){
		patternView.RemoveAnswerSprite(figureIndex);
		currentAnswers[figureIndex] = null;
	}

	public void CheckAnswer(){
		if(GetFirstEmptyAnswerPosition()==-1){
			bool correctAnswer = true;
			switch(currentStage){
			case Stage.sameColor:
				for(int i=0;i<6-1;i++){
					correctAnswer = currentAnswers[i].ColorF == currentAnswers[i+1].ColorF;
					if (!correctAnswer)
						break;
				}

				break;
			case Stage.sameForm:
				for(int i=0;i<6-1;i++){
					correctAnswer = currentAnswers[i].Form == currentAnswers[i+1].Form;
					if (!correctAnswer)
						break;
				}

				break;
			case Stage.sameSize:
				for(int i=0;i<6-1;i++){
					correctAnswer = currentAnswers[i].Size == currentAnswers[i+1].Size;
					if (!correctAnswer)
						break;
				}

				break;
			}
			CheckIfCorrect(correctAnswer);

		}else{
			SoundManager.instance.PlayFailureSound();
			LogAnswer(false);

		}
	}


	private void CheckIfCorrect(bool correctAnswer){
		if (correctAnswer) {
			SoundManager.instance.PlayRightAnswerSound ();
			LogAnswer (true);
			NextChallenge ();
		} else {
			SoundManager.instance.PlayFailureSound();
			LogAnswer (false);
		}
	}

	private int GetFirstEmptyAnswerPosition ()
	{
		for(int i=0;i<currentAnswers.Length;i++){
			if(currentAnswers[i]==null){
				return i;
			}
		}
		return -1;
	}

	private void EmptyAnswers ()
	{

		for(int i=0;i<currentAnswers.Length;i++){
			currentAnswers[i]=null;
			patternView.DisableAllAnswers();
		}
	}

	public override void ShowHint(){
		patternView.DisableHint ();
		showingHints = true;
		patternModel.RequestHint();
		ShowRandomFigures();
	}

	private void GetNextStage ()
	{
		if(stageCounter < stagesOrder.Length){
			currentStage = ((Stage)stagesOrder[stageCounter]);
			stageCounter++;
		}else{
			EndGame (patternModel.MinSeconds,patternModel.PointsPerSecond,patternModel.PointsPerError);  
		}

	}

	public void ShuffleArray<T>(T[] arr) {
		for (int i = arr.Length - 1; i > 0; i--) {
			int r = Random.Range(0, i);
			T tmp = arr[i];
			arr[i] = arr[r];
			arr[r] = tmp;
		}
	}






}
