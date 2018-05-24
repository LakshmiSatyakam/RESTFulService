using Newtonsoft.Json.Converters;

namespace RESTFulService.Models.Members.Config
{
    public class DateConverter : IsoDateTimeConverter
    {
        #region Public constants

        public const string DateFormat = "dd-MM-yyyy";

        #endregion

        #region Constructor
        public DateConverter(string dateTimeFormat)
        {
            DateTimeFormat = dateTimeFormat;
        }
        #endregion
    }
}
