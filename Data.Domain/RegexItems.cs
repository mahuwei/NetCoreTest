using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Project.Domain {
    public class RegexItem {
        public string Name { get; set; }
        public string NameCn { get; set; }
        public string RegexString { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class RegexItems {
        public const string Num = "Num";
        public const string NumChar = "NumAndChar";
        public const string Ncc = "NCC";
        public const string MobileNo = "MobileNo";

        private static List<RegexItem> _regexItems;

        public static List<RegexItem> Get() {
            if (_regexItems != null) return _regexItems;

            var jsons = File.ReadAllText("RegexItems.json");
            _regexItems = JsonConvert.DeserializeObject<List<RegexItem>>(jsons);
            return _regexItems;
        }
    }
}