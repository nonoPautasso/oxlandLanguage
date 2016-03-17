using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts.App;

public class MonsterCreatorController : LevelController {


	public static MonsterCreatorController instance;
	public enum Stage{Eyes, Feet, Arms, Tooth};


	public Image stageIcon;

	public Text eyesText;
	public Text feetText;
	public Text teethText;
	public Text armsText;

	public Button addBtn;
	public Button substractBtn;

	private MonsterLoaderOxLand4 monsterBaseLoader;

	private MonsterCreatorRound[] rounds;

	private Stage currentStage;
	private int currentRound;

	private Object[] stageIcons;


	private int currentValueAnalized;

	private int currentResult;


	public Image[] wrongFilter;

	private Image currentMonsterImage;

	private HintManager hintManager;

	public MonsterCreatorView view;


	private MonsterCreatorModel monsterCreatorModel;

	// Use this for initialization
	void Start () {
		if (instance == null)           
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		MetricsManager.instance.GameStart ();
		InitGame ();
	}


	// Use this for initialization
	public override void InitGame () {
		monsterCreatorModel = new MonsterCreatorModel();

		hintManager = GetComponent<HintManager>();

		monsterBaseLoader = GetComponent<MonsterLoaderOxLand4>();

		stageIcons = Resources.LoadAll ("Monsters/partsSquareBtn");



		rounds = new MonsterCreatorRound[5];

		for(int i=0;i<5;i++){
			MonsterOxLand4 currentMonster = (MonsterOxLand4)monsterBaseLoader.GetMonster(i);
			rounds[i] = new MonsterCreatorRound(currentMonster);


		}
		currentRound = 0;
		StartRound();
	}

	void StartRound(){
		substractBtn.interactable = false;
		view.EnableHint ();
		if(currentMonsterImage!=null){
			currentMonsterImage.gameObject.SetActive(false);
		}

		currentMonsterImage = rounds[currentRound].MonsterOxLand4.BodyPosition;

		currentMonsterImage.gameObject.SetActive(true);

		rounds[currentRound].MonsterOxLand4.EyesPosition.sprite = (Sprite)rounds[currentRound].MonsterOxLand4.EyesImages[0];
		rounds[currentRound].MonsterOxLand4.FeetPosition.sprite = (Sprite)rounds[currentRound].MonsterOxLand4.FeetImages[0];
		rounds[currentRound].MonsterOxLand4.TeethPosition.sprite = (Sprite)rounds[currentRound].MonsterOxLand4.TeethImages[0];
		rounds[currentRound].MonsterOxLand4.ArmsPosition.sprite = (Sprite)rounds[currentRound].MonsterOxLand4.ArmsImages[0];



		eyesText.text = rounds[currentRound].TotalEyes.ToString();
		feetText.text = rounds[currentRound].TotalFeet.ToString();
		teethText.text = rounds[currentRound].TotalTeeth.ToString();
		armsText.text = rounds[currentRound].TotalArms.ToString();


		StartStage (Stage.Eyes);

	}

	void StartStage(Stage stage){
		view.EnableHint ();
		currentValueAnalized = 0;
		currentStage = stage;

		switch(currentStage){
		case Stage.Eyes:
			stageIcon.sprite = (Sprite)stageIcons[1];
			currentResult = rounds[currentRound].TotalEyes;
			break;
		case Stage.Feet:
			stageIcon.sprite = (Sprite)stageIcons[2];
			currentResult = rounds[currentRound].TotalFeet;
			break;
		case Stage.Tooth:
			stageIcon.sprite = (Sprite)stageIcons[4];
			currentResult = rounds[currentRound].TotalTeeth;
			break;
		case Stage.Arms:
			stageIcon.sprite = (Sprite)stageIcons[3];	
			currentResult = rounds[currentRound].TotalArms;
			break;
		default:
			break;
		}

	}

