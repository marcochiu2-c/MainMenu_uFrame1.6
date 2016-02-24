using UnityEngine;
using System.Collections;

public class ChangeMusic : MonoBehaviour {
	public AudioClip storageBGM;
	public AudioClip setbattleBGM;

	private AudioSource source;

	void Awake(){
		source = GetComponent<AudioSource> ();
	}

	void OnLevelWasLoaded(int level){
		if (level == 2) {
			source.clip = storageBGM;
			source.Play();
		}
	}
}
