/**
 * @fileoverview added by tsickle
 * @suppress {checkTypes,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
 */
import { Inject, Injectable, InjectionToken } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { NgxPermissionsPredefinedStrategies } from '../enums/predefined-strategies.enum';
import { NgxPermissionsConfigurationStore } from '../store/configuration.store';
/** @type {?} */
export var USE_CONFIGURATION_STORE = new InjectionToken('USE_CONFIGURATION_STORE');
var NgxPermissionsConfigurationService = /** @class */ (function () {
    function NgxPermissionsConfigurationService(isolate, configurationStore) {
        if (isolate === void 0) { isolate = false; }
        this.isolate = isolate;
        this.configurationStore = configurationStore;
        this.strategiesSource = this.isolate ? new BehaviorSubject({}) : this.configurationStore.strategiesSource;
        this.strategies$ = this.strategiesSource.asObservable();
        this.onAuthorisedDefaultStrategy = this.isolate ? undefined : this.configurationStore.onAuthorisedDefaultStrategy;
        this.onUnAuthorisedDefaultStrategy = this.isolate ? undefined : this.configurationStore.onUnAuthorisedDefaultStrategy;
    }
    /**
     * @param {?} name
     * @return {?}
     */
    NgxPermissionsConfigurationService.prototype.setDefaultOnAuthorizedStrategy = /**
     * @param {?} name
     * @return {?}
     */
    function (name) {
        if (this.isolate) {
            this.onAuthorisedDefaultStrategy = this.getDefinedStrategy(name);
        }
        else {
            this.configurationStore.onAuthorisedDefaultStrategy = this.getDefinedStrategy(name);
            this.onAuthorisedDefaultStrategy = this.configurationStore.onAuthorisedDefaultStrategy;
        }
    };
    /**
     * @param {?} name
     * @return {?}
     */
    NgxPermissionsConfigurationService.prototype.setDefaultOnUnauthorizedStrategy = /**
     * @param {?} name
     * @return {?}
     */
    function (name) {
        if (this.isolate) {
            this.onUnAuthorisedDefaultStrategy = this.getDefinedStrategy(name);
        }
        else {
            this.configurationStore.onUnAuthorisedDefaultStrategy = this.getDefinedStrategy(name);
            this.onUnAuthorisedDefaultStrategy = this.configurationStore.onUnAuthorisedDefaultStrategy;
        }
    };
    /**
     * @param {?} key
     * @param {?} func
     * @return {?}
     */
    NgxPermissionsConfigurationService.prototype.addPermissionStrategy = /**
     * @param {?} key
     * @param {?} func
     * @return {?}
     */
    function (key, func) {
        this.strategiesSource.value[key] = func;
    };
    /**
     * @param {?} key
     * @return {?}
     */
    NgxPermissionsConfigurationService.prototype.getStrategy = /**
     * @param {?} key
     * @return {?}
     */
    function (key) {
        return this.strategiesSource.value[key];
    };
    /**
     * @return {?}
     */
    NgxPermissionsConfigurationService.prototype.getAllStrategies = /**
     * @return {?}
     */
    function () {
        return this.strategiesSource.value;
    };
    /**
     * @private
     * @param {?} name
     * @return {?}
     */
    NgxPermissionsConfigurationService.prototype.getDefinedStrategy = /**
     * @private
     * @param {?} name
     * @return {?}
     */
    function (name) {
        if (this.strategiesSource.value[name] || this.isPredefinedStrategy(name)) {
            return name;
        }
        else {
            throw new Error("No ' " + name + " ' strategy is found please define one");
        }
    };
    /**
     * @private
     * @param {?} strategy
     * @return {?}
     */
    NgxPermissionsConfigurationService.prototype.isPredefinedStrategy = /**
     * @private
     * @param {?} strategy
     * @return {?}
     */
    function (strategy) {
        return strategy === NgxPermissionsPredefinedStrategies.SHOW || strategy === NgxPermissionsPredefinedStrategies.REMOVE;
    };
    NgxPermissionsConfigurationService.decorators = [
        { type: Injectable }
    ];
    /** @nocollapse */
    NgxPermissionsConfigurationService.ctorParameters = function () { return [
        { type: Boolean, decorators: [{ type: Inject, args: [USE_CONFIGURATION_STORE,] }] },
        { type: NgxPermissionsConfigurationStore }
    ]; };
    return NgxPermissionsConfigurationService;
}());
export { NgxPermissionsConfigurationService };
if (false) {
    /**
     * @type {?}
     * @private
     */
    NgxPermissionsConfigurationService.prototype.strategiesSource;
    /** @type {?} */
    NgxPermissionsConfigurationService.prototype.strategies$;
    /** @type {?} */
    NgxPermissionsConfigurationService.prototype.onAuthorisedDefaultStrategy;
    /** @type {?} */
    NgxPermissionsConfigurationService.prototype.onUnAuthorisedDefaultStrategy;
    /**
     * @type {?}
     * @private
     */
    NgxPermissionsConfigurationService.prototype.isolate;
    /**
     * @type {?}
     * @private
     */
    NgxPermissionsConfigurationService.prototype.configurationStore;
}
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiY29uZmlndXJhdGlvbi5zZXJ2aWNlLmpzIiwic291cmNlUm9vdCI6Im5nOi8vbmd4LXBlcm1pc3Npb25zLyIsInNvdXJjZXMiOlsibGliL3NlcnZpY2UvY29uZmlndXJhdGlvbi5zZXJ2aWNlLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7Ozs7QUFBQSxPQUFPLEVBQUUsTUFBTSxFQUFFLFVBQVUsRUFBRSxjQUFjLEVBQWUsTUFBTSxlQUFlLENBQUM7QUFDaEYsT0FBTyxFQUFFLGVBQWUsRUFBYyxNQUFNLE1BQU0sQ0FBQztBQUNuRCxPQUFPLEVBQUUsa0NBQWtDLEVBQUUsTUFBTSxxQ0FBcUMsQ0FBQztBQUN6RixPQUFPLEVBQUUsZ0NBQWdDLEVBQUUsTUFBTSw4QkFBOEIsQ0FBQzs7QUFRaEYsTUFBTSxLQUFPLHVCQUF1QixHQUFHLElBQUksY0FBYyxDQUFDLHlCQUF5QixDQUFDO0FBRXBGO0lBUUksNENBQzZDLE9BQXdCLEVBQ3pELGtCQUFvRDtRQURuQix3QkFBQSxFQUFBLGVBQXdCO1FBQXhCLFlBQU8sR0FBUCxPQUFPLENBQWlCO1FBQ3pELHVCQUFrQixHQUFsQixrQkFBa0IsQ0FBa0M7UUFFNUQsSUFBSSxDQUFDLGdCQUFnQixHQUFHLElBQUksQ0FBQyxPQUFPLENBQUMsQ0FBQyxDQUFDLElBQUksZUFBZSxDQUFXLEVBQUUsQ0FBQyxDQUFDLENBQUMsQ0FBQyxJQUFJLENBQUMsa0JBQWtCLENBQUMsZ0JBQWdCLENBQUM7UUFDcEgsSUFBSSxDQUFDLFdBQVcsR0FBRyxJQUFJLENBQUMsZ0JBQWdCLENBQUMsWUFBWSxFQUFFLENBQUM7UUFFeEQsSUFBSSxDQUFDLDJCQUEyQixHQUFHLElBQUksQ0FBQyxPQUFPLENBQUMsQ0FBQyxDQUFDLFNBQVMsQ0FBQyxDQUFDLENBQUMsSUFBSSxDQUFDLGtCQUFrQixDQUFDLDJCQUEyQixDQUFDO1FBQ2xILElBQUksQ0FBQyw2QkFBNkIsR0FBRyxJQUFJLENBQUMsT0FBTyxDQUFDLENBQUMsQ0FBQyxTQUFTLENBQUMsQ0FBQyxDQUFDLElBQUksQ0FBQyxrQkFBa0IsQ0FBQyw2QkFBNkIsQ0FBQztJQUUxSCxDQUFDOzs7OztJQUVNLDJFQUE4Qjs7OztJQUFyQyxVQUFzQyxJQUFnQztRQUNsRSxJQUFJLElBQUksQ0FBQyxPQUFPLEVBQUU7WUFDZCxJQUFJLENBQUMsMkJBQTJCLEdBQUcsSUFBSSxDQUFDLGtCQUFrQixDQUFDLElBQUksQ0FBQyxDQUFDO1NBQ3BFO2FBQU07WUFDSCxJQUFJLENBQUMsa0JBQWtCLENBQUMsMkJBQTJCLEdBQUcsSUFBSSxDQUFDLGtCQUFrQixDQUFDLElBQUksQ0FBQyxDQUFDO1lBQ3BGLElBQUksQ0FBQywyQkFBMkIsR0FBRyxJQUFJLENBQUMsa0JBQWtCLENBQUMsMkJBQTJCLENBQUM7U0FDMUY7SUFDTCxDQUFDOzs7OztJQUVNLDZFQUFnQzs7OztJQUF2QyxVQUF3QyxJQUFnQztRQUNwRSxJQUFJLElBQUksQ0FBQyxPQUFPLEVBQUU7WUFDZCxJQUFJLENBQUMsNkJBQTZCLEdBQUcsSUFBSSxDQUFDLGtCQUFrQixDQUFDLElBQUksQ0FBQyxDQUFDO1NBQ3RFO2FBQU07WUFDSCxJQUFJLENBQUMsa0JBQWtCLENBQUMsNkJBQTZCLEdBQUcsSUFBSSxDQUFDLGtCQUFrQixDQUFDLElBQUksQ0FBQyxDQUFDO1lBQ3RGLElBQUksQ0FBQyw2QkFBNkIsR0FBRyxJQUFJLENBQUMsa0JBQWtCLENBQUMsNkJBQTZCLENBQUM7U0FDOUY7SUFDTCxDQUFDOzs7Ozs7SUFFTSxrRUFBcUI7Ozs7O0lBQTVCLFVBQTZCLEdBQVcsRUFBRSxJQUFzQjtRQUM1RCxJQUFJLENBQUMsZ0JBQWdCLENBQUMsS0FBSyxDQUFDLEdBQUcsQ0FBQyxHQUFHLElBQUksQ0FBQztJQUM1QyxDQUFDOzs7OztJQUVNLHdEQUFXOzs7O0lBQWxCLFVBQW1CLEdBQVc7UUFDMUIsT0FBTyxJQUFJLENBQUMsZ0JBQWdCLENBQUMsS0FBSyxDQUFDLEdBQUcsQ0FBQyxDQUFDO0lBQzVDLENBQUM7Ozs7SUFFTSw2REFBZ0I7OztJQUF2QjtRQUNJLE9BQU8sSUFBSSxDQUFDLGdCQUFnQixDQUFDLEtBQUssQ0FBQztJQUN2QyxDQUFDOzs7Ozs7SUFFTywrREFBa0I7Ozs7O0lBQTFCLFVBQTJCLElBQWdDO1FBQ3ZELElBQUksSUFBSSxDQUFDLGdCQUFnQixDQUFDLEtBQUssQ0FBQyxJQUFJLENBQUMsSUFBSSxJQUFJLENBQUMsb0JBQW9CLENBQUMsSUFBSSxDQUFDLEVBQUU7WUFDdEUsT0FBTyxJQUFJLENBQUM7U0FDZjthQUFNO1lBQ0gsTUFBTSxJQUFJLEtBQUssQ0FBQyxVQUFRLElBQUksMkNBQXdDLENBQUMsQ0FBQztTQUN6RTtJQUNMLENBQUM7Ozs7OztJQUVPLGlFQUFvQjs7Ozs7SUFBNUIsVUFBNkIsUUFBZ0I7UUFDekMsT0FBTyxRQUFRLEtBQUssa0NBQWtDLENBQUMsSUFBSSxJQUFJLFFBQVEsS0FBSyxrQ0FBa0MsQ0FBQyxNQUFNLENBQUM7SUFDMUgsQ0FBQzs7Z0JBNURKLFVBQVU7Ozs7OENBU0YsTUFBTSxTQUFDLHVCQUF1QjtnQkFuQjlCLGdDQUFnQzs7SUF3RXpDLHlDQUFDO0NBQUEsQUE5REQsSUE4REM7U0E3RFksa0NBQWtDOzs7Ozs7SUFFM0MsOERBQW9EOztJQUNwRCx5REFBeUM7O0lBQ3pDLHlFQUF1RDs7SUFDdkQsMkVBQXlEOzs7OztJQUdyRCxxREFBaUU7Ozs7O0lBQ2pFLGdFQUE0RCIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7IEluamVjdCwgSW5qZWN0YWJsZSwgSW5qZWN0aW9uVG9rZW4sIFRlbXBsYXRlUmVmIH0gZnJvbSAnQGFuZ3VsYXIvY29yZSc7XHJcbmltcG9ydCB7IEJlaGF2aW9yU3ViamVjdCwgT2JzZXJ2YWJsZSB9IGZyb20gJ3J4anMnO1xyXG5pbXBvcnQgeyBOZ3hQZXJtaXNzaW9uc1ByZWRlZmluZWRTdHJhdGVnaWVzIH0gZnJvbSAnLi4vZW51bXMvcHJlZGVmaW5lZC1zdHJhdGVnaWVzLmVudW0nO1xyXG5pbXBvcnQgeyBOZ3hQZXJtaXNzaW9uc0NvbmZpZ3VyYXRpb25TdG9yZSB9IGZyb20gJy4uL3N0b3JlL2NvbmZpZ3VyYXRpb24uc3RvcmUnO1xyXG5cclxuZXhwb3J0IHR5cGUgU3RyYXRlZ3lGdW5jdGlvbiA9ICh0ZW1wbGF0ZVJlZj86IFRlbXBsYXRlUmVmPGFueT4pID0+IHZvaWQ7XHJcblxyXG5leHBvcnQgdHlwZSBTdHJhdGVneSA9IHtcclxuICAgIFtrZXk6IHN0cmluZ106IFN0cmF0ZWd5RnVuY3Rpb25cclxufTtcclxuXHJcbmV4cG9ydCBjb25zdCBVU0VfQ09ORklHVVJBVElPTl9TVE9SRSA9IG5ldyBJbmplY3Rpb25Ub2tlbignVVNFX0NPTkZJR1VSQVRJT05fU1RPUkUnKTtcclxuXHJcbkBJbmplY3RhYmxlKClcclxuZXhwb3J0IGNsYXNzIE5neFBlcm1pc3Npb25zQ29uZmlndXJhdGlvblNlcnZpY2Uge1xyXG5cclxuICAgIHByaXZhdGUgc3RyYXRlZ2llc1NvdXJjZTogQmVoYXZpb3JTdWJqZWN0PFN0cmF0ZWd5PjtcclxuICAgIHB1YmxpYyBzdHJhdGVnaWVzJDogT2JzZXJ2YWJsZTxTdHJhdGVneT47XHJcbiAgICBwdWJsaWMgb25BdXRob3Jpc2VkRGVmYXVsdFN0cmF0ZWd5OiBzdHJpbmcgfCB1bmRlZmluZWQ7XHJcbiAgICBwdWJsaWMgb25VbkF1dGhvcmlzZWREZWZhdWx0U3RyYXRlZ3k6IHN0cmluZyB8IHVuZGVmaW5lZDtcclxuXHJcbiAgICBjb25zdHJ1Y3RvcihcclxuICAgICAgICBASW5qZWN0KFVTRV9DT05GSUdVUkFUSU9OX1NUT1JFKSBwcml2YXRlIGlzb2xhdGU6IGJvb2xlYW4gPSBmYWxzZSxcclxuICAgICAgICBwcml2YXRlIGNvbmZpZ3VyYXRpb25TdG9yZTogTmd4UGVybWlzc2lvbnNDb25maWd1cmF0aW9uU3RvcmVcclxuICAgICkge1xyXG4gICAgICAgIHRoaXMuc3RyYXRlZ2llc1NvdXJjZSA9IHRoaXMuaXNvbGF0ZSA/IG5ldyBCZWhhdmlvclN1YmplY3Q8U3RyYXRlZ3k+KHt9KSA6IHRoaXMuY29uZmlndXJhdGlvblN0b3JlLnN0cmF0ZWdpZXNTb3VyY2U7XHJcbiAgICAgICAgdGhpcy5zdHJhdGVnaWVzJCA9IHRoaXMuc3RyYXRlZ2llc1NvdXJjZS5hc09ic2VydmFibGUoKTtcclxuXHJcbiAgICAgICAgdGhpcy5vbkF1dGhvcmlzZWREZWZhdWx0U3RyYXRlZ3kgPSB0aGlzLmlzb2xhdGUgPyB1bmRlZmluZWQgOiB0aGlzLmNvbmZpZ3VyYXRpb25TdG9yZS5vbkF1dGhvcmlzZWREZWZhdWx0U3RyYXRlZ3k7XHJcbiAgICAgICAgdGhpcy5vblVuQXV0aG9yaXNlZERlZmF1bHRTdHJhdGVneSA9IHRoaXMuaXNvbGF0ZSA/IHVuZGVmaW5lZCA6IHRoaXMuY29uZmlndXJhdGlvblN0b3JlLm9uVW5BdXRob3Jpc2VkRGVmYXVsdFN0cmF0ZWd5O1xyXG5cclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgc2V0RGVmYXVsdE9uQXV0aG9yaXplZFN0cmF0ZWd5KG5hbWU6IHN0cmluZyB8ICdyZW1vdmUnIHwgJ3Nob3cnKTogdm9pZCB7XHJcbiAgICAgICAgaWYgKHRoaXMuaXNvbGF0ZSkge1xyXG4gICAgICAgICAgICB0aGlzLm9uQXV0aG9yaXNlZERlZmF1bHRTdHJhdGVneSA9IHRoaXMuZ2V0RGVmaW5lZFN0cmF0ZWd5KG5hbWUpO1xyXG4gICAgICAgIH0gZWxzZSB7XHJcbiAgICAgICAgICAgIHRoaXMuY29uZmlndXJhdGlvblN0b3JlLm9uQXV0aG9yaXNlZERlZmF1bHRTdHJhdGVneSA9IHRoaXMuZ2V0RGVmaW5lZFN0cmF0ZWd5KG5hbWUpO1xyXG4gICAgICAgICAgICB0aGlzLm9uQXV0aG9yaXNlZERlZmF1bHRTdHJhdGVneSA9IHRoaXMuY29uZmlndXJhdGlvblN0b3JlLm9uQXV0aG9yaXNlZERlZmF1bHRTdHJhdGVneTtcclxuICAgICAgICB9XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIHNldERlZmF1bHRPblVuYXV0aG9yaXplZFN0cmF0ZWd5KG5hbWU6IHN0cmluZyB8ICdyZW1vdmUnIHwgJ3Nob3cnKTogdm9pZCB7XHJcbiAgICAgICAgaWYgKHRoaXMuaXNvbGF0ZSkge1xyXG4gICAgICAgICAgICB0aGlzLm9uVW5BdXRob3Jpc2VkRGVmYXVsdFN0cmF0ZWd5ID0gdGhpcy5nZXREZWZpbmVkU3RyYXRlZ3kobmFtZSk7XHJcbiAgICAgICAgfSBlbHNlIHtcclxuICAgICAgICAgICAgdGhpcy5jb25maWd1cmF0aW9uU3RvcmUub25VbkF1dGhvcmlzZWREZWZhdWx0U3RyYXRlZ3kgPSB0aGlzLmdldERlZmluZWRTdHJhdGVneShuYW1lKTtcclxuICAgICAgICAgICAgdGhpcy5vblVuQXV0aG9yaXNlZERlZmF1bHRTdHJhdGVneSA9IHRoaXMuY29uZmlndXJhdGlvblN0b3JlLm9uVW5BdXRob3Jpc2VkRGVmYXVsdFN0cmF0ZWd5O1xyXG4gICAgICAgIH1cclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgYWRkUGVybWlzc2lvblN0cmF0ZWd5KGtleTogc3RyaW5nLCBmdW5jOiBTdHJhdGVneUZ1bmN0aW9uKTogdm9pZCB7XHJcbiAgICAgICAgdGhpcy5zdHJhdGVnaWVzU291cmNlLnZhbHVlW2tleV0gPSBmdW5jO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBnZXRTdHJhdGVneShrZXk6IHN0cmluZykge1xyXG4gICAgICAgIHJldHVybiB0aGlzLnN0cmF0ZWdpZXNTb3VyY2UudmFsdWVba2V5XTtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgZ2V0QWxsU3RyYXRlZ2llcygpIHtcclxuICAgICAgICByZXR1cm4gdGhpcy5zdHJhdGVnaWVzU291cmNlLnZhbHVlO1xyXG4gICAgfVxyXG5cclxuICAgIHByaXZhdGUgZ2V0RGVmaW5lZFN0cmF0ZWd5KG5hbWU6IHN0cmluZyB8ICdyZW1vdmUnIHwgJ3Nob3cnKSB7XHJcbiAgICAgICAgaWYgKHRoaXMuc3RyYXRlZ2llc1NvdXJjZS52YWx1ZVtuYW1lXSB8fCB0aGlzLmlzUHJlZGVmaW5lZFN0cmF0ZWd5KG5hbWUpKSB7XHJcbiAgICAgICAgICAgIHJldHVybiBuYW1lO1xyXG4gICAgICAgIH0gZWxzZSB7XHJcbiAgICAgICAgICAgIHRocm93IG5ldyBFcnJvcihgTm8gJyAke25hbWV9ICcgc3RyYXRlZ3kgaXMgZm91bmQgcGxlYXNlIGRlZmluZSBvbmVgKTtcclxuICAgICAgICB9XHJcbiAgICB9XHJcblxyXG4gICAgcHJpdmF0ZSBpc1ByZWRlZmluZWRTdHJhdGVneShzdHJhdGVneTogc3RyaW5nKTogYm9vbGVhbiB7XHJcbiAgICAgICAgcmV0dXJuIHN0cmF0ZWd5ID09PSBOZ3hQZXJtaXNzaW9uc1ByZWRlZmluZWRTdHJhdGVnaWVzLlNIT1cgfHwgc3RyYXRlZ3kgPT09IE5neFBlcm1pc3Npb25zUHJlZGVmaW5lZFN0cmF0ZWdpZXMuUkVNT1ZFO1xyXG4gICAgfVxyXG5cclxufVxyXG4iXX0=