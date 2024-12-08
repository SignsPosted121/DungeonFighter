using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedCard : MonoBehaviour {

	public GameObject Selected;
	public string Type = "Any";
	public bool Locked = true;

	void ReImage () {
		Destroy (transform.Find ("Picture"));
		Destroy (transform.Find ("CardName"));
		if (gameObject.GetComponent <Image> ().color.a <= 0) {
			return;
		}
		if (Selected != null) {
			GameObject Image = (GameObject)Instantiate (Selected.transform.Find ("Picture").gameObject);
			Image.transform.SetParent (transform);
			Image.GetComponent <RectTransform> ().offsetMin = new Vector2 (0, 0);
			Image.GetComponent <RectTransform> ().offsetMax = new Vector2 (0, 0);
			Image = (GameObject)Instantiate (Selected.transform.Find ("CardName").gameObject);
			Image.transform.SetParent (transform);
			Image.GetComponent <RectTransform> ().offsetMin = new Vector2 (0, 0);
			Image.GetComponent <RectTransform> ().offsetMax = new Vector2 (0, 0);
			gameObject.GetComponent <Image> ().color = new Color (1, 1, 1, 1);
		} else gameObject.GetComponent <Image> ().color = new Color (0.4f, 0.4f, 0.4f, 1);
		if (Locked == true) {
			gameObject.GetComponent <Image> ().color = new Color (0.2f, 0.2f, 0.2f, 1);
		}
	}

	void Start () {
		ReImage ();
	}
}
