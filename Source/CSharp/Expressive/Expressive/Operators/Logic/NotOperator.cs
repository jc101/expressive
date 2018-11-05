﻿using Expressive.Expressions;
using Expressive.Expressions.Unary.Logic;

namespace Expressive.Operators.Logic
{
    internal class NotOperator : OperatorBase
    {
        #region IOperator Members

        public override string[] Tags => new[] { "!", "not" };

        public override IExpression BuildExpression(Token previousToken, IExpression[] expressions, ExpressiveOptions options)
        {
            return new NotExpression(expressions[0] ?? expressions[1]);
        }

        public override OperatorPrecedence GetPrecedence(Token previousToken)
        {
            return OperatorPrecedence.Not;
        }

        #endregion
    }
}
