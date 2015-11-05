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

using System.Collections.Generic;
using Tup.Cobar.Parser.Visitor;

namespace Tup.Cobar.Parser.Ast.Expression.Misc
{
    /// <author><a href="mailto:shuo.qius@alibaba-inc.com">QIU Shuo</a></author>
    public class InExpressionList : AbstractExpression
    {
        private IList<Tup.Cobar.Parser.Ast.Expression.Expression> list;

        public InExpressionList(IList<Tup.Cobar.Parser.Ast.Expression.Expression> list)
        {
            if (list == null || list.Count == 0)
            {
                this.list = new List<Expression>(0);
            }
            else
            {
                if (list is List<Expression>)
                {
                    this.list = list;
                }
                else
                {
                    this.list = new List<Tup.Cobar.Parser.Ast.Expression.Expression>(list);
                }
            }
        }

        /// <returns>never null</returns>
        public virtual IList<Tup.Cobar.Parser.Ast.Expression.Expression> GetList()
        {
            return list;
        }

        public override int GetPrecedence()
        {
            return ExpressionConstants.PrecedencePrimary;
        }

        protected override object EvaluationInternal(IDictionary<Expression, Expression> parameters
            )
        {
            return Unevaluatable;
        }

        private IList<Tup.Cobar.Parser.Ast.Expression.Expression> replaceList;

        public virtual void SetReplaceExpr(IList<Tup.Cobar.Parser.Ast.Expression.Expression
            > replaceList)
        {
            this.replaceList = replaceList;
        }

        public virtual void ClearReplaceExpr()
        {
            this.replaceList = null;
        }

        public override void Accept(SQLASTVisitor visitor)
        {
            if (replaceList == null)
            {
                visitor.Visit(this);
            }
            else
            {
                IList<Tup.Cobar.Parser.Ast.Expression.Expression> temp = list;
                list = replaceList;
                visitor.Visit(this);
                list = temp;
            }
        }
    }
}