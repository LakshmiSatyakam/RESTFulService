using RESTFulService.Models.Members.Config;
using Xunit;

namespace RESTFulServiceUnitTest.Models.Members
{
    /// <summary>
    /// Unit test class for DateConverter
    /// </summary>
    public class DateConverterUnitTest
    {
        #region Test methods

        [Fact]
        public void DateConverterFormatString_Test()
        {
            Assert.Equal("dd-MM-yyyy", DateConverter.DateFormat);
        }
        #endregion
    }
}
