using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialDamageReceiver : MonoBehaviour, IDamageReceiver
{
  [SerializeField] private List<Material> hitMaterials;
  [SerializeField] private float flashTime = 0.075f;

  private Material prevMaterial;
  private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

  private void Start()
  {
    GetComponentsInChildren(meshRenderers);

    // could be better, prob doesnt matter
    prevMaterial = meshRenderers[0].material;
  }

  private IEnumerator MaterialFlashRoutine()
  {
    foreach (Material material in hitMaterials)
    {
      meshRenderers.ForEach(renderer => renderer.material = material);
      yield return new WaitForSeconds(flashTime);
    }

    meshRenderers.ForEach(renderer => renderer.material = prevMaterial);
  }

  public void OnReceiveDamage(float percentHealthRemaining, GameObject damageDealer, float rawDamageDealt)
  {
    StartCoroutine(MaterialFlashRoutine());
  }
}
