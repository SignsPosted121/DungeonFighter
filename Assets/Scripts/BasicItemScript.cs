using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicItemScript : MonoBehaviour {

	public float Amount = 0;
	public float MaxAmount = 0;

	void FixedUpdate () {
		transform.Find ("ItemAmount").GetComponent <Text> ().text = Amount + "/" + MaxAmount;
	}
}
