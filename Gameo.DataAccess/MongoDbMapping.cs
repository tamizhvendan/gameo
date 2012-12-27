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
            var dateTimeSerializationOptions = new DateTimeSerializationOptions { Kind = DateTimeKind.Local };

            BsonClassMap.RegisterClassMap<Entity>(classMap =>
                                                      {
                                                          classMap.AutoMap();
                                                          classMap.SetIdMember(classMap.GetMemberMap(entity => entity.Id));
                                                      });

            BsonClassMap.RegisterClassMap<Game>(classMap =>
                                                    {
                                                        classMap.AutoMap();

                                                        classMap.GetMemberMap(game => game.InTime)
                                                            .SetSerializationOptions(dateTimeSerializationOptions);
                                                        classMap.GetMemberMap(game => game.OutTime)
                                                            .SetSerializationOptions(dateTimeSerializationOptions);
                                                    });

            BsonClassMap.RegisterClassMap<Membership>(classMap =>
                                                          {
                                                              classMap.AutoMap();
                                                              classMap.MapProperty(membership => membership.Games);
                                                              classMap.MapProperty(membership => membership.ReCharges);
                                                              classMap.GetMemberMap(membership => membership.IssuedOn)
                                                                  .SetSerializationOptions(dateTimeSerializationOptions);
                                                             
                                                          });

            BsonClassMap.RegisterClassMap<MembershipReCharge>(classMap =>
                                                                {
                                                                    classMap.AutoMap();
                                                                    classMap.GetMemberMap(membershipRefil => membershipRefil.RechargedOn)
                                                                        .SetSerializationOptions(dateTimeSerializationOptions);
                                                                });
        }
    }
}