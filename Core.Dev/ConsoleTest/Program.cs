using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Test test = new Test()
            {
                Name = "张三",
                Age = 25
            };

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(test));
            Console.WriteLine((int)TestEnum.SignMethods);
        }
    }


    class Test
    {
        public string Name { get; set; }


        public int Age { get; set; }
    }

    enum TestEnum
    {
        SignMethods = 1,
        SignMethods2 = 2
    }
}
