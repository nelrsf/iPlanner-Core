using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;

public class UtcDateTimeConverter : ValueConverter<DateTime, DateTime>
{
    private static readonly Expression<Func<DateTime, DateTime>> convertFromProviderExpression = dateTime =>
        ConvertFromProvider(dateTime);

    private static readonly Expression<Func<DateTime, DateTime>> convertToProviderExpression = dateTime =>
        ConvertToProvider(dateTime);

    public UtcDateTimeConverter()
        : base(convertToProviderExpression, convertFromProviderExpression)
    {
    }

    private static new DateTime ConvertFromProvider(DateTime dateTime)
    {
        return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
    }

    private static new DateTime ConvertToProvider(DateTime dateTime)
    {
        return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
    }
}
