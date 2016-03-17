using UnityEngine;
using System.Collections;
using Assets.Scripts.App;

public class MonsterCreatorView : LevelView {

	public override void ShowHint ()
	{
		DisableHint ();
		MonsterCreatorController.instance.ShowHint ();
	}


	#region implemented abstract members of LevelView


	public override void EndGame ()
	{
		throw new System.NotImplementedException ();
	}

	#endregion
}
