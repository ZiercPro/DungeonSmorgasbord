using UnityEngine;
using ZiercCode.Runtime.Component;
using ZiercCode.Runtime.Hero;

namespace ZiercCode.Test
{

    public class MoveTest : MonoBehaviour
    {
        private HeroInputManager _heroInputManager;
        private Movement _movement;

        private void Awake()
        {
            _movement = GetComponent<Movement>();
            _heroInputManager = GetComponent<HeroInputManager>();
        }

        private void Start()
        {
            _movement.Initialize(2f);
            _heroInputManager.MovementInputPerforming += moveDir => { _movement.MovePerform(moveDir); };
        }
    }
}