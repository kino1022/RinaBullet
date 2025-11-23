using System;
using RinaBullet.Symbol;
using UnityEngine;

namespace RinaBullet.Context {
    /// <summary>
    /// ディープコピーでの利用を前提とした、弾丸の性能変化などの内容を記述する基底クラス
    /// </summary>
    [Serializable]
    public abstract record ABulletContext : IBulletContext {
        
        public abstract void Apply(GameObject bullet);
        
    }
}