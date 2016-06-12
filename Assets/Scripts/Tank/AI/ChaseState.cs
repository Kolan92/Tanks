using System;
using Assets.Scripts.Tank.Enums;
using UnityEngine;

namespace Assets.Scripts.Tank.AI {
    public class ChaseState : BaseState {
        public override StateStatus Status { get; set; }
        public override EnemyState NextState { get; set; }

        public ChaseState(Rigidbody rigidbody) : base(rigidbody) {
            TurnToEnemy();
        }

        public override void UpdateState() {
            CalculateDistanceFromPlayer();

            switch (Distance) {
                case Distance.TooFar:
                    NextState = EnemyState.LookForEnemy;
                    break;
                case Distance.InBeetween:
                case Distance.TooClose:
                    NextState = EnemyState.Fight;
                    break;
                case Distance.CloseToFight:
                    NextState = EnemyState.Fight;
                    Status = StateStatus.Sucess;
                    break;
            }
        }

        public override void Execute() {
            Move();
            ++executeCounter;

            if (executeCounter > 50) 
                Status = StateStatus.Failed;
            }


        protected override void Move() {
            if (Distance == Distance.TooClose) {
                Rigidbody.transform.position += Rigidbody.transform.forward * 10 * Time.deltaTime * - 1;
            }
            else if (Distance == Distance.InBeetween) {
                Rigidbody.transform.position += Rigidbody.transform.forward * 10 * Time.deltaTime;
            }
        }
    }
}