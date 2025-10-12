using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace RinaBullet.Context.Container {
    public class ContextContainer : SerializedMonoBehaviour, IContextContainer {

        [OdinSerialize]
        [LabelText("弾丸に対して適用する修飾")]
        private List<IBulletContext> m_contexts = new();
        
        public IReadOnlyList<IBulletContext> Contexts => m_contexts;

        public void Add([NotNull] IBulletContext context) {
            if (context == null) throw new ArgumentNullException(nameof(context));
            m_contexts.Add(context);
        }

        public void Remove([NotNull] IBulletContext context) {
            if (context == null) throw new ArgumentNullException(nameof(context));
            m_contexts.Remove(context);
        }

        public void Clear() {
            m_contexts.Clear();
        }
    }
}