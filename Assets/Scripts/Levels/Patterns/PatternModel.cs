using UnityEngine;
using Assets.Scripts.App;
using System.Collections;

public class PatternModel : LevelModel {

	private Figure[] figures;


	public enum FigureForm{circle,square,triangle,rombus};
	public enum FigureColor{yellow,red,blue,violet};
	public enum FigureSize{small, medium,large};

	public Figure[] hintFigures;

	public bool showingHints;

	public PatternController.Stage currentStage;


	public PatternModel(){
		minSeconds = 35;
		pointsPerError = 1000;
		pointsPerSecond = 100;


		Object[] figureImages = Resources.LoadAll("Patterns/figures");

		showingHints = false;

		figures = new Figure[figureImages.Length-1];
		int figureCounterCreator = 0;

		foreach(FigureForm currentForm in System.Enum.GetValues(typeof(FigureForm))){
			foreach(FigureSize currentSize in System.Enum.GetValues(typeof(FigureSize))){
				foreach(FigureColor currentColor in System.Enum.GetValues(typeof(FigureColor))){
					figures[figureCounterCreator] = new Figure(currentForm,currentColor,currentSize, 
						(Sprite)figureImages[figureCounterCreator+1]);
					figureCounterCreator++;
				}	
			}
		}
	}

	public void changeCurrentStage(PatternController.Stage currentStage){
		this.currentStage = currentStage;

	}

	#region implemented abstract members of LevelModel
	public override void StartGame ()
	{
		throw new System.NotImplementedException ();
	}
	public override void NextChallenge ()
	{
		throw new System.NotImplementedException ();
	}

	public override void RequestHint ()
	{
		showingHints = true;
		int hintFigureCounter = 0;
		int randomSize = Random.Range (0,3);
		int randomForm = Random.Range (0,4);
		int randomColor = Random.Range (0,4);
		switch(currentStage){
		case PatternController.Stage.sameColor:
			hintFigures = new Figure[4];
			for(int i=0;i<figures.Length;i++){
				if(figures[i].Size == (PatternModel.FigureSize)randomSize && figures[i].Form == (PatternModel.FigureForm) randomForm){
					hintFigures[hintFigureCounter] = figures[i];
					hintFigureCounter++;
				}
			}
			break;
		case PatternController.Stage.sameForm:
			hintFigures = new Figure[4];
			for(int i=0;i<figures.Length;i++){
				if(figures[i].Size == (PatternModel.FigureSize)randomSize && figures[i].ColorF == (PatternModel.FigureColor) randomColor){
					hintFigures[hintFigureCounter] = figures[i];
					hintFigureCounter++;
				}
			}
			break;
		case PatternController.Stage.sameSize:
			hintFigures = new Figure[3];
			for(int i=0;i<figures.Length;i++){
				if(figures[i].ColorF == (PatternModel.FigureColor)randomColor && figures[i].Form == (PatternModel.FigureForm) randomForm){
					hintFigures[hintFigureCounter] = figures[i];
					hintFigureCounter++;
				}
			}
			break;
		}
	}
	#endregion


	public Figure[] Figures {
		get {
			return figures;
		}
	}

	public Figure[] HintFigures {
		get {
			return hintFigures;
		}
	}


    private void AsignMetricsData(int currentGame){
        minSeconds = 35;
        pointsPerError = 1000;
        pointsPerSecond = 100;
    }

}
