using RinaBullet.Symbol;
using UnityEngine;
using VContainer;

namespace RinaBullet.Shooter.Pattern {
    /// <summary>
    /// 弾丸の生成パターンを表現するクラスに対して約束するインタフェース
    /// </summary>
    public interface IShootPattern {

        void Shoot(Bullet prefab, IObjectResolver resolver, Vector3 pos, Quaternion rot);
        
    }
}