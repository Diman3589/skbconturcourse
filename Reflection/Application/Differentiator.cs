using System;
using System.Linq.Expressions;

namespace Application
{
    public static class Differentiator
    {
        private static ParameterExpression GetParameter()
        {
            return Expression.Parameter(typeof(double), "x");
        }

        public static Expression<Func<double, double>> Differentiate(Expression expression)
        {
            if (expression is LambdaExpression)
            {
                var lambdaExpr = (LambdaExpression) expression;
                return Differentiate(lambdaExpr.Body);
            }

            if (expression is ConstantExpression)
            {
                return x => 0;
            }
            if (expression is ParameterExpression)
            {
                return x => 1;
            }

            if (expression is BinaryExpression)
            {
                var binaryExpression = (BinaryExpression) expression;
                switch (expression.NodeType)
                {
                    case ExpressionType.Add:
                    {
                        var left = Differentiate(binaryExpression.Left);
                        var right = Differentiate(binaryExpression.Right).Body;
                        return Expression.Lambda<Func<double, double>>(Expression.Add(left, right), GetParameter());
                    }
                    case ExpressionType.Multiply:
                    {
                        var left = Expression.Multiply(Differentiate(binaryExpression.Left).Body, binaryExpression.Right);
                        var right = Expression.Multiply(binaryExpression.Left, Differentiate(binaryExpression.Right).Body);
                        return Expression.Lambda<Func<double, double>>(Expression.Add(left, right), GetParameter());
                    }
                    default:
                        throw new ArgumentException("Only addition and multiply actions allowed!");
                }
            }

            if (expression is MethodCallExpression)
            {
                var methodCallExpr = (MethodCallExpression) expression;
                if (methodCallExpr.Method.Name != "Sin")
                    throw new ArgumentException("it is not sinus");
                var sinus = methodCallExpr.Arguments[0];
                var cosinus = Expression.Call(typeof(Math).GetMethod("Cos"), sinus);
                var resultExpr = Differentiate(sinus).Body;
                return Expression.Lambda<Func<double, double>>(Expression.Multiply(cosinus, resultExpr), GetParameter());
            }

            throw new ArgumentException("Incorrect expression!");
        }
    }
}