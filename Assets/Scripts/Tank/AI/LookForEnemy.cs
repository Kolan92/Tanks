using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tank.Enums;
using UnityEngine;

namespace Assets.Scripts.Tank.AI {
    class LookForEnemy : BaseState {
        public override StateStatus Status { get; set; }
        public override EnemyState NextState { get; set; }

        public LookForEnemy(Rigidbody rigidBody) : base(rigidBody) {
            Status = StateStatus.Running;
            NextState = EnemyState.Idle;
        }

        public override void UpdateState() {
            CalculateDistanceFromPlayer();
        }

        public override void Execute() {
            NextState = DistanceFromPlayer < maxDistance ? EnemyState.Chase : EnemyState.Patrol;
            Status = StateStatus.Sucess;
            //NextState = EnemyState.Patrol;
        }
    }
}
