using System;
using UnityEngine;

public class CheaterUICounterNote : MonoBehaviour
{
    [SerializeField] public GameObject foundedCheaterImage;

    private void Start()
    {
        foundedCheaterImage.SetActive(true);
    }

    public void ActivateNoteImage()
    {
        foundedCheaterImage.SetActive(false);
    }
}
