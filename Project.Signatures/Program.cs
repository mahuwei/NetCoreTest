using System;

namespace Project.Signatures {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            var dataNeedSign = "1393423891;马虎维";
            var rsaHelper = new RsaHelper();
            var dataSignResult = rsaHelper.Sign(dataNeedSign);

            var checkSuccess = rsaHelper.SignCheck(dataNeedSign, dataSignResult);
            Console.WriteLine($"验证签名结果：{checkSuccess}");
            Console.WriteLine("\n键入任意键退出系统...");
            Console.ReadKey();
        }
    }
}
