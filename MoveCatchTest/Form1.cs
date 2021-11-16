using Emgu.CV;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using MoveCatchTest.New;

namespace MoveCatchTest
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 测试C++ DLL
        /// </summary>
        [DllImport("test6.dll")]
        static extern void print_test();


        /// <summary>
        /// C++ 处理视频算法
        /// </summary>
        /// <param name="frame">视频的单个帧图片</param>
        /// <param name="roi_height">ROI区域高度</param>
        /// <param name="roi_width">ROI区域宽度</param>
        /// <param name="point_x">ROI矩形区域左上角坐标X值</param>
        /// <param name="point_y">ROI矩形区域左上角坐标Y值</param>
        /// <param name="min_th">判断小鼠体型的面积阈值最小值</param>
        /// <param name="max_th">判断小鼠体型的面积阈值最大值</param>
        /// <param name="pointx">传出参数，存放x坐标</param>
        /// <param name="pointy">传出参数，存放y坐标</param>
        [DllImport("MoveCatch.dll", EntryPoint = "MoveCatch")]
        static extern void MoveCatch(Mat frame, int roi_height, int roi_width, int point_x, int point_y, int min_th, int max_th, ref int pointx, ref int pointy);

        /// <summary>
        /// 导入dll后，请先运行 sc_lic_info() 解锁                                                                                                                
        /// </summary>
        [DllImport("MoveCatch.dll", EntryPoint = "sc_lic_info")]
        static extern void sc_lic_info();



        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //print_test();
            //sc_lic_info();
            //new LambdaTest().Show();
            TestDelegate();
        }


        public void TestThread(PeopleType a)
        {

            switch (a)
            {
                case PeopleType.chinese:
                    break;
                case PeopleType.america:
                    break;
                default:
                    break;
            }
        }

        public enum PeopleType
        {
            chinese,
            america
        }


        /// <summary>
        /// 同步方法
        /// 同步单线程：按顺序执行，每次调用完成后才能进入下一行，是同一个线程运行。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSync_Click(object sender, EventArgs e)
        {
            Console.WriteLine("");
            Console.WriteLine("*******************btnSync_Click 同步方法 start {0} *******************", Thread.CurrentThread.ManagedThreadId);
            //int j = 0;
            //int k = 1;
            //int m = j + k;
            Action<string> action = this.DoSomethingLong;
            for (int i = 0; i < 5; i++)
            {
                string name = string.Format("{0}_{1}", "btnSync_Click", i);
                //DoSomethingLong(name);
                action.Invoke(name);//同步的
            }
            Console.WriteLine("*******************btnSync_Click 同步方法 end {0} *******************", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("");
        }


        /// <summary>
        /// 任何的异步多线程都离不开委托
        /// 委托的异步调用
        /// 异步多线程：发起调用，不等待结束就进入下一行（主线程）;
        ///            动作会由一个新线程来执行（子线城）; 并发了
        ///            
        /// 多线程写法不难，但是用好很难，多年老司机，也会翻车。
        /// 
        /// 1、同步单线程方法卡界面----------主(UI)线程忙于计算，所以不能响应
        ///    异步多线程方法不卡界面----------计算任务交给子线程，主(UI)线程已经闲置了，可以响应别的操作
        ///    C/S开发：按钮后能不卡死--上传文件不卡死
        ///    B/S开发：用户注册发邮件/发短信/写日志
        ///    
        /// 2、同步单线程方法慢----因为只有一个线程计算
        ///    异步多线程方法快----因为多个线程并发计算
        ///    多线程就是用资源换性能，但并不是线性增长
        ///    一个线程13000 五个线程4296 性能只有3倍提升
        ///    a 多线程协调管理额外成本----项目经理
        ///    b 资源也有上限的----5辆车只有3条道
        ///    线程并不是越多越好，边际效应，可能会适得其反
        ///    
        /// 3、无序性----不可预测性
        ///    启动无序：几乎同一时间向操作系统请求线程，也是一个需要CPU处理的请求
        ///             因为线程是操作系统资源，CLR只能去申请，具体什么顺序，无法掌控
        ///    执行时间的不确定：同一个线程，同一个任务，耗时也不同
        ///                    其实操作系统的调度策略有关，CPU分片(计算能力太强，1s分拆1000份儿，宏观上就变成了并发的)
        ///                    任务执行过程就要看运气----线程的优先级可以影响操作系统的优先调度
        ///    结束无序：以上加起来
        ///    
        ///   正是因为多线程具有不可预测性，很多时候你的想法，并不一定能够贯彻实施，也许大多数情况是OK的
        ///   但是总会有一定的概率出问题（墨菲：任何小概率的坏事情，随着时间的推移，一定会发生）
        ///   
        ///   电商下订单：增加日志--日志--发邮件--生成支付--物流通知
        ///   多线程：有顺序要求，等待500ms执行执行下一次动作，顺序测试100次都没问题
        ///   上限没问题...但是，偶尔，一个月总有几次顺序错了，而且无法重现。
        ///   随着用户的增加，数据量累计，数据库压力变啊，服务器硬件资源不够，都会影响到执行效率。
        ///   
        ///   使用多线程时，不要通过延时等方式去掌控顺序，不要试图“风骚的多模式”掌控顺序。
        ///  
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAsync_Click(object sender, EventArgs e)
        {
            Console.WriteLine("");
            Console.WriteLine("*******************btnAsync_Click 异步方法 start {0} *******************", Thread.CurrentThread.ManagedThreadId);

            Action<string> action = this.DoSomethingLong;
            //action.Invoke("btnAsync_Click_1");
            //action("btnAsync_Click_2");
            //action.BeginInvoke("btnAsync_Click_3", null, null);

            for (int i = 0; i < 5; i++)
            {
                string name = string.Format("{0}_{1}", "btnAsync_Click", i);
                action.BeginInvoke(name, null, null);
            }


            Console.WriteLine("*******************btnSync_Click 异步方法 end {0} *******************", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("");
        }


        private void DoSomethingLong(string name)
        {
            Console.WriteLine("*******************DoSomethingLong  start {0} {1} {2}*******************",
                name, Thread.CurrentThread.ManagedThreadId.ToString("00"), DateTime.Now.ToString("HHmmss:fff"));

            long lResult = 0;
            for (int i = 0; i < 1000000000; i++)
            {
                lResult += i;
            }

            Console.WriteLine("*******************DoSomethingLong  end {0} {1} {2} {3} *******************",
                name, Thread.CurrentThread.ManagedThreadId.ToString("00"), DateTime.Now.ToString("HHmmss:fff"), lResult);
        }




        /// <summary>
        /// 1、异步的回调和状态参数
        /// 2、异步等待的三种方式
        /// 3、获取异步的返回值
        /// 
        /// 
        /// 多线程具有不可预测性，在很多时候业务场景有影响
        /// 解决多线程的顺序性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAsyncAdvanced_Click(object sender, EventArgs e)
        {
            Console.WriteLine("");
            Console.WriteLine("*******************btnAsyncAdvanced_Click 异步方法 start {0} *******************", Thread.CurrentThread.ManagedThreadId);


            //1 用户点击按钮，希望业务操作要做，但是不卡界面，所以可以用异步多线程
            //项目经理，不允许没有监控的项目上线----需要在业务操作后记录下日志
            Action<string> action = this.DoSomethingLong;
            //action.Invoke("btnAsyncAdvanced_Click_1");


            //AsyncCallback asyncCallback = ar =>
            //{
            //    Console.WriteLine($"btnAsyncAdvanced_Click方法完成.... {Thread.CurrentThread.ManagedThreadId}");
            //};
            //action.BeginInvoke("btnAsyncAdvanced_Click_2", zz =>
            //{
            //    Console.WriteLine(zz.AsyncState);
            //    Console.WriteLine($"btnAsyncAdvanced_Click方法完成.... {Thread.CurrentThread.ManagedThreadId}");
            //}, "success");

            //用户必须确定操作完成，才能返回----上传文件，只有成功之后才能预览
            //action.BeginInvoke("文件上传", null, null);
            //action.Invoke("文件上传");//同步能保证顺序，但是界面卡死，没有任何提示

            //希望一方面文件上传，完成后才预览；另一方面，还希望有个进度提示--只有主线程才能操作

            IAsyncResult iasyncResult = action.BeginInvoke("", null, null);


            int i = 0;
            while (!iasyncResult.IsCompleted)//IsCompleted是一个属性，用来描述异步动作是否完成；其实一开始就是false，异步动作完成后回去修改这个属性为true
            {
                //真实开发中，一开始可以读到文件的size，然后就直接获取上传好的size，做个比例就可以了

                //为啥界面没有实时更新，主线程在忙，忙完之后才能更新----怎么样才能实时更新，让主线程闲下来，其他操作都由
                //主线程完成
            }

            //for (int i = 0; i < 5; i++)
            //{
            //    string name = string.Format("{0}_{1}", "btnAsyncAdvanced_Click", i);
            //    action.BeginInvoke(name, null, null);
            //}


            Console.WriteLine("*******************btnAsyncAdvanced_Click 异步方法 end {0} *******************", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("");
        }




        delegate int NumberChanger(int n);

        static int num = 10;

        public int AddNum(int p)
        {
            num += p;
            return num;
        }

        public int MultNum(int q)
        {
            num *= q;
            return num;
        }

        public int getNum()
        {
            return num;
        }


        private void TestDelegate()
        {
            // 创建委托实例
            NumberChanger nc;
            NumberChanger nc1 = new NumberChanger(AddNum);
            NumberChanger nc2 = new NumberChanger(MultNum);

            nc = nc1;
            nc1 += nc2;
            // 调用多播
            nc1(5);
            Console.WriteLine("Value of Num: {0}", getNum());
            //Console.ReadKey();

        }
    }

}

