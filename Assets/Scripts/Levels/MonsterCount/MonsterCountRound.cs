using UnityEngine;
using System.Collections;

public class MonsterCountRound {

	private bool roundComplete;
	private Monster monster;

	private int currentEyes;
	private int currentLegs;
	private int currentTooth;
	private int currentArms;

	private int[] currentValues;

	public MonsterCountRound(Monster monster){
		roundComplete = false;
		this.monster = monster;
		currentEyes = 0;
		currentLegs = 0;
		currentArms = 0;
		currentTooth = 0;

	}



	public Monster Monster {
		get {
			return monster;
		}
	}

	public void SetCurrentValues(int currentEyes, int currentLegs, int currentArms, int currentTooth){
		this.currentEyes = currentEyes;
		this.currentLegs = currentLegs;
		this.currentArms = currentArms;
		this.currentTooth = currentTooth;
	}

	public int GetCurrentEyes(){
		return currentEyes;
	}

	public int GetCurrentLegs(){
		return currentLegs;
	}

	public int GetCurrentArms(){
		return currentArms;
	}

	public int GetCurrentTooth(){
		return currentTooth;
	}
	

	public bool RoundComplete {
		get {
			return roundComplete;
		}
		set {
			roundComplete = value;
		}
	}
}
