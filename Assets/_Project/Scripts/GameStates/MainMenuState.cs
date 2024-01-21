namespace GamJam.GameStates
{
    public class MainMenuState : BaseGameState
    {
        public MainMenuState(GameManager ctx) : base(ctx)
        {
        }

        public override void Enter()
        {
            _ctx.Ui.mainMenu.Show(true);
        }

        public override void Exit()
        {
            _ctx.Ui.mainMenu.Show(false);
        }
    }
}
