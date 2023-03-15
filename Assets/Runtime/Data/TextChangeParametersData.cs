namespace AKhvalov.IdleFarm.Runtime.Data
{
    public struct TextChangeParametersData
    {
        private const float ColorReturnDurationDivider = 4f;
        
        public TextChangeParametersData(int startValue, int endValue, float duration)
        {
            StartValue = startValue;
            EndValue = endValue;
            Duration = duration;
        }

        public int StartValue { get; }
        public int EndValue { get; }
        public float Duration { get; }
    }
}
