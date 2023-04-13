using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {
    [SerializeField] RectTransform coverImage;
    [SerializeField] float closeSpeed;
    [SerializeField] bool up;
    [SerializeField] string sceneName;

    public void Load() {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel(float lerp = 0) {
        AsyncOperation loadNextLevel = SceneManager.LoadSceneAsync(sceneName);
        loadNextLevel.allowSceneActivation = false;

        while (!loadNextLevel.isDone) {
            lerp += Time.deltaTime;

            float closeAmount = Mathf.SmoothStep(coverImage.offsetMin.y, 600 * (up? 0 : 1), closeSpeed * lerp);
            coverImage.offsetMin = new Vector2(coverImage.offsetMin.x, closeAmount);
            coverImage.offsetMax = new Vector2(coverImage.offsetMax.x, closeAmount);

                if (lerp >= 1) {
                loadNextLevel.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}