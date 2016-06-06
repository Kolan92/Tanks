using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Tank.Controllers;
using Assets.Scripts.Tank.Enums;
using Assets.Scripts.Tank.Interfaces;

public class TankController : MonoBehaviour {
    public IInputControler Input { get; set; }
    public ITankModel Model { get; set; }

    public Rigidbody Shell;
    private Rigidbody Rigidbody { get; set; }
    
    public void Setup(bool isComputerControled, int playerNumber) {
        Rigidbody = GetComponent<Rigidbody>();

        Model = new TankModel() {
            IsComputerControled = isComputerControled
        };

        var safeShellPosition = new Vector3(0, - (100 - playerNumber), 0); //TODO Find a way to keep shell alive during game. 
        if (Model.IsComputerControled) {
            Input = new ComputerControler() {
                _Rigidbody = Rigidbody,
                Shell = Instantiate(Shell, safeShellPosition, Shell.rotation) as Rigidbody
            };
        }
        else {
            Input = new PlayerControler(playerNumber) {
                _Rigidbody = Rigidbody,
                Shell = Instantiate(Shell, safeShellPosition, Shell.rotation) as Rigidbody
            };
        }

        enabled = true;

        Dispatcher.AddListener(GameEventEnum.HealObject, ApplayHealing);
        Dispatcher.AddListener(GameEventEnum.DemageObject, ApplayDamege);
    }

    private void Awake() {
        Model = new TankModel();
        enabled = false;
    }

    private void FixedUpdate() {
        Input.Execute(Model.Speed, Model.TurnSpeed);
    }

    private void Update() {
        Input.Update();
    }

    void OnCollisionEnter(Collision collision) {
        Dispatcher.Dispatch(GameEventEnum.TankColison);
    }

    private void ApplayDamege(object o) {
        var hitLocation = o as Transform;
        if (hitLocation == null) return;

        var explosionToTarget = hitLocation.position - transform.position;
        var explosionDistance = explosionToTarget.magnitude;
        var relativeDistance = (5 - explosionDistance) / 5; //TODO Remove constants
        var damage = relativeDistance * 100;
        damage = Mathf.Max(0f, damage);

        Model.HitPoints -= damage;
    }

    private void ApplayHealing(object o) {
        throw new NotImplementedException();
    }
}
