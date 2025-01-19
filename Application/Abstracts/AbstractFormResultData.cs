using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Application.Interfaces;

public abstract class AbstractFormResultData
{
	public AbstractFormResultData() { }
	public virtual IEnumerable<PropertyInfo> GetFormFields()
	{
		return GetType()
			.GetProperties(BindingFlags.Instance | BindingFlags.Public)
			.Where(x => x.GetCustomAttribute<DisplayAttribute>() != null).ToList();
	}
	public abstract Errors Validate();
}


public class Errors
{
	public Dictionary<string, List<string>> Value { get; set; } = new();

	public bool IsErrors { get =>  Value.Count > 0; }

	public void Add(string property, string message)
	{
		if (!Value.Keys.Any(x => x == property))
		{
			Value.Add(property, new List<string>());
		}

		Value[property].Add(message);
	}

	public ICollection<string> GetErrorsByKey(string property)
	{
		return Value[property];
	}

	public bool IsErrorsByKey(string property)
	{
		return Value.ContainsKey(property);
	}

	public void Clear()
	{
		Value.Clear();
	}
}