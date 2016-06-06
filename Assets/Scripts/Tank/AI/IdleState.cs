using System;
using Assets.Scripts.Tank.Enums;
using UnityEngine;

namespace Assets.Scripts.Tank.AI {
    public class IdleState : BaseState{
        public override StateStatus Status { get; set; }
        public override EnemyState NextState { get; set; }

        public IdleState(Rigidbody rigidbody) : base(rigidbody) {
          
        }

        public override void UpdateState() {
            Status = StateStatus.Sucess;
            NextState = EnemyState.LookForEnemy;
        }

        public override void Execute() {
            //Do nothing
        }
    }
}