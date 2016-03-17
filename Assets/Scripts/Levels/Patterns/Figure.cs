using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Figure  {

	private PatternModel.FigureForm form;
	private PatternModel.FigureColor color;
	private PatternModel.FigureSize size;

	private Sprite sprite;



	public Figure(PatternModel.FigureForm form,PatternModel.FigureColor color,PatternModel.FigureSize size,
	              Sprite sprite){
		this.form = form;
		this.color = color;
		this.size = size;
		this.sprite = sprite;
	}



	public Sprite Sprite {
		get {
			return sprite;
		}
	}

	public PatternModel.FigureSize Size {
		get {
			return size;
		}
	}

	public PatternModel.FigureColor ColorF {
		get {
			return color;
		}
	}

	public PatternModel.FigureForm Form {
		get {
			return form;
		}
	}
}
