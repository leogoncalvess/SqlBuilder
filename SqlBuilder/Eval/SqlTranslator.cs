using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SqlBuilder.Eval
{
    public class SqlTranslator
    {
        public string BuildExpression(IQueryable query) => BuildExpression(query.Expression);

        public string BuildExpression(Expression expression)
        {
            switch (expression)
            {
                case MethodCallExpression methodCallExpression:
                    var typesExpression = (ConstantExpression)methodCallExpression.Arguments[0];
                    var type = typesExpression.Value.GetType().GenericTypeArguments[0];
                    var method = FormatMethod(methodCallExpression.Method.Name);

                    var sqlStart = $"SELECT * FROM [{type.Name}] {method} ";

                    var condition = (UnaryExpression)methodCallExpression.Arguments[1];
                    var lambda = (LambdaExpression)condition.Operand;

                    var sqlEnd = BuildExpression(lambda.Body);

                    return sqlStart + sqlEnd;
                case BinaryExpression binaryExpression:
                    var left = BuildExpression(binaryExpression.Left);
                    var right = BuildExpression(binaryExpression.Right);
                    var binaryOperator = GetExpressionTypeString(binaryExpression.NodeType);
                    return $"({left} {binaryOperator} {right})";
                case ConstantExpression constantExpression:
                    return FormatValue(constantExpression.Value);
                case MemberExpression memberExpression:
                    return GetMemberExpressionString(memberExpression);
                default:
                    return null;
            }
        }

        private string GetExpressionTypeString(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.AndAlso:
                    return "AND";
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.Not:
                    return "NOT";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.OrElse:
                    return "OR";
                default:
                    return null ;
            }
        }

        private string GetMemberExpressionString(MemberExpression expression)
        {
            switch (expression.Member)
            {
                case PropertyInfo propertyInfo:
                    return $"[{propertyInfo.Name}]";
                case FieldInfo fieldInfo:
                    return FormatValue(GetExpressionValue(expression));
                default:
                    return null;
            }
        }

        private string GetExpressionValue(Expression expression)
        {
            var generalExpression = Expression.Convert(expression, typeof(object));
            var lambda = Expression.Lambda<Func<object>>(generalExpression);
            var function = lambda.Compile();
            var result = function().ToString();
            return result;
        }

        private string FormatMethod(string value)
        {
            switch (value)
            {
                case "Where":
                    return "WHERE";
                case "OrderBy":
                    return "ORDER BY";
                default:
                    return null;
            }
        }

        private string FormatValue(object value) => $"'{value.ToString()}'";
    }
}