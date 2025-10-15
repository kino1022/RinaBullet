namespace RinaBullet.Collision {
    public interface ICollisionCallBackManager {
        
        void Add (ICollisionCallBackElement element);
        
        void Remove (ICollisionCallBackElement element);
        
        void Clear ();
        
    }
}