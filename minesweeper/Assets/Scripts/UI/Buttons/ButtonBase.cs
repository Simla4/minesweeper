using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonBase : MonoBehaviour
{
    private Button button;

    public static Action OnJoinGameEvent;
    

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => OnClickedButton());
        
    }

    protected virtual void OnClickedButton()
    {
        OnJoinGameEvent?.Invoke();
    }
}
