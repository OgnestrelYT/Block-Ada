using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSettings : MonoBehaviour
{
    public Toggle toggle; // Check box


    void Start() {
        if (!PlayerPrefs.HasKey("LeverDrag")) {
            PlayerPrefs.SetInt("LeverDrag", 1);
        }

        if (PlayerPrefs.GetInt("LeverDrag") == 1) {
            toggle.isOn = true;
        } else {
            toggle.isOn = false;
        }
    }

    public void OnToggleValueChanged() {
        if (toggle.isOn) {
            PlayerPrefs.SetInt("LeverDrag", 1);
        } else {
            PlayerPrefs.SetInt("LeverDrag", 0);
        }
        Debug.Log(PlayerPrefs.GetInt("LeverDrag"));
    }

    public void Firstly() {
		PlayerPrefs.SetInt("isFirst", 1);
	}
}
