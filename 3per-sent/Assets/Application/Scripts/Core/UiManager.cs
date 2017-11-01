using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlayScene {

public class UiManager : SingletonMonoBehaviour<UiManager> {
	public Image skillImage;
	public Text hpText;

	public Image green;
	public Image red;

	private int pMaxHp = 0;


	void Start() {
		SetHp ();
	}

	public void SetSkillImage(SkillObject s){
		skillImage.sprite = s.image;
		skillImage.gameObject.SetActive (true);
	}

	public void LossSkillImage(){
		skillImage.gameObject.SetActive (false);
	}

	public void SetHp(){
		hpText.text = "HP:" + EnvironmentEvent.instance.player.hp;
	}
		
	public void UpdateHp(int hp){
		green.fillAmount = (float)hp / (float)EnvironmentEvent.instance.player.maxHp;
	}


}

}