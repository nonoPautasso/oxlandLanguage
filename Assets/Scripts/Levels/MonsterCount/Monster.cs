using UnityEngine;
using System.Collections;

public class Monster {

	private Sprite monsterImage;
	private Sprite hintMonsterImage;
	private int arms;
	private int eyes;
	private int tooth;
	private int legs;

	private bool hintShown;

	public Monster(Sprite monsterImage, Sprite hintMonsterImage,int arms, int eyes, int tooth, int legs){
		this.monsterImage = monsterImage;
		this.hintMonsterImage = hintMonsterImage;
		this.arms = arms;
		this.eyes = eyes;
		this.tooth = tooth;
		this.legs = legs;
		hintShown = false;
	}

	public Sprite HintMonsterImage {
		get {
			return hintMonsterImage;
		}
	}

	public Sprite MonsterImage {
		get {
			if(hintShown){
				return hintMonsterImage;

			}else{
				return monsterImage;

			}
		}
	}

	public int Arms {
		get {
			return arms;
		}
	}

	public int Eyes {
		get {
			return eyes;
		}
	}

	public int Tooth {
		get {
			return tooth;
		}
	}

	public int Legs {
		get {
			return legs;
		}
	}

	public bool HintShown {
		get {
			return hintShown;
		}
		set {
			hintShown = value;
		}
	}
}
