using UnityEngine;
using System.Collections;
using Assets.Scripts.Tank.Controllers;
using Assets.Scripts.Tank.Interfaces;

public class TankController : MonoBehaviour {
    public IInputControler InputControler { get; set; }
    public ITankModel TankModel { get; set; }
    
    public void Setup(bool isComputerControled, int playerNumber) {
        var rigidbody = GetComponent<Rigidbody>();
        TankModel = new TankModel() {
            Rigidbody = rigidbody,
            IsComputerControled = isComputerControled
        };
        if (TankModel.IsComputerControled)
            InputControler = new ComputerControler();
        else
            InputControler = new PlayerControler(playerNumber);

        enabled = true;
    }

    private void Awake() {
        
        TankModel = new TankModel();
        
        enabled = false;
    }

    private void FixedUpdate() {
        //InputControler.GetMovment();
    }

    private void Update() {

    }
}
