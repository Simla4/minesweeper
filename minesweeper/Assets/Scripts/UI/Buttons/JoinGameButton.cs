using Unity.Netcode;

public class JoinGameButton : ButtonBase
{
    protected override void OnClickedButton()
    {
        base.OnClickedButton();
        NetworkManager.Singleton.StartClient();
    }
}
