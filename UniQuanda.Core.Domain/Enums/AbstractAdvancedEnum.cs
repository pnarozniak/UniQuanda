namespace UniQuanda.Core.Domain.Enums
{
    public class AbstractAdvancedEnum<T>
    {
        protected AbstractAdvancedEnum(T val)
        {
            this.Value = val;
        }
        public T Value { get; private set; }
    }
}

