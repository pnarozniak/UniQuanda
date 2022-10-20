namespace UniQuanda.Core.Domain.Enums
{
	public class AbstractAdvancedEnum<T>
	{
		protected AbstractAdvancedEnum(T val)
		{
			this.Value = val;
		}
		public T Value { get; private set; }

		public static R FindByValue<R>(T val)
		{
			var type = typeof(R);
			var retVal = type
						.GetMethods()
						.Where(m => m.IsStatic)
						.Select(m => m.Invoke(null, null))
						.Where(m => ((AbstractAdvancedEnum<T>)m).Value.Equals(val))
						.FirstOrDefault();

			if (object.Equals(retVal, default(R)))
			{
				throw new KeyNotFoundException("Advanced enum with given value not found!");
			}
			return (R)retVal;
		}
    }
}

