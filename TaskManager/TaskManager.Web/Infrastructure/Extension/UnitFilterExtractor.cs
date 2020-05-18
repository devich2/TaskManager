using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.Models.Response;

namespace TaskManager.Web.Infrastructure.Extension
{
    public static class UnitFilterExtractor
    {
        private static readonly Dictionary<ContentFilterType,
            Tuple<Type, Func<string, Tuple<object, bool>>>> _filtersTypeAndConvertors;

        static ContentFilterExtractor()
        {
            _filtersTypeAndConvertors = new Dictionary<ContentFilterType, Tuple<Type, Func<string, Tuple<object, bool>>>>();
            //_filtersTypeAndConvertors.Add(ContentFilterType.IsDeleted,
            //    new Tuple<FormType,Func<string, Tuple<object,bool>>> (typeof(bool), (s) =>
            //    
            //    {
            //        bool res = Boolean.TryParse(s, out bool convRes);
            //        return new Tuple<object, bool>(convRes,res);
            //    }));

            _filtersTypeAndConvertors.Add(ContentFilterType.State,
                new Tuple<Type, Func<string, Tuple<object, bool>>>(typeof(State), (s) =>
                //TODO: think Possible migrate inside other class or replace
                //TODO:all logic to json serialize and use post method 
                {
                    bool res = Enum.TryParse(s, true, out State convRes);
                    return new Tuple<object, bool>(convRes, res);
                }));
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
                    UnitFilterType filterType;

                    if (Enum.TryParse(strKeyValue[0], true, out filterType))
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
