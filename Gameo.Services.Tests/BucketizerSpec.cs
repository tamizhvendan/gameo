using System.Linq;
using NUnit.Framework;
using Should;

namespace Gameo.Services.Tests
{
    [TestFixture]
    public class BucketizerSpec
    {
        private class SampleClassForTest
        {
            public int PredicateProperty { get; set; }
        }

        private Bucketizer<SampleClassForTest> bucketizer;
        private SampleClassForTest[] bucketContents;

        [SetUp]
        public void SetUp()
        {
            bucketContents = new[]
                                 {
                                     new SampleClassForTest {PredicateProperty = 0},
                                     new SampleClassForTest {PredicateProperty = 1},
                                     new SampleClassForTest {PredicateProperty = 2},
                                     new SampleClassForTest {PredicateProperty = 3},
                                     new SampleClassForTest {PredicateProperty = 4},
                                     new SampleClassForTest {PredicateProperty = 5}
                                 };

            bucketizer = new Bucketizer<SampleClassForTest>(bucketContents);
        }

        [Test]
        public void Should_put_data_in_buckets_by_rule()
        {
            bucketizer.AddRule("Zero", bucketContent => bucketContent.PredicateProperty == 0);
            bucketizer.AddRule("1-2", bucketContent => bucketContent.PredicateProperty >= 1 && bucketContent.PredicateProperty <= 2);
            bucketizer.AddRule("Greater than 3", bucketContent => bucketContent.PredicateProperty >= 3);
            bucketizer.AddRule("Less than zero", bucketContent => bucketContent.PredicateProperty < 0);

            var buckets = bucketizer.Bucketify().ToList();

            var firstBucket = buckets.ElementAt(0);
            var secondBucket = buckets.ElementAt(1);
            var thirdBucket = buckets.ElementAt(2);
            var fourthBucket = buckets.ElementAt(3);

            firstBucket.Label.ShouldEqual("Zero");
            firstBucket.Values.ShouldEqual(bucketContents.Take(1));
            secondBucket.Label.ShouldEqual("1-2");
            secondBucket.Values.ShouldEqual(bucketContents.Skip(1).Take(2));
            thirdBucket.Label.ShouldEqual("Greater than 3");
            thirdBucket.Values.ShouldEqual(bucketContents.Skip(3));
            fourthBucket.Label.ShouldEqual("Less than zero");
            fourthBucket.Values.ShouldEqual(Enumerable.Empty<SampleClassForTest>());
        }
    }
}