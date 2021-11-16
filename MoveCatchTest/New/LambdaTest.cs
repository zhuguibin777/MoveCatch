using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MoveCatchTest.Model;

namespace MoveCatchTest.New
{
    class LambdaTest
    {
        public delegate void NoReturnNoPara();
        public delegate void NoReturnWithPara(int id, string name);
        public delegate int WithReturnNoPara();
        public delegate void WithReturnWithPara(int id, string name);

        public void Show()
        {
            {
                NoReturnWithPara method = new NoReturnWithPara(this.Study);
                method.Invoke(123, "jam1");
            }
            {
                //.NetFramework2.0 匿名方法
                NoReturnWithPara method = new NoReturnWithPara(delegate (int id, string name)
                {
                    Console.WriteLine($"This is {id}-{name}  学习Lambda2");
                });
                method.Invoke(234, "jam2");
            }
            {
                //.NetFramework3.0 Lambda表达式 参数列表=>goes to方法体
                //什么是lambda表达式？ 其实就是一个匿名方法，就是一个方法。
                //lambda参数类型可以省略，能自动推算------(根据委托的约束)语法糖<编译器帮你提供的的快捷功能，可以不声明类型，编译器推算出类型，等价于自己声明类型>
                NoReturnWithPara method = new NoReturnWithPara(
                (int id, string name) =>
                {
                    Console.WriteLine($"This is {id}-{name}  学习Lambda3");
                });
                method.Invoke(345, "jam3");
            }
            {
                //lambda参数类型可以省略，能自动推算------(根据委托的约束)语法糖<编译器帮你提供的的快捷功能，可以不声明类型，编译器推算出类型，等价于自己声明类型>
                NoReturnWithPara method = new NoReturnWithPara(
                (id, name) =>
                {
                    Console.WriteLine($"This is {id}-{name}  学习Lambda4");
                });
                method.Invoke(456, "jam4");
            }
            {
                //如果方法只有一行 可以省略大括号+分号
                NoReturnWithPara method = new NoReturnWithPara(
                (id, name) => Console.WriteLine($"This is {id}-{name}  学习Lambda5"));
                method.Invoke(567, "jam5");
            }
            {
                //实例化委托 可以省略掉new --------语法糖
                NoReturnWithPara method = (id, name) => Console.WriteLine($"This is {id}-{name}  学习Lambda6");
                method.Invoke(678, "jam6");
            }
            {
                //Lambda表达式是什么？ 
                //其实就是一个匿名方法，就是一个方法。
                //首先不是一个委托，因为委托不是类。
                //其次也不是委托的实例，这里是语法糖！是省略了new，lambda实例化了一个委托的一个参数。

                //怎么勘破误会？反编译+TL

                //Action&Func 是3.0框架内置的2组委托

                //Action 0到16个参数 没有返回值的泛型委托
                Action action = () => Console.WriteLine("Action演示");
                Action<int> action1 = a => Console.WriteLine("Action演示");

                //Func 0到16个参数 有返回值的泛型委托
                Func<int> func = () => DateTime.Now.Month;
                Func<int, string> func1 = i => DateTime.Now.ToString();

            }
            {
                //lambda表达式
                //可以快捷声明方法(C#6)
                //lambda还有个很重要的应用叫表达式目录树
                //linq去数据库查询


                var userDbSet = new List<Student>().AsQueryable();
                {
                    var userlist = userDbSet.Where(s => s.id > 100 && s.name.Contains("1"));
                }

                Func<Student, bool> func = s => s.id > 100 && s.name.Contains("1");
                //lambda实际是个方法

                Expression<Func<Student, bool>> predicate = s => s.id > 100 && s.name.Contains("1");
                //lambda实际上是个表达目录树，是个二叉树结构
            }
        }

        private void Study(int id, string name)
        {
            Console.WriteLine($"This is {id}-{name}  学习Lambda1");
        }

    }


    /// <summary>
    /// C#6 新语法 Vs2015之后
    /// 最终变成IL
    /// </summary>
    public class LambdaOther
    {
        public int Id { get; set; } = 123;

        //private int _Id1 = 123;

        public int _Id1
        {
            get
            {
                return _Id1;
            }
            set
            {
                _Id1 = value;
            }
        }

        public string Name => "Lambda";

        public string Remark
        {
            get => "Test";
        }

        public string Get() => "风之林";
    }

}
