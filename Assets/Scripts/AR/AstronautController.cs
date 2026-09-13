using UnityEngine;

namespace AR
{
    public class AstronautController : MonoBehaviour
    {
        [Header("Floating")]
        [SerializeField] 
        private float floatHeight = 0.10f;
        
        [SerializeField] 
        private float floatSpeed = 2f;

        [Header("Rotation")]
        [SerializeField] 
        private float rotationSpeed = 20f;

        private Vector3 _initialPosition;
        private bool _isActivated;

        private void Awake()
        {
            _initialPosition = transform.localPosition;
        }

        public void Activate()
        {
            if (_isActivated)
            {
                return;
            }

            _isActivated = true;

            Debug.Log("Astronaut visual activation triggered.");
        }

        private void Update()
        {
            if (!_isActivated)
            {
                return;
            }

            var verticalOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;

            transform.localPosition = _initialPosition + Vector3.up * verticalOffset;

            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
        }
    }
}