using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tank.AI;
using Assets.Scripts.Tank.Enums;
using Assets.Scripts.Tank.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Tank.Controllers {
    public class ComputerControler : BaseInputControler, IInputControler {
        private IEnemyState _currentState;

        public ComputerControler() {
            _currentState = new IdleState(_Rigidbody);
            
        }
        public void Update() {
            _currentState.UpdateState();
        }

        public void Execute(float speed, float turnSpeed) {
            _currentState.Execute();
            GoToTheNaxtState();
        }

        private void GoToTheNaxtState() {
            if (_currentState.Status == StateStatus.Running) {
                return;
            }

            switch (_currentState.NextState) {
                case EnemyState.Fight:
                    _currentState = new FightState(_Rigidbody, Shell);
                    break;
                case EnemyState.Chase:
                        _currentState = new ChaseState(_Rigidbody);
                    break;
                case EnemyState.Patrol:
                        _currentState = new PatrolState(_Rigidbody);
                    break;
                case EnemyState.Idle:
                        _currentState = new IdleState(_Rigidbody);
                    break;
                case EnemyState.Run:
                    _currentState = new RunState(_Rigidbody);
                    break;
                case EnemyState.LookForEnemy:
                    _currentState = new LookForEnemy(_Rigidbody);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
