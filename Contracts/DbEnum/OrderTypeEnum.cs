using System.Runtime.Serialization;

namespace Services.DbEnum;

public enum OrderTypeEnum
{
    [EnumMember(Value = "Ascending")]
    Ascending = 1,
    [EnumMember(Value = "Descending")]
    Descending = 2
}