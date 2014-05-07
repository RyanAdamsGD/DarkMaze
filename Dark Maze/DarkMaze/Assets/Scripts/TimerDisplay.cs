using UnityEngine;
using System.Collections;
using System.Text;
using System;

public class TimerDisplay : MonoBehaviour 
{
    private float minutes;
    private float seconds;
    private float milliseconds;

    public GUIStyle LabelStyle;

	// Use this for initialization
	void Start () 
    {
        minutes = 0;
        seconds = 0;
        milliseconds = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        milliseconds += Time.deltaTime * 1000;

        while (milliseconds >= 1000)
        {
            milliseconds -= 1000;
            seconds += 1;
        }
        while (seconds >= 60)
        {
            seconds -= 60;
            minutes += 1;
        }
	}

    void OnGUI()
    {
        string displayvalue = String.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);

        GUI.Label(new Rect(10, 10, 100, 26), displayvalue, LabelStyle);
    }
}
