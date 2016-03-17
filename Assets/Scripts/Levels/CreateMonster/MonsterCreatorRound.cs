using UnityEngine;
using System.Collections;

public class MonsterCreatorRound  {


	private MonsterOxLand4 monsterImages;

	private int totalArms;
	private int totalEyes;
	private int totalTeeth;
	private int totalFeet;
	

	public MonsterCreatorRound(MonsterOxLand4 monsterImages){
		this.monsterImages = monsterImages;
		this.totalArms = Random.Range (1, monsterImages.Arms);
		this.totalEyes = Random.Range (1, monsterImages.Eyes);
		this.totalFeet = Random.Range (1, monsterImages.Legs);
		this.totalTeeth = Random.Range (1, monsterImages.Tooth);
	}

	public MonsterOxLand4 MonsterOxLand4 {
		get {
			return monsterImages;
		}
	}

	public int TotalArms {
		get {
			return totalArms;
		}
	}

	public int TotalEyes {
		get {
			return totalEyes;
		}
	}

	public int TotalTeeth {
		get {
			return totalTeeth;
		}
	}

	public int TotalFeet {
		get {
			return totalFeet;
		}
	}
}
