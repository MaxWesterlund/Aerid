using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupManager : MonoBehaviour {
    [SerializeField] float showTime;
	[SerializeField] float comboTime;
    [SerializeField] TMP_Text missileDestroyedText;
    [SerializeField] AudioSource soundEffect;

    Coroutine missileCounter;

    int missilesDestroyed;

    void Awake() {
        if (GameObject.Find("Game Manager") != null) {
            GameObject obj = GameObject.Find("Game Manager");
            GameManager gameManager = obj.GetComponent<GameManager>();
            gameManager.MissileDestroyed += OnMissileDestroyed;
        }
    }

    void OnMissileDestroyed() {
        if (missileCounter != null) {
            StopCoroutine(missileCounter);
        }
        missileCounter = StartCoroutine(CountMissilesDestroyed());
    }

    IEnumerator CountMissilesDestroyed() {
        if (missileDestroyedText.text == "") {
            soundEffect.Play();
        }
        missilesDestroyed++;
        if (missilesDestroyed == 1) {
            missileDestroyedText.text = "Missile Destroyed";
        }
        else {
            missileDestroyedText.text = "Missile Destroyed X" + missilesDestroyed;
        }
        yield return new WaitForSeconds(showTime);
        missileDestroyedText.text = "";
        yield return new WaitForSeconds(comboTime - showTime);
        missilesDestroyed = 0;
        missileDestroyedText.text = "";
    }
}
