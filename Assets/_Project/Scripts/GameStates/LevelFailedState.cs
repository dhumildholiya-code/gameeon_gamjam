using GamJam.GameStates;
using System.Threading.Tasks;

namespace GamJam
{
    public class LevelFailedState : BaseGameState
    {
        public LevelFailedState(GameManager ctx) : base(ctx)
        {
        }

        public override async void Enter()
        {
            await ShowDelay();
        }

        public override void Exit()
        {
            _ctx.Ui.levelFailed.Show(false);
        }

        private async Task ShowDelay()
        {
            await Task.Delay(500);
            _ctx.Ui.levelFailed.Show(true);
        }
    }
}
