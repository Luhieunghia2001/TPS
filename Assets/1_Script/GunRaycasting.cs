using UnityEngine;

public class GunRaycasting : MonoBehaviour
{
    [SerializeField] private Transform _firingPos;
    [SerializeField] private Transform _aimingCamera;
    [SerializeField] private Transform _hitMarkerPrefab;
    [SerializeField] private int _damage;

    public void PerformRaycast()
    {
        Ray aimingRay = new(_aimingCamera.position, _aimingCamera.forward);

        if (Physics.Raycast(aimingRay, out var cameraHit))
        {
            Vector3 directionToTarget = (cameraHit.point - _firingPos.position).normalized;

            Ray firingRay = new(_firingPos.position, directionToTarget);

            if (Physics.Raycast(firingRay, out var firingHit))
            {
                var hitMarker = Instantiate(
                    _hitMarkerPrefab,
                    firingHit.point,
                    Quaternion.LookRotation(firingHit.normal),
                    firingHit.collider.transform
                );
                hitMarker.localScale /= firingHit.collider.transform.lossyScale.x;

                


            }
        }
    }
}
    