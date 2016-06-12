using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tank.AI;
using Assets.Scripts.Tank.Enums;
using Assets.Scripts.Tank.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Tank.Controllers {
    public class ComputerInputControler : BaseInputControler, IInputControler {
        private IEnemyState _currentState;
        

        public ComputerInputControler(Rigidbody rigidbody, Rigidbody shell) 
            : base(rigidbody){
            _currentState = new IdleState(rigidbody);
            _weaponController = new ComputerWeaponController(shell, rigidbody);
            Dispatcher.AddListener(GameEventEnum.RoundEnded, RestartState);
        }

        private void RestartState(object @object) {
            _currentState = new IdleState(Rigidbody);
        }

        public void Update() {
            _currentState.UpdateState();
        }

        public void Execute(float speed, float turnSpeed) {
            _currentState.Execute();
            GoToTheNaxtState();
        }

        private void ShootWeapon(object sender, WeponEventArgs args) {
            _weaponController.Shoot();
        }

        private void GoToTheNaxtState() {
            if (_currentState.Status == StateStatus.Running) {
                return;
            }

            switch (_currentState.NextState) {
                case EnemyState.Fight:
                    var state = new FightState(Rigidbody);
                    state.OnWeaponFired += ShootWeapon;
                    _currentState = state;
                    break;
                case EnemyState.Chase:
                        _currentState = new ChaseState(Rigidbody);
                    break;
                case EnemyState.Patrol:
                        _currentState = new PatrolState(Rigidbody);
                    break;
                case EnemyState.Idle:
                        _currentState = new IdleState(Rigidbody);
                    break;
                case EnemyState.Run:
                    _currentState = new RunState(Rigidbody);
                    break;
                case EnemyState.LookForEnemy:
                    _currentState = new LookForEnemy(Rigidbody);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
