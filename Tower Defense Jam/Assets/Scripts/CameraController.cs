using UnityEngine;
using System.Collections;

namespace dt {
    public class CameraController : MonoBehaviour {

        public GameObject levelBoundary;

        public float moveSpeed = 1f;
        public uint moveOffset = 5;

        public float zoomSpeed = 1f;
        public float minZoom = 4f;
        public float maxZoom = 20f;
        public float zoomSmoothTime = 0.3f;

        Camera cameraComponent;
        float targetZoom;
        float zoomVelocity = 0f;

        void Start() {
            cameraComponent = GetComponent<Camera>();
            targetZoom = cameraComponent.orthographicSize;
        }

        void Update() {
            transform.position += KeyboardMove();
            transform.position += MouseMove();
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
            var movement = new Vector3(0, 0, 0);
            Vector3 mousePosition = Input.mousePosition;
            if ((int) mousePosition.x < moveOffset) {
                movement.x -= 1f;
            } else if ((int) mousePosition.x > Screen.width - moveOffset) {
                movement.x += 1f;
            }
            if ((int) mousePosition.y < moveOffset) {
                movement.y -= 1f;
            } else if ((int) mousePosition.y > Screen.height - moveOffset) {
                movement.y += 1f;
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
