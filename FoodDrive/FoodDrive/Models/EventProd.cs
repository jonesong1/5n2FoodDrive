using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FoodDrive.Models
{
    public class EventProd
    {
        [JsonPropertyName("success")]
        [BsonElement("success")]
        public Boolean success { get; set; } = false;
        [JsonPropertyName("message")]
        [BsonElement("message")]
        public string msg { get; set; }
        [JsonPropertyName("data")]
        [BsonElement("data")]
        public List<Event> dataInfo { get; set; }
    }
}
