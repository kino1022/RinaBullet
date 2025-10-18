using System.Collections.Generic;

namespace RinaBullet.Collision {
    public interface ICollisionCallBackManager {

        /// <summary>
        /// 衝突時に呼び出されるコールバック
        /// </summary>
        IReadOnlyList<ICollisionCallBackElement> OnCollision { get; }
        
        /// <summary>
        /// オブジェクトが止まっている間に発火されるコールバックとその呼び出し間隔
        /// </summary>
        IReadOnlyDictionary<float,List<ICollisionCallBackElement>> OnStay { get; }
        
        /// <summary>
        /// 通過時に呼び出されるコールバック
        /// </summary>
        IReadOnlyList<ICollisionCallBackElement> OnExit { get; }
        
        void AddOnCollision(ICollisionCallBackElement element);
        
        void RemoveOnCollision(ICollisionCallBackElement element);
        
        void ClearOnCollision();

        void AddOnStay(int interval, ICollisionCallBackElement element);
        
        void RemoveOnStay(ICollisionCallBackElement element);
        
        void ClearOnStay();
        
        void AddOnExit(ICollisionCallBackElement element);
        
        void RemoveOnExit(ICollisionCallBackElement element);
        
        void ClearOnExit();
    }
}