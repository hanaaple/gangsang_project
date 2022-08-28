using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End : MonoBehaviour, IPointerDownHandler
{
    public GameObject endingPanel; 
    public Image image;

    public Text title;
    
    public Text desc;


    public AudioClip[] endingAudioClips;
    public Sprite[] endingImages;
    private readonly string[] _endingTitle = {"엔딩 1. 한 우물만 파기", "엔딩 2. 황금 밸런스", "엔딩 3. 선택과 집중"};

    public Button loadButton;

    void Start()
    {
        loadButton.gameObject.SetActive(false);
        loadButton.onClick.AddListener(() =>
        {
            StartCoroutine(LoadScene());
        });
    }
    private IEnumerator LoadScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("TitleScene", LoadSceneMode.Single);
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
    public void PlayEnd(int endingType, string desc)
    {
        SoundManager.instance.PlayBgm(endingAudioClips[endingType]);
        endingPanel.SetActive(true);
        image.sprite = endingImages[endingType];
        title.text = _endingTitle[endingType];
        this.desc.text = desc;
        
        this.desc.gameObject.SetActive(true);
        StartCoroutine(Alpha());
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (!loadButton.gameObject.activeSelf)
        {
            StopAllCoroutines();
            var c1 = desc.color;
            c1.a = 1;
            desc.color = c1;
            loadButton.gameObject.SetActive(true);
        }
    }

    private IEnumerator Alpha()
    {
        var timer = 3f;
        var t = 0f;
        while (t <= 1)
        {
            t += Time.deltaTime / timer;
            var c = desc.color;
            c.a = t;
            desc.color = c;
            yield return null;
        }
        var c1 = desc.color;
        c1.a = 1;
        desc.color = c1;
        loadButton.gameObject.SetActive(true);
    }
}
