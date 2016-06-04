using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tank.Enums;
using UnityEngine;

namespace Assets.Scripts.Tank.AI {
    public abstract class BaseState : IEnemyState {
        public abstract StateStatus Status { get; set; }
        public abstract EnemyState NextState { get; set; }

        protected Rigidbody Rigidbody { get; set; }
        protected GameObject Player;
        protected float DistanceFromPlayer;
        protected const int maxDistance = 100;
        protected const int minDistance = 1;
        protected const int distanceMargin = 2;

        protected BaseState(Rigidbody rigidbody) {
            Status = StateStatus.Running;
            NextState = EnemyState.Idle;
            Rigidbody = rigidbody;
            Player = GameObject.FindGameObjectWithTag(Constants.Player);
        }

        public abstract void UpdateState();
        public abstract void GoToNextState();
        public abstract void Execute();

        protected virtual void CalculateDistanceFromPlayer() {
            DistanceFromPlayer = Vector3.Distance(Player.transform.position, Rigidbody.transform.position);
        }
    }
}
