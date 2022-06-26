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

    public Animator progressAnimator;

    private void Awake()
    {
        Instance = this;
        fillBar.fillAmount = (SceneManager.GetActiveScene().buildIndex * barIncreaseValue) / 100;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddValueToBar();
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
        icon.transform.DOLocalMoveX(Mathf.Clamp(xIncreasedValue, 0, 670), fillTime).SetEase(Ease.Linear).startValue = new Vector3(xIncreasedValue - barIncreaseValue * 670 / 100, iconPos.y, iconPos.z);
    }
}