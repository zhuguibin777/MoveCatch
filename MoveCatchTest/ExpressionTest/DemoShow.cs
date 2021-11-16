using MoveCatchTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MoveCatchTest.ExpressionTest
{
    class DemoShow
    {
        public static void Show()
        {
            var userDbSet = new List<Student>().AsQueryable();
            {
                var userlist = userDbSet.Where(s => s.id > 100 && s.name.Contains("1"));
            }

            Expression<Func<Student, bool>> predicate = s => s.id > 100 && s.name.Contains("1");

            {
                //把predicate 转成一个where子句
            }
        }
    }
}
