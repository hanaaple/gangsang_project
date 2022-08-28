using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleModule : MonoBehaviour, IPointerDownHandler
{
    public Image touchToPlay;

    public void OnEndTitle()
    {
        touchToPlay.raycastTarget = true;   
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("InGameScene", LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
            if (asyncOperation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.1f);
                asyncOperation.allowSceneActivation = true;       
            }
        }
    }
}
