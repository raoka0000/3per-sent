using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayScene {

public class ParticleManager : SingletonMonoBehaviour<ParticleManager> {

	[SerializeField] ParticleSystem hitEffectSimple;
	ParticleSystem _hitEffectSimple;

	[SerializeField] ParticleSystem reflection;
	ParticleSystem _reflection;


	private void Awake (){
		if (hitEffectSimple == null) {
			Debug.LogWarning ("hitEffectSimpleがぬるです");
		}

		_hitEffectSimple = UnityUtil.GetPrefub<ParticleSystem> (hitEffectSimple, this.transform);
		_reflection = UnityUtil.GetPrefub<ParticleSystem> (reflection, this.transform);

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


}

}
