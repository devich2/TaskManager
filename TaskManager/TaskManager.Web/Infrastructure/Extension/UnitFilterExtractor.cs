using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entities.Enum;
using TaskManager.Models;
using TaskManager.Models.Result;
using static System.Int32;

namespace TaskManager.Web.Infrastructure.Extension
{
    public static class UnitFilterExtractor
    {
        private static readonly Dictionary<UnitFilterType,
            Tuple<Type, Func<string, Tuple<object, bool>>>> _filtersTypeAndConvertors;

        static UnitFilterExtractor()
        {
            Tuple<object, bool> IntParser(string s)
            {
                bool res = TryParse(s, out var convRes);
                return new Tuple<object, bool>(convRes, res);
            }

            _filtersTypeAndConvertors = new Dictionary<UnitFilterType, Tuple<Type, Func<string, Tuple<object, bool>>>>
            {
                {
                    UnitFilterType.Status, new Tuple<Type, Func<string, Tuple<object, bool>>>(typeof(Status), (s) =>
                    {
                        bool res = Enum.TryParse(s, true, out Status convRes);
                        return new Tuple<object, bool>(convRes, res);
                    })
                },
                {UnitFilterType.Assignee, new Tuple<Type, Func<string, Tuple<object, bool>>>(typeof(int), IntParser)},
                {UnitFilterType.MileStone, new Tuple<Type, Func<string, Tuple<object, bool>>>(typeof(int), IntParser)},
                {UnitFilterType.Project, new Tuple<Type, Func<string, Tuple<object, bool>>>(typeof(int), IntParser)},
                {UnitFilterType.Author, new Tuple<Type, Func<string, Tuple<object, bool>>>(typeof(int), IntParser)},
                {
                    UnitFilterType.Label, new Tuple<Type, Func<string, Tuple<object, bool>>>(typeof(int),
                        (s) => new Tuple<object, bool>(s, true))
                }
            };
        }

        public static DataResult<FilterOptions> ExtractFilterOptionsDataResult(string query)
        {
            if (string.IsNullOrEmpty(query))
                return new DataResult<FilterOptions>()
                {
                    ResponseStatusType = ResponseStatusType.Succeed,
                    Message = ResponseMessageType.EmptyResult
                };

            FilterOptions fo = new FilterOptions();
            string[] filterItems = query.Split(',',
                StringSplitOptions.RemoveEmptyEntries);

            if (filterItems.Length == 0)
            {
                return new DataResult<FilterOptions>()
                {
                    Message = ResponseMessageType.InvalidModel,
                    ResponseStatusType = ResponseStatusType.Error
                };
            }

            fo.Filters = new Dictionary<UnitFilterType, dynamic>();

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
                    if (Enum.TryParse(strKeyValue[0], true, out UnitFilterType filterType))
                    {
                        Tuple<object, bool> res =
                            _filtersTypeAndConvertors[filterType].Item2.Invoke(strKeyValue[1]);

                        if (res.Item2)
                        {
                            fo.Filters.Add(filterType, res.Item1);
                        }
                        else
                        {
                            sbValidationErrors
                                .AppendLine($"Cant convert {strKeyValue[1]} to type " +
                                            $"{_filtersTypeAndConvertors[filterType].Item1.ToString()}");
                        }
                    }
                    else
                    {
                        sbValidationErrors.AppendLine($"invalid filter key {strKeyValue[0]}");
                    }
                }
            }

            string errorStr = sbValidationErrors.ToString();

            if (String.IsNullOrWhiteSpace(errorStr))
            {
                return new DataResult<FilterOptions>()
                {
                    Data = fo,
                    ResponseStatusType = ResponseStatusType.Succeed
                };
            }

            return new DataResult<FilterOptions>()
            {
                ResponseStatusType = ResponseStatusType.Error,
                Message = ResponseMessageType.InvalidModel,
                MessageDetails = errorStr
            };
        }
    }
}