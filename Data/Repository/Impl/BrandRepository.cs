using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using webecommerce.Common.Enums;
using webecommerce.Common.Utils;
using webecommerce.Models;

namespace webecommerce.Data.Repository.Impl
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        private readonly AppDbContext _context;

        public BrandRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Brand> FindByIds(List<int> ids)
        {
            return _dbSet.Where(b => ids.Contains(b.Id)).ToList();
        }

        public Brand FindByName(string name)
        {
            return _dbSet.FirstOrDefault(b => b.Name == name);
        }

        public StoreProcedureListResult<Brand> SpGListBrand(string keySearch, int status, Pagination pagination)
        {
            try
            {
                var totalRecordParam = new Microsoft.Data.SqlClient.SqlParameter("@total_record", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output
                };

                var statusCodeParam = new Microsoft.Data.SqlClient.SqlParameter("@status_code", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output
                };

                var messageErrorParam = new Microsoft.Data.SqlClient.SqlParameter("@message_error", System.Data.SqlDbType.NVarChar, 255)
                {
                    Direction = System.Data.ParameterDirection.Output
                };

                var result = _context.Brands.FromSqlRaw(
                    "EXEC sp_g_list_brand @keySearch, @status, @_limit, @_offset, @total_record OUTPUT, @status_code OUTPUT, @message_error OUTPUT",
                    new Microsoft.Data.SqlClient.SqlParameter("@keySearch", keySearch ?? (object)DBNull.Value),
                    new Microsoft.Data.SqlClient.SqlParameter("@status", status),
                    new Microsoft.Data.SqlClient.SqlParameter("@_limit", pagination.Limit),
                    new Microsoft.Data.SqlClient.SqlParameter("@_offset", pagination.Offset),
                    totalRecordParam,
                    statusCodeParam,
                    messageErrorParam
                ).ToList();

                var statusCode = (int)statusCodeParam.Value;
                var messageError = messageErrorParam.Value.ToString();
                var totalRecord = (int)totalRecordParam.Value;

                if (statusCode == (int)StoreProcedureStatusCodeEnum.INPUT_INVALID)
                {
                    throw new Exception(messageError);
                }

                return new StoreProcedureListResult<Brand>(statusCode, messageError, totalRecord, result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing sp_g_list_brand: {ex.Message}");
            }
        }
    }
} 