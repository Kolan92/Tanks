﻿using Assets.Scripts.Tank.Enums;
using UnityEngine;

namespace Assets.Scripts.Tank.AI {
    public class FightState : BaseState{
        public override StateStatus Status { get; set; }
        public override EnemyState NextState { get; set; }
        private Rigidbody shell;

        public FightState(Rigidbody rigidbody, Rigidbody Shell) : base(rigidbody) {
            shell = Shell;
        }

        public override void UpdateState() {
            TurnToEnemy();
        }

        public override void Execute() {
            Shoot();
        }

        private void Shoot() {

            var position = Rigidbody.position + new Vector3(0, 2f, 0); //TODO Improve start position and rotation of the shell
            Rigidbody shellInstance = UnityEngine.Object.Instantiate(shell, position, Rigidbody.rotation) as Rigidbody;

            shellInstance.velocity = 30 * Rigidbody.transform.forward;
        }
    }
}