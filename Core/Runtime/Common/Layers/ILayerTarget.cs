using UnityEngine;

namespace d4160.Runtime.Common
{
    public interface ILayerTarget
    {
        void AddOrRemoveTeleportLayer(int layer);
        void AddTeleportLayer(int layer);
        void RemoveTeleportLayer(int layer);
    }
}
