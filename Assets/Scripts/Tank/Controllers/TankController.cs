using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Tank.Controllers;
using Assets.Scripts.Tank.Enums;
using Assets.Scripts.Tank.Interfaces;

public class TankController : MonoBehaviour {
    public IMovmentControler MovmentController { get; set; }
    public ITankModel Model { get; set; }

    private Rigidbody Rigidbody;
    [SerializeField]
    private Rigidbody Shell;
    [SerializeField]
    private GameObject ExplosionPrefab;
    private ParticleSystem ExplosionParticles;
    private TankView view;

    public void Setup(bool isComputerControled, int playerNumber) {
        Rigidbody = GetComponent<Rigidbody>();


        Model = new TankModel() {
            IsComputerControled = isComputerControled
        };

        var shell = Instantiate(Shell, Shell.position, Shell.rotation) as Rigidbody;
        shell.gameObject.SetActive(false);
        
        if (Model.IsComputerControled) {
            MovmentController = new ComputerMovmentControler(Rigidbody, shell);
            
        }
        else {
            MovmentController = new PlayerMovmentControler(playerNumber, Rigidbody, shell);
            
        }

        enabled = true;

        InitializeView();

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
        MovmentController.Execute(Model.Speed, Model.TurnSpeed);
    }

    private void Update() {
        MovmentController.Update();
    }

    void OnCollisionEnter(Collision collision) {
        Dispatcher.Dispatch(GameEventEnum.TankColison);
    }

    private void ApplayDamege(object o) {
        var hitLocation = o as Transform;
        if (hitLocation == null) return;

        var explosionToTarget = hitLocation.position - transform.position;
        var explosionDistance = explosionToTarget.magnitude;
        var relativeDistance = (3 - explosionDistance) / 3; //TODO Remove constants
        var damage = relativeDistance * 50;
        damage = Mathf.Max(0f, damage);

        Model.HitPoints -= damage;
        if(Model.HitPoints < 0)
            Destroy();

        if(!Model.IsComputerControled)
            view.UpdateHealth(Model.HitPoints);
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

    private void InitializeView() {
        view = GetComponentInChildren<TankView>();
        view.StartingHealth = Model.HitPoints;
        view.UpdateHealth(Model.HitPoints);
    }
}
