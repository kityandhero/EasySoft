// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var o = new ConfigurationBuilder().AddJsonContent(
        JsonConvertAssist.SerializeObject(new { a = 1 }),
        out var source
    )
    .Build();

_ = ChangeToken.OnChange(() => o.GetReloadToken(), () => { Console.WriteLine("changed"); });

Thread.Sleep(1000);

source.SetJsonContent(JsonConvertAssist.SerializeObject(new { a = 2 }));

Thread.Sleep(1000);

source.SetJsonContent(JsonConvertAssist.SerializeObject(new { a = 3 }));

Thread.Sleep(1000);