using System;
using System.Collections.Generic;

namespace CSVFormatter
{
    class TxtFormat : IFormat
    {
        public string Execute<T>(IEnumerable<T> list, List<string> fields)
        {
            throw new NotImplementedException();
        }

        public void GetNeededInfo()
        {
            throw new NotImplementedException();
        }
    }
}