using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Tank.Controllers;
using Assets.Scripts.Tank.Enums;
using Assets.Scripts.Tank.Interfaces;

public class TankController : MonoBehaviour {
    public IInputControler InputController { get; set; }
    public ITankModel Model { get; set; }

    private Rigidbody Rigidbody;
    [SerializeField]
    private Rigidbody Shell;
    [SerializeField]
    private GameObject ExplosionPrefab;
    private ParticleSystem ExplosionParticles;

    public void Setup(bool isComputerControled, int playerNumber) {
        Rigidbody = GetComponent<Rigidbody>();


        Model = new TankModel() {
            IsComputerControled = isComputerControled
        };

        var shell = Instantiate(Shell, Shell.position, Shell.rotation) as Rigidbody;
        shell.gameObject.SetActive(false);
        if (Model.IsComputerControled) {
            InputController = new ComputerInputControler(Rigidbody, shell);
        }
        else {
            InputController = new PlayerInputControler(playerNumber, Rigidbody, shell);
        }

        enabled = true;

        Dispatcher.AddListener(GameEventEnum.HealObject, ApplayHealing);
        Dispatcher.AddListener(GameEventEnum.DemageObject, ApplayDamege);
    }

    private void Awake() {
        Model = new TankModel();
        Shell.gameObject.SetActive(false);
        ExplosionParticles = Instantiate(ExplosionPrefab).GetComponent<ParticleSystem>();
        ExplosionParticles.gameObject.SetActive(false);
        enabled = false;
    }

    private void FixedUpdate() {
        InputController.Execute(Model.Speed, Model.TurnSpeed);
    }

    private void Update() {
        InputController.Update();
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
        if(Model.HitPoints < 0)
            Destroy();
    }

    private void Destroy() {
        ExplosionParticles.transform.position = transform.position;
        ExplosionParticles.gameObject.SetActive(true);

        ExplosionParticles.Play();
        gameObject.SetActive(false);
    }

    private void ApplayHealing(object o) {
        throw new NotImplementedException();
    }
}
