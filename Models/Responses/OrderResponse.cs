using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace webecommerce.Models.Responses
{
    public class OrderResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("order_code")]
        public string OrderCode { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("total_price")]
        public decimal TotalPrice { get; set; }

        [JsonProperty("shipping_fee")]
        public decimal ShippingFee { get; set; }

        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("payment_method")]
        public int PaymentMethod { get; set; }

        [JsonProperty("payment_status")]
        public int PaymentStatus { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("order_details")]
        public List<OrderDetailResponse> OrderDetails { get; set; }

        public OrderResponse()
        {
            OrderDetails = new List<OrderDetailResponse>();
        }

        public OrderResponse(Order order)
        {
            Id = order.Id;
            OrderCode = order.OrderCode;
            UserId = order.UserId;
            TotalPrice = order.TotalPrice;
            ShippingFee = order.ShippingFee;
            TotalAmount = order.TotalAmount;
            PaymentMethod = order.PaymentMethod;
            PaymentStatus = order.PaymentStatus;
            Status = order.Status;
            CreatedAt = order.CreatedAt;
            UpdatedAt = order.UpdatedAt;
            OrderDetails = new List<OrderDetailResponse>();
        }
    }
} 