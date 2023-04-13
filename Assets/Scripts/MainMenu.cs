using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [SerializeField] GameObject options;
    [SerializeField] Button invertYButton;
    [SerializeField] Sprite buttonOffSprite;
    [SerializeField] Sprite buttonOnSprite;

    bool activateOptions = false;
    bool invertYButtonOn = false;
    
    public void ToggleOptions() {
        activateOptions = !activateOptions;
        options.SetActive(activateOptions);
    }

    public void ToggleInvertY() {
        invertYButtonOn = !invertYButtonOn;
        invertYButton.image.sprite = invertYButtonOn ? buttonOnSprite : buttonOffSprite;
    }

    public void Quit() {
        Application.Quit();
    }
}