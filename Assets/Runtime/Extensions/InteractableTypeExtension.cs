using AKhvalov.IdleFarm.Runtime.Data.Enums;

namespace AKhvalov.IdleFarm.Runtime.Extensions
{
    public static class InteractableTypeExtension
    {
        public static bool HasFlag(this InteractableType target, InteractableType interactableType)
        {
            return (interactableType & target) != 0;
        }
    }
}
