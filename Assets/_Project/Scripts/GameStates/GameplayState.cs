using UnityEngine;
namespace GamJam.GameStates
{
    public class GameplayState : BaseGameState
    {
        public GameplayState(GameManager ctx) : base(ctx)
        {
        }

        public override void Enter()
        {
            _ctx.Ui.gameplay.Show(true);

            _ctx.gameStateChange.Register(_ctx.ChangeState);
            _ctx.GameController = GameObject.FindObjectOfType<GameController>();
            if (_ctx.GameController == null)
            {
                throw new System.Exception("Game Controller is not in the scene which is mandatory for gameplay to work.");
            }
            _ctx.GameController.Init();
        }

        public override void Exit()
        {
            _ctx.gameStateChange.Unregister(_ctx.ChangeState);
            _ctx.StartCoroutine(_ctx.GameController.Exit());
            _ctx.Ui.gameplay.Show(false);
        }
    }
}
