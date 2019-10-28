using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TaskExpressionTree;
using Xunit;
using Xunit.Abstractions;

namespace ExpressionTreeTask1
{
    public class ParameterToConstantTransformationTest
    {
        private readonly ITestOutputHelper _output;
        public ParameterToConstantTransformationTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ParameterToConstantTest()
        {
            Expression<Func<int, int>> sourceExpression = (a) => a + (a + 1) * (a + 5) * (a + 1);
            var transformedExpression = (new LambdaTransform(new Dictionary<string, Object> {
                { "a", 5 }
            }).VisitAndConvert(sourceExpression, ""));
            
            _output.WriteLine(transformedExpression.ToString());

            var parameter = 5;
            var expected = sourceExpression.Compile().Invoke(parameter);
            var result = transformedExpression.Compile().Invoke(parameter);
            Assert.Equal(expected, result);

        }
    }
}
