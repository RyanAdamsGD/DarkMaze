using UnityEngine;
using System.Collections;

public class InstructionText_Script : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        GetComponent<TextMesh>().text = 
			"Use the light you collect to fill absorbers\n" +
        	"and progress to the next level.\n" +
        	"Each color will give you a special ability\n" +
        	"Use W A S D to move and R to reset.\n" +
        	"Click the color button to activate it.";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
