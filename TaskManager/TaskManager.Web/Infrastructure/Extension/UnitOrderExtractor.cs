using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.Models.Result;

namespace TaskManager.Web.Infrastructure.Extension
{
    public static class UnitOrderExtractor
    {
        public static DataResult<SortingOptions> ExtractSortingOptionsDataResult(string orderQuery)
        {
            if (string.IsNullOrEmpty(orderQuery))
                return new DataResult<SortingOptions>()
                {
                    ResponseStatusType = ResponseStatusType.Succeed,
                    Message = ResponseMessageType.EmptyResult
                };

            SortingOptions oo = new SortingOptions();
            string[] filterItems = orderQuery.Split(',',
                StringSplitOptions.RemoveEmptyEntries);

            if (filterItems.Length == 0)
            {
                return new DataResult<SortingOptions>()
                {
                    Message = ResponseMessageType.InvalidModel,
                    ResponseStatusType = ResponseStatusType.Error
                };
            }

            oo.Sortings = new Dictionary<string, SortingType>();

            StringBuilder sbValidationErrors = new StringBuilder();

            foreach (string filterItem in filterItems)
            {

                string[] strKeyValue = filterItem.Split(':',
                    StringSplitOptions.RemoveEmptyEntries);

                if (strKeyValue.Length != 2)
                {
                    sbValidationErrors.AppendLine(filterItem);
                }
                else
                {
                    string normalizedField = strKeyValue[0]
                        .Trim()
                        .Replace(" ", String.Empty)
                        .ToLower();

                    if (oo.Sortings.ContainsKey(normalizedField))
                    {
                        sbValidationErrors.AppendLine($"Multiply using : {normalizedField}");
                    }
                    else
                    {
                        if (Enum.TryParse(strKeyValue[1], true, out SortingType type))
                        {
                            oo.Sortings.Add(normalizedField, type);
                        }
                        else
                        {
                            sbValidationErrors.AppendLine($"Invalid sort value : {strKeyValue[1]}");
                        }
                    }
                }
            }

            string errorStr = sbValidationErrors.ToString();

            if (String.IsNullOrWhiteSpace(errorStr))
            {
                return new DataResult<SortingOptions>()
                {
                    Data = oo,
                    ResponseStatusType = ResponseStatusType.Succeed
                };
            }

            return new DataResult<SortingOptions>()
            {
                ResponseStatusType = ResponseStatusType.Error,
                Message = ResponseMessageType.InvalidModel,
                MessageDetails = errorStr
            };
        }
    }
}
