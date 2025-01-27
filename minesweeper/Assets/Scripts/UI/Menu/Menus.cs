using System;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class Menus : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public virtual void OpenMenu()
    {
        canvasGroup.alpha = 1;
    }

    public virtual void CloseMenu()
    {
        canvasGroup.alpha = 0;
    }
}
