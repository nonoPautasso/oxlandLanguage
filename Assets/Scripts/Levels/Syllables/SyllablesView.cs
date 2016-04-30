using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.Syllables {
	public class SyllablesView : LevelView {
		public Image objectImage;
		public Button tryBtn;
		public Image backPanel;
		public List<Button> syllables;

		private SyllablesController controller;

		public void TryClick(){
			
		}

		public void SyllableClick(int index){
			
		}

		public override void ShowHint () {
			
		}

		public override void EndGame () {
			
		}

		public void Controller (SyllablesController controller) { this.controller = controller; }
	}
}