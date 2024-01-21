using GamJam.GameStates;
using UnityEngine;

namespace GamJam.EventChannel
{
    [CreateAssetMenu(fileName = "GameState Channel", menuName = "Events/GameState Event")]
    public class GameStateEventChannel : BaseEventChannel<GameState> { }
}
