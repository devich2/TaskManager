﻿using TaskManager.Models.Attributes;

namespace TaskManager.Models.Result
{
    public enum ResponseMessageType
    {
        [HttpStatus(200)]
        None,
        [HttpStatus(201)]
        EmptyResult,
        [HttpStatus(400)]
        InvalidModel,
        [HttpStatus(401)]
        UserNotAuthorized,
        [HttpStatus(500)]
        InternalError,
        [HttpStatus(400)]
        IncorrectParameter,
        [HttpStatus(400)]
        IdIsMissing,
        [HttpStatus(400)]
        InvalidId,
        [HttpStatus(404)]
        NotFound,
        [HttpStatus(500)]
        MailServiceError,
        [HttpStatus(400)]
        OperationNotAllowedForUnitType,
        [HttpStatus(400)]
        TermInfoMissing,
        [HttpStatus(400)]
        UnitAccessDenied,
        [HttpStatus(400)]
        SelfEditForbidden,
        [HttpStatus(400)]
        ProjectMemberNotExisting,
        [HttpStatus(400)]
        AlreadyInProject
    }
}
