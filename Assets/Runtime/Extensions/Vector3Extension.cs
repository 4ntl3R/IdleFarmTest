using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Extensions
{
    public static class Vector3Extension
    {
        public static Vector3 TermDivision(this Vector3 a, Vector3 b) => new Vector3(a.x/b.x, a.y/b.y, a.z/b.z);

        public static Vector3 RandomVector(this Vector3 limits)
        {
            var result = new Vector3(
                Random.Range(-limits.x, limits.x), 
                Random.Range(-limits.y, limits.y), 
                Random.Range(-limits.z, limits.z));
            return result;
        }
    }
}
