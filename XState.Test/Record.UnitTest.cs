namespace XState.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            dynamic record = new XState.Dynamic.Record();
            record.A = 1;
            Assert.True(record.A == 1);
            record.More = new XState.Dynamic.Record();
            record.More.B = 2;
            Assert.True(record.More.B == 2);
            Assert.True(record["More"]["B"] == 2);
        }
    }
}