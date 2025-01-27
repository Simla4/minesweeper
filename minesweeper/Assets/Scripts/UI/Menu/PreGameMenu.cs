
public class PreGameMenu : Menus
{
    private void OnEnable()
    {
        ButtonBase.OnJoinGameEvent += CloseMenu;
    }

    private void OnDisable()
    {
        ButtonBase.OnJoinGameEvent -= CloseMenu;
    }
}
