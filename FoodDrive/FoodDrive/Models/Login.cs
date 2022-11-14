using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FoodDrive.Models
{
    public class Login
    {
        [JsonPropertyName("success")]
        [BsonElement("success")]
        public Boolean success { get; set; } = false;
        [JsonPropertyName("message")]
        [BsonElement("message")]
        public string msg { get; set; }
        [JsonPropertyName("userInfo")]
        [BsonElement("userInfo")]
        public User userInfo { get; set; }
        [JsonPropertyName("token")]
        [BsonElement("token")]
        public string token { get; set; }
    }
}
