using System;
using System.Text;

namespace ExtensionMethodTest
{
    public static class ExtractContactInformation
    {
        public static string GetContactInformation(this People people) =>
            new StringBuilder($"Name: {people.Name}").Append(Environment.NewLine)
                .Append($"E-mail: {people.Email}").Append(Environment.NewLine)
                .Append($"Phone: {people.Phone}").Append(Environment.NewLine)
                .Append($"Cellphone: {people.Cellphone}").ToString();
    }
}
