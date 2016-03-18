using UnityEngine;
using System.Collections;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels.Vowels
{
	public class VowelsController : LevelController
	{
		public VowelsView view;

		VowelsModel model;


		// Use this for initialization
		void Start ()
		{
	
		}

		public override void InitGame ()
		{
			model = new VowelsModel ();
		}

		public override void NextChallenge ()
		{
			
		}

		public override void ShowHint ()
		{
			
		}
	}
}