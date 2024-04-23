using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.Old.Component;
using ZiercCode.Old.Hero;

namespace ZiercCode.Test
{

    public class MoveTest : MonoBehaviour
    {
        private HeroInputManager _heroInputManager;
        private MoveComponent _moveComponent;

        private void Awake()
        {
            _moveComponent = GetComponent<MoveComponent>();
            _heroInputManager = GetComponent<HeroInputManager>();
        }

        private void Start()
        {
            _moveComponent.SetMoveSpeed(2f);
            _heroInputManager.MovementInputPerforming += moveDir => { _moveComponent.Move(moveDir); };
        }
    }
}