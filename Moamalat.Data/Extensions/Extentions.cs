using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;


namespace Moamalat.Data
{

    public static class Extensions
    {
        public static Expression<Func<T, bool>> strToFunc<T>(string propName, string opr, string value, Expression<Func<T, bool>> expr = null)
        {
            Expression<Func<T, bool>> func = null;
            try
            {
                var type = typeof(T);
                var prop = type.GetProperty(propName);
                ParameterExpression tpe = Expression.Parameter(typeof(T));
                Expression left = Expression.Property(tpe, prop);
                Expression right = Expression.Convert(ToExprConstant(prop, value), prop.PropertyType);
                Expression<Func<T, bool>> innerExpr = Expression.Lambda<Func<T, bool>>(ApplyFilter(opr, left, right), tpe);
                if (expr != null)
                    innerExpr = innerExpr.And(expr);
                func = innerExpr;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return func;
        }
        private static Expression ToExprConstant(PropertyInfo prop, string value)
        {
            object val = null;

            try
            {
                switch (prop.Name)
                {
                    case "System.Guid":
                        val = Guid.NewGuid();
                        break;
                    default:
                        {
                            val = Convert.ChangeType(value, prop.PropertyType);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Expression.Constant(val);
        }
        private static BinaryExpression ApplyFilter(string opr, Expression left, Expression right)
        {
            BinaryExpression InnerLambda = null;
            switch (opr)
            {
                case "==":
                case "=":
                    InnerLambda = Expression.Equal(left, right);
                    break;
                case "<":
                    InnerLambda = Expression.LessThan(left, right);
                    break;
                case ">":
                    InnerLambda = Expression.GreaterThan(left, right);
                    break;
                case ">=":
                    InnerLambda = Expression.GreaterThanOrEqual(left, right);
                    break;
                case "<=":
                    InnerLambda = Expression.LessThanOrEqual(left, right);
                    break;
                case "!=":
                    InnerLambda = Expression.NotEqual(left, right);
                    break;
                case "&&":
                    InnerLambda = Expression.And(left, right);
                    break;
                case "||":
                    InnerLambda = Expression.Or(left, right);
                    break;
            }
            return InnerLambda;
        }

        public static Expression<Func<T, TResult>> And<T, TResult>(this Expression<Func<T, TResult>> expr1, Expression<Func<T, TResult>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, TResult>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Func<T, TResult> ExpressionToFunc<T, TResult>(this Expression<Func<T, TResult>> expr)
        {
            var res = expr.Compile();
            return res;
        }


        public static Func<T, bool> CompileExpression<T>(string condition)
        {
            // Define parameter for the lambda expression
            var parameter = Expression.Parameter(typeof(T), "TEntity");

            try
            {
                var lambdaExpression = DynamicExpressionParser.ParseLambda(false, new[] { parameter }, typeof(bool), condition);

                // Compile the expression tree into a delegate
                var compiledExpression = (Func<T, bool>)lambdaExpression.Compile();

                return compiledExpression;
            }
            catch (Exception ex)
            {

                throw;
            }
            // Parse the condition string into an expression tree

        }

        //public static Func<T, bool> CompileExpression<T>(string discountFilter) // ROSLYN
        //{

        //https://www.strathweb.com/2018/01/easy-way-to-create-a-c-lambda-expression-from-a-string-with-roslyn/
        //var options = ScriptOptions.Default.AddReferences(typeof(T).Assembly);

        //Func<T, bool> discountFilterExpression = await CSharpScript.EvaluateAsync<Func<T, bool>>(discountFilter, options);

        // return discountFilterExpression;
        //}
        public static Expression<Func<T, bool>> StringToExpression<T>(string expressionString)
        {
            var dynamicExpression = DynamicExpressionParser.ParseLambda<T, bool>(null, false, expressionString);
            return dynamicExpression;
        }

    }
}
