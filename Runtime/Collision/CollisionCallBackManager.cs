using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace RinaBullet.Collision {
    public class CollisionCallBackManager : SerializedMonoBehaviour, ICollisionCallBackManager {
        
        [OdinSerialize]
        [LabelText("衝突時コールバック")]
        private List<ICollisionCallBackElement> m_callBacks = new List<ICollisionCallBackElement>();

        public void OnCollisionEnter(UnityEngine.Collision other) {
            foreach (var callBack in m_callBacks) {
                callBack.OnCollisionEnterCallBack(other);
            }
        }

        public void Add([NotNull] ICollisionCallBackElement callBack) {
            
            if (callBack == null) throw new ArgumentNullException(nameof(callBack));
            
            m_callBacks.Add(callBack);
            m_callBacks = SortByPriority(m_callBacks);
        }

        public void Remove(ICollisionCallBackElement callBack) {
            m_callBacks.Remove(callBack);
            m_callBacks = SortByPriority(m_callBacks);
        }

        public void Clear() {
            m_callBacks.Clear();
        }

        private List<ICollisionCallBackElement> SortByPriority(List<ICollisionCallBackElement> callbacks) {
            return callbacks.OrderBy(x => x.Priority).ToList();
        }
    }
}