using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
    private static HSVColor currentColor = new HSVColor();

	// Use this for initialization
	void Start () {
        currentColor.saturation = 1;
        currentColor.value = 1;
        gameObject.GetComponent<Light>().color = new Color(0.05f,0.05f,0.05f,0.05f);
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (allAbsorbersFilled())
        {
            gameObject.GetComponent<Light>().color = currentColor.RGBValue;
            renderer.material.color = currentColor.RGBValue;

            currentColor.hue += Time.deltaTime * 50;

            if (currentColor.hue > 360)
                currentColor.hue = 0;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (allAbsorbersFilled() && other.gameObject.tag == "Player")
        {
            Application.LoadLevel(Application.loadedLevel + 1);            
        }
    }

    private bool allAbsorbersFilled()
    {
        GameObject[] absorbers = GameObject.FindGameObjectsWithTag("Absorber");
        for (int i = 0; i < absorbers.Length; i++)
        {
            if (!absorbers[i].GetComponent<LightAbsorbingPortal_Script>().IsFull())
            {
                return false;
            }
        }
        return true;
    }
}
