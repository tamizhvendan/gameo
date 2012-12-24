using System;
using Gameo.Domain;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;

namespace Gameo.DataAccess
{
    public static class MongoDbMapping
    {
        public static void Initialize()
        {
            BsonClassMap.RegisterClassMap<Entity>(classMap =>
                                                      {
                                                          classMap.AutoMap();
                                                          classMap.SetIdMember(classMap.GetMemberMap(entity => entity.Id));
                                                      });
            BsonClassMap.RegisterClassMap<Game>(classMap =>
                                                    {
                                                        classMap.AutoMap();
                                                        classMap.GetMemberMap(game => game.InTime)
                                                            .SetSerializationOptions(new DateTimeSerializationOptions
                                                                                         {Kind = DateTimeKind.Local});
                                                        classMap.GetMemberMap(game => game.OutTime)
                                                            .SetSerializationOptions(new DateTimeSerializationOptions { Kind = DateTimeKind.Local });
                                                    });
        }
    }
}