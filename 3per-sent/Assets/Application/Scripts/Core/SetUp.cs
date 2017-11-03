using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayScene;

public class SetUp : MonoBehaviour {
	/*EnvironmentEvent ee;
	ParticleManager pm;
	ObjectPool op;
	UiManager ui;
	AudioManager am;*/
	void Awake(){
		/*
		ee = EnvironmentEvent.instance;
		pm = ParticleManager.instance;
		op = ObjectPool.instance;
		ui = UiManager.instance;
		am = AudioManager.instance;*/
	}

	void Start(){
		AudioManager.instance.PlayBGM (DEFINE.STEGE1_BGM);
	}
}
