public class LoseMenu : UIMenu 
{
    public override void Construct(CoreLoopSystem coreLoopSystem)
    {
        base.Construct(coreLoopSystem);
        coreLoopSystem.onLose += OpenMenu;
    }

    private void OnDisable()
    {
        coreLoopSystem.onWin -= OpenMenu;
    }
}