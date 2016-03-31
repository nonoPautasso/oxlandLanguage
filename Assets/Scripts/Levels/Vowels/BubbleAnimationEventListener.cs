using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts.Levels.Vowels;
/*
Event listener to reset animation when bubble has reached top of screen
*/
public class BubbleAnimationEventListener : MonoBehaviour
{

    public VowelsController controller;

    public void EventHandlerMethod()
    {
        controller.ResetBubble(this.GetComponent<Button>());
    }
}
