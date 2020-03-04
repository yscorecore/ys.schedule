using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Mathematics;
namespace YS.Schedule
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

        }
        [TestMethod]
        public void MyTestMethod()
        {
            
            object result = new DataTable().Compute("(2<>8)or(3>2)", "");
            Assert.AreEqual(2, result);
        }
    }
}
