using Newtonsoft.Json;

namespace VerifyLoopReference
{
    [UsesVerify]
    public class UnitTest1
    {
        
        private A ListReferenceData()
        {
            var act = new A()
            {
                Bs = new List<B>()
            };
            act.Bs.Add(new B()
            {
                A = act
            });
            act.Bs.Add(new B()
            {
                A = act
            });
            return act;
        }


        private A SimpleReferenceData()
        {
            var act = new A()
            {
                B = new B()
            };
            act.B.A = act;
            return act;
        }

        [Fact] // OK
        public async Task Simple_DefaultSetting()
        {
            await Verify(SimpleReferenceData());
        }

        [Fact] // OK
        public async Task Simple_IgnoreLoopReference()
        {
            var settings = new VerifySettings();
            settings.AddExtraSettings(jsonSetting =>
            {
                jsonSetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            await Verify(SimpleReferenceData(), settings);
        }

        [Fact] // OK
        public async Task Simple_SerializeNewtonJson()
        {
            var jsonString = JsonConvert.SerializeObject(SimpleReferenceData(), new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            await VerifyJson(jsonString);
        }

        [Fact] // Stack overflow
        public async Task List_DefaultSetting()
        {
            await Verify(ListReferenceData());
        }

        [Fact] // Stack overflow
        public async Task List_IgnoreLoopReference()
        {
            var settings = new VerifySettings();
            settings.AddExtraSettings(jsonSetting =>
            {
                jsonSetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            await Verify(ListReferenceData(), settings);
        }

        [Fact] // OK
        public async Task List_SerializeNewtonJson()
        {
            var jsonString = JsonConvert.SerializeObject(ListReferenceData(), new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            await VerifyJson(jsonString);
        }
    }
}