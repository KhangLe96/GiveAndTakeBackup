using System.Collections.Generic;

namespace Giveaway.API.Shared.Helpers
{
    public static class ValidateHelper
    {
        public static string ValidateInput(IDictionary<string, string> @params, IList<string> fields)
        {
            var error = "";
            foreach (string item in fields)
            {
                if (!@params.ContainsKey(item))
                {
                    error = error + item + " is required field. ";
                }
            }
            return error;
        }
    }
}
