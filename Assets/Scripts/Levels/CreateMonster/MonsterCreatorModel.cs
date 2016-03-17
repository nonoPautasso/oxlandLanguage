using UnityEngine;
using System.Collections;
using Assets.Scripts.App;

public class MonsterCreatorModel : LevelModel {


	public MonsterCreatorModel(){
		minSeconds = 50;
		pointsPerError = 500;
		pointsPerSecond = 100;
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
		throw new System.NotImplementedException ();
	}

	#endregion
}
