using GamJam.GameStates;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace GamJam
{
    public class LevelFailedState : BaseGameState
    {
        public LevelFailedState(GameManager ctx) : base(ctx)
        {
        }

        public override void Enter()
        {
            _ctx.StartCoroutine(ShowDelay());
        }

        public override void Exit()
        {
            _ctx.Ui.levelFailed.Show(false);
        }

        private IEnumerator ShowDelay()
        {
            yield return new WaitForSeconds(.5f);
            _ctx.Ui.levelFailed.Show(true);
        }
    }
}
