using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaskManager.Bll.Abstract.Converter;
using TaskManager.Models.Attributes;
using TaskManager.Models.Result;

namespace TaskManager.Bll.Impl.Services.Converter
{
    public class HttpStatusConverterService : IConverterService<int, ResponseMessageType>
    {
        private readonly Dictionary<ResponseMessageType, int> _msgToCode =
            new Dictionary<ResponseMessageType, int>();

        public HttpStatusConverterService()
        {
            foreach (ResponseMessageType messageType in Enum.GetValues(typeof(ResponseMessageType)))
            {
                MemberInfo memberInfo = messageType.GetType()
                    .GetMember(messageType.ToString()).FirstOrDefault();

                if (memberInfo != null)
                {
                    HttpStatusAttribute attribute = memberInfo.GetCustomAttribute<HttpStatusAttribute>();

                    if (attribute == null)
                        throw new MissingFieldException($"HttpStatusAttribute is absent in {messageType}");
                    _msgToCode.Add(messageType, attribute.Value);
                }

                else throw new TargetInvocationException(
                    new TargetException($"Retrieving meta-data of {typeof(HttpStatusAttribute)} crashed"));
            }
        }

        public int Convert(ResponseMessageType messageType)
        {
            return _msgToCode[messageType];
        }
    }
}
