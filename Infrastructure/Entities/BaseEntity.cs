using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public abstract class BaseEntity
{
	[Column("Id")]
	public long? Id { get; set; }
}
