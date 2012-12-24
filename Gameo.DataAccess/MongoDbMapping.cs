using Gameo.Domain;
using MongoDB.Bson.Serialization;

namespace Gameo.DataAccess
{
    public static class MongoDbMapping
    {
        static MongoDbMapping()
        {
            BsonClassMap.RegisterClassMap<Entity>(
                classMap => classMap.SetIdMember(classMap.GetMemberMap(entity => entity.Id)));
        }
    }
}