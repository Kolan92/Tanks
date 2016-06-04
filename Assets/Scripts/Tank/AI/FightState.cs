using Assets.Scripts.Tank.Enums;
using UnityEngine;

namespace Assets.Scripts.Tank.AI {
    public class FightState : BaseState{
        public override StateStatus Status { get; set; }
        public override EnemyState NextState { get; set; }

        public FightState(Rigidbody rigidbody) : base(rigidbody){
        }

        public override void UpdateState() {
            throw new System.NotImplementedException();
        }

        public override void Execute() {
            throw new System.NotImplementedException();
        }

        public override void GoToNextState() {
            throw new System.NotImplementedException();
        }
    }
}