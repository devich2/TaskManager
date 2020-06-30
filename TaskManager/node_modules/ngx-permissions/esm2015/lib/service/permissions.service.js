/**
 * @fileoverview added by tsickle
 * @suppress {checkTypes,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
 */
import { Inject, Injectable, InjectionToken } from '@angular/core';
import { BehaviorSubject, from, of } from 'rxjs';
import { catchError, first, map, mergeAll, switchMap } from 'rxjs/operators';
import { NgxPermissionsStore } from '../store/permissions.store';
import { isBoolean, isFunction, transformStringToArray } from '../utils/utils';
/** @type {?} */
export const USE_PERMISSIONS_STORE = new InjectionToken('USE_PERMISSIONS_STORE');
export class NgxPermissionsService {
    /**
     * @param {?=} isolate
     * @param {?=} permissionsStore
     */
    constructor(isolate = false, permissionsStore) {
        this.isolate = isolate;
        this.permissionsStore = permissionsStore;
        this.permissionsSource = isolate ? new BehaviorSubject({}) : permissionsStore.permissionsSource;
        this.permissions$ = this.permissionsSource.asObservable();
    }
    /**
     * Remove all permissions from permissions source
     * @return {?}
     */
    flushPermissions() {
        this.permissionsSource.next({});
    }
    /**
     * @param {?} permission
     * @return {?}
     */
    hasPermission(permission) {
        if (!permission || (Array.isArray(permission) && permission.length === 0)) {
            return Promise.resolve(true);
        }
        permission = transformStringToArray(permission);
        return this.hasArrayPermission(permission);
    }
    /**
     * @param {?} permissions
     * @param {?=} validationFunction
     * @return {?}
     */
    loadPermissions(permissions, validationFunction) {
        /** @type {?} */
        const newPermissions = permissions.reduce((/**
         * @param {?} source
         * @param {?} p
         * @return {?}
         */
        (source, p) => this.reducePermission(source, p, validationFunction)), {});
        this.permissionsSource.next(newPermissions);
    }
    /**
     * @param {?} permission
     * @param {?=} validationFunction
     * @return {?}
     */
    addPermission(permission, validationFunction) {
        if (Array.isArray(permission)) {
            /** @type {?} */
            const permissions = permission.reduce((/**
             * @param {?} source
             * @param {?} p
             * @return {?}
             */
            (source, p) => this.reducePermission(source, p, validationFunction)), this.permissionsSource.value);
            this.permissionsSource.next(permissions);
        }
        else {
            /** @type {?} */
            const permissions = this.reducePermission(this.permissionsSource.value, permission, validationFunction);
            this.permissionsSource.next(permissions);
        }
    }
    /**
     * @param {?} permissionName
     * @return {?}
     */
    removePermission(permissionName) {
        /** @type {?} */
        const permissions = Object.assign({}, this.permissionsSource.value);
        delete permissions[permissionName];
        this.permissionsSource.next(permissions);
    }
    /**
     * @param {?} name
     * @return {?}
     */
    getPermission(name) {
        return this.permissionsSource.value[name];
    }
    /**
     * @return {?}
     */
    getPermissions() {
        return this.permissionsSource.value;
    }
    /**
     * @private
     * @param {?} source
     * @param {?} name
     * @param {?=} validationFunction
     * @return {?}
     */
    reducePermission(source, name, validationFunction) {
        if (!!validationFunction && isFunction(validationFunction)) {
            return Object.assign({}, source, { [name]: { name, validationFunction } });
        }
        else {
            return Object.assign({}, source, { [name]: { name } });
        }
    }
    /**
     * @private
     * @param {?} permissions
     * @return {?}
     */
    hasArrayPermission(permissions) {
        /** @type {?} */
        const promises = permissions.map((/**
         * @param {?} key
         * @return {?}
         */
        (key) => {
            if (this.hasPermissionValidationFunction(key)) {
                /** @type {?} */
                const immutableValue = Object.assign({}, this.permissionsSource.value);
                /** @type {?} */
                const validationFunction = (/** @type {?} */ (this.permissionsSource.value[key].validationFunction));
                return of(null).pipe(map((/**
                 * @return {?}
                 */
                () => validationFunction(key, immutableValue))), switchMap((/**
                 * @param {?} promise
                 * @return {?}
                 */
                (promise) => isBoolean(promise) ?
                    of((/** @type {?} */ (promise))) : (/** @type {?} */ (promise)))), catchError((/**
                 * @return {?}
                 */
                () => of(false))));
            }
            // check for name of the permission if there is no validation function
            return of(!!this.permissionsSource.value[key]);
        }));
        return from(promises).pipe(mergeAll(), first((/**
         * @param {?} data
         * @return {?}
         */
        (data) => data !== false), false), map((/**
         * @param {?} data
         * @return {?}
         */
        (data) => data === false ? false : true))).toPromise().then((/**
         * @param {?} data
         * @return {?}
         */
        (data) => data));
    }
    /**
     * @private
     * @param {?} key
     * @return {?}
     */
    hasPermissionValidationFunction(key) {
        return !!this.permissionsSource.value[key] &&
            !!this.permissionsSource.value[key].validationFunction &&
            isFunction(this.permissionsSource.value[key].validationFunction);
    }
}
NgxPermissionsService.decorators = [
    { type: Injectable }
];
/** @nocollapse */
NgxPermissionsService.ctorParameters = () => [
    { type: Boolean, decorators: [{ type: Inject, args: [USE_PERMISSIONS_STORE,] }] },
    { type: NgxPermissionsStore }
];
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
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoicGVybWlzc2lvbnMuc2VydmljZS5qcyIsInNvdXJjZVJvb3QiOiJuZzovL25neC1wZXJtaXNzaW9ucy8iLCJzb3VyY2VzIjpbImxpYi9zZXJ2aWNlL3Blcm1pc3Npb25zLnNlcnZpY2UudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7OztBQUFBLE9BQU8sRUFBRSxNQUFNLEVBQUUsVUFBVSxFQUFFLGNBQWMsRUFBRSxNQUFNLGVBQWUsQ0FBQztBQUVuRSxPQUFPLEVBQUUsZUFBZSxFQUFFLElBQUksRUFBK0IsRUFBRSxFQUFFLE1BQU0sTUFBTSxDQUFDO0FBQzlFLE9BQU8sRUFBRSxVQUFVLEVBQUUsS0FBSyxFQUFFLEdBQUcsRUFBRSxRQUFRLEVBQUUsU0FBUyxFQUFFLE1BQU0sZ0JBQWdCLENBQUM7QUFHN0UsT0FBTyxFQUFFLG1CQUFtQixFQUFFLE1BQU0sNEJBQTRCLENBQUM7QUFFakUsT0FBTyxFQUFFLFNBQVMsRUFBRSxVQUFVLEVBQUUsc0JBQXNCLEVBQUUsTUFBTSxnQkFBZ0IsQ0FBQzs7QUFJL0UsTUFBTSxPQUFPLHFCQUFxQixHQUFHLElBQUksY0FBYyxDQUFDLHVCQUF1QixDQUFDO0FBR2hGLE1BQU0sT0FBTyxxQkFBcUI7Ozs7O0lBSzlCLFlBQzJDLFVBQW1CLEtBQUssRUFDdkQsZ0JBQXFDO1FBRE4sWUFBTyxHQUFQLE9BQU8sQ0FBaUI7UUFDdkQscUJBQWdCLEdBQWhCLGdCQUFnQixDQUFxQjtRQUU3QyxJQUFJLENBQUMsaUJBQWlCLEdBQUcsT0FBTyxDQUFDLENBQUMsQ0FBQyxJQUFJLGVBQWUsQ0FBdUIsRUFBRSxDQUFDLENBQUMsQ0FBQyxDQUFDLGdCQUFnQixDQUFDLGlCQUFpQixDQUFDO1FBQ3RILElBQUksQ0FBQyxZQUFZLEdBQUcsSUFBSSxDQUFDLGlCQUFpQixDQUFDLFlBQVksRUFBRSxDQUFDO0lBQzlELENBQUM7Ozs7O0lBS00sZ0JBQWdCO1FBQ25CLElBQUksQ0FBQyxpQkFBaUIsQ0FBQyxJQUFJLENBQUMsRUFBRSxDQUFD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LENBQUMsR0FBVztRQUMvQyxPQUFPLENBQUMsQ0FBQyxJQUFJLENBQUMsaUJBQWlCLENBQUMsS0FBSyxDQUFDLEdBQUcsQ0FBQztZQUN0QyxDQUFDLENBQUMsSUFBSSxDQUFDLGlCQUFpQixDQUFDLEtBQUssQ0FBQyxHQUFHLENBQUMsQ0FBQyxrQkFBa0I7WUFDdEQsVUFBVSxDQUFDLElBQUksQ0FBQyxpQkFBaUIsQ0FBQyxLQUFLLENBQUMsR0FBRyxDQUFDLENBQUMsa0JBQWtCLENBQUMsQ0FBQztJQUN6RSxDQUFDOzs7WUFuSEosVUFBVTs7OzswQ0FPRixNQUFNLFNBQUMscUJBQXFCO1lBZjVCLG1CQUFtQjs7Ozs7OztJQVd4QixrREFBaUU7O0lBQ2pFLDZDQUFzRDs7Ozs7SUFHbEQsd0NBQStEOzs7OztJQUMvRCxpREFBNkMiLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQgeyBJbmplY3QsIEluamVjdGFibGUsIEluamVjdGlvblRva2VuIH0gZnJvbSAnQGFuZ3VsYXIvY29yZSc7XHJcblxyXG5pbXBvcnQgeyBCZWhhdmlvclN1YmplY3QsIGZyb20sIE9ic2VydmFibGUsIE9ic2VydmFibGVJbnB1dCwgb2YgfSBmcm9tICdyeGpzJztcclxuaW1wb3J0IHsgY2F0Y2hFcnJvciwgZmlyc3QsIG1hcCwgbWVyZ2VBbGwsIHN3aXRjaE1hcCB9IGZyb20gJ3J4anMvb3BlcmF0b3JzJztcclxuXHJcbmltcG9ydCB7IE5neFBlcm1pc3Npb24gfSBmcm9tICcuLi9tb2RlbC9wZXJtaXNzaW9uLm1vZGVsJztcclxuaW1wb3J0IHsgTmd4UGVybWlzc2lvbnNTdG9yZSB9IGZyb20gJy4uL3N0b3JlL3Blcm1pc3Npb25zLnN0b3JlJztcclxuXHJcbmltcG9ydCB7IGlzQm9vbGVhbiwgaXNGdW5jdGlvbiwgdHJhbnNmb3JtU3RyaW5nVG9BcnJheSB9IGZyb20gJy4uL3V0aWxzL3V0aWxzJztcclxuXHJcbmV4cG9ydCB0eXBlIE5neFBlcm1pc3Npb25zT2JqZWN0ID0geyBbbmFtZTogc3RyaW5nXTogTmd4UGVybWlzc2lvbiB9O1xyXG5cclxuZXhwb3J0IGNvbnN0IFVTRV9QRVJNSVNTSU9OU19TVE9SRSA9IG5ldyBJbmplY3Rpb25Ub2tlbignVVNFX1BFUk1JU1NJT05TX1NUT1JFJyk7XHJcblxyXG5ASW5qZWN0YWJsZSgpXHJcbmV4cG9ydCBjbGFzcyBOZ3hQZXJtaXNzaW9uc1NlcnZpY2Uge1xyXG5cclxuICAgIHByaXZhdGUgcGVybWlzc2lvbnNTb3VyY2U6IEJlaGF2aW9yU3ViamVjdDxOZ3hQZXJtaXNzaW9uc09iamVjdD47XHJcbiAgICBwdWJsaWMgcGVybWlzc2lvbnMkOiBPYnNlcnZhYmxlPE5neFBlcm1pc3Npb25zT2JqZWN0PjtcclxuXHJcbiAgICBjb25zdHJ1Y3RvcihcclxuICAgICAgICBASW5qZWN0KFVTRV9QRVJNSVNTSU9OU19TVE9SRSkgcHJpdmF0ZSBpc29sYXRlOiBib29sZWFuID0gZmFsc2UsXHJcbiAgICAgICAgcHJpdmF0ZSBwZXJtaXNzaW9uc1N0b3JlOiBOZ3hQZXJtaXNzaW9uc1N0b3JlXHJcbiAgICApIHtcclxuICAgICAgICB0aGlzLnBlcm1pc3Npb25zU291cmNlID0gaXNvbGF0ZSA/IG5ldyBCZWhhdmlvclN1YmplY3Q8Tmd4UGVybWlzc2lvbnNPYmplY3Q+KHt9KSA6IHBlcm1pc3Npb25zU3RvcmUucGVybWlzc2lvbnNTb3VyY2U7XHJcbiAgICAgICAgdGhpcy5wZXJtaXNzaW9ucyQgPSB0aGlzLnBlcm1pc3Npb25zU291cmNlLmFzT2JzZXJ2YWJsZSgpO1xyXG4gICAgfVxyXG5cclxuICAgIC8qKlxyXG4gICAgICogUmVtb3ZlIGFsbCBwZXJtaXNzaW9ucyBmcm9tIHBlcm1pc3Npb25zIHNvdXJjZVxyXG4gICAgICovXHJcbiAgICBwdWJsaWMgZmx1c2hQZXJtaXNzaW9ucygpOiB2b2lkIHtcclxuICAgICAgICB0aGlzLnBlcm1pc3Npb25zU291cmNlLm5leHQoe30pO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBoYXNQZXJtaXNzaW9uKHBlcm1pc3Npb246IHN0cmluZyB8IHN0cmluZ1tdKTogUHJvbWlzZTxib29sZWFuPiB7XHJcbiAgICAgICAgaWYgKCFwZXJtaXNzaW9uIHx8IChBcnJheS5pc0FycmF5KHBlcm1pc3Npb24pICYmIHBlcm1pc3Npb24ubGVuZ3RoID09PSAwKSkge1xyXG4gICAgICAgICAgICByZXR1cm4gUHJvbWlzZS5yZXNvbHZlKHRydWUpO1xyXG4gICAgICAgIH1cclxuXHJcbiAgICAgICAgcGVybWlzc2lvbiA9IHRyYW5zZm9ybVN0cmluZ1RvQXJyYXkocGVybWlzc2lvbik7XHJcbiAgICAgICAgcmV0dXJuIHRoaXMuaGFzQXJyYXlQZXJtaXNzaW9uKHBlcm1pc3Npb24pO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBsb2FkUGVybWlzc2lvbnMocGVybWlzc2lvbnM6IHN0cmluZ1tdLCB2YWxpZGF0aW9uRnVuY3Rpb24/OiBGdW5jdGlvbik6IHZvaWQge1xyXG4gICAgICAgIGNvbnN0IG5ld1Blcm1pc3Npb25zID0gcGVybWlzc2lvbnMucmVkdWNlKChzb3VyY2UsIHApID0+XHJcbiAgICAgICAgICAgICAgICB0aGlzLnJlZHVjZVBlcm1pc3Npb24oc291cmNlLCBwLCB2YWxpZGF0aW9uRnVuY3Rpb24pXHJcbiAgICAgICAgICAgICwge30pO1xyXG5cclxuICAgICAgICB0aGlzLnBlcm1pc3Npb25zU291cmNlLm5leHQobmV3UGVybWlzc2lvbnMpO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBhZGRQZXJtaXNzaW9uKHBlcm1pc3Npb246IHN0cmluZyB8IHN0cmluZ1tdLCB2YWxpZGF0aW9uRnVuY3Rpb24/OiBGdW5jdGlvbik6IHZvaWQge1xyXG4gICAgICAgIGlmIChBcnJheS5pc0FycmF5KHBlcm1pc3Npb24pKSB7XHJcbiAgICAgICAgICAgIGNvbnN0IHBlcm1pc3Npb25zID0gcGVybWlzc2lvbi5yZWR1Y2UoKHNvdXJjZSwgcCkgPT5cclxuICAgICAgICAgICAgICAgICAgICB0aGlzLnJlZHVjZVBlcm1pc3Npb24oc291cmNlLCBwLCB2YWxpZGF0aW9uRnVuY3Rpb24pXHJcbiAgICAgICAgICAgICAgICAsIHRoaXMucGVybWlzc2lvbnNTb3VyY2UudmFsdWUpO1xyXG5cclxuICAgICAgICAgICAgdGhpcy5wZXJtaXNzaW9uc1NvdXJjZS5uZXh0KHBlcm1pc3Npb25zKTtcclxuICAgICAgICB9IGVsc2Uge1xyXG4gICAgICAgICAgICBjb25zdCBwZXJtaXNzaW9ucyA9IHRoaXMucmVkdWNlUGVybWlzc2lvbih0aGlzLnBlcm1pc3Npb25zU291cmNlLnZhbHVlLCBwZXJtaXNzaW9uLCB2YWxpZGF0aW9uRnVuY3Rpb24pO1xyXG5cclxuICAgICAgICAgICAgdGhpcy5wZXJtaXNzaW9uc1NvdXJjZS5uZXh0KHBlcm1pc3Npb25zKTtcclxuICAgICAgICB9XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIHJlbW92ZVBlcm1pc3Npb24ocGVybWlzc2lvbk5hbWU6IHN0cmluZyk6IHZvaWQge1xyXG4gICAgICAgIGNvbnN0IHBlcm1pc3Npb25zID0ge1xyXG4gICAgICAgICAgICAuLi50aGlzLnBlcm1pc3Npb25zU291cmNlLnZhbHVlXHJcbiAgICAgICAgfTtcclxuICAgICAgICBkZWxldGUgcGVybWlzc2lvbnNbcGVybWlzc2lvbk5hbWVdO1xyXG4gICAgICAgIHRoaXMucGVybWlzc2lvbnNTb3VyY2UubmV4dChwZXJtaXNzaW9ucyk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGdldFBlcm1pc3Npb24obmFtZTogc3RyaW5nKTogTmd4UGVybWlzc2lvbiB7XHJcbiAgICAgICAgcmV0dXJuIHRoaXMucGVybWlzc2lvbnNTb3VyY2UudmFsdWVbbmFtZV07XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGdldFBlcm1pc3Npb25zKCk6IE5neFBlcm1pc3Npb25zT2JqZWN0IHtcclxuICAgICAgICByZXR1cm4gdGhpcy5wZXJtaXNzaW9uc1NvdXJjZS52YWx1ZTtcclxuICAgIH1cclxuXHJcbiAgICBwcml2YXRlIHJlZHVjZVBlcm1pc3Npb24oXHJcbiAgICAgICAgc291cmNlOiBOZ3hQZXJtaXNzaW9uc09iamVjdCxcclxuICAgICAgICBuYW1lOiBzdHJpbmcsXHJcbiAgICAgICAgdmFsaWRhdGlvbkZ1bmN0aW9uPzogRnVuY3Rpb25cclxuICAgICk6IE5neFBlcm1pc3Npb25zT2JqZWN0IHtcclxuICAgICAgICBpZiAoISF2YWxpZGF0aW9uRnVuY3Rpb24gJiYgaXNGdW5jdGlvbih2YWxpZGF0aW9uRnVuY3Rpb24pKSB7XHJcbiAgICAgICAgICAgIHJldHVybiB7XHJcbiAgICAgICAgICAgICAgICAuLi5zb3VyY2UsXHJcbiAgICAgICAgICAgICAgICBbbmFtZV06IHtuYW1lLCB2YWxpZGF0aW9uRnVuY3Rpb259XHJcbiAgICAgICAgICAgIH07XHJcbiAgICAgICAgfSBlbHNlIHtcclxuICAgICAgICAgICAgcmV0dXJuIHtcclxuICAgICAgICAgICAgICAgIC4uLnNvdXJjZSxcclxuICAgICAgICAgICAgICAgIFtuYW1lXToge25hbWV9XHJcbiAgICAgICAgICAgIH07XHJcbiAgICAgICAgfVxyXG4gICAgfVxyXG5cclxuICAgIHByaXZhdGUgaGFzQXJyYXlQZXJtaXNzaW9uKHBlcm1pc3Npb25zOiBzdHJpbmdbXSk6IFByb21pc2U8Ym9vbGVhbj4ge1xyXG4gICAgICAgIGNvbnN0IHByb21pc2VzOiBPYnNlcnZhYmxlPGJvb2xlYW4+W10gPSBwZXJtaXNzaW9ucy5tYXAoKGtleSkgPT4ge1xyXG4gICAgICAgICAgICBpZiAodGhpcy5oYXNQZXJtaXNzaW9uVmFsaWRhdGlvbkZ1bmN0aW9uKGtleSkpIHtcclxuICAgICAgICAgICAgICAgIGNvbnN0IGltbXV0YWJsZVZhbHVlID0gey4uLnRoaXMucGVybWlzc2lvbnNTb3VyY2UudmFsdWV9O1xyXG4gICAgICAgICAgICAgICAgY29uc3QgdmFsaWRhdGlvbkZ1bmN0aW9uOiBGdW5jdGlvbiA9IDxGdW5jdGlvbj50aGlzLnBlcm1pc3Npb25zU291cmNlLnZhbHVlW2tleV0udmFsaWRhdGlvbkZ1bmN0aW9uO1xyXG5cclxuICAgICAgICAgICAgICAgIHJldHVybiBvZihudWxsKS5waXBlKFxyXG4gICAgICAgICAgICAgICAgICAgIG1hcCgoKSA9PiB2YWxpZGF0aW9uRnVuY3Rpb24oa2V5LCBpbW11dGFibGVWYWx1ZSkpLFxyXG4gICAgICAgICAgICAgICAgICAgIHN3aXRjaE1hcCgocHJvbWlzZTogUHJvbWlzZTxib29sZWFuPiB8IGJvb2xlYW4pOiBPYnNlcnZhYmxlSW5wdXQ8Ym9vbGVhbj4gPT4gaXNCb29sZWFuKHByb21pc2UpID9cclxuICAgICAgICAgICAgICAgICAgICAgICAgb2YocHJvbWlzZSBhcyBib29sZWFuKSA6IHByb21pc2UgYXMgUHJvbWlzZTxib29sZWFuPiksXHJcbiAgICAgICAgICAgICAgICAgICAgY2F0Y2hFcnJvcigoKSA9PiBvZihmYWxzZSkpXHJcbiAgICAgICAgICAgICAgICApO1xyXG4gICAgICAgICAgICB9XHJcblxyXG4gICAgICAgICAgICAvLyBjaGVjayBmb3IgbmFtZSBvZiB0aGUgcGVybWlzc2lvbiBpZiB0aGVyZSBpcyBubyB2YWxpZGF0aW9uIGZ1bmN0aW9uXHJcbiAgICAgICAgICAgIHJldHVybiBvZighIXRoaXMucGVybWlzc2lvbnNTb3VyY2UudmFsdWVba2V5XSk7XHJcbiAgICAgICAgfSk7XHJcblxyXG4gICAgICAgIHJldHVybiBmcm9tKHByb21pc2VzKS5waXBlKFxyXG4gICAgICAgICAgICBtZXJnZUFsbCgpLFxyXG4gICAgICAgICAgICBmaXJzdCgoZGF0YSkgPT4gZGF0YSAhPT0gZmFsc2UsIGZhbHNlKSxcclxuICAgICAgICAgICAgbWFwKChkYXRhKSA9PiBkYXRhID09PSBmYWxzZSA/IGZhbHNlIDogdHJ1ZSlcclxuICAgICAgICApLnRvUHJvbWlzZSgpLnRoZW4oKGRhdGE6IGFueSkgPT4gZGF0YSk7XHJcbiAgICB9XHJcblxyXG4gICAgcHJpdmF0ZSBoYXNQZXJtaXNzaW9uVmFsaWRhdGlvbkZ1bmN0aW9uKGtleTogc3RyaW5nKTogYm9vbGVhbiB7XHJcbiAgICAgICAgcmV0dXJuICEhdGhpcy5wZXJtaXNzaW9uc1NvdXJjZS52YWx1ZVtrZXldICYmXHJcbiAgICAgICAgICAgICEhdGhpcy5wZXJtaXNzaW9uc1NvdXJjZS52YWx1ZVtrZXldLnZhbGlkYXRpb25GdW5jdGlvbiAmJlxyXG4gICAgICAgICAgICBpc0Z1bmN0aW9uKHRoaXMucGVybWlzc2lvbnNTb3VyY2UudmFsdWVba2V5XS52YWxpZGF0aW9uRnVuY3Rpb24pO1xyXG4gICAgfVxyXG5cclxufVxyXG4iXX0=