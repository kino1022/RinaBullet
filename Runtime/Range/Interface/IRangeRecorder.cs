using R3;

namespace RinaBullet.Range.Interface
{
    /// <summary>
    /// 生成物が進行した距離を記録するクラスに対して約束するインタフェース
    /// </summary>
    public interface IRangeRecorder
    {
        /// <summary>
        /// 進行した距離
        /// </summary>
        /// <value></value>
        ReadOnlyReactiveProperty<float> Range { get; }
    }
}