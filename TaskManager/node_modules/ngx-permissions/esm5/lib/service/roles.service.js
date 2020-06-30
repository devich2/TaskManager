/**
 * @fileoverview added by tsickle
 * @suppress {checkTypes,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
 */
import * as tslib_1 from "tslib";
import { Inject, Injectable, InjectionToken } from '@angular/core';
import { BehaviorSubject, from, of } from 'rxjs';
import { catchError, every, first, map, mergeAll, mergeMap, switchMap } from 'rxjs/operators';
import { NgxRolesStore } from '../store/roles.store';
import { isBoolean, isFunction, isPromise, transformStringToArray } from '../utils/utils';
import { NgxPermissionsService } from './permissions.service';
/** @type {?} */
export var USE_ROLES_STORE = new InjectionToken('USE_ROLES_STORE');
var NgxRolesService = /** @class */ (function () {
    function NgxRolesService(isolate, rolesStore, permissionsService) {
        if (isolate === void 0) { isolate = false; }
        this.isolate = isolate;
        this.rolesStore = rolesStore;
        this.permissionsService = permissionsService;
        this.rolesSource = this.isolate ? new BehaviorSubject({}) : this.rolesStore.rolesSource;
        this.roles$ = this.rolesSource.asObservable();
    }
    /**
     * @param {?} name
     * @param {?} validationFunction
     * @return {?}
     */
    NgxRolesService.prototype.addRole = /**
     * @param {?} name
     * @param {?} validationFunction
     * @return {?}
     */
    function (name, validationFunction) {
        var _a;
        /** @type {?} */
        var roles = tslib_1.__assign({}, this.rolesSource.value, (_a = {}, _a[name] = { name: name, validationFunction: validationFunction }, _a));
        this.rolesSource.next(roles);
    };
    /**
     * @param {?} rolesObj
     * @return {?}
     */
    NgxRolesService.prototype.addRoles = /**
     * @param {?} rolesObj
     * @return {?}
     */
    function (rolesObj) {
        var _this = this;
        Object.keys(rolesObj).forEach((/**
         * @param {?} key
         * @param {?} index
         * @return {?}
         */
        function (key, index) {
            _this.addRole(key, rolesObj[key]);
        }));
    };
    /**
     * @return {?}
     */
    NgxRolesService.prototype.flushRoles = /**
     * @return {?}
     */
    function () {
        this.rolesSource.next({});
    };
    /**
     * @param {?} roleName
     * @return {?}
     */
    NgxRolesService.prototype.removeRole = /**
     * @param {?} roleName
     * @return {?}
     */
    function (roleName) {
        /** @type {?} */
        var roles = tslib_1.__assign({}, this.rolesSource.value);
        delete roles[roleName];
        this.rolesSource.next(roles);
    };
    /**
     * @return {?}
     */
    NgxRolesService.prototype.getRoles = /**
     * @return {?}
     */
    function () {
        return this.rolesSource.value;
    };
    /**
     * @param {?} name
     * @return {?}
     */
    NgxRolesService.prototype.getRole = /**
     * @param {?} name
     * @return {?}
     */
    function (name) {
        return this.rolesSource.value[name];
    };
    /**
     * @param {?} names
     * @return {?}
     */
    NgxRolesService.prototype.hasOnlyRoles = /**
     * @param {?} names
     * @return {?}
     */
    function (names) {
        /** @type {?} */
        var isNamesEmpty = !names || (Array.isArray(names) && names.length === 0);
        if (isNamesEmpty)
            return Promise.resolve(true);
        names = transformStringToArray(names);
        return Promise.all([this.hasRoleKey(names), this.hasRolePermission(this.rolesSource.value, names)])
            .then((/**
         * @param {?} __0
         * @return {?}
         */
        function (_a) {
            var _b = tslib_1.__read(_a, 2), hasRoles = _b[0], hasPermissions = _b[1];
            return hasRoles || hasPermissions;
        }));
    };
    /**
     * @private
     * @param {?} roleName
     * @return {?}
     */
    NgxRolesService.prototype.hasRoleKey = /**
     * @private
     * @param {?} roleName
     * @return {?}
     */
    function (roleName) {
        var _this = this;
        /** @type {?} */
        var promises = roleName.map((/**
         * @param {?} key
         * @return {?}
         */
        function (key) {
            /** @type {?} */
            var hasValidationFunction = !!_this.rolesSource.value[key] &&
                !!_this.rolesSource.value[key].validationFunction &&
                isFunction(_this.rolesSource.value[key].validationFunction);
            if (hasValidationFunction && !isPromise(_this.rolesSource.value[key].validationFunction)) {
                /** @type {?} */
                var validationFunction_1 = (/** @type {?} */ (_this.rolesSource.value[key].validationFunction));
                /** @type {?} */
                var immutableValue_1 = tslib_1.__assign({}, _this.rolesSource.value);
                return of(null).pipe(map((/**
                 * @return {?}
                 */
                function () { return validationFunction_1(key, immutableValue_1); })), switchMap((/**
                 * @param {?} promise
                 * @return {?}
                 */
                function (promise) { return isBoolean(promise) ?
                    of((/** @type {?} */ (promise))) : (/** @type {?} */ (promise)); })), catchError((/**
                 * @return {?}
                 */
                function () { return of(false); })));
            }
            return of(false);
        }));
        return from(promises).pipe(mergeAll(), first((/**
         * @param {?} data
         * @return {?}
         */
        function (data) { return data !== false; }), false), map((/**
         * @param {?} data
         * @return {?}
         */
        function (data) { return data !== false; }))).toPromise().then((/**
         * @param {?} data
         * @return {?}
         */
        function (data) { return data; }));
    };
    /**
     * @private
     * @param {?} roles
     * @param {?} roleNames
     * @return {?}
     */
    NgxRolesService.prototype.hasRolePermission = /**
     * @private
     * @param {?} roles
     * @param {?} roleNames
     * @return {?}
     */
    function (roles, roleNames) {
        var _this = this;
        return from(roleNames).pipe(mergeMap((/**
         * @param {?} key
         * @return {?}
         */
        function (key) {
            if (roles[key] && Array.isArray(roles[key].validationFunction)) {
                return from((/** @type {?} */ (roles[key].validationFunction))).pipe(mergeMap((/**
                 * @param {?} permission
                 * @return {?}
                 */
                function (permission) { return _this.permissionsService.hasPermission(permission); })), every((/**
                 * @param {?} hasPermissions
                 * @return {?}
                 */
                function (hasPermissions) { return hasPermissions === true; })));
            }
            return of(false);
        })), first((/**
         * @param {?} hasPermission
         * @return {?}
         */
        function (hasPermission) { return hasPermission === true; }), false)).toPromise();
    };
    NgxRolesService.decorators = [
        { type: Injectable }
    ];
    /** @nocollapse */
    NgxRolesService.ctorParameters = function () { return [
        { type: Boolean, decorators: [{ type: Inject, args: [USE_ROLES_STORE,] }] },
        { type: NgxRolesStore },
        { type: NgxPermissionsService }
    ]; };
    return NgxRolesService;
}());
export { NgxRolesService };
if (false) {
    /**
     * @type {?}
     * @private
     */
    NgxRolesService.prototype.rolesSource;
    /** @type {?} */
    NgxRolesService.prototype.roles$;
    /**
     * @type {?}
     * @private
     */
    NgxRolesService.prototype.isolate;
    /**
     * @type {?}
     * @private
     */
    NgxRolesService.prototype.rolesStore;
    /**
     * @type {?}
     * @private
     */
    NgxRolesService.prototype.permissionsService;
}
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoicm9sZXMuc2VydmljZS5qcyIsInNvdXJjZVJvb3QiOiJuZzovL25neC1wZXJtaXNzaW9ucy8iLCJzb3VyY2VzIjpbImxpYi9zZXJ2aWNlL3JvbGVzLnNlcnZpY2UudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7Ozs7QUFBQSxPQUFPLEVBQUUsTUFBTSxFQUFFLFVBQVUsRUFBRSxjQUFjLEVBQUUsTUFBTSxlQUFlLENBQUM7QUFFbkUsT0FBTyxFQUFFLGVBQWUsRUFBRSxJQUFJLEVBQStCLEVBQUUsRUFBRSxNQUFNLE1BQU0sQ0FBQztBQUM5RSxPQUFPLEVBQUUsVUFBVSxFQUFFLEtBQUssRUFBRSxLQUFLLEVBQUUsR0FBRyxFQUFFLFFBQVEsRUFBRSxRQUFRLEVBQUUsU0FBUyxFQUFFLE1BQU0sZ0JBQWdCLENBQUM7QUFHOUYsT0FBTyxFQUFFLGFBQWEsRUFBRSxNQUFNLHNCQUFzQixDQUFDO0FBQ3JELE9BQU8sRUFBRSxTQUFTLEVBQUUsVUFBVSxFQUFFLFNBQVMsRUFBRSxzQkFBc0IsRUFBRSxNQUFNLGdCQUFnQixDQUFDO0FBQzFGLE9BQU8sRUFBRSxxQkFBcUIsRUFBRSxNQUFNLHVCQUF1QixDQUFDOztBQUU5RCxNQUFNLEtBQU8sZUFBZSxHQUFHLElBQUksY0FBYyxDQUFDLGlCQUFpQixDQUFDO0FBSXBFO0lBT0kseUJBQ3FDLE9BQXdCLEVBQ2pELFVBQXlCLEVBQ3pCLGtCQUF5QztRQUZoQix3QkFBQSxFQUFBLGVBQXdCO1FBQXhCLFlBQU8sR0FBUCxPQUFPLENBQWlCO1FBQ2pELGVBQVUsR0FBVixVQUFVLENBQWU7UUFDekIsdUJBQWtCLEdBQWxCLGtCQUFrQixDQUF1QjtRQUVqRCxJQUFJLENBQUMsV0FBVyxHQUFHLElBQUksQ0FBQyxPQUFPLENBQUMsQ0FBQyxDQUFDLElBQUksZUFBZSxDQUFpQixFQUFFLENBQUMsQ0FBQyxDQUFDLENBQUMsSUFBSSxDQUFDLFVBQVUsQ0FBQyxXQUFXLENBQUM7UUFDeEcsSUFBSSxDQUFDLE1BQU0sR0FBRyxJ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fSBmcm9tICdAYW5ndWxhci9jb3JlJztcclxuXHJcbmltcG9ydCB7IEJlaGF2aW9yU3ViamVjdCwgZnJvbSwgT2JzZXJ2YWJsZSwgT2JzZXJ2YWJsZUlucHV0LCBvZiB9IGZyb20gJ3J4anMnO1xyXG5pbXBvcnQgeyBjYXRjaEVycm9yLCBldmVyeSwgZmlyc3QsIG1hcCwgbWVyZ2VBbGwsIG1lcmdlTWFwLCBzd2l0Y2hNYXAgfSBmcm9tICdyeGpzL29wZXJhdG9ycyc7XHJcblxyXG5pbXBvcnQgeyBOZ3hSb2xlIH0gZnJvbSAnLi4vbW9kZWwvcm9sZS5tb2RlbCc7XHJcbmltcG9ydCB7IE5neFJvbGVzU3RvcmUgfSBmcm9tICcuLi9zdG9yZS9yb2xlcy5zdG9yZSc7XHJcbmltcG9ydCB7IGlzQm9vbGVhbiwgaXNGdW5jdGlvbiwgaXNQcm9taXNlLCB0cmFuc2Zvcm1TdHJpbmdUb0FycmF5IH0gZnJvbSAnLi4vdXRpbHMvdXRpbHMnO1xyXG5pbXBvcnQgeyBOZ3hQZXJtaXNzaW9uc1NlcnZpY2UgfSBmcm9tICcuL3Blcm1pc3Npb25zLnNlcnZpY2UnO1xyXG5cclxuZXhwb3J0IGNvbnN0IFVTRV9ST0xFU19TVE9SRSA9IG5ldyBJbmplY3Rpb25Ub2tlbignVVNFX1JPTEVTX1NUT1JFJyk7XHJcblxyXG5leHBvcnQgdHlwZSBOZ3hSb2xlc09iamVjdCA9IHsgW25hbWU6IHN0cmluZ106IE5neFJvbGUgfTtcclxuXHJcbkBJbmplY3RhYmxlKClcclxuZXhwb3J0IGNsYXNzIE5neFJvbGVzU2VydmljZSB7XHJcblxyXG4gICAgcHJpdmF0ZSByb2xlc1NvdXJjZTogQmVoYXZpb3JTdWJqZWN0PE5neFJvbGVzT2JqZWN0PjtcclxuXHJcbiAgICBwdWJsaWMgcm9sZXMkOiBPYnNlcnZhYmxlPE5neFJvbGVzT2JqZWN0PjtcclxuXHJcbiAgICBjb25zdHJ1Y3RvcihcclxuICAgICAgICBASW5qZWN0KFVTRV9ST0xFU19TVE9SRSkgcHJpdmF0ZSBpc29sYXRlOiBib29sZWFuID0gZmFsc2UsXHJcbiAgICAgICAgcHJpdmF0ZSByb2xlc1N0b3JlOiBOZ3hSb2xlc1N0b3JlLFxyXG4gICAgICAgIHByaXZhdGUgcGVybWlzc2lvbnNTZXJ2aWNlOiBOZ3hQZXJtaXNzaW9uc1NlcnZpY2VcclxuICAgICkge1xyXG4gICAgICAgIHRoaXMucm9sZXNTb3VyY2UgPSB0aGlzLmlzb2xhdGUgPyBuZXcgQmVoYXZpb3JTdWJqZWN0PE5neFJvbGVzT2JqZWN0Pih7fSkgOiB0aGlzLnJvbGVzU3RvcmUucm9sZXNTb3VyY2U7XHJcbiAgICAgICAgdGhpcy5yb2xlcyQgPSB0aGlzLnJvbGVzU291cmNlLmFzT2JzZXJ2YWJsZSgpO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBhZGRSb2xlKG5hbWU6IHN0cmluZywgdmFsaWRhdGlvbkZ1bmN0aW9uOiBGdW5jdGlvbiB8IHN0cmluZ1tdKSB7XHJcbiAgICAgICAgY29uc3Qgcm9sZXMgPSB7XHJcbiAgICAgICAgICAgIC4uLnRoaXMucm9sZXNTb3VyY2UudmFsdWUsXHJcbiAgICAgICAgICAgIFtuYW1lXToge25hbWUsIHZhbGlkYXRpb25GdW5jdGlvbn1cclxuICAgICAgICB9O1xyXG4gICAgICAgIHRoaXMucm9sZXNTb3VyY2UubmV4dChyb2xlcyk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGFkZFJvbGVzKHJvbGVzT2JqOiB7IFtuYW1lOiBzdHJpbmddOiBGdW5jdGlvbiB8IHN0cmluZ1tdIH0pIHtcclxuICAgICAgICBPYmplY3Qua2V5cyhyb2xlc09iaikuZm9yRWFjaCgoa2V5LCBpbmRleCkgPT4ge1xyXG4gICAgICAgICAgICB0aGlzLmFkZFJvbGUoa2V5LCByb2xlc09ialtrZXldKTtcclxuICAgICAgICB9KTtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgZmx1c2hSb2xlcygpIHtcclxuICAgICAgICB0aGlzLnJvbGVzU291cmNlLm5leHQoe30pO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyByZW1vdmVSb2xlKHJvbGVOYW1lOiBzdHJpbmcpIHtcclxuICAgICAgICBsZXQgcm9sZXMgPSB7XHJcbiAgICAgICAgICAgIC4uLnRoaXMucm9sZXNTb3VyY2UudmFsdWVcclxuICAgICAgICB9O1xyXG4gICAgICAgIGRlbGV0ZSByb2xlc1tyb2xlTmFtZV07XHJcbiAgICAgICAgdGhpcy5yb2xlc1NvdXJjZS5uZXh0KHJvbGVzKTtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgZ2V0Um9sZXMoKSB7XHJcbiAgICAgICAgcmV0dXJuIHRoaXMucm9sZXNTb3VyY2UudmFsdWU7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGdldFJvbGUobmFtZTogc3RyaW5nKSB7XHJcbiAgICAgICAgcmV0dXJuIHRoaXMucm9sZXNTb3VyY2UudmFsdWVbbmFtZV07XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGhhc09ubHlSb2xlcyhuYW1lczogc3RyaW5nIHwgc3RyaW5nW10pOiBQcm9taXNlPGJvb2xlYW4+IHtcclxuICAgICAgICBjb25zdCBpc05hbWVzRW1wdHkgPSAhbmFtZXMgfHwgKEFycmF5LmlzQXJyYXkobmFtZXMpICYmIG5hbWVzLmxlbmd0aCA9PT0gMCk7XHJcblxyXG4gICAgICAgIGlmIChpc05hbWVzRW1wdHkpIHJldHVybiBQcm9taXNlLnJlc29sdmUodHJ1ZSk7XHJcblxyXG4gICAgICAgIG5hbWVzID0gdHJhbnNmb3JtU3RyaW5nVG9BcnJheShuYW1lcyk7XHJcblxyXG4gICAgICAgIHJldHVybiBQcm9taXNlLmFsbChbdGhpcy5oYXNSb2xlS2V5KG5hbWVzKSwgdGhpcy5oYXNSb2xlUGVybWlzc2lvbih0aGlzLnJvbGVzU291cmNlLnZhbHVlLCBuYW1lcyldKVxyXG4gICAgICAgICAgICAudGhlbigoW2hhc1JvbGVzLCBoYXNQZXJtaXNzaW9uc106IFtib29sZWFuLCBib29sZWFuXSkgPT4ge1xyXG4gICAgICAgICAgICAgICAgcmV0dXJuIGhhc1JvbGVzIHx8IGhhc1Blcm1pc3Npb25zO1xyXG4gICAgICAgICAgICB9KTtcclxuICAgIH1cclxuXHJcbiAgICBwcml2YXRlIGhhc1JvbGVLZXkocm9sZU5hbWU6IHN0cmluZ1tdKTogUHJvbWlzZTxib29sZWFuPiB7XHJcbiAgICAgICAgY29uc3QgcHJvbWlzZXM6IE9ic2VydmFibGU8Ym9vbGVhbj5bXSA9IHJvbGVOYW1lLm1hcCgoa2V5KSA9PiB7XHJcbiAgICAgICAgICAgIGNvbnN0IGhhc1ZhbGlkYXRpb25GdW5jdGlvbiA9ICEhdGhpcy5yb2xlc1NvdXJjZS52YWx1ZVtrZXldICYmXHJcbiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICEhdGhpcy5yb2xlc1NvdXJjZS52YWx1ZVtrZXldLnZhbGlkYXRpb25GdW5jdGlvbiAmJlxyXG4gICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICBpc0Z1bmN0aW9uKHRoaXMucm9sZXNTb3VyY2UudmFsdWVba2V5XS52YWxpZGF0aW9uRnVuY3Rpb24pO1xyXG5cclxuICAgICAgICAgICAgaWYgKGhhc1ZhbGlkYXRpb25GdW5jdGlvbiAmJiAhaXNQcm9taXNlKHRoaXMucm9sZXNTb3VyY2UudmFsdWVba2V5XS52YWxpZGF0aW9uRnVuY3Rpb24pKSB7XHJcbiAgICAgICAgICAgICAgICBjb25zdCB2YWxpZGF0aW9uRnVuY3Rpb246IEZ1bmN0aW9uID0gPEZ1bmN0aW9uPnRoaXMucm9sZXNTb3VyY2UudmFsdWVba2V5XS52YWxpZGF0aW9uRnVuY3Rpb247XHJcbiAgICAgICAgICAgICAgICBjb25zdCBpbW11dGFibGVWYWx1ZSA9IHsuLi50aGlzLnJvbGVzU291cmNlLnZhbHVlfTtcclxuXHJcbiAgICAgICAgICAgICAgICByZXR1cm4gb2YobnVsbCkucGlwZShcclxuICAgICAgICAgICAgICAgICAgICBtYXAoKCkgPT4gdmFsaWRhdGlvbkZ1bmN0aW9uKGtleSwgaW1tdXRhYmxlVmFsdWUpKSxcclxuICAgICAgICAgICAgICAgICAgICBzd2l0Y2hNYXAoKHByb21pc2U6IFByb21pc2U8Ym9vbGVhbj4gfCBib29sZWFuKTogT2JzZXJ2YWJsZUlucHV0PGJvb2xlYW4+ID0+IGlzQm9vbGVhbihwcm9taXNlKSA/XHJcbiAgICAgICAgICAgICAgICAgICAgICAgIG9mKHByb21pc2UgYXMgYm9vbGVhbikgOiBwcm9taXNlIGFzIFByb21pc2U8Ym9vbGVhbj4pLFxyXG4gICAgICAgICAgICAgICAgICAgIGNhdGNoRXJyb3IoKCkgPT4gb2YoZmFsc2UpKVxyXG4gICAgICAgICAgICAgICAgKTtcclxuICAgICAgICAgICAgfVxyXG5cclxuICAgICAgICAgICAgcmV0dXJuIG9mKGZhbHNlKTtcclxuICAgICAgICB9KTtcclxuXHJcbiAgICAgICAgcmV0dXJuIGZyb20ocHJvbWlzZXMpLnBpcGUoXHJcbiAgICAgICAgICAgIG1lcmdlQWxsKCksXHJcbiAgICAgICAgICAgIGZpcnN0KChkYXRhOiBhbnkpID0+IGRhdGEgIT09IGZhbHNlLCBmYWxzZSksXHJcbiAgICAgICAgICAgIG1hcCgoZGF0YSkgPT4gZGF0YSAhPT0gZmFsc2UpXHJcbiAgICAgICAgKS50b1Byb21pc2UoKS50aGVuKChkYXRhOiBhbnkpID0+IGRhdGEpO1xyXG4gICAgfVxyXG5cclxuICAgIHByaXZhdGUgaGFzUm9sZVBlcm1pc3Npb24ocm9sZXM6IE5neFJvbGVzT2JqZWN0LCByb2xlTmFtZXM6IHN0cmluZ1tdKTogUHJvbWlzZTxib29sZWFuPiB7XHJcbiAgICAgICAgcmV0dXJuIGZyb20ocm9sZU5hbWVzKS5waXBlKFxyXG4gICAgICAgICAgICBtZXJnZU1hcCgoa2V5KSA9PiB7XHJcbiAgICAgICAgICAgICAgICBpZiAocm9sZXNba2V5XSAmJiBBcnJheS5pc0FycmF5KHJvbGVzW2tleV0udmFsaWRhdGlvbkZ1bmN0aW9uKSkge1xyXG4gICAgICAgICAgICAgICAgICAgIHJldHVybiBmcm9tKDxzdHJpbmdbXT5yb2xlc1trZXldLnZhbGlkYXRpb25GdW5jdGlvbikucGlwZShcclxuICAgICAgICAgICAgICAgICAgICAgICAgbWVyZ2VNYXAoKHBlcm1pc3Npb24pID0+IHRoaXMucGVybWlzc2lvbnNTZXJ2aWNlLmhhc1Blcm1pc3Npb24ocGVybWlzc2lvbikpLFxyXG4gICAgICAgICAgICAgICAgICAgICAgICBldmVyeSgoaGFzUGVybWlzc2lvbnMpID0+IGhhc1Blcm1pc3Npb25zID09PSB0cnVlKVxyXG4gICAgICAgICAgICAgICAgICAgICk7XHJcbiAgICAgICAgICAgICAgICB9XHJcblxyXG4gICAgICAgICAgICAgICAgcmV0dXJuIG9mKGZhbHNlKTtcclxuICAgICAgICAgICAgfSksXHJcbiAgICAgICAgICAgIGZpcnN0KChoYXNQZXJtaXNzaW9uKSA9PiBoYXNQZXJtaXNzaW9uID09PSB0cnVlLCBmYWxzZSlcclxuICAgICAgICApLnRvUHJvbWlzZSgpO1xyXG4gICAgfVxyXG5cclxufVxyXG4iXX0=