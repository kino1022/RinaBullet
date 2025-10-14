using System;
using RinaBullet.Symbol;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace RinaBullet.Shooter.Pattern {
    [CreateAssetMenu(menuName = "RinaBullet/ShootPattern/単発")]
    public class SingleShootPattern : AShootPattern, IShootPattern {
        

        public override void Shoot(Bullet prefab, IObjectResolver resolver, Vector3 pos, Quaternion rot) {

            if (prefab is null) {
                throw new ArgumentNullException();
            }

            if (resolver is null) {
                throw new ArgumentNullException();
            }

            resolver.Instantiate(
                prefab,
                pos,
                CalculateSpread() * rot * prefab.transform.rotation
                );
        }
        
    }
}