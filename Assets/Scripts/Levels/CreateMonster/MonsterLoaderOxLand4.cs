using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class MonsterLoaderOxLand4 : MonoBehaviour {
	

	private MonsterOxLand4[] roundMonster;
	

	private MonsterOxLand4 blueMonster;
	private MonsterOxLand4 orangeMonster;
	private MonsterOxLand4 greenMonster;
	private MonsterOxLand4 darkGreenMonster;
	private MonsterOxLand4 pinkMonster;
	

	public Image blueMonsterTeethPosition;
	public Image blueMonsterFeetPosition;
	public Image blueMonsterArmsPosition;
	public Image blueMonsterEyesPosition;
	public Image blueMonsterPosition;

	public Image orangeMonsterTeethPosition;
	public Image orangeMonsterFeetPosition;
	public Image orangeMonsterArmsPosition;
	public Image orangeMonsterEyesPosition;
	public Image orangeMonsterPosition;

	public Image greenMonsterTeethPosition;
	public Image greenMonsterFeetPosition;
	public Image greenMonsterArmsPosition;
	public Image greenMonsterEyesPosition;
	public Image greenMonsterPosition;

	public Image pinkMonsterTeethPosition;
	public Image pinkMonsterFeetPosition;
	public Image pinkMonsterArmsPosition;
	public Image pinkMonsterEyesPosition;
	public Image pinkMonsterPosition;

	public Image darkGreenMonsterTeethPosition;
	public Image darkGreenMonsterFeetPosition;
	public Image darkGreenMonsterArmsPosition;
	public Image darkGreenMonsterEyesPosition;
	public Image darkGreenMonsterPosition;

	public Sprite transparent;
	
	// Use this for initialization
	void Start () {

		Sprite blueMonsterSprite = blueMonsterPosition.sprite;
		Sprite orangeMonsterSprite = orangeMonsterPosition.sprite;
		Sprite greenMonsterSprite = greenMonsterPosition.sprite ;
		Sprite darkGreenMonsterSprite = darkGreenMonsterPosition.sprite;
		Sprite pinkMonsterSprite = pinkMonsterPosition.sprite;

		//Blue Monster

		Object[] blueMonsterEyesSprite = Resources.LoadAll ("Monsters/monster5-eyes");
		Object[] blueMonsterArmsSprite = Resources.LoadAll ("Monsters/monster5-arms");
		Object[] blueMonsterTeethSprite = Resources.LoadAll ("Monsters/monster5-teeth");
		blueMonsterTeethSprite = FixTeethArray (blueMonsterTeethSprite,1);
		Object[] blueMonsterFeetSprite = Resources.LoadAll ("Monsters/monster5-legs");

		int blueMonsterEyes = blueMonsterEyesSprite.Length-1;
		int blueMonsterArms = blueMonsterArmsSprite.Length-1;
		int blueMonsterTeeth = blueMonsterTeethSprite.Length-1;
		int blueMonsterFeet = blueMonsterFeetSprite.Length-1;

		blueMonsterEyesSprite[0] = transparent;
		blueMonsterArmsSprite[0] = transparent;
		blueMonsterTeethSprite[0] = transparent;
		blueMonsterFeetSprite[0] = transparent;

		blueMonster = new MonsterOxLand4(blueMonsterSprite,blueMonsterArms,blueMonsterEyes,blueMonsterTeeth,blueMonsterFeet,
		                          blueMonsterArmsSprite,blueMonsterEyesSprite,
		                                 blueMonsterTeethSprite,blueMonsterFeetSprite, 
		                                 blueMonsterEyesPosition, blueMonsterFeetPosition,
		                                 blueMonsterTeethPosition, blueMonsterArmsPosition, blueMonsterPosition);

		//Orange Monster
		
		Object [] orangeMonsterEyesSprite = Resources.LoadAll ("Monsters/monster4-eyes");
		Object [] orangeMonsterArmsSprite = Resources.LoadAll ("Monsters/monster4-arms");
		Object [] orangeMonsterTeethSprite = Resources.LoadAll ("Monsters/monster4-teeth");
		orangeMonsterTeethSprite = FixTeethArray (orangeMonsterTeethSprite,1);
		Object [] orangeMonsterFeetSprite = Resources.LoadAll ("Monsters/monster4-legs");
		
		int orangeMonsterEyes = orangeMonsterEyesSprite.Length-1;
		int orangeMonsterArms = orangeMonsterArmsSprite.Length-1;
		int orangeMonsterTeeth = orangeMonsterTeethSprite.Length-1;
		int orangeMonsterFeet = orangeMonsterFeetSprite.Length-1;
		
		orangeMonsterEyesSprite[0] = transparent;
		orangeMonsterArmsSprite[0] = transparent;
		orangeMonsterTeethSprite[0] = transparent;
		orangeMonsterFeetSprite[0] = transparent;
		
		orangeMonster = new MonsterOxLand4(orangeMonsterSprite,orangeMonsterArms,orangeMonsterEyes,orangeMonsterTeeth,orangeMonsterFeet,
		                                 orangeMonsterArmsSprite,orangeMonsterEyesSprite,
		                                 orangeMonsterTeethSprite,orangeMonsterFeetSprite, 
		                                 orangeMonsterEyesPosition, orangeMonsterFeetPosition,
		                                 orangeMonsterTeethPosition, orangeMonsterArmsPosition, orangeMonsterPosition);

		//Green Monster
		
		Object [] greenMonsterEyesSprite = Resources.LoadAll ("Monsters/monster1-eyes");
		Object [] greenMonsterArmsSprite = Resources.LoadAll ("Monsters/monster1-arms");
		Object [] greenMonsterTeethSprite = Resources.LoadAll ("Monsters/monster1-teeth");
		greenMonsterTeethSprite = FixTeethArray (greenMonsterTeethSprite,1);
		Object [] greenMonsterFeetSprite = Resources.LoadAll ("Monsters/monster1-legs");
		
		int greenMonsterEyes = greenMonsterEyesSprite.Length-1;
		int greenMonsterArms = greenMonsterArmsSprite.Length-1;
		int greenMonsterTeeth = greenMonsterTeethSprite.Length-1;
		int greenMonsterFeet = greenMonsterFeetSprite.Length-1;
		
		greenMonsterEyesSprite[0] = transparent;
		greenMonsterArmsSprite[0] = transparent;
		greenMonsterTeethSprite[0] = transparent;
		greenMonsterFeetSprite[0] = transparent;
		
		greenMonster = new MonsterOxLand4(greenMonsterSprite,greenMonsterArms,greenMonsterEyes,greenMonsterTeeth,greenMonsterFeet,
		                                   greenMonsterArmsSprite,greenMonsterEyesSprite,
		                                   greenMonsterTeethSprite,greenMonsterFeetSprite, 
		                                   greenMonsterEyesPosition, greenMonsterFeetPosition,
		                                   greenMonsterTeethPosition, greenMonsterArmsPosition, greenMonsterPosition);


		//Pink Monster
		
		Object [] pinkMonsterEyesSprite = Resources.LoadAll ("Monsters/monster2-eyes");
		Object [] pinkMonsterArmsSprite = Resources.LoadAll ("Monsters/monster2-arms");
		Object [] pinkMonsterTeethSprite = Resources.LoadAll ("Monsters/monster2-teeth");
		pinkMonsterTeethSprite = FixTeethArray (pinkMonsterTeethSprite,1);
		Object [] pinkMonsterFeetSprite = Resources.LoadAll ("Monsters/monster2-legs");
		
		int pinkMonsterEyes = pinkMonsterEyesSprite.Length-1;
		int pinkMonsterArms = pinkMonsterArmsSprite.Length-1;
		int pinkMonsterTeeth = pinkMonsterTeethSprite.Length-1;
		int pinkMonsterFeet = pinkMonsterFeetSprite.Length-1;
		
		pinkMonsterEyesSprite[0] = transparent;
		pinkMonsterArmsSprite[0] = transparent;
		pinkMonsterTeethSprite[0] = transparent;
		pinkMonsterFeetSprite[0] = transparent;
		
		pinkMonster = new MonsterOxLand4(pinkMonsterSprite,pinkMonsterArms,pinkMonsterEyes,pinkMonsterTeeth,pinkMonsterFeet,
		                                  pinkMonsterArmsSprite,pinkMonsterEyesSprite,
		                                  pinkMonsterTeethSprite,pinkMonsterFeetSprite, 
		                                  pinkMonsterEyesPosition, pinkMonsterFeetPosition,
		                                  pinkMonsterTeethPosition, pinkMonsterArmsPosition, pinkMonsterPosition);

		//Dark Green Monster
		
		Object [] darkGreenMonsterEyesSprite = Resources.LoadAll ("Monsters/monster3-eyes");
		Object [] darkGreenMonsterArmsSprite = Resources.LoadAll ("Monsters/monster3-arms");
		Object [] darkGreenMonsterTeethSprite = Resources.LoadAll ("Monsters/monster3-teeth");
//		darkGreenMonsterTeethSprite = FixTeethArray (darkGreenMonsterTeethSprite,1);
		Object [] darkGreenMonsterFeetSprite = Resources.LoadAll ("Monsters/monster3-legs");
		
		int darkGreenMonsterEyes = darkGreenMonsterEyesSprite.Length-1;
		int darkGreenMonsterArms = darkGreenMonsterArmsSprite.Length-1;
		int darkGreenMonsterTeeth = darkGreenMonsterTeethSprite.Length-1;
		int darkGreenMonsterFeet = darkGreenMonsterFeetSprite.Length-1;
		
		darkGreenMonsterEyesSprite[0] = transparent;
		darkGreenMonsterArmsSprite[0] = transparent;
		darkGreenMonsterTeethSprite[0] = transparent;
		darkGreenMonsterFeetSprite[0] = transparent;
		
		darkGreenMonster = new MonsterOxLand4(darkGreenMonsterSprite,darkGreenMonsterArms,darkGreenMonsterEyes,darkGreenMonsterTeeth,darkGreenMonsterFeet,
		                                 darkGreenMonsterArmsSprite,darkGreenMonsterEyesSprite,
		                                 darkGreenMonsterTeethSprite,darkGreenMonsterFeetSprite, 
		                                 darkGreenMonsterEyesPosition, darkGreenMonsterFeetPosition,
		                                 darkGreenMonsterTeethPosition, darkGreenMonsterArmsPosition, darkGreenMonsterPosition);

		
	
		roundMonster = new MonsterOxLand4[5];
		
		roundMonster[0]= darkGreenMonster;
		roundMonster[1] = orangeMonster;
		roundMonster[2]= greenMonster;
		roundMonster[3] = pinkMonster;
		roundMonster[4]= blueMonster;
		
	}
	
	
	public MonsterOxLand4 GetMonster(int round){
		return roundMonster[round];
	}

	private Object[] FixTeethArray(Object[] array,int index){
		System.Collections.Generic.List<Object> list = new System.Collections.Generic.List<Object>(array);
		list.RemoveAt(index);
		array = list.ToArray();
		return array;
	}

}
