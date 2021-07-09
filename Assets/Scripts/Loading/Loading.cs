using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Loading : MonoBehaviour
{
    [SerializeField]
    private LoadingView loadingView;
    [SerializeField]
    private int index;

    private float loadingProgressTime = 0.9f;

    public float LoadingProgressTime
    {
        get { return loadingProgressTime; }
    }

    public void WaitForProgram()
    {
        StartCoroutine(LoadAsyncScene());
    }

    private IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        asyncLoad.allowSceneActivation = false;
        yield return (asyncLoad.progress > loadingProgressTime);

        StartCoroutine(loaded(asyncLoad));
    }

    private IEnumerator loaded(AsyncOperation sync)
    {
        yield return new WaitForSeconds(1);
        sync.allowSceneActivation = true;
    }

}
