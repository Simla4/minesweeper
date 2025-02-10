
public class PreGameMenu : Menus
{
    private void OnEnable()
    {
        ButtonBase.OnGameStartEvent += CloseMenu;
    }

    private void OnDisable()
    {
        ButtonBase.OnGameStartEvent -= CloseMenu;
    }
}
