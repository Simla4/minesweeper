using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonBase : MonoBehaviour
{
    private Button button;

    public static Action OnGameStartEvent;
    

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => OnClickedButton());
        
    }

    protected virtual void OnClickedButton()
    {
        OnGameStartEvent?.Invoke();
    }
}
