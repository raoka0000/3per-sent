using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayScene {

public class ParticleManager : SingletonMonoBehaviour<ParticleManager> {

	[SerializeField] ParticleSystem hitEffectSimple;
	ParticleSystem _hitEffectSimple;

	[SerializeField] ParticleSystem reflection;
	ParticleSystem _reflection;

	[SerializeField] ParticleSystem kirakira;
	ParticleSystem _kirakira;



	private void Awake (){
		_hitEffectSimple = UnityUtil.GetPrefub<ParticleSystem> (hitEffectSimple, this.transform);
		_reflection = UnityUtil.GetPrefub<ParticleSystem> (reflection, this.transform);
		_kirakira = UnityUtil.GetPrefub<ParticleSystem> (kirakira, this.transform);
	}

	private Vector3 tmp = new Vector3 ();

	public void HitEffect(Transform tra,int n = 5){
		if (_hitEffectSimple == null) {
			return;
		}
		tmp.x = tra.rotation.z + 90;
		tmp.y = 90;
		tmp.z = 0;
		//_hitEffectSimple.transform.eulerAngles += tmp;
		_hitEffectSimple.transform.position = tra.position;
		_hitEffectSimple.Emit(n);
	}

	public void reflectionEffect(Vector3 pos,int n = 1){
		if (_reflection == null) {
			return;
		}
		_reflection.transform.position = pos;
		_reflection.Emit(n);
	}

	public void kirakiraEffect(Vector3 pos,int n = 30){
		if (_kirakira == null) {
			return;
		}
		_kirakira.transform.position = pos;
		_kirakira.Emit(n);
	}


}

}
