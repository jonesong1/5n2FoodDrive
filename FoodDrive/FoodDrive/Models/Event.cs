using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FoodDrive.Models
{
    // Database name "Events"
    public class Event
    {
        [JsonPropertyName("_id")]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        
        [JsonPropertyName("title")]
        [BsonElement("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        [BsonElement("description")]
        public string Description { get; set; }
        
        [JsonPropertyName("priority")]
        [BsonElement("priority")]
        public string Priority { get; set; }

        [JsonPropertyName("created_by")]
        [BsonElement("created_by")]
        public ObjectId CreatedBy { get; set; }

        [JsonPropertyName("start_date")]
        [BsonElement("start_date")]
        public DateTime? StartDate { get; set; }

        [JsonPropertyName("end_date")]
        [BsonElement("end_date")]
        public DateTime? EndDate { get; set; }

        [JsonPropertyName("is_active")]
        [BsonElement("is_active")]
        public Boolean? IsActive { get; set; }

        [JsonPropertyName("from_street1")]
        [BsonElement("from_street1")]
        public string FromStreet { get; set; }

        [JsonPropertyName("from_postalcode")]
        [BsonElement("from_postalcode")]
        public string FromPostalCode { get; set; }

        [JsonPropertyName("from_city")]
        [BsonElement("from_city")]
        public string FromCity { get; set; }

        [JsonPropertyName("from_country")]
        [BsonElement("from_country")]
        public string FromCountry { get; set; }

        [JsonPropertyName("to_street1")]
        [BsonElement("to_street1")]
        public string ToStreet { get; set; }

        [JsonPropertyName("to_postalcode")]
        [BsonElement("to_postalcode")]
        public string ToPostalCode { get; set; }

        [JsonPropertyName("to_city")]
        [BsonElement("to_city")]
        public string ToCity { get; set; }

        [JsonPropertyName("to_country")]
        [BsonElement("to_country")]
        public string ToCountry { get; set; }

        [JsonPropertyName("created_at")]
        [BsonElement("created_at")]
        public DateTime? CreatedAt { get; set; }
        [JsonPropertyName("updated_at")]
        [BsonElement("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
