using System;

namespace AKhvalov.IdleFarm.Runtime.Data.Enums
{
    [Serializable]
    [Flags]
    public enum InteractableType
    {
        Loot = 1 << 1,
        Gather = 1 << 2,
        Deliver = 1 << 3,
    }
}
