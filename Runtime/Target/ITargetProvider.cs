using R3;
using UnityEngine;

namespace RinaBullet.Target {
    /// <summary>
    /// 弾丸が対象にするターゲットを保持するクラスに対して約束するインターフェース
    /// </summary>
    public interface ITargetProvider {
        
        ReadOnlyReactiveProperty<GameObject> Target { get; }
        
    }
}