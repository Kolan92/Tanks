using Assets.Scripts.Tank.Enums;
using UnityEngine;

namespace Assets.Scripts.Tank.AI {
    public class PatrolState : BaseState{
        public override StateStatus Status { get; set; }
        public override EnemyState NextState { get; set; }
        
        private float randomTurn;
        private bool isReverse;

        public PatrolState(Rigidbody rigidbody) : base (rigidbody) {
            TurnRandomInRange();
            //AttachEvents();
        }

        public override void UpdateState() {
            NextState = EnemyState.LookForEnemy;
        }

        public override void Execute() {
            //TurnRandomInRange();
            Turn();
            Move();
            executeCounter++;

            if(executeCounter>50)
                Status = StateStatus.Sucess;
            /*
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
            */
        }

        private void TurnRandomInRange(int range = 1) {
            randomTurn = Random.Range(Rigidbody.rotation.y - range, Rigidbody.rotation.y + range);
        }

        protected override void Move() {
            var movement = Rigidbody.transform.forward * speed * Time.deltaTime;
            movement = isReverse ? movement * - 1 : movement;
            Rigidbody.MovePosition(Rigidbody.position + movement);
        }

        protected override void Turn() {
            var turn = randomTurn * turnSpeed * Time.deltaTime;
            var turnRotation = Quaternion.Euler(0f, turn, 0f);
            Rigidbody.MoveRotation(Rigidbody.rotation * turnRotation);
        }

        protected override void OnColision(object o) {
            isReverse = true;
        }


    }
}