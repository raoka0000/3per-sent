using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayScene {

public abstract class StageWaveEmitterNode : ScriptableObject{
	public abstract FunctionIEnumerator emit ();
 }


}