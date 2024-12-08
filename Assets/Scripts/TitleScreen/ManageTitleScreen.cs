using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class ManageTitleScreen : MonoBehaviour {

	public Text Highscore;
	public Transform EndlessMode;
	public Transform StoryMode;

	private bool Loading = false;

	private IEnumerator ShowAdWhenReady()
	{
		while (!Advertisement.IsReady("StartAdvertisement"))
		{
			yield return new WaitForSeconds(0.25f);
		}

		Advertisement.Show ("StartAdvertisement");
	}

	IEnumerator PlayGame () {
		if (Loading == false)
		{
			Loading = true;
			AsyncOperation NextScene = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
			while (!NextScene.isDone)
			{
				EndlessMode.Find("Text").GetComponent<Text>().text = "Loaded: " + (int)(NextScene.progress * 100) + "%";
				yield return null;
			}
		}
	}

	IEnumerator PlayTutorial () {
		AsyncOperation NextScene = SceneManager.LoadSceneAsync (2, LoadSceneMode.Single);
		while (!NextScene.isDone) {
			StoryMode.Find ("Text").GetComponent <Text> ().text = "Loaded: " + (int) (NextScene.progress * 100) + "%";
			yield return null;
		}
	}

	void Start ()
	{
		Advertisement.Initialize("3019218", false);
		StartCoroutine(ShowAdWhenReady());
		Highscore.text = "Highscore: " + PlayerPrefs.GetInt ("BETA_Highscore");
		EndlessMode.GetComponent <Button> ().onClick.AddListener (() => StartCoroutine (PlayGame ()));
	}
}
