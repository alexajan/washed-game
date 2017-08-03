using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JellyScore : MonoBehaviour {
	int jelly = 0;
	int slime = 0;
	float TIMER = 0.0f;
	bool PLAYING = true;

	string FormatTimer() {
		if (TIMER <= 30.0f) {
			return (Mathf.Round (TIMER * 100) / 100).ToString();
		} else {
			int displayTimeMin = (int)(TIMER / 60.0f);
			float displayTimeSec = Mathf.Round ((TIMER % 60) * 10 / 10);
			if (displayTimeSec<10) {
				return displayTimeMin.ToString() + ":0" + displayTimeSec.ToString();
			} else {
				return displayTimeMin.ToString() + ":" + displayTimeSec.ToString();
			}
		}
	}

	public void DisplayTime() {
		GameObject.FindGameObjectWithTag("Screen").GetComponent<Text>().text = "Time: " + FormatTimer()
			+ System.Environment.NewLine + "Growth: " + jelly + System.Environment.NewLine + "Slimes Destroyed:" + slime + "/6";
	}

	public void EndGame(int status) {
		PLAYING = false;
		if (status == 1) {	
			if(!PlayerPrefs.HasKey("BestTime") || PlayerPrefs.GetFloat("BestTime") < TIMER) {
				PlayerPrefs.SetFloat("BestTime", TIMER);
				GameObject.FindGameObjectWithTag("Screen").GetComponent<Text>().text = "Time: " + FormatTimer()
				+ System.Environment.NewLine + "New High Score!";
			} else {
				GameObject.FindGameObjectWithTag("Screen").GetComponent<Text>().text = "Time: " + FormatTimer()
					+ System.Environment.NewLine + "Bacteria prevail!";
			}
		} else if (status == 0) {
			GameObject.FindGameObjectWithTag("Screen").GetComponent<Text>().text = "Time: " + FormatTimer()
				+ System.Environment.NewLine + "You've been destroyed by antibacterial slime.";
		}
	}
			
	public void IncrementScore() {
		jelly++;
	}

	public void DecreaseScore() {
		jelly = jelly - 1;
	}

	public void SlimeScore() {
		slime++;
	}

	void Update() {
		if (PLAYING) {
			TIMER += Time.deltaTime;
			DisplayTime();
			if (slime >= 6) {
				EndGame(1);
			} else if (jelly < 0) {
				EndGame(0);
			}
		}
	}
}
