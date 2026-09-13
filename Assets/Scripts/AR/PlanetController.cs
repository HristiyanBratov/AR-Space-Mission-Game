using UnityEngine;

namespace AR
{
    public class PlanetController : MonoBehaviour
    {
        [Header("Rotation")]
        [SerializeField] 
        private float rotationSpeed = 30f;

        public bool IsActivated { get; private set; }

        public void Activate()
        {
            if (IsActivated)
            {
                return;
            }

            IsActivated = true;

            Debug.Log("Planet visual activation triggered.");
        }

        private void Update()
        {
            if (!IsActivated)
            {
                return;
            }

            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
        }
    }
}