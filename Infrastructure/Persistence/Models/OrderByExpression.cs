using Domain.Types;
using Infrastructure.Persistence.Abstractions;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Models;

public class OrderByExpression<TEntity> : IOrderByExpression<TEntity> where TEntity : class
{
    public Expression<Func<TEntity, object>> OrderExpression { get; set; } = null!;
    public FilterType FilterType { get; set; } = FilterType.None;
}
