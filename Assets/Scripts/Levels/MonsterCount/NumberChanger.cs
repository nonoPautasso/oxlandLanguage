using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberChanger : MonoBehaviour {

	public Sprite[] numbers;

	private int currentNumber;
	private int limit;

	private Image currentImage;

	private Image wrongBorder;
	// Use this for initialization
	public void InitNumber () {
		currentNumber = 0;
		limit = numbers.Length - 1;
		currentImage = GetComponent<Image>();
		UpdateImage();
		wrongBorder = transform.GetChild(0).GetComponent<Image>();
		wrongBorder.gameObject.SetActive(false);
	}
	


	public void ChangeNumber(){
		if(currentNumber++ == limit){
			currentNumber = 0;
		}
		currentImage.sprite = numbers[currentNumber];
	}


	public int CurrentNumber {
		get {
			return currentNumber;
		}
		set {
			currentNumber = value;
		}
	}

	public void UpdateImage(){
		currentImage.sprite = numbers[currentNumber];
	}

	public void ShowWrongBorder(bool showBorder){
		wrongBorder.gameObject.SetActive(!showBorder);
	}

	public void HideWrongBorder(){
		wrongBorder.gameObject.SetActive(false);
		ChangeNumber();
	}

}
