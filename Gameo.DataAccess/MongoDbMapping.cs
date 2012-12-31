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
                                                        classMap.UnmapProperty(game => game.HoursPlayed);
                                                    });

            BsonClassMap.RegisterClassMap<Membership>(classMap =>
                                                          {
                                                              classMap.AutoMap();
                                                              classMap.UnmapProperty(membership => membership.ExpiresOn);
                                                              classMap.UnmapProperty(membership => membership.RemainingHours);
                                                              classMap.UnmapProperty(membership => membership.RemainingHours);
                                                              classMap.UnmapProperty(membership => membership.ExpiresOn);
                                                              /*classMap.MapProperty(membership => membership.Games);
                                                              classMap.MapProperty(membership => membership.ReCharges);*/
                                                              classMap.MapProperty(membership => membership.MembershipId);
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