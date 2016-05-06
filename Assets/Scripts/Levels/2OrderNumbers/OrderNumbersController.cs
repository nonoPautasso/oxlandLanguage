using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels
{
public class OrderNumbersController : LevelController {

public static OrderNumbersController instance;
public OrderNumbersView view;
public OrderNumbersModel model;


public void Start(){
	if (instance == null)           
		instance = this;
	else if (instance != this)
		Destroy(gameObject);

	MetricsManager.instance.GameStart ();
	InitGame ();
}

public override void InitGame ()
{
	model = new OrderNumbersModel ();	
	NextChallenge ();
}


		
public override void NextChallenge(){
	view.NextChallenge ();
	
if (model.Ascending) {
	
	if (model.CurrentNumber < 10)
			model.NextChallenge ();
	else 
		ChangeMode ();

} else {
		model.NextChallenge ();
	if (model.CurrentNumber == 0)
		EndGame (model.MinSeconds,model.PointsPerSecond,model.PointsPerError);
}

Debug.Log (model.CurrentNumber);
}

void ChangeMode(){
	model.ChangeMode ();	
	view.ChangeMode ();
}

public int GetSlotIndex(){
	return model.SlotIndex;
}

public List<int> GetHintIndexes(){
	return model.HintIndexes;
}
		
public override void ShowHint(){
	LogHint ();
	model.RequestHint ();
//	view.ShowHint ();
}

public void CheckNumber(int number){
if (model.NumberIsCorrect(number)) {
	LogAnswer (true);
	view.ShowRightAnswer (number);
	NextChallenge ();	
} else {
	LogAnswer (false);	
	view.ShowWrongAnswer (number);
}
		
}

public bool IsAscending(){
	return model.Ascending;
}

}
}
