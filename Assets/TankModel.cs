using UnityEngine;
using System.Collections;
using Assets.Scripts.Tank.Interfaces;

public class TankModel : ITankModel {
    public float HitPoints { get; set; }
    public float Armor { get; set; }
    public int Ammunition { get; set; }
    public float Speed { get; set; }
    public float Velocity { get; set; }
    public string MovementAxisName { get; set; }
    public string TurnAxisName { get; set; }
    public Rigidbody Rigidbody { get; set; }
    public float MovementInputValue { get; set; }
    public float TurnInputValue { get; set; }
    public int PlayerNumber { get; set; }
    public bool IsComputerControled { get; set; }

    public TankModel() {
        HitPoints = 100;
        Armor = 100;
        Ammunition = 100;
        Speed = 5;
        Velocity = 1;
    }
}
