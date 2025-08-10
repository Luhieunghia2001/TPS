using UnityEngine;

public class GunAnimationController : MonoBehaviour
{
    [SerializeField] private int fireLayerIndex = 1;
    [SerializeField] private int reloadLayerIndex = 2;

    [SerializeField] private float fadeSpeed = 5f;


    private float reloadTargetWeight = 0f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();


    }

    private void Update()
    {
        float currentReload = animator.GetLayerWeight(reloadLayerIndex);

        animator.SetLayerWeight(reloadLayerIndex, Mathf.MoveTowards(currentReload, reloadTargetWeight, fadeSpeed * Time.deltaTime));
    }

    public void StartFire()
    {

        animator.SetLayerWeight(fireLayerIndex, 1f);
    }

    public void StopFire()
    {
        animator.SetLayerWeight(fireLayerIndex, 0f);
    }

    public void Reload()
    {
        reloadTargetWeight = 1f;
    }

    public void StopReload()
    {
        reloadTargetWeight = 0f;
    }
}
