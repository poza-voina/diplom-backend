using System.Runtime.Serialization;

namespace Infrastructure.Enums;

public enum RouteExampleRecordStatus
{
	[EnumMember(Value = "ожидает")]
	Pending,
	[EnumMember(Value = "одобрено")]
	Approved,
	[EnumMember(Value = "отклонено")]
	Rejected
}
