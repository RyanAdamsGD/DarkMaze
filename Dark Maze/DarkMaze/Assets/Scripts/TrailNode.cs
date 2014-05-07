using UnityEngine;
using System.Collections;

public class TrailNode : MonoBehaviour {
    float LifeTime = 4.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        LifeTime -= UnityEngine.Time.deltaTime;
        if (LifeTime <= 0)
        {
            Destroy(gameObject);
        }
	}

    public void setColor(Color color)
    {
        GetComponent<ParticleSystem>().startColor = color;
        GetComponent<Light>().color = color;
    }
}
