using UnityEngine;
using System.Collections;
using Assets.Scripts.App;

public class MonsterCounterModel : LevelModel {


	private Monster[] blueMonsters;
	private Monster[] orangeMonsters;
	private Monster[] greenMonsters;
	private Monster[] darkGreenMonsters;
	private Monster[] pinkMonsters;

	private Monster[] roundMonster;

	public MonsterCounterModel(){
		minSeconds = 50;
		pointsPerError = 500;
		pointsPerSecond = 100;
		StartGame();
	}


	// Use this for initialization
	public override void StartGame (){
		blueMonsters = new Monster[3];
		orangeMonsters = new Monster[3];
		greenMonsters = new Monster[3];
		darkGreenMonsters = new Monster[3];
		pinkMonsters = new Monster[3];


		Object [] blueMonsterSprite = Resources.LoadAll ("Monsters/Matematica-ActivityMonsters");
		Object [] orangeSprite = Resources.LoadAll ("Monsters/monsterNaranja");
		Object [] greenSprite = Resources.LoadAll ("Monsters/monsterVerde");
		Object [] darkGreenSprite = Resources.LoadAll ("Monsters/monsterVerdeOscuro");
		Object [] pinkSprite = Resources.LoadAll ("Monsters/monsterRosa");


		blueMonsters[0] = new Monster((Sprite)blueMonsterSprite[1],(Sprite)blueMonsterSprite[2],3,7,5,4);
		blueMonsters[1] = new Monster((Sprite)blueMonsterSprite[3],(Sprite)blueMonsterSprite[4],6,8,3,2);
		blueMonsters[2] = new Monster((Sprite)blueMonsterSprite[5],(Sprite)blueMonsterSprite[6],7,3,2,1);


		orangeMonsters[0] = new Monster((Sprite)orangeSprite[1],(Sprite)orangeSprite[2],4,3,3,6);
		orangeMonsters[1] = new Monster((Sprite)orangeSprite[3],(Sprite)orangeSprite[4],3,5,2,4);
		orangeMonsters[2] = new Monster((Sprite)orangeSprite[5],(Sprite)orangeSprite[6],1,7,1,2);

		greenMonsters[0] = new Monster((Sprite)greenSprite[1],(Sprite)greenSprite[2],2,4,1,2);
		greenMonsters[1] = new Monster((Sprite)greenSprite[3],(Sprite)greenSprite[4],2,3,3,4);
		greenMonsters[2] = new Monster((Sprite)greenSprite[5],(Sprite)greenSprite[6],5,5,2,3);

		darkGreenMonsters[0] = new Monster((Sprite)darkGreenSprite[1],(Sprite)darkGreenSprite[2],4,2,5,2);
		darkGreenMonsters[1] = new Monster((Sprite)darkGreenSprite[3],(Sprite)darkGreenSprite[4],3,3,3,3);
		darkGreenMonsters[2] = new Monster((Sprite)darkGreenSprite[5],(Sprite)darkGreenSprite[6],5,6,2,4);

		pinkMonsters[0] = new Monster((Sprite)pinkSprite[1],(Sprite)pinkSprite[2],2,1,2,3);
		pinkMonsters[1] = new Monster((Sprite)pinkSprite[3],(Sprite)pinkSprite[4],6,2,1,2);
		pinkMonsters[2] = new Monster((Sprite)pinkSprite[5],(Sprite)pinkSprite[6],5,5,4,1);

		roundMonster = new Monster[5];

		roundMonster[0]= blueMonsters[Random.Range(0,3)];
		roundMonster[1]= orangeMonsters[Random.Range(0,3)];
		roundMonster[2]= greenMonsters[Random.Range(0,3)];
		roundMonster[3]= darkGreenMonsters[Random.Range(0,3)];
		roundMonster[4]= pinkMonsters[Random.Range(0,3)];

	}


	public Monster GetMonster(int round){
		return roundMonster[round];
	}

	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of LevelModel



	public override void NextChallenge ()
	{
		throw new System.NotImplementedException ();
	}

	public override void RequestHint ()
	{
		throw new System.NotImplementedException ();
	}

	#endregion
}
