using System.Text;

namespace Command.CatalogApp.Start
{
    public class HandlerResponse
    {
        private StringBuilder _stringBuilder;
        private string _aLlWorkshopsStylesheet;

        public HandlerResponse(StringBuilder stringBuilder, string aLlWorkshopsStylesheet)
        {
            _stringBuilder = stringBuilder;
            _aLlWorkshopsStylesheet = aLlWorkshopsStylesheet;
        }

        public object GetXmlString()
        {
            return _stringBuilder.ToString();
        }
    }
}