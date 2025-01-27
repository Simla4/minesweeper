using System;
using Unity.Netcode;
using UnityEngine;

public class CreateNewGameButton : ButtonBase
{
    public static Action OnCreateNewGameEvent;
    
    
    protected override void OnClickedButton()
    {
        base.OnClickedButton();
        NetworkManager.Singleton.StartServer();
        OnCreateNewGameEvent?.Invoke();
    }
}
