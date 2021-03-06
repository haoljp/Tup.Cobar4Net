/*
* Copyright 1999-2012 Alibaba Group.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using Deveel.Math;
using Tup.Cobar4Net.Parser.Visitor;

namespace Tup.Cobar4Net.Parser.Ast.Expression.Arithmeic
{
    /// <summary>
    ///     <code>higherExpr '-' higherExpr</code>
    /// </summary>
    /// <author>
    ///     <a href="mailto:shuo.qius@alibaba-inc.com">QIU Shuo</a>
    /// </author>
    public class ArithmeticSubtractExpression : ArithmeticBinaryOperatorExpression
    {
        public ArithmeticSubtractExpression(IExpression leftOprand,
                                            IExpression rightOprand)
            : base(leftOprand, rightOprand, ExpressionConstants.PrecedenceArithmeticTermOp)
        {
        }

        public override string Operator
        {
            get { return "-"; }
        }

        public override Number Calculate(int integer1, int integer2)
        {
            if (integer1 == 0 && integer2 == 0)
            {
                return 0;
            }
            var i1 = integer1;
            var i2 = integer2;
            if (i2 == 0)
            {
                return integer1;
            }
            if (i1 == 0)
            {
                if (i2 == int.MinValue)
                {
                    return -(long)i2;
                }
                return -i2;
            }
            if (i1 >= 0 && i2 >= 0 || i1 <= 0 && i2 <= 0)
            {
                return i1 - i2;
            }
            var rst = i1 - i2;
            if (i1 > 0 && rst < i1 || i1 < 0 && rst > i1)
            {
                return i1 - (long)i2;
            }
            return rst;
        }

        public override Number Calculate(long long1, long long2)
        {
            if (long1 == 0 && long2 == 0)
            {
                return 0;
            }
            var l1 = long1;
            var l2 = long1;
            if (l2 == 0L)
            {
                return long1;
            }
            if (l1 == 0L)
            {
                if (l2 == long.MinValue)
                {
                    return BigInteger.ValueOf(l2).Negate();
                }
                return -l2;
            }
            if (l1 >= 0L && l2 >= 0L || l1 <= 0L && l2 <= 0L)
            {
                return l1 - l2;
            }
            var rst = l1 - l2;
            if (l1 > 0L && rst < l1 || l1 < 00L && rst > l1)
            {
                var bi1 = BigInteger.ValueOf(l1);
                var bi2 = BigInteger.ValueOf(l2);
                return bi1.Subtract(bi2);
            }
            return rst;
        }

        public override Number Calculate(BigInteger bigint1, BigInteger bigint2)
        {
            if (bigint1 == null || bigint2 == null)
            {
                return 0;
            }
            return bigint1.Subtract(bigint2);
        }

        public override Number Calculate(BigDecimal bigDecimal1, BigDecimal bigDecimal2)
        {
            if (bigDecimal1 == null || bigDecimal2 == null)
            {
                return 0;
            }
            return bigDecimal1.Subtract(bigDecimal2);
        }

        public override void Accept(ISqlAstVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}