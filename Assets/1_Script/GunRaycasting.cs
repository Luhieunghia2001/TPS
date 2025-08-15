using UnityEngine;

public class GunRaycasting : MonoBehaviour
{
    [SerializeField] private Transform _firingPos;
    [SerializeField] private Transform _aimingCamera;
    [SerializeField] private Transform _hitMarkerPrefab;
    [SerializeField] private Transform _hitZombiePrefab;

    public void PerformRaycast(float damage)
    {
        Ray aimingRay = new(_aimingCamera.position, _aimingCamera.forward);

        if (Physics.Raycast(aimingRay, out var cameraHit))
        {
            Vector3 directionToTarget = (cameraHit.point - _firingPos.position).normalized;

            Ray firingRay = new(_firingPos.position, directionToTarget);

            if (Physics.Raycast(firingRay, out var firingHit))
            {
                if (firingHit.collider.CompareTag("Zombie"))
                {
                    var zombieHealth = firingHit.collider.GetComponentInParent<ZomebieHeal>();
                    if (zombieHealth != null)
                    {
                        zombieHealth.TakeDamage(damage);
                    }

                    var hitZombie = Instantiate(
                        _hitZombiePrefab,
                        firingHit.point,
                        Quaternion.LookRotation(firingHit.normal),
                        firingHit.collider.transform
                    );
                    hitZombie.localScale /= firingHit.collider.transform.lossyScale.x;
                }
                else
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
}
