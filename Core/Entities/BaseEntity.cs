using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public abstract class BaseEntity(long id)
{
	[Column("Id")]
	public long Id { get; set; } = id;
}
