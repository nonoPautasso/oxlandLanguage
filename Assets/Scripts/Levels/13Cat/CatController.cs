using UnityEngine;
using System.Collections;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels
{
public class CatController : LevelController {
	
	public static CatController instance;
	private CatModel model;
	public CatView view;

	private const int CAT_GAME_ONE = 13;
	private const int CAT_GAME_TWO = 14;
	private const int CAT_GAME_THREE = 15;
	private const int CAT_GAME_FOUR = 16;
	
	private int streak;
	private int gameCounter;

		public int GameCounter {
			get {
				return gameCounter;
			}
		}

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		InitGame();
	}

	

	public override void InitGame()
	{
		MetricsManager.instance.GameStart();
		gameCounter = 0;	
		streak = 0;
			switch (AppController.instance.GetCurrentLevel())
		{
			case CAT_GAME_ONE:
				model = new CatModel (0);
				break;
			case CAT_GAME_TWO:
				model = new CatModel(1);
				break;
			case CAT_GAME_THREE:
				model = new CatModel(2);
				break; 
			case CAT_GAME_FOUR:
				model = new CatModel(3);
				break; 
		}
		view.InitGame ();
		NextChallenge();
	}
	public override void NextChallenge()
		{	
			if (gameCounter > 9&&streak>3) {
				EndGame (model.MinSeconds,model.PointsPerSecond,model.PointsPerError);  
			} else {
				gameCounter++;	
				model.NextChallenge ();
				view.NextChallenge ();
			}
	}
			
	public override void ShowHint()
	{
		LogHint();
		model.RequestHint();

	}

	public void CheckAnswer(int answer){
			if (model.IsCorrectAnswer (answer)) {
				LogAnswer (true);
				view.ShowCorrectAnswer ();
			} else {
				LogAnswer (false);
				view.ShowWrongAnswer ();
				streak = 0;
			}
	}

	public int GetCurrentGame(){
		return model.CurrentGame;
	}

	public int[] GetBillNumbers(){
		return model.BillNumbers;
	}

	public int GetCandyPrice(){
		return model.CandyPrice;
	}

	public int GetAlreadyPaid(){
			return model.AlreadyPaid;
	}

	 internal void OnRightAnswerAnimationEnd()
		{
			streak++;
			view.OnRightAnswerAnimationEnd ();
			NextChallenge ();
		}

		internal void OnWrongAnswerAnimationEnd()
		{
			streak = 0;
			view.OnWrongAnswerAnimationEnd();
		}



}
}
