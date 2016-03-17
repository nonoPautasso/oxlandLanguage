using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.App;

public class MonsterCountController : LevelController {

	public static MonsterCountController instance;
	public MonsterCounterView monsterCounterView;

	private MonsterCounterModel monsterCounterModel;

	private NumberChanger eyes;
	private NumberChanger legs;
	private NumberChanger tooth;
	private NumberChanger arms;

	private Monster currentMonster;


	private MonsterCountRound[] rounds; 

	private int currentChallenge;
	private int totalGoodResults;

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
		monsterCounterView.InitView();


		monsterCounterModel = new MonsterCounterModel();

		eyes = GameObject.Find("Eyes").GetComponent<NumberChanger>();
		eyes.InitNumber();
		legs = GameObject.Find("Legs").GetComponent<NumberChanger>();
		legs.InitNumber();
		tooth = GameObject.Find("Tooth").GetComponent<NumberChanger>();
		tooth.InitNumber();
		arms = GameObject.Find("Arms").GetComponent<NumberChanger>();
		arms.InitNumber();

		rounds = new MonsterCountRound[5];
		rounds[0] = new MonsterCountRound(monsterCounterModel.GetMonster (0));
		rounds[1] = new MonsterCountRound(monsterCounterModel.GetMonster (1));
		rounds[2] = new MonsterCountRound(monsterCounterModel.GetMonster (2));
		rounds[3] = new MonsterCountRound(monsterCounterModel.GetMonster (3));
		rounds[4] = new MonsterCountRound(monsterCounterModel.GetMonster (4));

		currentChallenge = 0;
		totalGoodResults = 0;
		NextChallenge();
	}

	public void PreviousChallenge(){


		rounds[currentChallenge].SetCurrentValues(eyes.CurrentNumber, legs.CurrentNumber, arms.CurrentNumber, tooth.CurrentNumber);

		currentChallenge = currentChallenge - 1 < 0 ? 4 : currentChallenge-1;

		monsterCounterView.ShowImageMonster(rounds[currentChallenge].Monster.MonsterImage);

		EnableButtons(!rounds[currentChallenge].RoundComplete);

		eyes.CurrentNumber = rounds[currentChallenge].GetCurrentEyes();
		legs.CurrentNumber = rounds[currentChallenge].GetCurrentLegs();
		arms.CurrentNumber = rounds[currentChallenge].GetCurrentArms();
		tooth.CurrentNumber = rounds[currentChallenge].GetCurrentTooth();

		eyes.UpdateImage();
		eyes.ShowWrongBorder (true);
		legs.UpdateImage();
		legs.ShowWrongBorder (true);
		arms.UpdateImage();
		arms.ShowWrongBorder (true);
		tooth.UpdateImage();
		tooth.ShowWrongBorder (true);


		if(rounds[currentChallenge].Monster.HintShown){
			monsterCounterView.DisableHint();

		}else{
			monsterCounterView.EnableHint();

		}
	}

	public override void NextChallenge ()
	{
		rounds[currentChallenge].SetCurrentValues(eyes.CurrentNumber, legs.CurrentNumber, arms.CurrentNumber, tooth.CurrentNumber);

		currentChallenge = (currentChallenge + 1) % 5;

		monsterCounterView.ShowImageMonster(rounds[currentChallenge].Monster.MonsterImage);

		EnableButtons(!rounds[currentChallenge].RoundComplete);

		eyes.CurrentNumber = rounds[currentChallenge].GetCurrentEyes();
		legs.CurrentNumber = rounds[currentChallenge].GetCurrentLegs();
		arms.CurrentNumber = rounds[currentChallenge].GetCurrentArms();
		tooth.CurrentNumber = rounds[currentChallenge].GetCurrentTooth();

		eyes.UpdateImage();
		eyes.ShowWrongBorder (true);
		legs.UpdateImage();
		legs.ShowWrongBorder (true);
		arms.UpdateImage();
		arms.ShowWrongBorder (true);
		tooth.UpdateImage();
		tooth.ShowWrongBorder (true);

		if(rounds[currentChallenge].Monster.HintShown){
			monsterCounterView.DisableHint();

		}else{
			monsterCounterView.EnableHint();
		}

	}



	public void EnableButtons(bool buttonsEnabled){
		eyes.gameObject.GetComponent<Button>().enabled = buttonsEnabled;
		tooth.gameObject.GetComponent<Button>().enabled = buttonsEnabled;
		arms.gameObject.GetComponent<Button>().enabled = buttonsEnabled;
		legs.gameObject.GetComponent<Button>().enabled = buttonsEnabled;
		monsterCounterView.EnableTick(buttonsEnabled);
		if(!buttonsEnabled){
			monsterCounterView.ShowGoodResultStatus(!buttonsEnabled);
		}else{
			monsterCounterView.ShowGoodResultStatus(!buttonsEnabled);
			monsterCounterView.ShowBadResultStatus(!buttonsEnabled);
		}
	}

	public void CheckAnswer(){
		Monster currentChallengeMonster = rounds[currentChallenge].Monster;


		if(currentChallengeMonster.Legs == legs.CurrentNumber && currentChallengeMonster.Eyes == eyes.CurrentNumber &&
			currentChallengeMonster.Arms == arms.CurrentNumber && currentChallengeMonster.Tooth == tooth.CurrentNumber){
			rounds[currentChallenge].RoundComplete = true;
			EnableButtons(!rounds[currentChallenge].RoundComplete);
			LogAnswer (true);
			ShowGoodResult();
		}else{
			SoundManager.instance.PlayFailureSound();
			monsterCounterView.ShowBadResultStatus(true);
			LogAnswer (false);
			eyes.ShowWrongBorder(currentChallengeMonster.Eyes == eyes.CurrentNumber);
			tooth.ShowWrongBorder(currentChallengeMonster.Tooth == tooth.CurrentNumber);
			arms.ShowWrongBorder(currentChallengeMonster.Arms == arms.CurrentNumber);
			legs.ShowWrongBorder(currentChallengeMonster.Legs == legs.CurrentNumber);
		}

	}

	public void ShowGoodResult(){		
		SoundManager.instance.PlayRightAnswerSound();
		monsterCounterView.ShowGoodResultStatus(true);
		totalGoodResults++;

		if(totalGoodResults == 5){                
			EndGame (monsterCounterModel.MinSeconds,monsterCounterModel.PointsPerSecond,monsterCounterModel.PointsPerError);  
		}

	}




	public override void ShowHint ()
	{
		LogHint ();
		rounds[currentChallenge].Monster.HintShown = true;
		monsterCounterView.ShowImageMonster(rounds[currentChallenge].Monster.MonsterImage);
		monsterCounterView.DisableHint();
	}



}
