using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class LevelLoader : MonoBehaviour {


    public GameObject PanelLoading;
    public Slider progressBar;
    public TextMeshProUGUI texto;


    
    public void loadLevel(string nombre)
    {
        StartCoroutine(LoadAsynchronous(nombre));
    }
    IEnumerator LoadAsynchronous(string sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        PanelLoading.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            texto.text = progress * 100+" %";
            yield return null;
        }

    }
}
