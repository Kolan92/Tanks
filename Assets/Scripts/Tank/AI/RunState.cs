using Assets.Scripts.Tank.Enums;
using UnityEngine;

namespace Assets.Scripts.Tank.AI {
    public class RunState : BaseState{
        public override StateStatus Status { get; set; }
        public override EnemyState NextState { get; set; }

        public RunState(Rigidbody rigidbody) : base(rigidbody) {

        }

        public override void UpdateState() {
            throw new System.NotImplementedException();
        }

        public override void Execute() {
            throw new System.NotImplementedException();
        }
    }
}