using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts.Levels.Vowels;

public class BubbleAnimationEventListener : MonoBehaviour {

    public VowelsController controller;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void EventHandlerMethod(AnimationEvent animationEvent)
    {
        controller.ResetBubble(GameObject.Find("letter" + animationEvent.intParameter.ToString() + "Button").GetComponent<Button>());
    }
    }
