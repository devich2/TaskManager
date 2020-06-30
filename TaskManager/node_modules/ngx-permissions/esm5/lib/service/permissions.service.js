/**
 * @fileoverview added by tsickle
 * @suppress {checkTypes,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
 */
import * as tslib_1 from "tslib";
import { Inject, Injectable, InjectionToken } from '@angular/core';
import { BehaviorSubject, from, of } from 'rxjs';
import { catchError, first, map, mergeAll, switchMap } from 'rxjs/operators';
import { NgxPermissionsStore } from '../store/permissions.store';
import { isBoolean, isFunction, transformStringToArray } from '../utils/utils';
/** @type {?} */
export var USE_PERMISSIONS_STORE = new InjectionToken('USE_PERMISSIONS_STORE');
var NgxPermissionsService = /** @class */ (function () {
    function NgxPermissionsService(isolate, permissionsStore) {
        if (isolate === void 0) { isolate = false; }
        this.isolate = isolate;
        this.permissionsStore = permissionsStore;
        this.permissionsSource = isolate ? new BehaviorSubject({}) : permissionsStore.permissionsSource;
        this.permissions$ = this.permissionsSource.asObservable();
    }
    /**
     * Remove all permissions from permissions source
     */
    /**
     * Remove all permissions from permissions source
     * @return {?}
     */
    NgxPermissionsService.prototype.flushPermissions = /**
     * Remove all permissions from permissions source
     * @return {?}
     */
    function () {
        this.permissionsSource.next({});
    };
    /**
     * @param {?} permission
     * @return {?}
     */
    NgxPermissionsService.prototype.hasPermission = /**
     * @param {?} permission
     * @return {?}
     */
    function (permission) {
        if (!permission || (Array.isArray(permission) && permission.length === 0)) {
            return Promise.resolve(true);
        }
        permission = transformStringToArray(permission);
        return this.hasArrayPermission(permission);
    };
    /**
     * @param {?} permissions
     * @param {?=} validationFunction
     * @return {?}
     */
    NgxPermissionsService.prototype.loadPermissions = /**
     * @param {?} permissions
     * @param {?=} validationFunction
     * @return {?}
     */
    function (permissions, validationFunction) {
        var _this = this;
        /** @type {?} */
        var newPermissions = permissions.reduce((/**
         * @param {?} source
         * @param {?} p
         * @return {?}
         */
        function (source, p) {
            return _this.reducePermission(source, p, validationFunction);
        }), {});
        this.permissionsSource.next(newPermissions);
    };
    /**
     * @param {?} permission
     * @param {?=} validationFunction
     * @return {?}
     */
    NgxPermissionsService.prototype.addPermission = /**
     * @param {?} permission
     * @param {?=} validationFunction
     * @return {?}
     */
    function (permission, validationFunction) {
        var _this = this;
        if (Array.isArray(permission)) {
            /** @type {?} */
            var permissions = permission.reduce((/**
             * @param {?} source
             * @param {?} p
             * @return {?}
             */
            function (source, p) {
                return _this.reducePermission(source, p, validationFunction);
            }), this.permissionsSource.value);
            this.permissionsSource.next(permissions);
        }
        else {
            /** @type {?} */
            var permissions = this.reducePermission(this.permissionsSource.value, permission, validationFunction);
            this.permissionsSource.next(permissions);
        }
    };
    /**
     * @param {?} permissionName
     * @return {?}
     */
    NgxPermissionsService.prototype.removePermission = /**
     * @param {?} permissionName
     * @return {?}
     */
    function (permissionName) {
        /** @type {?} */
        var permissions = tslib_1.__assign({}, this.permissionsSource.value);
        delete permissions[permissionName];
        this.permissionsSource.next(permissions);
    };
    /**
     * @param {?} name
     * @return {?}
     */
    NgxPermissionsService.prototype.getPermission = /**
     * @param {?} name
     * @return {?}
     */
    function (name) {
        return this.permissionsSource.value[name];
    };
    /**
     * @return {?}
     */
    NgxPermissionsService.prototype.getPermissions = /**
     * @return {?}
     */
    function () {
        return this.permissionsSource.value;
    };
    /**
     * @private
     * @param {?} source
     * @param {?} name
     * @param {?=} validationFunction
     * @return {?}
     */
    NgxPermissionsService.prototype.reducePermission = /**
     * @private
     * @param {?} source
     * @param {?} name
     * @param {?=} validationFunction
     * @return {?}
     */
    function (source, name, validationFunction) {
        var _a, _b;
        if (!!validationFunction && isFunction(validationFunction)) {
            return tslib_1.__assign({}, source, (_a = {}, _a[name] = { name: name, validationFunction: validationFunction }, _a));
        }
        else {
            return tslib_1.__assign({}, source, (_b = {}, _b[name] = { name: name }, _b));
        }
    };
    /**
     * @private
     * @param {?} permissions
     * @return {?}
     */
    NgxPermissionsService.prototype.hasArrayPermission = /**
     * @private
     * @param {?} permissions
     * @return {?}
     */
    function (permissions) {
        var _this = this;
        /** @type {?} */
        var promises = permissions.map((/**
         * @param {?} key
         * @return {?}
         */
        function (key) {
            if (_this.hasPermissionValidationFunction(key)) {
                /** @type {?} */
                var immutableValue_1 = tslib_1.__assign({}, _this.permissionsSource.value);
                /** @type {?} */
                var validationFunction_1 = (/** @type {?} */ (_this.permissionsSource.value[key].validationFunction));
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
            // check for name of the permission if there is no validation function
            return of(!!_this.permissionsSource.value[key]);
        }));
        return from(promises).pipe(mergeAll(), first((/**
         * @param {?} data
         * @return {?}
         */
        function (data) { return data !== false; }), false), map((/**
         * @param {?} data
         * @return {?}
         */
        function (data) { return data === false ? false : true; }))).toPromise().then((/**
         * @param {?} data
         * @return {?}
         */
        function (data) { return data; }));
    };
    /**
     * @private
     * @param {?} key
     * @return {?}
     */
    NgxPermissionsService.prototype.hasPermissionValidationFunction = /**
     * @private
     * @param {?} key
     * @return {?}
     */
    function (key) {
        return !!this.permissionsSource.value[key] &&
            !!this.permissionsSource.value[key].validationFunction &&
            isFunction(this.permissionsSource.value[key].validationFunction);
    };
    NgxPermissionsService.decorators = [
        { type: Injectable }
    ];
    /** @nocollapse */
    NgxPermissionsService.ctorParameters = function () { return [
        { type: Boolean, decorators: [{ type: Inject, args: [USE_PERMISSIONS_STORE,] }] },
        { type: NgxPermissionsStore }
    ]; };
    return NgxPermissionsService;
}());
export { NgxPermissionsService };
if (false) {
    /**
     * @type {?}
     * @private
     */
    NgxPermissionsService.prototype.permissionsSource;
    /** @type {?} */
    NgxPermissionsService.prototype.permissions$;
    /**
     * @type {?}
     * @private
     */
    NgxPermissionsService.prototype.isolate;
    /**
     * @type {?}
     * @private
     */
    NgxPermissionsService.prototype.permissionsStore;
}
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoicGVybWlzc2lvbnMuc2VydmljZS5qcyIsInNvdXJjZVJvb3QiOiJuZzovL25neC1wZXJtaXNzaW9ucy8iLCJzb3VyY2VzIjpbImxpYi9zZXJ2aWNlL3Blcm1pc3Npb25zLnNlcnZpY2UudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7Ozs7QUFBQSxPQUFPLEVBQUUsTUFBTSxFQUFFLFVBQVUsRUFBRSxjQUFjLEVBQUUsTUFBTSxlQUFlLENBQUM7QUFFbkUsT0FBTyxFQUFFLGVBQWUsRUFBRSxJQUFJLEVBQStCLEVBQUUsRUFBRSxNQUFNLE1BQU0sQ0FBQztBQUM5RSxPQUFPLEVBQUUsVUFBVSxFQUFFLEtBQUssRUFBRSxHQUFHLEVBQUUsUUFBUSxFQUFFLFNBQVMsRUFBRSxNQUFNLGdCQUFnQixDQUFDO0FBRzdFLE9BQU8sRUFBRSxtQkFBbUIsRUFBRSxNQUFNLDRCQUE0QixDQUFDO0FBRWpFLE9BQU8sRUFBRSxTQUFTLEVBQUUsVUFBVSxFQUFFLHNCQUFzQixFQUFFLE1BQU0sZ0JBQWdCLENBQUM7O0FBSS9FLE1BQU0sS0FBTyxxQkFBcUIsR0FBRyxJQUFJLGNBQWMsQ0FBQyx1QkFBdUIsQ0FBQztBQUVoRjtJQU1JLCtCQUMyQyxPQUF3QixFQUN2RCxnQkFBcUM7UUFETix3QkFBQSxFQUFBLGVBQXdCO1FBQXhCLFlBQU8sR0FBUCxPQUFPLENBQWlCO1FBQ3ZELHFCQUFnQixHQUFoQixnQkFBZ0IsQ0FBcUI7UUFFN0MsSUFBSSxDQUFDLGlCQUFpQixHQUFHLE9BQU8sQ0FBQyxDQUFDLENBQUMsSUFBSSxlQUFlLENBQXVCLEVBQUUsQ0FBQyxDQUFDLENBQUMsQ0FBQyxnQkFBZ0IsQ0FBQyxpQkFBaUIsQ0FBQztRQUN0SCxJQUFJLENBQUMsWUFBWSxHQUFHLElBQUksQ0FBQyxpQkFBaUIsQ0FBQyxZQUFZLEVBQUUsQ0FBQztJQUM5RCxDQUFDO0lBRUQ7O09BRUc7Ozs7O0lBQ0ksZ0RBQWdCOzs7O0lBQXZCO1FBQ0ksSUFBSSxDQUFDLGlCQUFpQixDQUFDLElB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S0FBSyxLQUFLLENBQUMsQ0FBQyxDQUFDLEtBQUssQ0FBQyxDQUFDLENBQUMsSUFBSSxFQUE3QixDQUE2QixFQUFDLENBQy9DLENBQUMsU0FBUyxFQUFFLENBQUMsSUFBSTs7OztRQUFDLFVBQUMsSUFBUyxJQUFLLE9BQUEsSUFBSSxFQUFKLENBQUksRUFBQyxDQUFDO0lBQzVDLENBQUM7Ozs7OztJQUVPLCtEQUErQjs7Ozs7SUFBdkMsVUFBd0MsR0FBVztRQUMvQyxPQUFPLENBQUMsQ0FBQyxJQUFJLENBQUMsaUJBQWlCLENBQUMsS0FBSyxDQUFDLEdBQUcsQ0FBQztZQUN0QyxDQUFDLENBQUMsSUFBSSxDQUFDLGlCQUFpQixDQUFDLEtBQUssQ0FBQyxHQUFHLENBQUMsQ0FBQyxrQkFBa0I7WUFDdEQsVUFBVSxDQUFDLElBQUksQ0FBQyxpQkFBaUIsQ0FBQyxLQUFLLENBQUMsR0FBRyxDQUFDLENBQUMsa0JBQWtCLENBQUMsQ0FBQztJQUN6RSxDQUFDOztnQkFuSEosVUFBVTs7Ozs4Q0FPRixNQUFNLFNBQUMscUJBQXFCO2dCQWY1QixtQkFBbUI7O0lBNkg1Qiw0QkFBQztDQUFBLEFBckhELElBcUhDO1NBcEhZLHFCQUFxQjs7Ozs7O0lBRTlCLGtEQUFpRTs7SUFDakUsNkNBQXNEOzs7OztJQUdsRCx3Q0FBK0Q7Ozs7O0lBQy9ELGlEQUE2QyIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7IEluamVjdCwgSW5qZWN0YWJsZSwgSW5qZWN0aW9uVG9rZW4gfSBmcm9tICdAYW5ndWxhci9jb3JlJztcclxuXHJcbmltcG9ydCB7IEJlaGF2aW9yU3ViamVjdCwgZnJvbSwgT2JzZXJ2YWJsZSwgT2JzZXJ2YWJsZUlucHV0LCBvZiB9IGZyb20gJ3J4anMnO1xyXG5pbXBvcnQgeyBjYXRjaEVycm9yLCBmaXJzdCwgbWFwLCBtZXJnZUFsbCwgc3dpdGNoTWFwIH0gZnJvbSAncnhqcy9vcGVyYXRvcnMnO1xyXG5cclxuaW1wb3J0IHsgTmd4UGVybWlzc2lvbiB9IGZyb20gJy4uL21vZGVsL3Blcm1pc3Npb24ubW9kZWwnO1xyXG5pbXBvcnQgeyBOZ3hQZXJtaXNzaW9uc1N0b3JlIH0gZnJvbSAnLi4vc3RvcmUvcGVybWlzc2lvbnMuc3RvcmUnO1xyXG5cclxuaW1wb3J0IHsgaXNCb29sZWFuLCBpc0Z1bmN0aW9uLCB0cmFuc2Zvcm1TdHJpbmdUb0FycmF5IH0gZnJvbSAnLi4vdXRpbHMvdXRpbHMnO1xyXG5cclxuZXhwb3J0IHR5cGUgTmd4UGVybWlzc2lvbnNPYmplY3QgPSB7IFtuYW1lOiBzdHJpbmddOiBOZ3hQZXJtaXNzaW9uIH07XHJcblxyXG5leHBvcnQgY29uc3QgVVNFX1BFUk1JU1NJT05TX1NUT1JFID0gbmV3IEluamVjdGlvblRva2VuKCdVU0VfUEVSTUlTU0lPTlNfU1RPUkUnKTtcclxuXHJcbkBJbmplY3RhYmxlKClcclxuZXhwb3J0IGNsYXNzIE5neFBlcm1pc3Npb25zU2VydmljZSB7XHJcblxyXG4gICAgcHJpdmF0ZSBwZXJtaXNzaW9uc1NvdXJjZTogQmVoYXZpb3JTdWJqZWN0PE5neFBlcm1pc3Npb25zT2JqZWN0PjtcclxuICAgIHB1YmxpYyBwZXJtaXNzaW9ucyQ6IE9ic2VydmFibGU8Tmd4UGVybWlzc2lvbnNPYmplY3Q+O1xyXG5cclxuICAgIGNvbnN0cnVjdG9yKFxyXG4gICAgICAgIEBJbmplY3QoVVNFX1BFUk1JU1NJT05TX1NUT1JFKSBwcml2YXRlIGlzb2xhdGU6IGJvb2xlYW4gPSBmYWxzZSxcclxuICAgICAgICBwcml2YXRlIHBlcm1pc3Npb25zU3RvcmU6IE5neFBlcm1pc3Npb25zU3RvcmVcclxuICAgICkge1xyXG4gICAgICAgIHRoaXMucGVybWlzc2lvbnNTb3VyY2UgPSBpc29sYXRlID8gbmV3IEJlaGF2aW9yU3ViamVjdDxOZ3hQZXJtaXNzaW9uc09iamVjdD4oe30pIDogcGVybWlzc2lvbnNTdG9yZS5wZXJtaXNzaW9uc1NvdXJjZTtcclxuICAgICAgICB0aGlzLnBlcm1pc3Npb25zJCA9IHRoaXMucGVybWlzc2lvbnNTb3VyY2UuYXNPYnNlcnZhYmxlKCk7XHJcbiAgICB9XHJcblxyXG4gICAgLyoqXHJcbiAgICAgKiBSZW1vdmUgYWxsIHBlcm1pc3Npb25zIGZyb20gcGVybWlzc2lvbnMgc291cmNlXHJcbiAgICAgKi9cclxuICAgIHB1YmxpYyBmbHVzaFBlcm1pc3Npb25zKCk6IHZvaWQge1xyXG4gICAgICAgIHRoaXMucGVybWlzc2lvbnNTb3VyY2UubmV4dCh7fSk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGhhc1Blcm1pc3Npb24ocGVybWlzc2lvbjogc3RyaW5nIHwgc3RyaW5nW10pOiBQcm9taXNlPGJvb2xlYW4+IHtcclxuICAgICAgICBpZiAoIXBlcm1pc3Npb24gfHwgKEFycmF5LmlzQXJyYXkocGVybWlzc2lvbikgJiYgcGVybWlzc2lvbi5sZW5ndGggPT09IDApKSB7XHJcbiAgICAgICAgICAgIHJldHVybiBQcm9taXNlLnJlc29sdmUodHJ1ZSk7XHJcbiAgICAgICAgfVxyXG5cclxuICAgICAgICBwZXJtaXNzaW9uID0gdHJhbnNmb3JtU3RyaW5nVG9BcnJheShwZXJtaXNzaW9uKTtcclxuICAgICAgICByZXR1cm4gdGhpcy5oYXNBcnJheVBlcm1pc3Npb24ocGVybWlzc2lvbik7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGxvYWRQZXJtaXNzaW9ucyhwZXJtaXNzaW9uczogc3RyaW5nW10sIHZhbGlkYXRpb25GdW5jdGlvbj86IEZ1bmN0aW9uKTogdm9pZCB7XHJcbiAgICAgICAgY29uc3QgbmV3UGVybWlzc2lvbnMgPSBwZXJtaXNzaW9ucy5yZWR1Y2UoKHNvdXJjZSwgcCkgPT5cclxuICAgICAgICAgICAgICAgIHRoaXMucmVkdWNlUGVybWlzc2lvbihzb3VyY2UsIHAsIHZhbGlkYXRpb25GdW5jdGlvbilcclxuICAgICAgICAgICAgLCB7fSk7XHJcblxyXG4gICAgICAgIHRoaXMucGVybWlzc2lvbnNTb3VyY2UubmV4dChuZXdQZXJtaXNzaW9ucyk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGFkZFBlcm1pc3Npb24ocGVybWlzc2lvbjogc3RyaW5nIHwgc3RyaW5nW10sIHZhbGlkYXRpb25GdW5jdGlvbj86IEZ1bmN0aW9uKTogdm9pZCB7XHJcbiAgICAgICAgaWYgKEFycmF5LmlzQXJyYXkocGVybWlzc2lvbikpIHtcclxuICAgICAgICAgICAgY29uc3QgcGVybWlzc2lvbnMgPSBwZXJtaXNzaW9uLnJlZHVjZSgoc291cmNlLCBwKSA9PlxyXG4gICAgICAgICAgICAgICAgICAgIHRoaXMucmVkdWNlUGVybWlzc2lvbihzb3VyY2UsIHAsIHZhbGlkYXRpb25GdW5jdGlvbilcclxuICAgICAgICAgICAgICAgICwgdGhpcy5wZXJtaXNzaW9uc1NvdXJjZS52YWx1ZSk7XHJcblxyXG4gICAgICAgICAgICB0aGlzLnBlcm1pc3Npb25zU291cmNlLm5leHQocGVybWlzc2lvbnMpO1xyXG4gICAgICAgIH0gZWxzZSB7XHJcbiAgICAgICAgICAgIGNvbnN0IHBlcm1pc3Npb25zID0gdGhpcy5yZWR1Y2VQZXJtaXNzaW9uKHRoaXMucGVybWlzc2lvbnNTb3VyY2UudmFsdWUsIHBlcm1pc3Npb24sIHZhbGlkYXRpb25GdW5jdGlvbik7XHJcblxyXG4gICAgICAgICAgICB0aGlzLnBlcm1pc3Npb25zU291cmNlLm5leHQocGVybWlzc2lvbnMpO1xyXG4gICAgICAgIH1cclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgcmVtb3ZlUGVybWlzc2lvbihwZXJtaXNzaW9uTmFtZTogc3RyaW5nKTogdm9pZCB7XHJcbiAgICAgICAgY29uc3QgcGVybWlzc2lvbnMgPSB7XHJcbiAgICAgICAgICAgIC4uLnRoaXMucGVybWlzc2lvbnNTb3VyY2UudmFsdWVcclxuICAgICAgICB9O1xyXG4gICAgICAgIGRlbGV0ZSBwZXJtaXNzaW9uc1twZXJtaXNzaW9uTmFtZV07XHJcbiAgICAgICAgdGhpcy5wZXJtaXNzaW9uc1NvdXJjZS5uZXh0KHBlcm1pc3Npb25zKTtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgZ2V0UGVybWlzc2lvbihuYW1lOiBzdHJpbmcpOiBOZ3hQZXJtaXNzaW9uIHtcclxuICAgICAgICByZXR1cm4gdGhpcy5wZXJtaXNzaW9uc1NvdXJjZS52YWx1ZVtuYW1lXTtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgZ2V0UGVybWlzc2lvbnMoKTogTmd4UGVybWlzc2lvbnNPYmplY3Qge1xyXG4gICAgICAgIHJldHVybiB0aGlzLnBlcm1pc3Npb25zU291cmNlLnZhbHVlO1xyXG4gICAgfVxyXG5cclxuICAgIHByaXZhdGUgcmVkdWNlUGVybWlzc2lvbihcclxuICAgICAgICBzb3VyY2U6IE5neFBlcm1pc3Npb25zT2JqZWN0LFxyXG4gICAgICAgIG5hbWU6IHN0cmluZyxcclxuICAgICAgICB2YWxpZGF0aW9uRnVuY3Rpb24/OiBGdW5jdGlvblxyXG4gICAgKTogTmd4UGVybWlzc2lvbnNPYmplY3Qge1xyXG4gICAgICAgIGlmICghIXZhbGlkYXRpb25GdW5jdGlvbiAmJiBpc0Z1bmN0aW9uKHZhbGlkYXRpb25GdW5jdGlvbikpIHtcclxuICAgICAgICAgICAgcmV0dXJuIHtcclxuICAgICAgICAgICAgICAgIC4uLnNvdXJjZSxcclxuICAgICAgICAgICAgICAgIFtuYW1lXToge25hbWUsIHZhbGlkYXRpb25GdW5jdGlvbn1cclxuICAgICAgICAgICAgfTtcclxuICAgICAgICB9IGVsc2Uge1xyXG4gICAgICAgICAgICByZXR1cm4ge1xyXG4gICAgICAgICAgICAgICAgLi4uc291cmNlLFxyXG4gICAgICAgICAgICAgICAgW25hbWVdOiB7bmFtZX1cclxuICAgICAgICAgICAgfTtcclxuICAgICAgICB9XHJcbiAgICB9XHJcblxyXG4gICAgcHJpdmF0ZSBoYXNBcnJheVBlcm1pc3Npb24ocGVybWlzc2lvbnM6IHN0cmluZ1tdKTogUHJvbWlzZTxib29sZWFuPiB7XHJcbiAgICAgICAgY29uc3QgcHJvbWlzZXM6IE9ic2VydmFibGU8Ym9vbGVhbj5bXSA9IHBlcm1pc3Npb25zLm1hcCgoa2V5KSA9PiB7XHJcbiAgICAgICAgICAgIGlmICh0aGlzLmhhc1Blcm1pc3Npb25WYWxpZGF0aW9uRnVuY3Rpb24oa2V5KSkge1xyXG4gICAgICAgICAgICAgICAgY29uc3QgaW1tdXRhYmxlVmFsdWUgPSB7Li4udGhpcy5wZXJtaXNzaW9uc1NvdXJjZS52YWx1ZX07XHJcbiAgICAgICAgICAgICAgICBjb25zdCB2YWxpZGF0aW9uRnVuY3Rpb246IEZ1bmN0aW9uID0gPEZ1bmN0aW9uPnRoaXMucGVybWlzc2lvbnNTb3VyY2UudmFsdWVba2V5XS52YWxpZGF0aW9uRnVuY3Rpb247XHJcblxyXG4gICAgICAgICAgICAgICAgcmV0dXJuIG9mKG51bGwpLnBpcGUoXHJcbiAgICAgICAgICAgICAgICAgICAgbWFwKCgpID0+IHZhbGlkYXRpb25GdW5jdGlvbihrZXksIGltbXV0YWJsZVZhbHVlKSksXHJcbiAgICAgICAgICAgICAgICAgICAgc3dpdGNoTWFwKChwcm9taXNlOiBQcm9taXNlPGJvb2xlYW4+IHwgYm9vbGVhbik6IE9ic2VydmFibGVJbnB1dDxib29sZWFuPiA9PiBpc0Jvb2xlYW4ocHJvbWlzZSkgP1xyXG4gICAgICAgICAgICAgICAgICAgICAgICBvZihwcm9taXNlIGFzIGJvb2xlYW4pIDogcHJvbWlzZSBhcyBQcm9taXNlPGJvb2xlYW4+KSxcclxuICAgICAgICAgICAgICAgICAgICBjYXRjaEVycm9yKCgpID0+IG9mKGZhbHNlKSlcclxuICAgICAgICAgICAgICAgICk7XHJcbiAgICAgICAgICAgIH1cclxuXHJcbiAgICAgICAgICAgIC8vIGNoZWNrIGZvciBuYW1lIG9mIHRoZSBwZXJtaXNzaW9uIGlmIHRoZXJlIGlzIG5vIHZhbGlkYXRpb24gZnVuY3Rpb25cclxuICAgICAgICAgICAgcmV0dXJuIG9mKCEhdGhpcy5wZXJtaXNzaW9uc1NvdXJjZS52YWx1ZVtrZXldKTtcclxuICAgICAgICB9KTtcclxuXHJcbiAgICAgICAgcmV0dXJuIGZyb20ocHJvbWlzZXMpLnBpcGUoXHJcbiAgICAgICAgICAgIG1lcmdlQWxsKCksXHJcbiAgICAgICAgICAgIGZpcnN0KChkYXRhKSA9PiBkYXRhICE9PSBmYWxzZSwgZmFsc2UpLFxyXG4gICAgICAgICAgICBtYXAoKGRhdGEpID0+IGRhdGEgPT09IGZhbHNlID8gZmFsc2UgOiB0cnVlKVxyXG4gICAgICAgICkudG9Qcm9taXNlKCkudGhlbigoZGF0YTogYW55KSA9PiBkYXRhKTtcclxuICAgIH1cclxuXHJcbiAgICBwcml2YXRlIGhhc1Blcm1pc3Npb25WYWxpZGF0aW9uRnVuY3Rpb24oa2V5OiBzdHJpbmcpOiBib29sZWFuIHtcclxuICAgICAgICByZXR1cm4gISF0aGlzLnBlcm1pc3Npb25zU291cmNlLnZhbHVlW2tleV0gJiZcclxuICAgICAgICAgICAgISF0aGlzLnBlcm1pc3Npb25zU291cmNlLnZhbHVlW2tleV0udmFsaWRhdGlvbkZ1bmN0aW9uICYmXHJcbiAgICAgICAgICAgIGlzRnVuY3Rpb24odGhpcy5wZXJtaXNzaW9uc1NvdXJjZS52YWx1ZVtrZXldLnZhbGlkYXRpb25GdW5jdGlvbik7XHJcbiAgICB9XHJcblxyXG59XHJcbiJdfQ==