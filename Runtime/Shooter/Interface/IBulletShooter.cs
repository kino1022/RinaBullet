
using RinaBullet.Shooter.Pattern;
using RinaBullet.Symbol;

namespace RinaBullet.Shooter.Interface {
    /// <summary>
    /// 弾丸の生成処理を行うクラスに対して約束するインタフェース
    /// </summary>
    public interface IBulletShooter {

        /// <summary>
        /// 弾丸の生成処理
        /// </summary>
        /// <param name="prefab">生成する弾丸のPrefab</param>
        /// <param name="pattern">弾丸の生成パターン</param>
        void Shoot(Bullet prefab, IShootPattern pattern);
    }
}