namespace DefaultNamespace
{
    using UnityEngine;

    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float zoomSpeed = 1f;
        [SerializeField] private float minZoom = 1f;
        [SerializeField] private float maxZoom = 10f;

        private Camera _camera;
        private Vector3 _startPosition;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _startPosition = transform.position;
        }

        private void Update()
        {
            MoveCamera();
            ZoomCamera();
            JumpToOrigin();
        }

        private void MoveCamera()
        {
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            var verticalInput = Input.GetAxisRaw("Vertical");

            var movement = new Vector3(horizontalInput, verticalInput, 0f) * (moveSpeed * Time.deltaTime);
            transform.Translate(movement);

            // Clamp camera position within level bounds (optional)
            // ...
        }

        private void ZoomCamera()
        {
            var scrollInput = Input.GetAxis("Mouse ScrollWheel");

            var newZoom = _camera.orthographicSize - scrollInput * zoomSpeed;
            _camera.orthographicSize = Mathf.Clamp(newZoom, minZoom, maxZoom);
        }

        private void JumpToOrigin()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.position = _startPosition;
            }
        }
    }

}