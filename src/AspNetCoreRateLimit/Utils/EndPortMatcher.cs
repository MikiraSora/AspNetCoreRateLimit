using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AspNetCoreRateLimit.Utils
{
    public class EndPortMatcher
    {
        private static Dictionary<RateLimitRule, Regex> CacheRegexMap = new Dictionary<RateLimitRule, Regex>();

        private static Regex GetRegex(RateLimitRule rule)
        {
            if (CacheRegexMap.TryGetValue(rule, out var r))
                return r;
            r = new Regex(WildCardToRegular(rule.Endpoint), RegexOptions.IgnoreCase);
            CacheRegexMap[rule] = r;
            return r;
        }

        public static IEnumerable<RateLimitRule> Checked(IEnumerable<RateLimitRule> rules, ClientRequestIdentity identity)
        {
            List<RateLimitRule> result=new List<RateLimitRule>();
            var check_string = $"{identity.HttpVerb}:{identity.Path}";

            foreach (var rule in rules)
            {
                var regex = GetRegex(rule);

                if (regex.IsMatch(check_string))
                {
                    result.Add(rule);
                }
            }

            return result;
        }

        private static String WildCardToRegular(String value)
        {
            return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }
    }
}
