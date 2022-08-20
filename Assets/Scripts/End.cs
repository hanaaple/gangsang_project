using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class End : MonoBehaviour, IPointerDownHandler
{

    public Image image;

    public Text title;
    
    public Text desc;


    public void PlayEnd(Sprite sprite, string t, string desc)
    {
        this.gameObject.SetActive(true);
        image.sprite = sprite;
        title.text = t;
        this.desc.text = desc;
    }
    
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!desc.gameObject.activeSelf)
        {
            desc.gameObject.SetActive(true);
            StartCoroutine(Alpha());
            // desc.gameObject.activeSelf ? Alpha() : desc.gameObject.activeSelf ? Alpha() : PlayEnd();
        }
    }

    private IEnumerator Alpha()
    {
        var t = 0f;
        while (t <= 1)
        {
            t += Time.deltaTime;
            var c = desc.color;
            c.a = t;
            desc.color = c;
            yield return null;
        }
        var c1 = desc.color;
        c1.a = 1;
        desc.color = c1;
    }
}
