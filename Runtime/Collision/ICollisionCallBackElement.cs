namespace RinaBullet.Collision {
    public interface ICollisionCallBackElement {
        
        /// <summary>
        /// コールバック処理の優先度
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// 衝突時のコールバック処理
        /// </summary>
        /// <param name="other"></param>
        void OnCollisionEnterCallBack(UnityEngine.Collision other);
        
    }
}