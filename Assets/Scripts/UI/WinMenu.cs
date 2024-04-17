public class WinMenu : UIMenu
{

    public override void Construct(CoreLoopSystem coreLoopSystem)
    {
        base.Construct(coreLoopSystem);
        coreLoopSystem.onWin += OpenMenu;
    }

    private void OnDisable()
    {
        coreLoopSystem.onWin -= OpenMenu;
    }
}