using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.MayusMin {
	public class MayusMinView : LevelView {
		public List<Image> letters;
		public Image sentencePanel;
		public Text hintText;
		public Button nextBtn;

		private MayusMinController controller;

		public override void ShowHint () {
			
		}

		public void NextClick ()
		{
			
		}

		public override void EndGame () { }
		public void Controller(MayusMinController controller){ this.controller = controller; }
	}
}

