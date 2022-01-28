using UnityEngine;

namespace Game.Scripts.Core_LevelManagement.CameraManagement
{
    public class CameraMovement : MonoBehaviour
    {
        public static CameraMovement Current;

        [SerializeField] public new CameraController camera;

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

        [SerializeField] private CameraStates cameraState;
        #endregion

        [SerializeField] private Vector2 staticPosition;
        [SerializeField] private Vector2 camOffset;
        [SerializeField] private float size;

        [Header("General information")]
        [SerializeField] private float smoothSpeed = 0.125f;

        private Vector2 _desiredPosition;

        // Following variables
        [SerializeField] public Transform objectToFollow;

        // Static variables
        private float _positionThreshold = .05f;

        // Moving variables
        private Vector2 _velocity;

        // TODO: Change this immediately pls 
        private bool _onlyX, _onlyY;

        // TODO: Delete these if not needed anymore
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P)) ChangeToFollowing(objectToFollow, false, false, size);
            if (Input.GetKeyDown(KeyCode.O)) ChangeToStatic(Vector2.zero, size);
            if (Input.GetKeyDown(KeyCode.I)) ChangeToMoving(new Vector2(12, 0), size);
        }

        private void Awake() => Current = this;

        private void Start()
        {
            if (cameraState == CameraStates.Static) ChangeToStatic(staticPosition + camOffset, size);
            if (cameraState == CameraStates.Following) ChangeToFollowing(objectToFollow, false, false, size);
            if(cameraState == CameraStates.Moving) ChangeToMoving(new Vector2(12, 0), size);
        }

        // Moves player smoothly
        private void FixedUpdate()
        {
            if (cameraState == CameraStates.Following) FollowingBehaviour();
            if (cameraState == CameraStates.Moving) MovingBehaviour();
        }

        // Moves transition smoothly
        private void LateUpdate()
        {
            if (cameraState == CameraStates.Static) StaticBehaviour();
        }

        // State Changing functions
        public void ChangeToMoving(Vector2 velocity, float viewSize = 7.7f)
        {
            cameraState = CameraStates.Moving;
            _velocity = velocity;
            camera.ScaleToDesiredSize(viewSize, smoothSpeed);
        }

        public void ChangeToStatic(Vector2 desiredPosition, float viewSize = 7.7f)
        {
            cameraState = CameraStates.Static;
            _desiredPosition = desiredPosition;
            camera.ScaleToDesiredSize(viewSize, smoothSpeed);
        }

        public void ChangeToFollowing(Transform following, bool onlyX = false, bool onlyY = false, float viewSize = 7.7f)
        {
            cameraState = CameraStates.Following;
            _onlyX = onlyX;
            _onlyY = onlyY;
            this.objectToFollow = following;
            camera.ScaleToDesiredSize(viewSize, smoothSpeed);
        }

        // TODO: add a square deadzone
        // Behaviour Functions
        private void FollowingBehaviour(bool onlyX = false, bool onlyY = false)
        {
            if (onlyX) MoveToLerp(new Vector2(objectToFollow.position.x, transform.position.y));
            else if (onlyY) MoveToLerp(new Vector2(transform.position.x, objectToFollow.position.y));
            //else MoveToLerp(following.position);

            MoveToLerp(objectToFollow.position);
        }

        private void StaticBehaviour()
        {
            // Moving to desired position
            if (Vector3.Distance(transform.position, _desiredPosition) < _positionThreshold) transform.position = _desiredPosition;
            else MoveToLerp(_desiredPosition);
        }

        private void MovingBehaviour()
        {
            Move(_velocity);
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
}
