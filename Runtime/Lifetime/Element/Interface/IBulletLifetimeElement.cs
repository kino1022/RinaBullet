using System;
using VContainer;
using VContainer.Unity;

namespace RinaBullet.Lifetime.Element.Interface
{
    /// <summary>
    /// 生成物の生存期間に関わる要素に対して約束するインタフェース
    /// </summary>
    public interface IBulletLifetimeElement : IStartable, IDisposable
    {
        /// <summary>
        /// 弾丸の生存条件を満たさなくなった場合に発火されるコールバック
        /// </summary>
        /// <value></value>
        Action IsDead { get; set; }

        void Initialize(IObjectResolver resolver);
    }
}