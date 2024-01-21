namespace GamJam.GameStates
{
    public class GameCompleteState : BaseGameState
    {
        public GameCompleteState(GameManager ctx) : base(ctx)
        {
        }

        public override void Enter()
        {
            _ctx.Ui.gameComplete.SetActive(true);
        }

        public override void Exit()
        {
        }
    }
}
