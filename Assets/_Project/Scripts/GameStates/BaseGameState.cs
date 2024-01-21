namespace GamJam.GameStates
{
    public enum GameState
    {
        MainMenu,
        Gameplay,
        LevelComplete,
        LevelFailed,
        GameComplete,
        Quit
    }
    public abstract class BaseGameState
    {
        protected GameManager _ctx;
        public BaseGameState(GameManager ctx)
        {
            _ctx = ctx;
        }
        public abstract void Enter();
        public abstract void Exit();
    }
}