using UnityEngine;

public class TankMovement : MonoBehaviour {
    public int PlayerNumber = 0;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
    //[HideInInspector]
    public float Speed = 12f;                 // How fast the tank moves forward and back.
    public float TurnSpeed = 180f;            // How fast the tank turns in degrees per second.
    [SerializeField]
    private AudioSource MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
    [SerializeField]
    private AudioClip EngineIdling;            // Audio to play when the tank isn't moving.
    [SerializeField]
    private AudioClip EngineDriving;           // Audio to play when the tank is moving.
    public float PitchRange = 0.2f;           // The amount by which the pitch of the engine noises can vary.
    public bool IsControledByComputer;

    protected string _movementAxisName;          // The name of the input axis for moving forward and back.
    protected string _turnAxisName;              // The name of the input axis for turning.
    protected Rigidbody _rigidbody;              // Reference used to move the tank.
    protected float _movementInputValue;         // The current value of the movement input.
    protected float _turnInputValue;             // The current value of the turn input.
    protected float _originalPitch;              // The pitch of the audio source at the start of the scene.


    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable() {
        // When the tank is turned on, make sure it's not kinematic.
        _rigidbody.isKinematic = false;
        

        // Also reset the input values.
        _movementInputValue = 0f;
        _turnInputValue = 0f;
    }


    private void OnDisable() {
        // When the tank is turned off, set it to kinematic so it stops moving.
        _rigidbody.isKinematic = true;
    }


    private void Start() {
        // The axes names are based on player number.
        _movementAxisName = "Vertical" + PlayerNumber;
        _turnAxisName = "Horizontal" + PlayerNumber;

        // Store the original pitch of the audio source.
        _originalPitch = MovementAudio.pitch;
    }


    //private void Update() {
    //    // Store the value of both input axes.
    //    if(IsControledByComputer)
    //        ComputerUpdate();
    //    else 
    //        PlayerUpdate();

    //    EngineAudio();
    //}

    private void PlayerUpdate() {
        _movementInputValue = Input.GetAxis(_movementAxisName);
        _turnInputValue = Input.GetAxis(_turnAxisName);
    }

    private void ComputerUpdate() {
        _movementInputValue = 1f;
        _turnInputValue = 0.1f;
    }


    private void EngineAudio() {
        // If there is no input (the tank is stationary)...
        if (Mathf.Abs(_movementInputValue) < 0.1f && Mathf.Abs(_turnInputValue) < 0.1f) {
            // ... and if the audio source is currently playing the driving clip...
            if (MovementAudio.clip != EngineDriving) return;
            // ... change the clip to idling and play it.
            MovementAudio.clip = EngineIdling;
            MovementAudio.pitch = Random.Range(_originalPitch - PitchRange, _originalPitch + PitchRange);
            MovementAudio.Play();
        }
        else {
            // Otherwise if the tank is moving and if the idling clip is currently playing...
            if (MovementAudio.clip != EngineIdling) return;
            // ... change the clip to driving and play.
            MovementAudio.clip = EngineDriving;
            MovementAudio.pitch = Random.Range(_originalPitch - PitchRange, _originalPitch + PitchRange);
            MovementAudio.Play();
        }
    }


    //protected virtual void FixedUpdate() {
    //    // Adjust the rigidbodies position and orientation in FixedUpdate.
    //    //Move();
    //    //Turn();
    //}


    //protected virtual void Move() {
    //    //// Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
    //    //Vector3 movement = transform.forward * MovementInputValue * Speed * Time.deltaTime;

    //    //// Apply this movement to the rigidbody's position.
    //    //Rigidbody.MovePosition(Rigidbody.position + movement);
    //}


    //protected virtual void Turn() {
    //    //// Determine the number of degrees to be turned based on the input, speed and time between frames.
    //    //float turn = TurnInputValue * TurnSpeed * Time.deltaTime;

    //    //// Make this into a rotation in the y axis.
    //    //Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

    //    //// Apply this rotation to the rigidbody's rotation.
    //    //Rigidbody.MoveRotation(Rigidbody.rotation * turnRotation);
    //}
}