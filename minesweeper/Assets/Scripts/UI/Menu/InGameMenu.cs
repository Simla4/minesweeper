public class InGameMenu : Menus
{
    private void OnEnable()
    {
        ButtonBase.OnGameStartEvent += OpenMenu;
    }

    private void OnDisable()
    {
        ButtonBase.OnGameStartEvent -= OpenMenu;
    }
}
