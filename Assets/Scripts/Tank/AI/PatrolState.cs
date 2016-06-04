using Assets.Scripts.Tank.Enums;
using UnityEngine;

namespace Assets.Scripts.Tank.AI {
    public class PatrolState : BaseState{
        public override StateStatus Status { get; set; }
        public override EnemyState NextState { get; set; }

        public PatrolState(Rigidbody rigidbody) : base (rigidbody){
        }

        public override void UpdateState() {
            CalculateDistanceFromPlayer();
        }

        public override void Execute() {
            Rigidbody.transform.rotation = Quaternion.Slerp(Rigidbody.transform.rotation,
                Quaternion.LookRotation(Player.transform.position - Rigidbody.transform.position), 180 * Time.deltaTime);

            var distance = Vector3.Distance(Player.transform.position, Rigidbody.transform.position);
            if (distance > maxDistance)
                return;
            if (distance < minDistance - distanceMargin) {
                Rigidbody.transform.position += Rigidbody.transform.forward * 10 * Time.deltaTime * -1;
            }
            else if (distance > minDistance + distanceMargin) {
                Rigidbody.transform.position += Rigidbody.transform.forward * 10 * Time.deltaTime;
            }
        }

        public override void GoToNextState() {
            NextState = EnemyState.Patrol;
        }
    }
}