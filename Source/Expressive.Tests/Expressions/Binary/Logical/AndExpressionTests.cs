﻿using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Logical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Expressions.Binary.Logical
{
    [TestClass]
    public class AndExpressionTests
    {
        [TestMethod]
        public void TestBothTrueEvaluate()
        {
            var expression = new AndExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) true),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) true),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(true, expression.Evaluate(null));
        }

        [TestMethod]
        public void TestLeftTrueEvaluate()
        {
            var expression = new AndExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) true),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) false),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(false, expression.Evaluate(null));
        }

        [TestMethod]
        public void TestNeitherTrueEvaluate()
        {
            var expression = new AndExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) false),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) false),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(false, expression.Evaluate(null));
        }

        [TestMethod]
        public void TestRightTrueEvaluate()
        {
            var expression = new AndExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) false),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) true),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(false, expression.Evaluate(null));
        }

        [TestMethod]
        public void TestShortCircuit()
        {
            var rightHandMock = new Mock<IExpression>();

            var expression = new AndExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)false),
                rightHandMock.Object,
                new Context(ExpressiveOptions.None));

            expression.Evaluate(null);

            rightHandMock.Verify(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()), Times.Never);
        }
    }
}