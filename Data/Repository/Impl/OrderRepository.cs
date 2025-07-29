using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using webecommerce.Common.Enums;
using webecommerce.Common.Utils;
using webecommerce.Models;

namespace webecommerce.Data.Repository.Impl
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Order> FindByUserId(int userId)
        {
            return _dbSet.Where(o => o.UserId == userId).ToList();
        }

        public List<Order> FindByStatus(int status)
        {
            return _dbSet.Where(o => o.Status == status).ToList();
        }

        public List<Order> FindByUserIdAndStatus(int userId, int status)
        {
            return _dbSet.Where(o => o.UserId == userId && o.Status == status).ToList();
        }

        public Order FindByOrderCode(string orderCode)
        {
            return _dbSet.FirstOrDefault(o => o.OrderCode == orderCode);
        }

        public StoreProcedureListResult<Order> SpGListOrder(int userId, string keySearch, int status, int paymentStatus, int paymentMethod, Pagination pagination)
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

                var result = _context.Orders.FromSqlRaw(
                    "EXEC sp_g_list_order @userId, @keySearch, @status, @paymentStatus, @paymentMethod, @_limit, @_offset, @total_record OUTPUT, @status_code OUTPUT, @message_error OUTPUT",
                    new Microsoft.Data.SqlClient.SqlParameter("@userId", userId),
                    new Microsoft.Data.SqlClient.SqlParameter("@keySearch", keySearch ?? (object)DBNull.Value),
                    new Microsoft.Data.SqlClient.SqlParameter("@status", status),
                    new Microsoft.Data.SqlClient.SqlParameter("@paymentStatus", paymentStatus),
                    new Microsoft.Data.SqlClient.SqlParameter("@paymentMethod", paymentMethod),
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

                return new StoreProcedureListResult<Order>(statusCode, messageError, totalRecord, result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing sp_g_list_order: {ex.Message}");
            }
        }
    }
} 