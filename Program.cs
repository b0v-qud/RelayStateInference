using System;

namespace ConsoleApp1
{
    class Program
    {
        public int[] valueA = new int[] { 6,7,8,50,80,20,10};
        public int[] valueB = new int[] { 2, 3, 4, 30, 190, 60, 100 };
        public Operator[] operatorA = new Operator[] { Operator.Mul, Operator.Mul, Operator.Mul, Operator.Sub, Operator.Add, Operator.Add, Operator.Sub };
        public Operator[] operatorB = new Operator[] { Operator.Mul, Operator.Mul, Operator.Mul, Operator.Add, Operator.Sub, Operator.Sub, Operator.Sub };
        public int currentVoltageA = 120;
        public int currentVoltageB = 120;
        public int targetValueA = 800;
        public int targetValueB = 230;
        public bool[] relayStatus;//继电器通断状态序列

        Random random = new Random();

        public enum Operator
        {
            /// <summary>加</summary>
            Add,
            /// <summary>减</summary>
            Sub,
            /// <summary>乘</summary>
            Mul,
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            Console.WriteLine("Hello World!");

            while (true)
            {
                program.relayStatus = new bool[program.valueA.Length];
                for (int i = 0; i < program.relayStatus.Length; i++)
                {
                    program.relayStatus[i] = program.RandomBool(program.random);//生成 随机继电器通断状态序列
                }

                for (int i = 0; i < program.relayStatus.Length; i++)
                {
                    if (program.relayStatus[i])
                    {//如果继电器联通 计算电压组合
                        program.currentVoltageA = program.Calculate(program.currentVoltageA, program.valueA[i], program.operatorA[i]);
                    }
                }

                for (int i = 0; i < program.relayStatus.Length; i++)
                {
                    if (program.relayStatus[i])
                    {//如果继电器联通 计算电压组合
                        program.currentVoltageB = program.Calculate(program.currentVoltageB, program.valueB[i], program.operatorB[i]);
                    }
                }

                if (program.currentVoltageA == program.targetValueA && program.currentVoltageB == program.targetValueB)
                {
                    Console.Write("发现了匹配的组合 ");
                    foreach (var item in program.relayStatus)
                    {
                        Console.Write(item ? "on" : "off");
                        Console.Write(" - ");
                    }

                    Console.WriteLine("\ncurrentVoltageA " + program.currentVoltageA + " currentVoltageB " + program.currentVoltageB);
                    Console.ReadLine();
                    return;
                }
                else
                {
                    Console.Write("不匹配的组合 ");
                    foreach (var item in program.relayStatus)
                    {
                        Console.Write(item ? "on" : "off");
                        Console.Write(" - ");
                    }
                    Console.WriteLine("\ncurrentVoltageA " + program.currentVoltageA + " currentVoltageB " + program.currentVoltageB);
                }

                program = new Program();
            }
        }

        //--------------------------------------------------------
        /// <summary>计算</summary>
        public int Calculate(int mainValue, int inValue, Operator inOperator)
        {
            int rV = 0;
            switch (inOperator)
            {
                case Operator.Add:
                    rV = mainValue + inValue;
                    break;
                case Operator.Sub:
                    rV = mainValue - inValue;
                    break;
                case Operator.Mul:
                    rV = mainValue * inValue;
                    break;
            }

            return rV == 0 ? 0 : rV;
        }


        /// <summary>随机布尔值</summary>
        public bool RandomBool(Random random)
        {
            int n = 5;
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine();
            }
            return random.Next(2) == 1;
        }
    }
}
