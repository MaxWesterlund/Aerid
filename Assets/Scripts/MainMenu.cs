using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour {
    [SerializeField] GameObject options;
    [SerializeField] Button invertYButton;
    [SerializeField] Sprite buttonOffSprite;
    [SerializeField] Sprite buttonOnSprite;
    [SerializeField] Slider volumeSlider;
    [SerializeField] TMP_Text highScoreText;

    bool activateOptions = false;
    
    void Update() {
        highScoreText.text = System.Math.Round(PlayerPrefs.GetFloat("High Score"), 2).ToString();
        invertYButton.image.sprite = Options.Instance.InvertY ? buttonOnSprite : buttonOffSprite;
        volumeSlider.value = Options.Instance.Volume;
    }

    public void ToggleOptions() {
        activateOptions = !activateOptions;
        options.SetActive(activateOptions);
    }

    public void Quit() {
        Application.Quit();
    }
}