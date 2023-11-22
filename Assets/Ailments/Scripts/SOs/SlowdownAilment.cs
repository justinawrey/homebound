// using UnityEngine;

// [CreateAssetMenu(fileName = "SlowdownAilment", menuName = "ScriptableObjects/Ailments/Slowdown")]
// public class SlowdownAilment : BaseAilmentSO
// {
//   [SerializeField] private float slowdownMultiplier = 0.5f;
//   private float originalMass;

//   public override void OnStart(GameObject ailed, Collider agentCollider)
//   {
//     Rigidbody rigidbody = ailed.GetComponent<Rigidbody>();
//     if (rigidbody)
//     {
//       originalMass = rigidbody.mass;
//       rigidbody.mass = originalMass / slowdownMultiplier;
//     }
//   }

//   public override void OnFinish(GameObject ailed, Collider agentCollider)
//   {
//     Rigidbody rigidbody = ailed.GetComponent<Rigidbody>();
//     if (rigidbody)
//     {
//       rigidbody.mass = originalMass;
//     }
//   }
// }