	public void AddComponentToMainImage(){
		currentValueAnalized += 1;

		if (!substractBtn.IsInteractable ())
			substractBtn.interactable = true;

		MonsterOxLand4 currentMonster = rounds[currentRound].MonsterOxLand4;

		switch(currentStage){
		case Stage.Eyes:
			
			rounds [currentRound].MonsterOxLand4.EyesPosition.sprite = 
					(Sprite)currentMonster.EyesImages [currentValueAnalized];
				
			if (currentValueAnalized == currentMonster.Eyes)
				addBtn.interactable = false;

			break;
		case Stage.Feet:
			
			rounds[currentRound].MonsterOxLand4.FeetPosition.sprite = 
				(Sprite)currentMonster.FeetImages[currentValueAnalized];

			if (currentValueAnalized == currentMonster.Legs)
				addBtn.interactable = false;

			break;
		case Stage.Tooth:

			rounds[currentRound].MonsterOxLand4.TeethPosition.sprite = 
					(Sprite)currentMonster.TeethImages[currentValueAnalized];
		
			if (currentValueAnalized == currentMonster.Tooth)
				addBtn.interactable = false;

			break;
		case Stage.Arms:
			
			rounds[currentRound].MonsterOxLand4.ArmsPosition.sprite = 
				(Sprite)currentMonster.ArmsImages[currentValueAnalized];

			if (currentValueAnalized == currentMonster.Arms)
				addBtn.interactable = false;
			
			break;
		default:
			break;
		}
	}

	public void SubstractComponentToMainImage(){

		currentValueAnalized -= 1;

		if (!addBtn.IsInteractable ())
			addBtn.interactable = true;

		if (currentValueAnalized == 0)
			substractBtn.interactable = false;
		
		MonsterOxLand4 currentMonster = rounds[currentRound].MonsterOxLand4;

		switch(currentStage){
		case Stage.Eyes:
			
			currentMonster.EyesPosition.sprite = 
				(Sprite)rounds [currentRound].MonsterOxLand4.EyesImages [currentValueAnalized];

			break;
		case Stage.Feet:
			
			currentMonster.FeetPosition.sprite = 
				(Sprite)rounds[currentRound].MonsterOxLand4.FeetImages[currentValueAnalized];
			break;
		case Stage.Tooth:
			
			if(currentRound == 0){
				rounds[currentRound].MonsterOxLand4.TeethPosition.sprite = 
					(Sprite)currentMonster.TeethImages[currentValueAnalized];
			}else{
				rounds[currentRound].MonsterOxLand4.TeethPosition.sprite = 
					(Sprite)currentMonster.TeethImages[currentValueAnalized];
			}
			break;
		case Stage.Arms:
			
			currentMonster.ArmsPosition.sprite = 
				(Sprite)rounds[currentRound].MonsterOxLand4.ArmsImages[currentValueAnalized];
			break;
		default:
			break;
		}
	}

	public void CheckResult(){
		wrongFilter[(int)currentStage].gameObject.SetActive(false);
		HideHint ();
		if(currentValueAnalized == currentResult){
			SoundManager.instance.PlayRightAnswerSound();
			addBtn.interactable= true;
			substractBtn.interactable= false;

			switch(currentStage){
			case Stage.Eyes:
				StartStage(Stage.Feet);
				break;
			case Stage.Feet:
				StartStage(Stage.Arms);
				break;
			case Stage.Tooth:
				if(currentRound+1 < 5){
					currentRound++;
					LogAnswer (true);
					StartRound();

				}else{
					LogAnswer (true);
					EndGame (monsterCreatorModel.MinSeconds,
						monsterCreatorModel.PointsPerSecond,monsterCreatorModel.PointsPerError);  
				}
				break;
			case Stage.Arms:
				StartStage(Stage.Tooth);
				break;
			default:
				break;
			}
		}else{
			SoundManager.instance.PlayFailureSound();
			LogAnswer (false);
			wrongFilter[(int)currentStage].gameObject.SetActive(true);
		}
	}

	public override void ShowHint(){
		LogHint ();
		hintManager.ShowImage(currentStage,currentResult);
	}

	public void HideHint(){
		hintManager.HideImage();
	}

	public void OnClickMenuBtn(){
		SoundManager.instance.PlayClicSound();
		AppController.instance.ShowInGameMenu();
	}



	#region implemented abstract members of LevelController

	public override void NextChallenge ()
	{
		throw new System.NotImplementedException ();
	}





	#endregion
}
