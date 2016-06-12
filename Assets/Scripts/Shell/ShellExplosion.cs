using Assets.Scripts;
using Assets.Scripts.Tank.Enums;
using UnityEngine;


public class ShellExplosion : MonoBehaviour
{
    [SerializeField]
    private GameObject ExplosionPrefab;
    private ParticleSystem ExplosionParticles;

    private void Awake() {
        var tempParticles = ExplosionPrefab.GetComponent<ParticleSystem>();
        ExplosionParticles = Instantiate(tempParticles);
        ExplosionParticles.gameObject.SetActive(false);
    }

    private void OnTriggerEnter (Collider other){
        Dispatcher.Dispatch(GameEventEnum.DemageObject, transform);

        ExplosionParticles.gameObject.SetActive(true);
        ExplosionParticles.transform.position = transform.position;
        ExplosionParticles.Play();

        Destroy (ExplosionParticles.gameObject, ExplosionParticles.duration);
        Destroy(gameObject);
    }
}