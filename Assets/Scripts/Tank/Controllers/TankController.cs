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
            Input = new PlayerControler(playerNumber){
                _Rigidbody = Rigidbody
            };

        enabled = true;
    }

    private void Awake() {
        
        Model = new TankModel();
        
        enabled = false;
    }

    private void FixedUpdate() {
        Input.Execute(Model.Speed, Model.TurnSpeed);
        //Input.Move(Model.Speed);
        //Input.Turn(Model.TurnSpeed);
    }

    private void Update() {
        Input.Update();
    }
}
