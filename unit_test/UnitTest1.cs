using System;
using Xunit;
using sample_lib;

namespace unit_test
{
    public class UnitTest1
    {
        [Fact]
        public void TestAdd2_2()
        {
            double a = 2, b = 2;
            double expected = 4;
            var calc = new Calculator();

            double actual = calc.Add(a,b);
            Assert.Equal(expected,actual);
        }
        [Fact]
        public void TestAdd3_3()
        {
        //Given
            double a = 3, b = 3;
            double expected = 6;
        //When
            var calc = new Calculator();
            double actual = calc.Add(a,b);
        //Then
            Assert.Equal(expected,actual);
        }
    }
}
