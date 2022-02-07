using System;

namespace ExtensionMethodTest
{
    public class Employee : People
    {
        public string Company { get; set; }
        public string ContactInformation => $"Company: {Company}{Environment.NewLine}{this.GetContactInformation()}";
    }
}