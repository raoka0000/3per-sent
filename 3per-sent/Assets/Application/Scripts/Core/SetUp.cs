using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayScene;

public class SetUp : MonoBehaviour {
	void Awake(){
	}

	void Start(){
		AudioManager.instance.PlayBGM (DEFINE.STEGE1_BGM);
	}
}
