using UnityEngine;
using System.Collections;

namespace dt {
    public class CameraController : MonoBehaviour {

        public GameObject levelBoundary;

        public float moveSpeed = 1f;
        public uint moveScreenOffset = 10;
        public float moveSmoothTime = 0.2f;

        public float zoomSpeed = 1f;
        public float minZoom = 4f;
        public float maxZoom = 20f;
        public float zoomSmoothTime = 0.2f;

        public bool mouseMoveEnabled = true;

        Camera cameraComponent;
        Vector3 targetPosition;
        Vector3 positionVelocity;
        float targetZoom;
        float zoomVelocity;

        void Start() {
            cameraComponent = GetComponent<Camera>();
            targetPosition = transform.localPosition;
            targetZoom = cameraComponent.orthographicSize;
        }

        void Update() {
            var movement = new Vector3();
            movement += KeyboardMove() * cameraComponent.orthographicSize;
            if (mouseMoveEnabled) {
                movement += MouseMove() * cameraComponent.orthographicSize;
            }
            targetPosition += movement;
            transform.localPosition = Vector3.SmoothDamp(
                    transform.localPosition, targetPosition,
                    ref positionVelocity, moveSmoothTime);
            // TODO: Clamp the movement to levelBoundary.
            MouseWheelZoom();
        }

        Vector3 KeyboardMove() {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            var movement = new Vector3(h, 0f, v);
            return movement.normalized * moveSpeed * Time.deltaTime;
        }

        Vector3 MouseMove() {
            var movement = new Vector3();
            Vector3 mousePosition = Input.mousePosition;
            if ((int) mousePosition.x < moveScreenOffset) {
                movement.x -= 1f;
            } else if ((int) mousePosition.x > Screen.width - moveScreenOffset) {
                movement.x += 1f;
            }
            if ((int) mousePosition.y < moveScreenOffset) {
                movement.z -= 1f;
            } else if ((int) mousePosition.y > Screen.height - moveScreenOffset) {
                movement.z += 1f;
            }
            return movement.normalized * moveSpeed * Time.deltaTime;
        }

        void MouseWheelZoom() {
            float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
            if (mouseScroll > 0 && targetZoom > minZoom) {
                targetZoom -= zoomSpeed;
            } else if (mouseScroll < 0 && targetZoom < maxZoom) {
                targetZoom += zoomSpeed;
            }
            float newZoom = Mathf.SmoothDamp(
                    cameraComponent.orthographicSize, targetZoom,
                    ref zoomVelocity, zoomSmoothTime);
            cameraComponent.orthographicSize = newZoom;
        }
    }
}
