using Assets.Scripts.Tank.Enums;
using UnityEngine;

namespace Assets.Scripts.Tank.AI {
    public abstract class BaseState : IEnemyState {
        public abstract StateStatus Status { get; set; }
        public abstract EnemyState NextState { get; set; }
        protected Distance Distance {
            get{
                if (DistanceFromPlayer > maxDistance)
                    return Distance.TooFar;
                if (DistanceFromPlayer < minDistance - distanceMargin)
                    return Distance.TooClose;
                if (DistanceFromPlayer > minDistance + distanceMargin)
                    return Distance.InBeetween;
                if(DistanceFromPlayer>minDistance)
                    return Distance.CloseToFight;
                return Distance.TooFar;
            }
        }

        protected Rigidbody Rigidbody { get; set; }
        protected GameObject Player;
        protected float DistanceFromPlayer;
        protected int executeCounter;
        protected const int maxDistance = 25;
        protected const int minDistance = 10;
        protected const int distanceMargin = 5;
        protected const float speed = 10;
        protected const float turnSpeed = 180;

        protected BaseState(Rigidbody rigidbody) {
            Status = StateStatus.Running;
            NextState = EnemyState.Idle;
            Rigidbody = rigidbody;
            Player = GameObject.FindGameObjectWithTag(Constants.Player);

            AttachEvents();
        }

        public abstract void UpdateState();
        public abstract void Execute();

        protected void TurnToEnemy() {
            Rigidbody.transform.rotation = Quaternion.Slerp(Rigidbody.transform.rotation,
                Quaternion.LookRotation(Player.transform.position - Rigidbody.transform.position), 180 * Time.deltaTime);
        }

        protected virtual void Turn() {
            var turn = turnSpeed * Time.deltaTime;
            var turnRotation = Quaternion.Euler(0f, turn, 0f);
            Rigidbody.MoveRotation(Rigidbody.rotation * turnRotation);
        }

        protected virtual void Move() {
            var movement = Rigidbody.transform.forward * speed * Time.deltaTime;
            Rigidbody.MovePosition(Rigidbody.position + movement);
        }

        protected virtual void CalculateDistanceFromPlayer() {
            DistanceFromPlayer = Vector3.Distance(Player.transform.position, Rigidbody.transform.position);
        }

        protected virtual void AttachEvents() {
            Dispatcher.AddListener(GameEventEnum.TankColison, OnColision);
        }

        protected virtual void OnColision(object o) {
            //TODO maybe abstract?
        }
    }
}
