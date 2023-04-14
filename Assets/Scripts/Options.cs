using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour {
    [SerializeField] AudioMixer mixer;

    public static Options Instance;

    public bool InvertY = false;
    public float Volume = .5f;

    void Awake() {
        if (Instance != null) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    void Start() {
        InvertY = PlayerPrefs.GetInt("Invert Y") == 1 ? true : false;
        Volume = PlayerPrefs.GetFloat("Volume");
    }

    public void ToggleInvertY() {
        InvertY = !InvertY;
        PlayerPrefs.SetInt("Invert Y", InvertY? 1 : 0);
    }

    public void SetVolume(Slider slider) {
        float volume = Mathf.Log10(slider.value) * 20;
        mixer.SetFloat("Master", volume);
        Volume = slider.value;
        PlayerPrefs.SetFloat("Volume", slider.value);
    }
}