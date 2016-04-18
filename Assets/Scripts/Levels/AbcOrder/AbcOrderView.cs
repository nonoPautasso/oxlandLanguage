using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.AbcOrder {
	public class AbcOrderView : LevelView {
		public Button nextBtn;
		public List<Image> answers;
		public List<Button> letters;

		private AbcOrderController controller;

		public void NextChallenge (List<string> answers, List<string> options, int currentRound) {
			bool isRandom = (AbcOrderModel.FIRST_LETTER_ROUNDS - currentRound) <= 0;
			int helpLetters = isRandom ? AbcOrderModel.ROUNDS - currentRound : AbcOrderModel.FIRST_LETTER_ROUNDS - currentRound;
			SetAnswerLetters (answers, helpLetters, isRandom);

		}

		void SetAnswerLetters (List<string> answers, int helpLetters, bool isRandom) {
			
		}

		public override void ShowHint () {
			
		}

		public override void EndGame () {
			
		}

		public void Controller (AbcOrderController controller) {
			this.controller = controller;
		}
	}
}