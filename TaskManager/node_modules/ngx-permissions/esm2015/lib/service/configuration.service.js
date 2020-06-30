/**
 * @fileoverview added by tsickle
 * @suppress {checkTypes,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
 */
import { Inject, Injectable, InjectionToken } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { NgxPermissionsPredefinedStrategies } from '../enums/predefined-strategies.enum';
import { NgxPermissionsConfigurationStore } from '../store/configuration.store';
/** @type {?} */
export const USE_CONFIGURATION_STORE = new InjectionToken('USE_CONFIGURATION_STORE');
export class NgxPermissionsConfigurationService {
    /**
     * @param {?=} isolate
     * @param {?=} configurationStore
     */
    constructor(isolate = false, configurationStore) {
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
    setDefaultOnAuthorizedStrategy(name) {
        if (this.isolate) {
            this.onAuthorisedDefaultStrategy = this.getDefinedStrategy(name);
        }
        else {
            this.configurationStore.onAuthorisedDefaultStrategy = this.getDefinedStrategy(name);
            this.onAuthorisedDefaultStrategy = this.configurationStore.onAuthorisedDefaultStrategy;
        }
    }
    /**
     * @param {?} name
     * @return {?}
     */
    setDefaultOnUnauthorizedStrategy(name) {
        if (this.isolate) {
            this.onUnAuthorisedDefaultStrategy = this.getDefinedStrategy(name);
        }
        else {
            this.configurationStore.onUnAuthorisedDefaultStrategy = this.getDefinedStrategy(name);
            this.onUnAuthorisedDefaultStrategy = this.configurationStore.onUnAuthorisedDefaultStrategy;
        }
    }
    /**
     * @param {?} key
     * @param {?} func
     * @return {?}
     */
    addPermissionStrategy(key, func) {
        this.strategiesSource.value[key] = func;
    }
    /**
     * @param {?} key
     * @return {?}
     */
    getStrategy(key) {
        return this.strategiesSource.value[key];
    }
    /**
     * @return {?}
     */
    getAllStrategies() {
        return this.strategiesSource.value;
    }
    /**
     * @private
     * @param {?} name
     * @return {?}
     */
    getDefinedStrategy(name) {
        if (this.strategiesSource.value[name] || this.isPredefinedStrategy(name)) {
            return name;
        }
        else {
            throw new Error(`No ' ${name} ' strategy is found please define one`);
        }
    }
    /**
     * @private
     * @param {?} strategy
     * @return {?}
     */
    isPredefinedStrategy(strategy) {
        return strategy === NgxPermissionsPredefinedStrategies.SHOW || strategy === NgxPermissionsPredefinedStrategies.REMOVE;
    }
}
NgxPermissionsConfigurationService.decorators = [
    { type: Injectable }
];
/** @nocollapse */
NgxPermissionsConfigurationService.ctorParameters = () => [
    { type: Boolean, decorators: [{ type: Inject, args: [USE_CONFIGURATION_STORE,] }] },
    { type: NgxPermissionsConfigurationStore }
];
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
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiY29uZmlndXJhdGlvbi5zZXJ2aWNlLmpzIiwic291cmNlUm9vdCI6Im5nOi8vbmd4LXBlcm1pc3Npb25zLyIsInNvdXJjZXMiOlsibGliL3NlcnZpY2UvY29uZmlndXJhdGlvbi5zZXJ2aWNlLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7Ozs7QUFBQSxPQUFPLEVBQUUsTUFBTSxFQUFFLFVBQVUsRUFBRSxjQUFjLEVBQWUsTUFBTSxlQUFlLENBQUM7QUFDaEYsT0FBTyxFQUFFLGVBQWUsRUFBYyxNQUFNLE1BQU0sQ0FBQztBQUNuRCxPQUFPLEVBQUUsa0NBQWtDLEVBQUUsTUFBTSxxQ0FBcUMsQ0FBQztBQUN6RixPQUFPLEVBQUUsZ0NBQWdDLEVBQUUsTUFBTSw4QkFBOEIsQ0FBQzs7QUFRaEYsTUFBTSxPQUFPLHVCQUF1QixHQUFHLElBQUksY0FBYyxDQUFDLHlCQUF5QixDQUFDO0FBR3BGLE1BQU0sT0FBTyxrQ0FBa0M7Ozs7O0lBTzNDLFlBQzZDLFVBQW1CLEtBQUssRUFDekQsa0JBQW9EO1FBRG5CLFlBQU8sR0FBUCxPQUFPLENBQWlCO1FBQ3pELHVCQUFrQixHQUFsQixrQkFBa0IsQ0FBa0M7UUFFNUQsSUFBSSxDQUFDLGdCQUFnQixHQUFHLElBQUksQ0FBQyxPQUFPLENBQUMsQ0FBQyxDQUFDLElBQUksZUFBZSxDQUFXLEVBQUUsQ0FBQyxDQUFDLENBQUMsQ0FBQyxJQUFJLENBQUMsa0JBQWtCLENBQUMsZ0JBQWdCLENBQUM7UUFDcEgsSUFBSSxDQUFDLFdBQVcsR0FBRyxJQUFJLENBQUMsZ0JBQWdCLENBQUMsWUFBWSxFQUFFLENBQUM7UUFFeEQsSUFBSSxDQUFDLDJCQUEyQixHQUFHLElBQUksQ0FBQyxPQUFPLENBQUMsQ0FBQyxDQUFDLFNBQVMsQ0FBQyxDQUFDLENBQUMsSUFBSSxDQUFDLGtCQUFrQixDQUFDLDJCQUEyQixDQUFDO1FBQ2xILElBQUksQ0FBQyw2QkFBNkIsR0FBRyxJQUFJLENBQUMsT0FBTyxDQUFDLENBQUMsQ0FBQyxTQUFTLENBQUMsQ0FBQyxDQUFDLElBQUksQ0FBQyxrQkFBa0IsQ0FBQyw2QkFBNkIsQ0FBQztJQUUxSCxDQUFDOzs7OztJQUVNLDhCQUE4QixDQUFDLElBQWdDO1FBQ2xFLElBQUksSUFBSSxDQUFDLE9BQU8sRUFBRTtZQUNkLElBQUksQ0FBQywyQkFBMkIsR0FBRyxJQUFJLENBQUMsa0JBQWtCLENBQUMsSUFBSSxDQUFDLENBQUM7U0FDcEU7YUFBTTtZQUNILElBQUksQ0FBQyxrQkFBa0IsQ0FBQywyQkFBMkIsR0FBRyxJQUFJLENBQUMsa0JBQWtCLENBQUMsSUFBSSxDQUFDLENBQUM7WUFDcEYsSUFBSSxDQUFDLDJCQUEyQixHQUFHLElBQUksQ0FBQyxrQkFBa0IsQ0FBQywyQkFBMkIsQ0FBQztTQUMxRjtJQUNMLENBQUM7Ozs7O0lBRU0sZ0NBQWdDLENBQUMsSUFBZ0M7UUFDcEUsSUFBSSxJQUFJLENBQUMsT0FBTyxFQUFFO1lBQ2QsSUFBSSxDQUFDLDZCQUE2QixHQUFHLElBQUksQ0FBQyxrQkFBa0IsQ0FBQyxJQUFJLENBQUMsQ0FBQztTQUN0RTthQUFNO1lBQ0gsSUFBSSxDQUFDLGtCQUFrQixDQUFDLDZCQUE2QixHQUFHLElBQUksQ0FBQyxrQkFBa0IsQ0FBQyxJQUFJLENBQUMsQ0FBQztZQUN0RixJQUFJLENBQUMsNkJBQTZCLEdBQUcsSUFBSSxDQUFDLGtCQUFrQixDQUFDLDZCQUE2QixDQUFDO1NBQzlGO0lBQ0wsQ0FBQzs7Ozs7O0lBRU0scUJBQXFCLENBQUMsR0FBVyxFQUFFLElBQXNCO1FBQzVELElBQUksQ0FBQyxnQkFBZ0IsQ0FBQyxLQUFLLENBQUMsR0FBRyxDQUFDLEdBQUcsSUFBSSxDQUFDO0lBQzVDLENBQUM7Ozs7O0lBRU0sV0FBVyxDQUFDLEdBQVc7UUFDMUIsT0FBTyxJQUFJLENBQUMsZ0JBQWdCLENBQUMsS0FBSyxDQUFDLEdBQUcsQ0FBQyxDQUFDO0lBQzVDLENBQUM7Ozs7SUFFTSxnQkFBZ0I7UUFDbkIsT0FBTyxJQUFJLENBQUMsZ0JBQWdCLENBQUMsS0FBSyxDQUFDO0lBQ3ZDLENBQUM7Ozs7OztJQUVPLGtCQUFrQixDQUFDLElBQWdDO1FBQ3ZELElBQUksSUFBSSxDQUFDLGdCQUFnQixDQUFDLEtBQUssQ0FBQyxJQUFJLENBQUMsSUFBSSxJQUFJLENBQUMsb0JBQW9CLENBQUMsSUFBSSxDQUFDLEVBQUU7WUFDdEUsT0FBTyxJQUFJLENBQUM7U0FDZjthQUFNO1lBQ0gsTUFBTSxJQUFJLEtBQUssQ0FBQyxRQUFRLElBQUksd0NBQXdDLENBQUMsQ0FBQztTQUN6RTtJQUNMLENBQUM7Ozs7OztJQUVPLG9CQUFvQixDQUFDLFFBQWdCO1FBQ3pDLE9BQU8sUUFBUSxLQUFLLGtDQUFrQyxDQUFDLElBQUksSUFBSSxRQUFRLEtBQUssa0NBQWtDLENBQUMsTUFBTSxDQUFDO0lBQzFILENBQUM7OztZQTVESixVQUFVOzs7OzBDQVNGLE1BQU0sU0FBQyx1QkFBdUI7WUFuQjlCLGdDQUFnQzs7Ozs7OztJQWFyQyw4REFBb0Q7O0lBQ3BELHlEQUF5Qzs7SUFDekMseUVBQXVEOztJQUN2RCwyRUFBeUQ7Ozs7O0lBR3JELHFEQUFpRTs7Ozs7SUFDakUsZ0VBQTREIiwic291cmNlc0NvbnRlbnQiOlsiaW1wb3J0IHsgSW5qZWN0LCBJbmplY3RhYmxlLCBJbmplY3Rpb25Ub2tlbiwgVGVtcGxhdGVSZWYgfSBmcm9tICdAYW5ndWxhci9jb3JlJztcclxuaW1wb3J0IHsgQmVoYXZpb3JTdWJqZWN0LCBPYnNlcnZhYmxlIH0gZnJvbSAncnhqcyc7XHJcbmltcG9ydCB7IE5neFBlcm1pc3Npb25zUHJlZGVmaW5lZFN0cmF0ZWdpZXMgfSBmcm9tICcuLi9lbnVtcy9wcmVkZWZpbmVkLXN0cmF0ZWdpZXMuZW51bSc7XHJcbmltcG9ydCB7IE5neFBlcm1pc3Npb25zQ29uZmlndXJhdGlvblN0b3JlIH0gZnJvbSAnLi4vc3RvcmUvY29uZmlndXJhdGlvbi5zdG9yZSc7XHJcblxyXG5leHBvcnQgdHlwZSBTdHJhdGVneUZ1bmN0aW9uID0gKHRlbXBsYXRlUmVmPzogVGVtcGxhdGVSZWY8YW55PikgPT4gdm9pZDtcclxuXHJcbmV4cG9ydCB0eXBlIFN0cmF0ZWd5ID0ge1xyXG4gICAgW2tleTogc3RyaW5nXTogU3RyYXRlZ3lGdW5jdGlvblxyXG59O1xyXG5cclxuZXhwb3J0IGNvbnN0IFVTRV9DT05GSUdVUkFUSU9OX1NUT1JFID0gbmV3IEluamVjdGlvblRva2VuKCdVU0VfQ09ORklHVVJBVElPTl9TVE9SRScpO1xyXG5cclxuQEluamVjdGFibGUoKVxyXG5leHBvcnQgY2xhc3MgTmd4UGVybWlzc2lvbnNDb25maWd1cmF0aW9uU2VydmljZSB7XHJcblxyXG4gICAgcHJpdmF0ZSBzdHJhdGVnaWVzU291cmNlOiBCZWhhdmlvclN1YmplY3Q8U3RyYXRlZ3k+O1xyXG4gICAgcHVibGljIHN0cmF0ZWdpZXMkOiBPYnNlcnZhYmxlPFN0cmF0ZWd5PjtcclxuICAgIHB1YmxpYyBvbkF1dGhvcmlzZWREZWZhdWx0U3RyYXRlZ3k6IHN0cmluZyB8IHVuZGVmaW5lZDtcclxuICAgIHB1YmxpYyBvblVuQXV0aG9yaXNlZERlZmF1bHRTdHJhdGVneTogc3RyaW5nIHwgdW5kZWZpbmVkO1xyXG5cclxuICAgIGNvbnN0cnVjdG9yKFxyXG4gICAgICAgIEBJbmplY3QoVVNFX0NPTkZJR1VSQVRJT05fU1RPUkUpIHByaXZhdGUgaXNvbGF0ZTogYm9vbGVhbiA9IGZhbHNlLFxyXG4gICAgICAgIHByaXZhdGUgY29uZmlndXJhdGlvblN0b3JlOiBOZ3hQZXJtaXNzaW9uc0NvbmZpZ3VyYXRpb25TdG9yZVxyXG4gICAgKSB7XHJcbiAgICAgICAgdGhpcy5zdHJhdGVnaWVzU291cmNlID0gdGhpcy5pc29sYXRlID8gbmV3IEJlaGF2aW9yU3ViamVjdDxTdHJhdGVneT4oe30pIDogdGhpcy5jb25maWd1cmF0aW9uU3RvcmUuc3RyYXRlZ2llc1NvdXJjZTtcclxuICAgICAgICB0aGlzLnN0cmF0ZWdpZXMkID0gdGhpcy5zdHJhdGVnaWVzU291cmNlLmFzT2JzZXJ2YWJsZSgpO1xyXG5cclxuICAgICAgICB0aGlzLm9uQXV0aG9yaXNlZERlZmF1bHRTdHJhdGVneSA9IHRoaXMuaXNvbGF0ZSA/IHVuZGVmaW5lZCA6IHRoaXMuY29uZmlndXJhdGlvblN0b3JlLm9uQXV0aG9yaXNlZERlZmF1bHRTdHJhdGVneTtcclxuICAgICAgICB0aGlzLm9uVW5BdXRob3Jpc2VkRGVmYXVsdFN0cmF0ZWd5ID0gdGhpcy5pc29sYXRlID8gdW5kZWZpbmVkIDogdGhpcy5jb25maWd1cmF0aW9uU3RvcmUub25VbkF1dGhvcmlzZWREZWZhdWx0U3RyYXRlZ3k7XHJcblxyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBzZXREZWZhdWx0T25BdXRob3JpemVkU3RyYXRlZ3kobmFtZTogc3RyaW5nIHwgJ3JlbW92ZScgfCAnc2hvdycpOiB2b2lkIHtcclxuICAgICAgICBpZiAodGhpcy5pc29sYXRlKSB7XHJcbiAgICAgICAgICAgIHRoaXMub25BdXRob3Jpc2VkRGVmYXVsdFN0cmF0ZWd5ID0gdGhpcy5nZXREZWZpbmVkU3RyYXRlZ3kobmFtZSk7XHJcbiAgICAgICAgfSBlbHNlIHtcclxuICAgICAgICAgICAgdGhpcy5jb25maWd1cmF0aW9uU3RvcmUub25BdXRob3Jpc2VkRGVmYXVsdFN0cmF0ZWd5ID0gdGhpcy5nZXREZWZpbmVkU3RyYXRlZ3kobmFtZSk7XHJcbiAgICAgICAgICAgIHRoaXMub25BdXRob3Jpc2VkRGVmYXVsdFN0cmF0ZWd5ID0gdGhpcy5jb25maWd1cmF0aW9uU3RvcmUub25BdXRob3Jpc2VkRGVmYXVsdFN0cmF0ZWd5O1xyXG4gICAgICAgIH1cclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgc2V0RGVmYXVsdE9uVW5hdXRob3JpemVkU3RyYXRlZ3kobmFtZTogc3RyaW5nIHwgJ3JlbW92ZScgfCAnc2hvdycpOiB2b2lkIHtcclxuICAgICAgICBpZiAodGhpcy5pc29sYXRlKSB7XHJcbiAgICAgICAgICAgIHRoaXMub25VbkF1dGhvcmlzZWREZWZhdWx0U3RyYXRlZ3kgPSB0aGlzLmdldERlZmluZWRTdHJhdGVneShuYW1lKTtcclxuICAgICAgICB9IGVsc2Uge1xyXG4gICAgICAgICAgICB0aGlzLmNvbmZpZ3VyYXRpb25TdG9yZS5vblVuQXV0aG9yaXNlZERlZmF1bHRTdHJhdGVneSA9IHRoaXMuZ2V0RGVmaW5lZFN0cmF0ZWd5KG5hbWUpO1xyXG4gICAgICAgICAgICB0aGlzLm9uVW5BdXRob3Jpc2VkRGVmYXVsdFN0cmF0ZWd5ID0gdGhpcy5jb25maWd1cmF0aW9uU3RvcmUub25VbkF1dGhvcmlzZWREZWZhdWx0U3RyYXRlZ3k7XHJcbiAgICAgICAgfVxyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBhZGRQZXJtaXNzaW9uU3RyYXRlZ3koa2V5OiBzdHJpbmcsIGZ1bmM6IFN0cmF0ZWd5RnVuY3Rpb24pOiB2b2lkIHtcclxuICAgICAgICB0aGlzLnN0cmF0ZWdpZXNTb3VyY2UudmFsdWVba2V5XSA9IGZ1bmM7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGdldFN0cmF0ZWd5KGtleTogc3RyaW5nKSB7XHJcbiAgICAgICAgcmV0dXJuIHRoaXMuc3RyYXRlZ2llc1NvdXJjZS52YWx1ZVtrZXldO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBnZXRBbGxTdHJhdGVnaWVzKCkge1xyXG4gICAgICAgIHJldHVybiB0aGlzLnN0cmF0ZWdpZXNTb3VyY2UudmFsdWU7XHJcbiAgICB9XHJcblxyXG4gICAgcHJpdmF0ZSBnZXREZWZpbmVkU3RyYXRlZ3kobmFtZTogc3RyaW5nIHwgJ3JlbW92ZScgfCAnc2hvdycpIHtcclxuICAgICAgICBpZiAodGhpcy5zdHJhdGVnaWVzU291cmNlLnZhbHVlW25hbWVdIHx8IHRoaXMuaXNQcmVkZWZpbmVkU3RyYXRlZ3kobmFtZSkpIHtcclxuICAgICAgICAgICAgcmV0dXJuIG5hbWU7XHJcbiAgICAgICAgfSBlbHNlIHtcclxuICAgICAgICAgICAgdGhyb3cgbmV3IEVycm9yKGBObyAnICR7bmFtZX0gJyBzdHJhdGVneSBpcyBmb3VuZCBwbGVhc2UgZGVmaW5lIG9uZWApO1xyXG4gICAgICAgIH1cclxuICAgIH1cclxuXHJcbiAgICBwcml2YXRlIGlzUHJlZGVmaW5lZFN0cmF0ZWd5KHN0cmF0ZWd5OiBzdHJpbmcpOiBib29sZWFuIHtcclxuICAgICAgICByZXR1cm4gc3RyYXRlZ3kgPT09IE5neFBlcm1pc3Npb25zUHJlZGVmaW5lZFN0cmF0ZWdpZXMuU0hPVyB8fCBzdHJhdGVneSA9PT0gTmd4UGVybWlzc2lvbnNQcmVkZWZpbmVkU3RyYXRlZ2llcy5SRU1PVkU7XHJcbiAgICB9XHJcblxyXG59XHJcbiJdfQ==