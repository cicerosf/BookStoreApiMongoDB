﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace BookStoreApiMongoDB.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Name")]
        [JsonPropertyName("BookName")]
        public string BookName { get; set; } = null;
        public Decimal Price { get; set; }
        public string Category { get; set; } = null;
        public string Author { get; set; } = null;
    }
}