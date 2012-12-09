using Gameo.Domain;
using MongoDB.Bson.Serialization;

namespace Gameo.DataAccess
{
    public static class MongoClassMap
    {
        static MongoClassMap()
        {
            BsonClassMap.
                RegisterClassMap<Entity>(classMap => classMap.SetIdMember(classMap.GetMemberMap(entity => entity.Id)));
        }
    }
}