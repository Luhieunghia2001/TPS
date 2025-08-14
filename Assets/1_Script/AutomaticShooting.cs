using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class AutomaticShooting : MonoBehaviour
{
    [SerializeField] private InputActionReference _shootAction;
    [SerializeField] private float _cooldown;

    [SerializeField] private int currentAmmo;
    [SerializeField] private int magazineSize;
    [SerializeField] private int totalAmmo;

    [SerializeField] private float reloadTime;
    private bool isReloading = false;
    private bool isShooting = false;

    [SerializeField] private GunRaycasting _gunRaycasting;


    public UnityEvent<int, int, int> OnAmmoChanged;

    private float _lastShotTime;

    public UnityEvent OnShoot;

    public bool Fire = true;

    public bool IsShooting => isShooting && currentAmmo > 0 && Fire && !isReloading;
    public bool IsReloading => isReloading;

    private Animator animator;
    private GunAnimationController _gunAnimationController;

    [SerializeField] private CinemachineRotationComposer _rotationComposer;

    [SerializeField] private float targetYWhenShooting = 0.7f;
    [SerializeField] private float normalY = 0.5f;
    [SerializeField] private float yChangeSpeed = 3f;
    private float _lastRecoilTime;

    private void Start()
    {
        currentAmmo = magazineSize;
        OnAmmoChanged?.Invoke(currentAmmo, totalAmmo, magazineSize);

        animator = GetComponent<Animator>();
        _gunAnimationController = GetComponent<GunAnimationController>();
    }

    private void Update()
    {
        isShooting = _shootAction.action.IsPressed();

        if (_rotationComposer != null)
        {
            var comp = _rotationComposer.Composition;

            if (isShooting && Fire && currentAmmo > 0 && !isReloading)
            {
                if (Time.time - _lastRecoilTime >= _cooldown && FinishCooldown())
                {
                    comp.ScreenPosition.y += 0.05f;
                    comp.ScreenPosition.y = Mathf.Clamp(comp.ScreenPosition.y, normalY, targetYWhenShooting);

                    _lastRecoilTime = Time.time;
                }
                else
                {
                    comp.ScreenPosition.y = Mathf.Lerp(comp.ScreenPosition.y, normalY, Time.deltaTime * yChangeSpeed);
                }
            }
            else
            {
                comp.ScreenPosition.y = Mathf.Lerp(comp.ScreenPosition.y, normalY, Time.deltaTime * yChangeSpeed);
            }

            _rotationComposer.Composition = comp;
        }

        Shooting();

        if (!isShooting)
        {
            _gunAnimationController.StopFire();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void AddAmmo()
    {
        totalAmmo += 500;
        OnAmmoChanged?.Invoke(currentAmmo, totalAmmo, magazineSize);
    }

    private void Shooting()
    {
        if (isShooting && Fire && !isReloading && currentAmmo > 0 && FinishCooldown())
        {
            Shoot();
            _gunAnimationController.StartFire();
            DesBullet();
            _lastShotTime = Time.time;
        }
    }

    void DesBullet()
    {
        currentAmmo--;
        OnAmmoChanged?.Invoke(currentAmmo, totalAmmo, magazineSize);
        if (currentAmmo == 0)
        {
            Fire = false;
        }
    }

    void Reload()
    {
        if (isReloading || currentAmmo == magazineSize || totalAmmo == 0)
        {
            return;
        }
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        Fire = false;

        _gunAnimationController.Reload();
        yield return new WaitForSeconds(reloadTime);

        int neededAmmo = magazineSize - currentAmmo;
        if (totalAmmo >= neededAmmo)
        {
            currentAmmo += neededAmmo;
            totalAmmo -= neededAmmo;
        }
        else
        {
            currentAmmo += totalAmmo;
            totalAmmo = 0;
        }

        _gunAnimationController.StopReload();
        isReloading = false;
        Fire = true;
        OnAmmoChanged?.Invoke(currentAmmo, totalAmmo, magazineSize);
    }

    private bool FinishCooldown() => Time.time - _lastShotTime >= _cooldown;

    private void Shoot()
    {
        OnShoot.Invoke();
        float damageFromPlayerStats = PlayerStats.Instance.PlayerData.damage;
        _gunRaycasting.PerformRaycast(damageFromPlayerStats);
    }

    public void ApplyItemBonus(float bonusCooldown, int bonusMagazineSize, int bonusTotalAmmo)
    {
        _cooldown -= bonusCooldown;
        magazineSize += bonusMagazineSize;
        totalAmmo += bonusTotalAmmo;

        if (_cooldown < 0)
        {
            _cooldown = 0;
        }

        OnAmmoChanged?.Invoke(currentAmmo, totalAmmo, magazineSize);
    }
}