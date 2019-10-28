using System;
using System.Linq.Expressions;
using TaskExpressionTree;
using Xunit;
using Xunit.Abstractions;

namespace ExpressionTreeTask1
{
    public class LambdaTransformTest
    {
        private readonly ITestOutputHelper _output;
        public LambdaTransformTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void AddToIncrenetTest()
        {
            Expression<Func<int, int>> sourceExpression = (a) => a + (a + 1) * (a + 5) * (a + 1);
            var transformedExpression = (new IncrementDecrementTransform().VisitAndConvert(sourceExpression, ""));
            
            _output.WriteLine(transformedExpression.ToString());

            var parameter = 3;
            var expected = sourceExpression.Compile().Invoke(parameter);
            var result = transformedExpression.Compile().Invoke(parameter);
            Assert.Equal(expected, result);

        }

        [Fact]
        public void SubtractToDecrenetTest()
        {
            Expression<Func<int, int>> sourceExpression = (a) => a + (a - 1) * (a - 5) * (a - 1);
            var transformedExpression = (new IncrementDecrementTransform().VisitAndConvert(sourceExpression, ""));

            _output.WriteLine(transformedExpression.ToString());

            var parameter = 3;
            var expected = sourceExpression.Compile().Invoke(parameter);
            var result = transformedExpression.Compile().Invoke(parameter);
            Assert.Equal(expected, result);

        }
    }
}
