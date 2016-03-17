using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts.App;

public class PatternView : LevelView {

	private Image[] figurePositions;
	private Image[] answersPositions;


	public Text objective;



	// Use this for initialization
	void Start () {
		answersPositions = new Image[6];
		for(int i=0;i<answersPositions.Length;i++){
			answersPositions[i] = GameObject.Find ("AnswerPosition" + i).GetComponent<Image>();
		}
		DisableAllAnswers();
		figurePositions = new Image[9];
		for(int i=0;i<figurePositions.Length;i++){
			figurePositions[i] = GameObject.Find ("FigurePosition" + i).GetComponent<Image>();
		}
	}
	


	public void ChangeObjectiveName(string newName){
		objective.text = newName;
	}

	public void ShowSprite(int index, Sprite sprite){
		figurePositions[index].sprite = sprite;
	}

	public void ShowAnswerSprite(int index, Sprite sprite){
		answersPositions[index].gameObject.SetActive(true);
		answersPositions[index].sprite = sprite;
	}

	public void RemoveAnswerSprite(int index){
		answersPositions[index].gameObject.SetActive(false);

	}

	public void DisableAllAnswers(){
		if(answersPositions!= null){
			for(int i = 0;i<answersPositions.Length;i++){
				answersPositions[i].gameObject.SetActive(false);
			}
		}

	}


	#region implemented abstract members of LevelView
	public override void ShowHint ()
	{
		PatternController.instance.ShowHint ();
	}
	public override void EndGame ()
	{
		throw new System.NotImplementedException ();
	}
	#endregion
}
