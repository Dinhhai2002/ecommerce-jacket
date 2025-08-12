using System.Collections.Generic;

namespace webecommerce.Common.Utils
{
    public class StoreProcedureListResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalRecords { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public StoreProcedureListResult()
        {
            Items = new List<T>();
            TotalRecords = 0;
            StatusCode = 200;
            Message = "Success";
        }

        public StoreProcedureListResult(int statusCode, string message, int totalRecords, List<T> items)
        {
            StatusCode = statusCode;
            Message = message;
            TotalRecords = totalRecords;
            Items = items;
        }
    }
} 