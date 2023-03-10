using NUnit.Framework;
using BusinessLogic;

namespace tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SumMainDiagonal()
        {
            double[,] A = new double[3, 3] { { 3, 8, 2 }, { 2, 9, 1 }, { 5, 6, 5 } };
            double result = 17;
            double actual = BL.SumMainDiagonal(A);
            Assert.AreEqual(result, actual);
        }

        [Test]
        public void Form_Massiv_X()
        {
            double[] C = new double[3] { 2, 7, 9 };
            double[,] A = new double[3, 3] { { 3, 8, 2 }, { 2, 9, 1 }, { 5, 6, 5 } };
            int m = 3;
            double B = 5;
            double[] X = BL.CalcArrayX(C, A, m, B);
            double[] result = { 40.715, 0.802, 7.65 };
            Assert.AreEqual(result, X);
        }

        [Test]
        public void Form_Massiv_Y()
        {
            BL bl = new BL();
            double h =  1;
            double[] X = { 40.715, 0.802, 7.65 };
            double[] Y = BL.CalcArrayY(X,h);
            double[] result = { 40.715,  4.99  ,  653.76 , 924.81 ,34404.64 ,   36221.84 };
            Assert.AreEqual(result, Y);
        }

        [Test]
        public void Sort_Just_Obmen()
        {
            BL bl = new BL();
            double[] y = { 3, 7, 50, 110 };
            double[] result = { 110, 50, 7, 3 };
          //  double[] sort_y =BL.Sort_Puzurek_Y(y);
          //  Assert.AreEqual(result, sort_y);
        }
    }
}