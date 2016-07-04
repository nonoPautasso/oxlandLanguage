using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Levels.OracionesPictogramas {
	public class OracionesPictogramasView : LevelView {


	private OracionesPictogramasController controller;
	
	public override void ShowHint () {
				DisableHint ();
				controller.ShowHint ();
	}
	public override void EndGame () { }
	public void Controller (OracionesPictogramasController controller) { this.controller = controller; }

}
}
