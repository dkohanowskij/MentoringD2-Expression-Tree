using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TaskExpressionTree
{
    public class LambdaTransform : ExpressionVisitor
    {
        private Dictionary<string, Object> _parameters;
        public LambdaTransform(Dictionary<string, Object> parameters = null)
        {
            _parameters = parameters;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            return Expression.Lambda(Visit(node.Body), node.Parameters);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _parameters.ContainsKey(node.Name) ? Expression.Constant(_parameters[node.Name], node.Type) : base.VisitParameter(node);
        }
    }
}