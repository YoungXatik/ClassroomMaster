using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookFromShelf : MonoBehaviour
{
    [SerializeField] private Animator cheaterAnim;

    public void Look()
    {
        cheaterAnim.SetTrigger("Look");
    }
}
