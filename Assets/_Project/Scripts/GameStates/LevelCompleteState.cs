using GamJam.GameStates;
using System.Threading.Tasks;

namespace GamJam
{
    public class LevelCompleteState : BaseGameState
    {
        public LevelCompleteState(GameManager ctx) : base(ctx)
        {
        }

        public override async void Enter()
        {
            _ctx.SetLevelComplete();
            await ShowDelay();
        }

        public override void Exit()
        {
            _ctx.Ui.levelComplete.Show(false);
        }
        private async Task ShowDelay()
        {
            await Task.Delay(500);
            _ctx.Ui.levelComplete.Show(true);
        }
    }
}
