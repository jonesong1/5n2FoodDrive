using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace FoodDrive.Models
{
    // Database name "Rescuer"
    public class User 
    {
        [JsonPropertyName("_id")]
        public ObjectId Id  { get; set; }
        
        [JsonPropertyName("email")]
        [BsonElement("email")]
        public string Email { get; set; }
        
        [JsonPropertyName("first_name")]
        [BsonElement("first_name")]
        public string FirstName { get; set; }
        
        [JsonPropertyName("last_name")]
        [BsonElement("last_name")]
        public string LastName { get; set; }
        
        [JsonPropertyName("password")]
        [BsonElement("password")]
        public string Password { get; set; }
        
        [JsonPropertyName("phone_number")]
        [BsonElement("phone_number")]
        public string PhoneNumber { get; set; }
        
        [JsonPropertyName("username")]
        [BsonElement("username")]
        public string Username { get; set; }
        
        [JsonPropertyName("created_at")]
        [BsonElement("created_at")]
        public DateTime? CreatedAt { get; set; }
        
        [JsonPropertyName("updated_at")]
        [BsonElement("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonPropertyName("__v")]
        [BsonElement("__v")]
        public double Version { get; set; }
    }
}
