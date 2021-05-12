using System.Collections.Generic;

namespace CSVFormatter
{
    public interface IFormat
    {
        public string Execute<T>(IEnumerable<T> list, List<string> fields);

        public void GetNeededInfo();
    }
}