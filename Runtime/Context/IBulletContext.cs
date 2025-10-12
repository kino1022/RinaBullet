using UnityEngine;

namespace RinaBullet.Context {
    /// <summary>
    /// 弾丸の性能変化などの内容を記述するクラスに対して約束するインタフェース
    /// </summary>
    public interface IBulletContext {
        /// <summary>
        /// 弾丸の持つコンポーネントなどに対して変化を適用する
        /// </summary>
        /// <param name="obj"></param>
        void Apply(GameObject obj);
    }
}