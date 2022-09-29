using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UltraAchievement;

public class Achievement : MonoBehaviour
{
    private float endTimer = 0;
    private float startTimer = 0;
    private float animTimer;
    private bool endTimerStarted = false;
    private bool startTimerStarted = false;
    private RectTransform rect;
    private Image icon;
    private Text title,description;

    public void InitAchievement(string iconPath, string name = "Achievement", string desc = "Example description")
    {
        rect = GetComponent<RectTransform>();
        title = transform.GetChild(0).GetComponent<Text>();
        description = transform.GetChild(1).GetComponent<Text>();
        icon = transform.GetChild(2).GetComponent<Image>();
        title.text = name;
        description.text = desc;
        icon.sprite = Core.LoadSprite(iconPath,Vector4.zero,100);
        rect.anchoredPosition = new Vector2(400,0);
        startTimerStarted = true;
    }
    public void InitAchievementI(Sprite ico, string name = "Achievement", string desc = "Example description")
    {
        rect = GetComponent<RectTransform>();
        title = transform.GetChild(0).GetComponent<Text>();
        description = transform.GetChild(1).GetComponent<Text>();
        icon = GetComponentInChildren<Image>();
        title.text = name;
        description.text = desc;
        icon.sprite = ico;
        rect.anchoredPosition = new Vector2(400,0);
        startTimerStarted = true;
    }
    void Update()
    {
        if(startTimerStarted)
        {
            startTimer += Time.deltaTime;
            animTimer += Time.deltaTime * 1.35f;
            if (animTimer >= 1) animTimer = 1;
            rect.anchoredPosition = new Vector2(Mathf.Lerp(400f, 0f, animTimer),0);
            if(startTimer >= 5)
            {
                animTimer = 0;
                endTimerStarted = true;
                startTimerStarted = false;
            }
        }
        if(endTimerStarted)
        {
            endTimer += Time.deltaTime;
            animTimer += Time.deltaTime * 1.35f;
            if (animTimer >= 1) animTimer = 1;
            rect.anchoredPosition = new Vector2(Mathf.Lerp(0f, 400f, animTimer), 0);
            if (endTimer >= 5)
            {
                endTimerStarted = false;
                Destroy(this.gameObject);
            }
        }
    }
}
