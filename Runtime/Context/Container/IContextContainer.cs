using System.Collections.Generic;

namespace RinaBullet.Context.Container {
    /// <summary>
    /// 弾丸に対して適用する性能変化をまとめるコンテナクラスに対して約束するインタフェース
    /// </summary>
    public interface IContextContainer {
        
        IReadOnlyList<IBulletContext> Contexts { get; }
        
        void Add (IBulletContext context);
        
        void Remove (IBulletContext context);
        
        void Clear ();
    }
}