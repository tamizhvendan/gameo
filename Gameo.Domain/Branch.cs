using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gameo.Domain
{
    public class Branch
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }
    }
}