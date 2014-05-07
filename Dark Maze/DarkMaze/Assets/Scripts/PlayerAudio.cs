using UnityEngine;
using System.Collections;

public class PlayerAudio : MonoBehaviour {
    public AudioSource rollingSound;
	public AudioSource bounceSound;
	Vector3 lastVelocity;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((lastVelocity-rigidbody.velocity).magnitude >=3 && !bounceSound.isPlaying){
			bounceSound.Play();
		}
		lastVelocity = rigidbody.velocity;
        if (detectGround()){
			if (rollingSound.isPlaying){
                rollingSound.volume = Mathf.Clamp01(rigidbody.velocity.magnitude / 4);
			}else{
                rollingSound.volume = Mathf.Clamp01(rigidbody.velocity.magnitude / 4);
                rollingSound.Play();
			}
		}else{
			rollingSound.Stop();	
		}
	}
	private bool detectGround(){
		Ray ray = new Ray(rigidbody.position, new Vector3(0,-1,0));
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)){
			return hit.distance<=0.7;
		}
        return false;
	}
}
