using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    public static LevelProgress Instance;
    public float fillTime = 2;
    public float barIncreaseValue = 7;
    public Image fillBar;
    public Image icon;

    private float storedFillAmount;
    private float xIncreasedValue;

    [SerializeField] private Image[] enemyIcons;
    [SerializeField] private Image[] defeatedSprites;

    public Animator progressAnimator;

    private void Awake()
    {
        for (int i = 0; i < defeatedSprites.Length; i++)
        {
            defeatedSprites[i].gameObject.SetActive(false);
        }
        
        Instance = this;
        fillBar.fillAmount = ((SceneManager.GetActiveScene().buildIndex - 1) * barIncreaseValue) / 100;   
        icon.transform.localPosition = new Vector3(((SceneManager.GetActiveScene().buildIndex - 1) * 60.9f),14);

        Debug.Log("Icon pos " + icon.transform.localPosition.x);
        Debug.Log("enemy 0 Icon pos " + enemyIcons[0].transform.localPosition.x);

    }
    
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddValueToBar();
        }

        if (icon.transform.localPosition.x >= Mathf.Abs(enemyIcons[0].transform.localPosition.x))
        {
            defeatedSprites[0].gameObject.SetActive(true);
        }
        for (int i = 0; i < enemyIcons.Length; i++)
        {
            if (icon.transform.localPosition.x >= Mathf.Abs(enemyIcons[i].transform.localPosition.x))
            {
                defeatedSprites[i].gameObject.SetActive(true);
            }
        }
        
    }

    public void OpenBar()
    {
        StartCoroutine(OpenFillAndCloseBar());
    }

    public void FirstLevelOpenBar()
    {
        StartCoroutine(OpenAndCloseBar());
    }

    public IEnumerator OpenFillAndCloseBar()
    {
        progressAnimator.SetBool("Open", true);
        yield return new WaitForSeconds(1f);
        AddValueToBar();
        yield return new WaitForSeconds(fillTime);
        progressAnimator.SetBool("Open",false);
    }
    
    public IEnumerator OpenAndCloseBar()
    {
        progressAnimator.SetBool("Open", true);
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(fillTime);
        progressAnimator.SetBool("Open",false);
    }

    public void AddValueToBar()
    {
        storedFillAmount += barIncreaseValue / 100f;

        var twen = fillBar.DOFillAmount( fillBar.fillAmount + storedFillAmount, fillTime);
        twen.startValue = (storedFillAmount - barIncreaseValue / 100f);
        twen.SetEase(Ease.Linear);
        var iconPos = icon.transform.localPosition;
        if (iconPos.x >= 670)
            return;
        xIncreasedValue += barIncreaseValue * 670 / 100;
        icon.transform.DOLocalMoveX(Mathf.Clamp(icon.transform.localPosition.x + xIncreasedValue, 0, 670), fillTime)
            .SetEase(Ease
                .Linear); //.startValue = new Vector3(icon.transform.localPosition.x + xIncreasedValue - barIncreaseValue * 670 / 100, iconPos.y, iconPos.z);
    }
}