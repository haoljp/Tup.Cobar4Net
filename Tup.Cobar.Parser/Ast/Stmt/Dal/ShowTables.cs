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

using Tup.Cobar.Parser.Ast.Expression.Primary;
using Tup.Cobar.Parser.Visitor;

namespace Tup.Cobar.Parser.Ast.Stmt.Dal
{
    /// <author><a href="mailto:shuo.qius@alibaba-inc.com">QIU Shuo</a></author>
    public class ShowTables : DALShowStatement
    {
        private readonly bool full;

        private Identifier schema;

        private readonly string pattern;

        private readonly Tup.Cobar.Parser.Ast.Expression.Expression where;

        public ShowTables(bool full, Identifier schema, string pattern)
        {
            this.full = full;
            this.schema = schema;
            this.pattern = pattern;
            this.where = null;
        }

        public ShowTables(bool full, Identifier schema, Tup.Cobar.Parser.Ast.Expression.Expression
             where)
        {
            this.full = full;
            this.schema = schema;
            this.pattern = null;
            this.where = where;
        }

        public ShowTables(bool full, Identifier schema)
        {
            this.full = full;
            this.schema = schema;
            this.pattern = null;
            this.where = null;
        }

        public virtual bool IsFull()
        {
            return full;
        }

        public virtual void SetSchema(Identifier schema)
        {
            this.schema = schema;
        }

        public virtual Identifier GetSchema()
        {
            return schema;
        }

        public virtual string GetPattern()
        {
            return pattern;
        }

        public virtual Tup.Cobar.Parser.Ast.Expression.Expression GetWhere()
        {
            return where;
        }

        public override void Accept(SQLASTVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}