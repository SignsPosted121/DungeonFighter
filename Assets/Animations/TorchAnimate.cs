using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorchAnimate : MonoBehaviour {

	private bool Currently = false;

	IEnumerator UpdateTexture () {
		bool Updated = false;
		if (Updated == false && transform.Find ("Torch1").GetComponent <Image> ().color.a == 1) {
			Updated = true;
			transform.Find ("Torch1").GetComponent <Image> ().color = new Color (1, 1, 1, 0);
			transform.Find ("Torch2").GetComponent <Image> ().color = new Color (1, 1, 1, 1);
		}
		if (Updated == false && transform.Find ("Torch2").GetComponent <Image> ().color.a == 1) {
			Updated = true;
			transform.Find ("Torch2").GetComponent <Image> ().color = new Color (1, 1, 1, 0);
			transform.Find ("Torch3").GetComponent <Image> ().color = new Color (1, 1, 1, 1);
		}
		if (Updated == false && transform.Find ("Torch3").GetComponent <Image> ().color.a == 1) {
			Updated = true;
			transform.Find ("Torch3").GetComponent <Image> ().color = new Color (1, 1, 1, 0);
			transform.Find ("Torch4").GetComponent <Image> ().color = new Color (1, 1, 1, 1);
		}
		if (Updated == false && transform.Find ("Torch4").GetComponent <Image> ().color.a == 1) {
			Updated = true;
			transform.Find ("Torch4").GetComponent <Image> ().color = new Color (1, 1, 1, 0);
			transform.Find ("Torch1").GetComponent <Image> ().color = new Color (1, 1, 1, 1);
		}
		yield return new WaitForSeconds (0.15f);
		Currently = false;
	}

	void FixedUpdate () {
		if (Currently == false) {
			Currently = true;
			StartCoroutine (UpdateTexture ());
		}
	}
}
