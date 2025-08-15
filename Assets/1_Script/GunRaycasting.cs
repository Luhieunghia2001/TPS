using UnityEngine;

public class GunRaycasting : MonoBehaviour
{
    [SerializeField] private Transform _firingPos;
    [SerializeField] private Transform _aimingCamera;
    [SerializeField] private Transform _hitMarkerPrefab;

    public void PerformRaycast(float damage)
    {
        Ray aimingRay = new(_aimingCamera.position, _aimingCamera.forward);

        if (Physics.Raycast(aimingRay, out var cameraHit))
        {
            Vector3 directionToTarget = (cameraHit.point - _firingPos.position).normalized;

            Ray firingRay = new(_firingPos.position, directionToTarget);

            if (Physics.Raycast(firingRay, out var firingHit))
            {
                Debug.Log("Firing ray hit: " + firingHit.collider.name);

                var zombieHealth = firingHit.collider.GetComponentInParent<ZomebieHeal>();
                if (zombieHealth != null)
                {
                    Debug.Log("Found ZomebieHeal component on: " + firingHit.collider.name);
                    zombieHealth.TakeDamage(damage);
                }

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
    