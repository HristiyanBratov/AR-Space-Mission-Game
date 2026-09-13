using Mission;
using UnityEngine;

namespace AR
{
    public class ARObjectInteraction : MonoBehaviour
    {
        [Header("Mission")]
        [SerializeField] 
        private MissionManager missionManager;
        
        [Header("Visual Feedback")]
        [SerializeField]
        private PlanetController planetController;

        private void OnMouseDown()
        {
            Debug.Log($"AR object clicked: {gameObject.name}");

            if (missionManager == null)
            {
                Debug.LogError(
                    $"ARObjectInteraction: Mission Manager is not assigned on {gameObject.name}."
                );

                return;
            }

            missionManager.PlanetActivated();

            if (planetController != null)
            {
                planetController.Activate();
            }
        }
    }
}