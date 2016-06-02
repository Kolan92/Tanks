using UnityEngine;
using System.Collections;
using Assets.Scripts.Tank.Controllers;
using Assets.Scripts.Tank.Interfaces;

public class TankController : MonoBehaviour {
    public IInputControler Input { get; set; }
    public ITankModel Model { get; set; }
    private Rigidbody Rigidbody { get; set; }
    
    public void Setup(bool isComputerControled, int playerNumber) {
        Rigidbody = GetComponent<Rigidbody>();

        Model = new TankModel() {
            IsComputerControled = isComputerControled
        };

        if (Model.IsComputerControled)
            Input = new ComputerControler() {
                _Rigidbody = Rigidbody
            };
        else
            Input = new PlayerControler(playerNumber);

        enabled = true;
    }

    private void Awake() {
        
        Model = new TankModel();
        
        enabled = false;
    }

    private void FixedUpdate() {
        var movment = Input.GetMovment(Model.Speed, Rigidbody.transform.forward);
        var rotation = Input.GetTurn(Model.TurnSpeed);

        Move(movment);
        Turn(rotation);
    }

    private void Update() {
        Input.Update();
    }

    private void Turn(Quaternion rotation) {
        Model.Rigidbody.MoveRotation(Model.Rigidbody.rotation * rotation);
    }

    private void Move(Vector3 movement) {
        Model.Rigidbody.MovePosition(Model.Rigidbody.position + movement);
    }
}
