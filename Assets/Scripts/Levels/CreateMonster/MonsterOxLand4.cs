using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonsterOxLand4 : Monster {

	private Object[] feetImages;
	private Object[] armsImages;
	private Object[] teethImages;
	private Object[] eyesImages;

	private Image eyesPosition;
	private Image feetPosition;
	private Image teethPosition;
	private Image armsPosition;
	private Image bodyPosition;

	public MonsterOxLand4(Sprite monsterImage, int arms, int eyes, int teeth, int feet,
	                    Object[] armsImages, Object[] eyesImages, Object[] teethImages, Object[] feetImages,
	                      Image eyesPosition, Image feetPosition, Image teethPosition, Image armsPosition, Image bodyPosition) :
						base(monsterImage,null,arms,eyes,teeth,feet){

		this.feetImages = feetImages;
		this.armsImages = armsImages;
		this.eyesImages = eyesImages;
		this.teethImages = teethImages;

		this.eyesPosition = eyesPosition;
		this.feetPosition = feetPosition;
		this.armsPosition = armsPosition;
		this.teethPosition = teethPosition;

		this.bodyPosition = bodyPosition;

	}

	public Object[] FeetImages {
		get {
			return feetImages;
		}
	}



	public Object[] ArmsImages {
		get {
			return armsImages;
		}
	}

	public Object[] TeethImages {
		get {
			return teethImages;
		}
	}



	public Object[] EyesImages {
		get {
			return eyesImages;
		}
	}

	public Image EyesPosition {
		get {
			return eyesPosition;
		}
		set {
			eyesPosition = value;
		}
	}

	public Image FeetPosition {
		get {
			return feetPosition;
		}
		set {
			feetPosition = value;
		}
	}

	public Image TeethPosition {
		get {
			return teethPosition;
		}
		set {
			teethPosition = value;
		}
	}

	public Image ArmsPosition {
		get {
			return armsPosition;
		}
		set {
			armsPosition = value;
		}
	}

	public Image BodyPosition {
		get {
			return bodyPosition;
		}
	}
}
