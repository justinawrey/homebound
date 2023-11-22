// using UnityEngine;

// [CreateAssetMenu(fileName = "PoisonAilment", menuName = "ScriptableObjects/Ailments/Poison")]
// public class PoisonAilment : BaseAilmentSO
// {
//   [SerializeField] private float poisonDamage = 1f;

//   public override void Tick(GameObject ailed, Collider agentCollider)
//   {
//     Health health = ailed.GetComponent<Health>();
//     if (health)
//     {
//       health.DecrementHealth(poisonDamage, agentCollider);
//     }
//   }
// }