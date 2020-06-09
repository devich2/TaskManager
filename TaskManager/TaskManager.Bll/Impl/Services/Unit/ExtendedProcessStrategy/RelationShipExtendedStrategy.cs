// using System;
// using System.Collections.Generic;
// using System.Security.Cryptography.X509Certificates;
// using System.Text;
// using System.Threading.Tasks;
// using AutoMapper;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Options;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;
// using TaskManager.Bll.Abstract.Unit.ExtendedProcessStrategy.Base;
// using TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy.Base;
// using TaskManager.Dal.Abstract.IRepository;
// using TaskManager.Entities.Tables;
// using TaskManager.Models.RelationShip;
// using TaskManager.Models.Response;
//
//
// namespace TaskManager.Bll.Impl.Services.Unit.ExtendedProcessStrategy
// {
//     public class RelationShipExtendedStrategy : BaseStrategy<int, Entities.Tables.RelationShip, RelationShipModel>, IUnitExtendedStrategy
//     {
//
//         public RelationShipExtendedStrategy(IRelationShipRepository relationShipRepository,
//                                 IMapper mapper, IOptions<MvcNewtonsoftJsonOptions> jsonOptions)
//             : base(relationShipRepository, mapper, jsonOptions)
//         {
//         }
//
//         protected override bool ContinueProcessing(RelationShipModel model) => model.ParentUnitId != null;
//     }
// }
