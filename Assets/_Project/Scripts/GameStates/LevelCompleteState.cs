using GamJam.GameStates;
using System.Collections;
using UnityEngine;

namespace GamJam
{
    public class LevelCompleteState : BaseGameState
    {
        public LevelCompleteState(GameManager ctx) : base(ctx)
        {
        }

        public override void Enter()
        {
            _ctx.SetLevelComplete();
            GameManager.Instance.StartCoroutine(ShowDelay());
        }

        public override void Exit()
        {
            _ctx.Ui.levelComplete.Show(false);
        }
        private IEnumerator ShowDelay()
        {
            yield return new WaitForSeconds(.5f);
            _ctx.Ui.levelComplete.Show(true);
        }
    }
}
