using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement current;

    [SerializeField] public CameraController Camera;

    #region CameraStates
    private enum CameraStates
    {
        // Follows the player
        Following,
        // Doesn't move
        Static,
        // Moves indepently of the player
        Moving
    }

    [SerializeField] private CameraStates CameraState;
    #endregion

    [SerializeField] private Vector2 staticPosition;
    [SerializeField] private Vector2 camOffset;
    [SerializeField] private float viewSize;

    [Header("General information")]
    [SerializeField] private float smoothSpeed = 0.125f;

    private Vector2 desiredPosition;

    // Following variables
    [SerializeField] public Transform objectToFollow;

    // Static variables
    private float positionThreshold = .05f;

    // Moving variables
    private Vector2 velocity;

    // TODO: Change this immediately pls 
    private bool OnlyX, OnlyY;

    // TODO: Delete these if not needed anymore
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) ChangeToFollowing(objectToFollow, false, false, viewSize);
        if (Input.GetKeyDown(KeyCode.O)) ChangeToStatic(Vector2.zero, viewSize);
        if (Input.GetKeyDown(KeyCode.I)) ChangeToMoving(new Vector2(12, 0), viewSize);
    }

    private void Awake() => current = this;

    private void Start()
    {
        if (CameraState == CameraStates.Static) ChangeToStatic(staticPosition + camOffset, viewSize);
        if (CameraState == CameraStates.Following) ChangeToFollowing(objectToFollow, false, false, viewSize);
        if(CameraState == CameraStates.Moving) ChangeToMoving(new Vector2(12, 0), viewSize);
    }

    // Moves player smoothly
    private void FixedUpdate()
    {
        if (CameraState == CameraStates.Following) FollowingBehaviour();
        if (CameraState == CameraStates.Moving) MovingBehaviour();
    }

    // Moves transition smoothly
    private void LateUpdate()
    {
        if (CameraState == CameraStates.Static) StaticBehaviour();
    }

    // State Changing functions
    public void ChangeToMoving(Vector2 velocity, float viewSize = 7.7f)
    {
        CameraState = CameraStates.Moving;
        this.velocity = velocity;
        Camera.ScaleToDesiredSize(viewSize, smoothSpeed);
    }

    public void ChangeToStatic(Vector2 desiredPosition, float viewSize = 7.7f)
    {
        CameraState = CameraStates.Static;
        this.desiredPosition = desiredPosition;
        Camera.ScaleToDesiredSize(viewSize, smoothSpeed);
    }

    public void ChangeToFollowing(Transform objectToFollow, bool onlyX = false, bool onlyY = false, float viewSize = 7.7f)
    {
        CameraState = CameraStates.Following;
        OnlyX = onlyX;
        OnlyY = onlyY;
        this.objectToFollow = objectToFollow;
        Camera.ScaleToDesiredSize(viewSize, smoothSpeed);
    }

    // TODO: add a square deadzone
    // Behaviour Functions
    private void FollowingBehaviour(bool onlyX = false, bool onlyY = false)
    {
        if (onlyX) MoveToLerp(new Vector2(objectToFollow.position.x, transform.position.y));
        else if (onlyY) MoveToLerp(new Vector2(transform.position.x, objectToFollow.position.y));
        //else MoveToLerp(objectToFollow.position);

        MoveToLerp(objectToFollow.position);
    }

    private void StaticBehaviour()
    {
        // Moving to desired position
        if (Vector3.Distance(transform.position, desiredPosition) < positionThreshold) transform.position = desiredPosition;
        else MoveToLerp(desiredPosition);
    }

    private void MovingBehaviour()
    {
        Move(velocity);
    }
    
    // Mover functions
    private void MoveToLerp(Vector2 desiredPosition) 
    {
        var despo = new Vector3(desiredPosition.x, desiredPosition.y, -10);
        transform.position = Vector3.Lerp(transform.position, despo, smoothSpeed);  
    }

    private void Move(Vector2 vel) 
        => transform.position += new Vector3(vel.x, vel.y, -10) * Time.deltaTime;
}
