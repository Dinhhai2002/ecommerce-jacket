using System.Collections.Generic;

namespace webecommerce.Models
{
    public class StoreProcedureListResult<T>
    {
        public int StatusCode { get; set; }
        public string MessageError { get; set; }
        public int TotalRecord { get; set; }
        public List<T> Data { get; set; }

        public StoreProcedureListResult(int statusCode, string messageError, int totalRecord, List<T> data)
        {
            StatusCode = statusCode;
            MessageError = messageError;
            TotalRecord = totalRecord;
            Data = data;
        }
    }
} 