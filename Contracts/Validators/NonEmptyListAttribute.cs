using System.ComponentModel.DataAnnotations;

namespace Services.Validators;

public class NonEmptyListAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        var list = value as IEnumerable<object>;
        return list != null && list.GetEnumerator().MoveNext();
    }
}