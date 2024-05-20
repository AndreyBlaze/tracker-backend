using Domain.Types;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Abstractions;

public interface IOrderByExpression<TEntity> where TEntity : class
{
    Expression<Func<TEntity, object>> OrderExpression { get; set; }
    FilterType FilterType { get; set; }
}
