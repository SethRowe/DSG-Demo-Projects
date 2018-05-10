using System;
using System.Linq.Expressions;

namespace UnitTests.Helpers
{
    public static class ExpressionHelper
    {   
        // Thank you StackOverflow for this method:
        // https://stackoverflow.com/questions/2616638/access-the-value-of-a-member-expression
        public static object GetValue(this MemberExpression member)
        {
            var objectMember = Expression.Convert(member, typeof(object));
            var getterLambda = Expression.Lambda<Func<object>>(objectMember);
            var getter = getterLambda.Compile();

            return getter();
        }
    }
}