using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {
    [SerializeField] string sceneName;

    public void Load() {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel(float lerp = 0) {
        AsyncOperation loadNextLevel = SceneManager.LoadSceneAsync(sceneName);

        while (!loadNextLevel.isDone) {
            // Do progress bar

            yield return null;
        }
    }
}