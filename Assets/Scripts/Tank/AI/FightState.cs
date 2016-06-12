using Assets.Scripts.Tank.Controllers;
using Assets.Scripts.Tank.Enums;
using UnityEngine;

namespace Assets.Scripts.Tank.AI {
    public class FightState : BaseState{
        public override StateStatus Status { get; set; }
        public override EnemyState NextState { get; set; }
        private Rigidbody shell;

        public delegate void WeponFiredHandler(object sender, WeponEventArgs args);
        public event WeponFiredHandler OnWeaponFired;

        public FightState(Rigidbody rigidbody) : base(rigidbody) {
        }

        public override void UpdateState() {
            if (Distance == Distance.TooFar) {
                NextState = EnemyState.LookForEnemy;
                Status = StateStatus.Failed;
            }
            TurnToEnemy();
        }

        public override void Execute() {
            Shoot();
        }

        public void Shoot() {
            if(OnWeaponFired == null) return;

            var args = new WeponEventArgs();
            OnWeaponFired(this, args);
        }
    }
}