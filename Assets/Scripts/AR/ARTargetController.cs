using Mission;
using UnityEngine;
using Vuforia;

namespace AR
{
    public class ARTargetController : MonoBehaviour
    {
        [Header("AR Object")]
        [SerializeField] 
        private GameObject targetObject;

        [Header("Mission")]
        [SerializeField] 
        private MissionManager missionManager;
        
        [Header("Persistent Object")]
        [SerializeField]
        private PlanetController planetController;
        
        [Header("Visual Reaction")]
        [SerializeField]
        private AstronautController astronautController;

        private enum TargetType
        {
            Planet,
            Astronaut,
            Spaceship
        }

        [SerializeField] 
        private TargetType targetType;

        private ObserverBehaviour _observerBehaviour;

        private void Awake()
        {
            _observerBehaviour = GetComponent<ObserverBehaviour>();

            if (_observerBehaviour == null)
            {
                Debug.LogError(
                    $"[{nameof(ARTargetController)}] " +
                    $"No ObserverBehaviour found on {gameObject.name}."
                );

                return;
            }

            _observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        private void OnDestroy()
        {
            if (_observerBehaviour != null)
            {
                _observerBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
            }
        }

        private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
        {
            Debug.Log(
                $"Target: {behaviour.TargetName} | " +
                $"Status: {status.Status} | " +
                $"StatusInfo: {status.StatusInfo}"
            );

            var isTracked = status.Status is Status.TRACKED or Status.EXTENDED_TRACKED;
            
            var isDirectlyTracked = status.Status is Status.TRACKED;

            UpdateTargetVisibility(isTracked, isDirectlyTracked);

            UpdateMissionManager(isTracked);
        }

        private void UpdateTargetVisibility(bool isTracked, bool isDirectlyTracked)
        {
            if (targetObject == null)
            {
                return;
            }
            
            // The Planet becomes persistent after activation:
            if (targetType == TargetType.Planet && planetController != null && planetController.IsActivated)
            {
                targetObject.SetActive(true);
                return;
            }
            
            // Before activation, only direct image tracking
            // should control the visibility of the object.
            targetObject.SetActive(isDirectlyTracked);
        }

        private void UpdateMissionManager(bool isTracked)
        {
            if (missionManager == null)
            {
                return;
            }

            switch (targetType)
            {
                case TargetType.Planet:
                    missionManager.SetPlanetDetected(isTracked);
                    break;

                case TargetType.Astronaut:
                    missionManager.SetAstronautDetected(isTracked);

                    if (isTracked && 
                        missionManager.CurrentState == MissionManager.MissionState.MissionActive &&
                        missionManager.PlanetObjectiveCompleted)
                    {
                        if (astronautController != null)
                        {
                            astronautController.Activate();
                        }
                    }
                    
                    break;

                case TargetType.Spaceship:
                    missionManager.SetSpaceshipDetected(isTracked);
                    break;
            }
        }
    }
}