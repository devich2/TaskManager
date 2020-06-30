/**
 * @fileoverview added by tsickle
 * @suppress {checkTypes,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
 */
import { ChangeDetectorRef, Directive, EventEmitter, Input, Output, TemplateRef, ViewContainerRef } from '@angular/core';
import { merge } from 'rxjs';
import { skip, take } from 'rxjs/operators';
import { NgxPermissionsPredefinedStrategies } from '../enums/predefined-strategies.enum';
import { NgxPermissionsConfigurationService } from '../service/configuration.service';
import { NgxPermissionsService } from '../service/permissions.service';
import { NgxRolesService } from '../service/roles.service';
import { isBoolean, isFunction, isString, notEmptyValue } from '../utils/utils';
export class NgxPermissionsDirective {
    /**
     * @param {?} permissionsService
     * @param {?} configurationService
     * @param {?} rolesService
     * @param {?} viewContainer
     * @param {?} changeDetector
     * @param {?} templateRef
     */
    constructor(permissionsService, configurationService, rolesService, viewContainer, changeDetector, templateRef) {
        this.permissionsService = permissionsService;
        this.configurationService = configurationService;
        this.rolesService = rolesService;
        this.viewContainer = viewContainer;
        this.changeDetector = changeDetector;
        this.templateRef = templateRef;
        this.permissionsAuthorized = new EventEmitter();
        this.permissionsUnauthorized = new EventEmitter();
        // skip first run cause merge will fire twice
        this.firstMergeUnusedRun = 1;
    }
    /**
     * @return {?}
     */
    ngOnInit() {
        this.viewContainer.clear();
        this.initPermissionSubscription = this.validateExceptOnlyPermissions();
    }
    /**
     * @param {?} changes
     * @return {?}
     */
    ngOnChanges(changes) {
        /** @type {?} */
        const onlyChanges = changes['ngxPermissionsOnly'];
        /** @type {?} */
        const exceptChanges = changes['ngxPermissionsExcept'];
        if (onlyChanges || exceptChanges) {
            // Due to bug when you pass empty array
            if (onlyChanges && onlyChanges.firstChange)
                return;
            if (exceptChanges && exceptChanges.firstChange)
                return;
            merge(this.permissionsService.permissions$, this.rolesService.roles$)
                .pipe(skip(this.firstMergeUnusedRun), take(1))
                .subscribe((/**
             * @return {?}
             */
            () => {
                if (notEmptyValue(this.ngxPermissionsExcept)) {
                    this.validateExceptAndOnlyPermissions();
                    return;
                }
                if (notEmptyValue(this.ngxPermissionsOnly)) {
                    this.validateOnlyPermissions();
                    return;
                }
                this.handleAuthorisedPermission(this.getAuthorisedTemplates());
            }));
        }
    }
    /**
     * @return {?}
     */
    ngOnDestroy() {
        if (this.initPermissionSubscription) {
            this.initPermissionSubscription.unsubscribe();
        }
    }
    /**
     * @private
     * @return {?}
     */
    validateExceptOnlyPermissions() {
        return merge(this.permissionsService.permissions$, this.rolesService.roles$)
            .pipe(skip(this.firstMergeUnusedRun))
            .subscribe((/**
         * @return {?}
         */
        () => {
            if (notEmptyValue(this.ngxPermissionsExcept)) {
                this.validateExceptAndOnlyPermissions();
                return;
            }
            if (notEmptyValue(this.ngxPermissionsOnly)) {
                this.validateOnlyPermissions();
                return;
            }
            this.handleAuthorisedPermission(this.getAuthorisedTemplates());
        }));
    }
    /**
     * @private
     * @return {?}
     */
    validateExceptAndOnlyPermissions() {
        Promise.all([this.permissionsService.hasPermission(this.ngxPermissionsExcept), this.rolesService.hasOnlyRoles(this.ngxPermissionsExcept)])
            .then((/**
         * @param {?} __0
         * @return {?}
         */
        ([hasPermission, hasRole]) => {
            if (hasPermission || hasRole) {
                this.handleUnauthorisedPermission(this.ngxPermissionsExceptElse || this.ngxPermissionsElse);
                return;
            }
            if (!!this.ngxPermissionsOnly)
                throw false;
            this.handleAuthorisedPermission(this.ngxPermissionsExceptThen || this.ngxPermissionsThen || this.templateRef);
        })).catch((/**
         * @return {?}
         */
        () => {
            if (!!this.ngxPermissionsOnly) {
                this.validateOnlyPermissions();
            }
            else {
                this.handleAuthorisedPermission(this.ngxPermissionsExceptThen || this.ngxPermissionsThen || this.templateRef);
            }
        }));
    }
    /**
     * @private
     * @return {?}
     */
    validateOnlyPermissions() {
        Promise.all([this.permissionsService.hasPermission(this.ngxPermissionsOnly), this.rolesService.hasOnlyRoles(this.ngxPermissionsOnly)])
            .then((/**
         * @param {?} __0
         * @return {?}
         */
        ([hasPermissions, hasRoles]) => {
            if (hasPermissions || hasRoles) {
                this.handleAuthorisedPermission(this.ngxPermissionsOnlyThen || this.ngxPermissionsThen || this.templateRef);
            }
            else {
                this.handleUnauthorisedPermission(this.ngxPermissionsOnlyElse || this.ngxPermissionsElse);
            }
        })).catch((/**
         * @return {?}
         */
        () => {
            this.handleUnauthorisedPermission(this.ngxPermissionsOnlyElse || this.ngxPermissionsElse);
        }));
    }
    /**
     * @private
     * @param {?} template
     * @return {?}
     */
    handleUnauthorisedPermission(template) {
        if (isBoolean(this.currentAuthorizedState) && !this.currentAuthorizedState)
            return;
        this.currentAuthorizedState = false;
        this.permissionsUnauthorized.emit();
        if (this.getUnAuthorizedStrategyInput()) {
            this.applyStrategyAccordingToStrategyType(this.getUnAuthorizedStrategyInput());
            return;
        }
        if (this.configurationService.onUnAuthorisedDefaultStrategy && !this.elseBlockDefined()) {
            this.applyStrategy(this.configurationService.onUnAuthorisedDefaultStrategy);
        }
        else {
            this.showTemplateBlockInView(template);
        }
    }
    /**
     * @private
     * @param {?} template
     * @return {?}
     */
    handleAuthorisedPermission(template) {
        if (isBoolean(this.currentAuthorizedState) && this.currentAuthorizedState)
            return;
        this.currentAuthorizedState = true;
        this.permissionsAuthorized.emit();
        if (this.getAuthorizedStrategyInput()) {
            this.applyStrategyAccordingToStrategyType(this.getAuthorizedStrategyInput());
            return;
        }
        if (this.configurationService.onAuthorisedDefaultStrategy && !this.thenBlockDefined()) {
            this.applyStrategy(this.configurationService.onAuthorisedDefaultStrategy);
        }
        else {
            this.showTemplateBlockInView(template);
        }
    }
    /**
     * @private
     * @param {?} strategy
     * @return {?}
     */
    applyStrategyAccordingToStrategyType(strategy) {
        if (isString(strategy)) {
            this.applyStrategy(strategy);
            return;
        }
        if (isFunction(strategy)) {
            this.showTemplateBlockInView(this.templateRef);
            ((/** @type {?} */ (strategy)))(this.templateRef);
            return;
        }
    }
    /**
     * @private
     * @param {?} template
     * @return {?}
     */
    showTemplateBlockInView(template) {
        this.viewContainer.clear();
        if (!template) {
            return;
        }
        this.viewContainer.createEmbeddedView(template);
        this.changeDetector.markForCheck();
    }
    /**
     * @private
     * @return {?}
     */
    getAuthorisedTemplates() {
        return this.ngxPermissionsOnlyThen
            || this.ngxPermissionsExceptThen
            || this.ngxPermissionsThen
            || this.templateRef;
    }
    /**
     * @private
     * @return {?}
     */
    elseBlockDefined() {
        return !!this.ngxPermissionsExceptElse || !!this.ngxPermissionsElse;
    }
    /**
     * @private
     * @return {?}
     */
    thenBlockDefined() {
        return !!this.ngxPermissionsExceptThen || !!this.ngxPermissionsThen;
    }
    /**
     * @private
     * @return {?}
     */
    getAuthorizedStrategyInput() {
        return this.ngxPermissionsOnlyAuthorisedStrategy ||
            this.ngxPermissionsExceptAuthorisedStrategy ||
            this.ngxPermissionsAuthorisedStrategy;
    }
    /**
     * @private
     * @return {?}
     */
    getUnAuthorizedStrategyInput() {
        return this.ngxPermissionsOnlyUnauthorisedStrategy ||
            this.ngxPermissionsExceptUnauthorisedStrategy ||
            this.ngxPermissionsUnauthorisedStrategy;
    }
    /**
     * @private
     * @param {?} str
     * @return {?}
     */
    applyStrategy(str) {
        if (str === NgxPermissionsPredefinedStrategies.SHOW) {
            this.showTemplateBlockInView(this.templateRef);
            return;
        }
        if (str === NgxPermissionsPredefinedStrategies.REMOVE) {
            this.viewContainer.clear();
            return;
        }
        /** @type {?} */
        const strategy = this.configurationService.getStrategy(str);
        this.showTemplateBlockInView(this.templateRef);
        strategy(this.templateRef);
    }
}
NgxPermissionsDirective.decorators = [
    { type: Directive, args: [{
                selector: '[ngxPermissionsOnly],[ngxPermissionsExcept]'
            },] }
];
/** @nocollapse */
NgxPermissionsDirective.ctorParameters = () => [
    { type: NgxPermissionsService },
    { type: NgxPermissionsConfigurationService },
    { type: NgxRolesService },
    { type: ViewContainerRef },
    { type: ChangeDetectorRef },
    { type: TemplateRef }
];
NgxPermissionsDirective.propDecorators = {
    ngxPermissionsOnly: [{ type: Input }],
    ngxPermissionsOnlyThen: [{ type: Input }],
    ngxPermissionsOnlyElse: [{ type: Input }],
    ngxPermissionsExcept: [{ type: Input }],
    ngxPermissionsExceptElse: [{ type: Input }],
    ngxPermissionsExceptThen: [{ type: Input }],
    ngxPermissionsThen: [{ type: Input }],
    ngxPermissionsElse: [{ type: Input }],
    ngxPermissionsOnlyAuthorisedStrategy: [{ type: Input }],
    ngxPermissionsOnlyUnauthorisedStrategy: [{ type: Input }],
    ngxPermissionsExceptUnauthorisedStrategy: [{ type: Input }],
    ngxPermissionsExceptAuthorisedStrategy: [{ type: Input }],
    ngxPermissionsUnauthorisedStrategy: [{ type: Input }],
    ngxPermissionsAuthorisedStrategy: [{ type: Input }],
    permissionsAuthorized: [{ type: Output }],
    permissionsUnauthorized: [{ type: Output }]
};
if (false) {
    /** @type {?} */
    NgxPermissionsDirective.prototype.ngxPermissionsOnly;
    /** @type {?} */
    NgxPermissionsDirective.prototype.ngxPermissionsOnlyThen;
    /** @type {?} */
    NgxPermissionsDirective.prototype.ngxPermissionsOnlyElse;
    /** @type {?} */
    NgxPermissionsDirective.prototype.ngxPermissionsExcept;
    /** @type {?} */
    NgxPermissionsDirective.prototype.ngxPermissionsExceptElse;
    /** @type {?} */
    NgxPermissionsDirective.prototype.ngxPermissionsExceptThen;
    /** @type {?} */
    NgxPermissionsDirective.prototype.ngxPermissionsThen;
    /** @type {?} */
    NgxPermissionsDirective.prototype.ngxPermissionsElse;
    /** @type {?} */
    NgxPermissionsDirective.prototype.ngxPermissionsOnlyAuthorisedStrategy;
    /** @type {?} */
    NgxPermissionsDirective.prototype.ngxPermissionsOnlyUnauthorisedStrategy;
    /** @type {?} */
    NgxPermissionsDirective.prototype.ngxPermissionsExceptUnauthorisedStrategy;
    /** @type {?} */
    NgxPermissionsDirective.prototype.ngxPermissionsExceptAuthorisedStrategy;
    /** @type {?} */
    NgxPermissionsDirective.prototype.ngxPermissionsUnauthorisedStrategy;
    /** @type {?} */
    NgxPermissionsDirective.prototype.ngxPermissionsAuthorisedStrategy;
    /** @type {?} */
    NgxPermissionsDirective.prototype.permissionsAuthorized;
    /** @type {?} */
    NgxPermissionsDirective.prototype.permissionsUnauthorized;
    /**
     * @type {?}
     * @private
     */
    NgxPermissionsDirective.prototype.initPermissionSubscription;
    /**
     * @type {?}
     * @private
     */
    NgxPermissionsDirective.prototype.firstMergeUnusedRun;
    /**
     * @type {?}
     * @private
     */
    NgxPermissionsDirective.prototype.currentAuthorizedState;
    /**
     * @type {?}
     * @private
     */
    NgxPermissionsDirective.prototype.permissionsService;
    /**
     * @type {?}
     * @private
     */
    NgxPermissionsDirective.prototype.configurationService;
    /**
     * @type {?}
     * @private
     */
    NgxPermissionsDirective.prototype.rolesService;
    /**
     * @type {?}
     * @private
     */
    NgxPermissionsDirective.prototype.viewContainer;
    /**
     * @type {?}
     * @private
     */
    NgxPermissionsDirective.prototype.changeDetector;
    /**
     * @type {?}
     * @private
     */
    NgxPermissionsDirective.prototype.templateRef;
}
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoicGVybWlzc2lvbnMuZGlyZWN0aXZlLmpzIiwic291cmNlUm9vdCI6Im5nOi8vbmd4LXBlcm1pc3Npb25zLyIsInNvdXJjZXMiOlsibGliL2RpcmVjdGl2ZS9wZXJtaXNzaW9ucy5kaXJlY3RpdmUudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7OztBQUFBLE9BQU8sRUFDSCxpQkFBaUIsRUFDakIsU0FBUyxFQUNULFlBQVksRUFDWixLQUFLLEVBR0wsTUFBTSxFQUNOLFdBQVcsRUFDWCxnQkFBZ0IsRUFDbkIsTUFBTSxlQUFlLENBQUM7QUFFdkIsT0FBTyxFQUFFLEtBQUssRUFBZ0IsTUFBTSxNQUFNLENBQUM7QUFDM0MsT0FBTyxFQUFFLElBQUksRUFBRSxJQUFJLEVBQUUsTUFBTSxnQkFBZ0IsQ0FBQztBQUU1QyxPQUFPLEVBQUUsa0NBQWtDLEVBQUUsTUFBTSxxQ0FBcUMsQ0FBQztBQUN6RixPQUFPLEVBQUUsa0NBQWtDLEVBQW9CLE1BQU0sa0NBQWtDLENBQUM7QUFDeEcsT0FBTyxFQUFFLHFCQUFxQixFQUFFLE1BQU0sZ0NBQWdDLENBQUM7QUFDdkUsT0FBTyxFQUFFLGVBQWUsRUFBRSxNQUFNLDBCQUEwQixDQUFDO0FBQzNELE9BQU8sRUFBRSxTQUFTLEVBQUUsVUFBVSxFQUFFLFFBQVEsRUFBRSxhQUFhLEVBQUUsTUFBTSxnQkFBZ0IsQ0FBQztBQUtoRixNQUFNLE9BQU8sdUJBQXVCOzs7Ozs7Ozs7SUE4QmhDLFlBQ1ksa0JBQXlDLEVBQ3pDLG9CQUF3RCxFQUN4RCxZQUE2QixFQUM3QixhQUErQixFQUMvQixjQUFpQyxFQUNqQyxXQUE2QjtRQUw3Qix1QkFBa0IsR0FBbEIsa0JBQWtCLENBQXVCO1FBQ3pDLHlCQUFvQixHQUFwQixvQkFBb0IsQ0FBb0M7UUFDeEQsaUJBQVksR0FBWixZQUFZLENBQWlCO1FBQzdCLGtCQUFhLEdBQWIsYUFBYSxDQUFrQjtRQUMvQixtQkFBYyxHQUFkLGNBQWMsQ0FBbUI7UUFDakMsZ0JBQVcsR0FBWCxXQUFXLENB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UCxRQUFRLEVBQUUsNkNBQTZDO2FBQzFEOzs7O1lBTlEscUJBQXFCO1lBRHJCLGtDQUFrQztZQUVsQyxlQUFlO1lBVHBCLGdCQUFnQjtZQVJoQixpQkFBaUI7WUFPakIsV0FBVzs7O2lDQWtCVixLQUFLO3FDQUNMLEtBQUs7cUNBQ0wsS0FBSzttQ0FFTCxLQUFLO3VDQUNMLEtBQUs7dUNBQ0wsS0FBSztpQ0FFTCxLQUFLO2lDQUNMLEtBQUs7bURBRUwsS0FBSztxREFDTCxLQUFLO3VEQUVMLEtBQUs7cURBQ0wsS0FBSztpREFFTCxLQUFLOytDQUNMLEtBQUs7b0NBRUwsTUFBTTtzQ0FDTixNQUFNOzs7O0lBckJQLHFEQUErQzs7SUFDL0MseURBQWtEOztJQUNsRCx5REFBa0Q7O0lBRWxELHVEQUFpRDs7SUFDakQsMkRBQW9EOztJQUNwRCwyREFBb0Q7O0lBRXBELHFEQUE4Qzs7SUFDOUMscURBQThDOztJQUU5Qyx1RUFBeUU7O0lBQ3pFLHlFQUEyRTs7SUFFM0UsMkVBQTZFOztJQUM3RSx5RUFBMkU7O0lBRTNFLHFFQUF1RTs7SUFDdkUsbUVBQXFFOztJQUVyRSx3REFBcUQ7O0lBQ3JELDBEQUF1RDs7Ozs7SUFFdkQsNkRBQWlEOzs7OztJQUVqRCxzREFBZ0M7Ozs7O0lBQ2hDLHlEQUF3Qzs7Ozs7SUFHcEMscURBQWlEOzs7OztJQUNqRCx1REFBZ0U7Ozs7O0lBQ2hFLCtDQUFxQzs7Ozs7SUFDckMsZ0RBQXVDOzs7OztJQUN2QyxpREFBeUM7Ozs7O0lBQ3pDLDhDQUFxQyIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7XHJcbiAgICBDaGFuZ2VEZXRlY3RvclJlZixcclxuICAgIERpcmVjdGl2ZSxcclxuICAgIEV2ZW50RW1pdHRlcixcclxuICAgIElucHV0LCBPbkNoYW5nZXMsXHJcbiAgICBPbkRlc3Ryb3ksXHJcbiAgICBPbkluaXQsXHJcbiAgICBPdXRwdXQsIFNpbXBsZUNoYW5nZXMsXHJcbiAgICBUZW1wbGF0ZVJlZixcclxuICAgIFZpZXdDb250YWluZXJSZWZcclxufSBmcm9tICdAYW5ndWxhci9jb3JlJztcclxuXHJcbmltcG9ydCB7IG1lcmdlLCBTdWJzY3JpcHRpb24gfSBmcm9tICdyeGpzJztcclxuaW1wb3J0IHsgc2tpcCwgdGFrZSB9IGZyb20gJ3J4anMvb3BlcmF0b3JzJztcclxuXHJcbmltcG9ydCB7IE5neFBlcm1pc3Npb25zUHJlZGVmaW5lZFN0cmF0ZWdpZXMgfSBmcm9tICcuLi9lbnVtcy9wcmVkZWZpbmVkLXN0cmF0ZWdpZXMuZW51bSc7XHJcbmltcG9ydCB7IE5neFBlcm1pc3Npb25zQ29uZmlndXJhdGlvblNlcnZpY2UsIFN0cmF0ZWd5RnVuY3Rpb24gfSBmcm9tICcuLi9zZXJ2aWNlL2NvbmZpZ3VyYXRpb24uc2VydmljZSc7XHJcbmltcG9ydCB7IE5neFBlcm1pc3Npb25zU2VydmljZSB9IGZyb20gJy4uL3NlcnZpY2UvcGVybWlzc2lvbnMuc2VydmljZSc7XHJcbmltcG9ydCB7IE5neFJvbGVzU2VydmljZSB9IGZyb20gJy4uL3NlcnZpY2Uvcm9sZXMuc2VydmljZSc7XHJcbmltcG9ydCB7IGlzQm9vbGVhbiwgaXNGdW5jdGlvbiwgaXNTdHJpbmcsIG5vdEVtcHR5VmFsdWUgfSBmcm9tICcuLi91dGlscy91dGlscyc7XHJcblxyXG5ARGlyZWN0aXZlKHtcclxuICAgIHNlbGVjdG9yOiAnW25neFBlcm1pc3Npb25zT25seV0sW25neFBlcm1pc3Npb25zRXhjZXB0XSdcclxufSlcclxuZXhwb3J0IGNsYXNzIE5neFBlcm1pc3Npb25zRGlyZWN0aXZlIGltcGxlbWVudHMgT25Jbml0LCBPbkRlc3Ryb3ksIE9uQ2hhbmdlcyAge1xyXG5cclxuICAgIEBJbnB1dCgpIG5neFBlcm1pc3Npb25zT25seTogc3RyaW5nIHwgc3RyaW5nW107XHJcbiAgICBASW5wdXQoKSBuZ3hQZXJtaXNzaW9uc09ubHlUaGVuOiBUZW1wbGF0ZVJlZjxhbnk+O1xyXG4gICAgQElucHV0KCkgbmd4UGVybWlzc2lvbnNPbmx5RWxzZTogVGVtcGxhdGVSZWY8YW55PjtcclxuXHJcbiAgICBASW5wdXQoKSBuZ3hQZXJtaXNzaW9uc0V4Y2VwdDogc3RyaW5nIHwgc3RyaW5nW107XHJcbiAgICBASW5wdXQoKSBuZ3hQZXJtaXNzaW9uc0V4Y2VwdEVsc2U6IFRlbXBsYXRlUmVmPGFueT47XHJcbiAgICBASW5wdXQoKSBuZ3hQZXJtaXNzaW9uc0V4Y2VwdFRoZW46IFRlbXBsYXRlUmVmPGFueT47XHJcblxyXG4gICAgQElucHV0KCkgbmd4UGVybWlzc2lvbnNUaGVuOiBUZW1wbGF0ZVJlZjxhbnk+O1xyXG4gICAgQElucHV0KCkgbmd4UGVybWlzc2lvbnNFbHNlOiBUZW1wbGF0ZVJlZjxhbnk+O1xyXG5cclxuICAgIEBJbnB1dCgpIG5neFBlcm1pc3Npb25zT25seUF1dGhvcmlzZWRTdHJhdGVneTogc3RyaW5nIHwgU3RyYXRlZ3lGdW5jdGlvbjtcclxuICAgIEBJbnB1dCgpIG5neFBlcm1pc3Npb25zT25seVVuYXV0aG9yaXNlZFN0cmF0ZWd5OiBzdHJpbmcgfCBTdHJhdGVneUZ1bmN0aW9uO1xyXG5cclxuICAgIEBJbnB1dCgpIG5neFBlcm1pc3Npb25zRXhjZXB0VW5hdXRob3Jpc2VkU3RyYXRlZ3k6IHN0cmluZyB8IFN0cmF0ZWd5RnVuY3Rpb247XHJcbiAgICBASW5wdXQoKSBuZ3hQZXJtaXNzaW9uc0V4Y2VwdEF1dGhvcmlzZWRTdHJhdGVneTogc3RyaW5nIHwgU3RyYXRlZ3lGdW5jdGlvbjtcclxuXHJcbiAgICBASW5wdXQoKSBuZ3hQZXJtaXNzaW9uc1VuYXV0aG9yaXNlZFN0cmF0ZWd5OiBzdHJpbmcgfCBTdHJhdGVneUZ1bmN0aW9uO1xyXG4gICAgQElucHV0KCkgbmd4UGVybWlzc2lvbnNBdXRob3Jpc2VkU3RyYXRlZ3k6IHN0cmluZyB8IFN0cmF0ZWd5RnVuY3Rpb247XHJcblxyXG4gICAgQE91dHB1dCgpIHBlcm1pc3Npb25zQXV0aG9yaXplZCA9IG5ldyBFdmVudEVtaXR0ZXIoKTtcclxuICAgIEBPdXRwdXQoKSBwZXJtaXNzaW9uc1VuYXV0aG9yaXplZCA9IG5ldyBFdmVudEVtaXR0ZXIoKTtcclxuXHJcbiAgICBwcml2YXRlIGluaXRQZXJtaXNzaW9uU3Vic2NyaXB0aW9uOiBTdWJzY3JpcHRpb247XHJcbiAgICAvLyBza2lwIGZpcnN0IHJ1biBjYXVzZSBtZXJnZSB3aWxsIGZpcmUgdHdpY2VcclxuICAgIHByaXZhdGUgZmlyc3RNZXJnZVVudXNlZFJ1biA9IDE7XHJcbiAgICBwcml2YXRlIGN1cnJlbnRBdXRob3JpemVkU3RhdGU6IGJvb2xlYW47XHJcblxyXG4gICAgY29uc3RydWN0b3IoXHJcbiAgICAgICAgcHJpdmF0ZSBwZXJtaXNzaW9uc1NlcnZpY2U6IE5neFBlcm1pc3Npb25zU2VydmljZSxcclxuICAgICAgICBwcml2YXRlIGNvbmZpZ3VyYXRpb25TZXJ2aWNlOiBOZ3hQZXJtaXNzaW9uc0NvbmZpZ3VyYXRpb25TZXJ2aWNlLFxyXG4gICAgICAgIHByaXZhdGUgcm9sZXNTZXJ2aWNlOiBOZ3hSb2xlc1NlcnZpY2UsXHJcbiAgICAgICAgcHJpdmF0ZSB2aWV3Q29udGFpbmVyOiBWaWV3Q29udGFpbmVyUmVmLFxyXG4gICAgICAgIHByaXZhdGUgY2hhbmdlRGV0ZWN0b3I6IENoYW5nZURldGVjdG9yUmVmLFxyXG4gICAgICAgIHByaXZhdGUgdGVtcGxhdGVSZWY6IFRlbXBsYXRlUmVmPGFueT5cclxuICAgICkge1xyXG4gICAgfVxyXG5cclxuICAgIG5nT25Jbml0KCk6IHZvaWQge1xyXG4gICAgICAgIHRoaXMudmlld0NvbnRhaW5lci5jbGVhcigpO1xyXG4gICAgICAgIHRoaXMuaW5pdFBlcm1pc3Npb25TdWJzY3JpcHRpb24gPSB0aGlzLnZhbGlkYXRlRXhjZXB0T25seVBlcm1pc3Npb25zKCk7XHJcbiAgICB9XHJcblxyXG5cclxuICAgIG5nT25DaGFuZ2VzKGNoYW5nZXM6IFNpbXBsZUNoYW5nZXMpOiB2b2lkIHtcclxuICAgICAgICBjb25zdCBvbmx5Q2hhbmdlcyA9IGNoYW5nZXNbJ25neFBlcm1pc3Npb25zT25seSddO1xyXG4gICAgICAgIGNvbnN0IGV4Y2VwdENoYW5nZXMgPSBjaGFuZ2VzWyduZ3hQZXJtaXNzaW9uc0V4Y2VwdCddO1xyXG4gICAgICAgIGlmIChvbmx5Q2hhbmdlcyB8fCBleGNlcHRDaGFuZ2VzKSB7XHJcbiAgICAgICAgICAgIC8vIER1ZSB0byBidWcgd2hlbiB5b3UgcGFzcyBlbXB0eSBhcnJheVxyXG4gICAgICAgICAgICBpZiAob25seUNoYW5nZXMgJiYgb25seUNoYW5nZXMuZmlyc3RDaGFuZ2UpIHJldHVybjtcclxuICAgICAgICAgICAgaWYgKGV4Y2VwdENoYW5nZXMgJiYgZXhjZXB0Q2hhbmdlcy5maXJzdENoYW5nZSkgcmV0dXJuO1xyXG5cclxuICAgICAgICAgICAgbWVyZ2UodGhpcy5wZXJtaXNzaW9uc1NlcnZpY2UucGVybWlzc2lvbnMkLCB0aGlzLnJvbGVzU2VydmljZS5yb2xlcyQpXHJcbiAgICAgICAgICAgICAgICAucGlwZShza2lwKHRoaXMuZmlyc3RNZXJnZVVudXNlZFJ1biksIHRha2UoMSkpXHJcbiAgICAgICAgICAgICAgICAuc3Vic2NyaWJlKCgpID0+IHtcclxuICAgICAgICAgICAgICAgICAgICBpZiAobm90RW1wdHlWYWx1ZSh0aGlzLm5neFBlcm1pc3Npb25zRXhjZXB0KSkge1xyXG4gICAgICAgICAgICAgICAgICAgICAgICB0aGlzLnZhbGlkYXRlRXhjZXB0QW5kT25seVBlcm1pc3Npb25zKCk7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgIHJldHVybjtcclxuICAgICAgICAgICAgICAgICAgICB9XHJcblxyXG4gICAgICAgICAgICAgICAgICAgIGlmIChub3RFbXB0eVZhbHVlKHRoaXMubmd4UGVybWlzc2lvbnNPbmx5KSkge1xyXG4gICAgICAgICAgICAgICAgICAgICAgICB0aGlzLnZhbGlkYXRlT25seVBlcm1pc3Npb25zKCk7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgIHJldHVybjtcclxuICAgICAgICAgICAgICAgICAgICB9XHJcblxyXG4gICAgICAgICAgICAgICAgICAgIHRoaXMuaGFuZGxlQXV0aG9yaXNlZFBlcm1pc3Npb24odGhpcy5nZXRBdXRob3Jpc2VkVGVtcGxhdGVzKCkpO1xyXG4gICAgICAgICAgICAgICAgfSk7XHJcbiAgICAgICAgfVxyXG4gICAgfVxyXG5cclxuICAgIG5nT25EZXN0cm95KCk6IHZvaWQge1xyXG4gICAgICAgIGlmICh0aGlzLmluaXRQZXJtaXNzaW9uU3Vic2NyaXB0aW9uKSB7XHJcbiAgICAgICAgICAgIHRoaXMuaW5pdFBlcm1pc3Npb25TdWJzY3JpcHRpb24udW5zdWJzY3JpYmUoKTtcclxuICAgICAgICB9XHJcbiAgICB9XHJcblxyXG4gICAgcHJpdmF0ZSB2YWxpZGF0ZUV4Y2VwdE9ubHlQZXJtaXNzaW9ucygpOiBTdWJzY3JpcHRpb24ge1xyXG4gICAgICAgIHJldHVybiBtZXJnZSh0aGlzLnBlcm1pc3Npb25zU2VydmljZS5wZXJtaXNzaW9ucyQsIHRoaXMucm9sZXNTZXJ2aWNlLnJvbGVzJClcclxuICAgICAgICAgICAgLnBpcGUoc2tpcCh0aGlzLmZpcnN0TWVyZ2VVbnVzZWRSdW4pKVxyXG4gICAgICAgICAgICAuc3Vic2NyaWJlKCgpID0+IHtcclxuICAgICAgICAgICAgICAgIGlmIChub3RFbXB0eVZhbHVlKHRoaXMubmd4UGVybWlzc2lvbnNFeGNlcHQpKSB7XHJcbiAgICAgICAgICAgICAgICAgICAgdGhpcy52YWxpZGF0ZUV4Y2VwdEFuZE9ubHlQZXJtaXNzaW9ucygpO1xyXG4gICAgICAgICAgICAgICAgICAgIHJldHVybjtcclxuICAgICAgICAgICAgICAgIH1cclxuXHJcbiAgICAgICAgICAgICAgICBpZiAobm90RW1wdHlWYWx1ZSh0aGlzLm5neFBlcm1pc3Npb25zT25seSkpIHtcclxuICAgICAgICAgICAgICAgICAgICB0aGlzLnZhbGlkYXRlT25seVBlcm1pc3Npb25zKCk7XHJcbiAgICAgICAgICAgICAgICAgICAgcmV0dXJuO1xyXG4gICAgICAgICAgICAgICAgfVxyXG4gICAgICAgICAgICAgICAgdGhpcy5oYW5kbGVBdXRob3Jpc2VkUGVybWlzc2lvbih0aGlzLmdldEF1dGhvcmlzZWRUZW1wbGF0ZXMoKSk7XHJcbiAgICAgICAgICAgIH0pO1xyXG4gICAgfVxyXG5cclxuICAgIHByaXZhdGUgdmFsaWRhdGVFeGNlcHRBbmRPbmx5UGVybWlzc2lvbnMoKTogdm9pZCB7XHJcbiAgICAgICAgUHJvbWlzZS5hbGwoW3RoaXMucGVybWlzc2lvbnNTZXJ2aWNlLmhhc1Blcm1pc3Npb24odGhpcy5uZ3hQZXJtaXNzaW9uc0V4Y2VwdCksIHRoaXMucm9sZXNTZXJ2aWNlLmhhc09ubHlSb2xlcyh0aGlzLm5neFBlcm1pc3Npb25zRXhjZXB0KV0pXHJcbiAgICAgICAgICAgIC50aGVuKChbaGFzUGVybWlzc2lvbiwgaGFzUm9sZV0pID0+IHtcclxuICAgICAgICAgICAgICAgIGlmIChoYXNQZXJtaXNzaW9uIHx8IGhhc1JvbGUpIHtcclxuICAgICAgICAgICAgICAgICAgICB0aGlzLmhhbmRsZVVuYXV0aG9yaXNlZFBlcm1pc3Npb24odGhpcy5uZ3hQZXJtaXNzaW9uc0V4Y2VwdEVsc2UgfHwgdGhpcy5uZ3hQZXJtaXNzaW9uc0Vsc2UpO1xyXG4gICAgICAgICAgICAgICAgICAgIHJldHVybjtcclxuICAgICAgICAgICAgICAgIH1cclxuXHJcbiAgICAgICAgICAgICAgICBpZiAoISF0aGlzLm5neFBlcm1pc3Npb25zT25seSkgIHRocm93IGZhbHNlO1xyXG5cclxuICAgICAgICAgICAgICAgIHRoaXMuaGFuZGxlQXV0aG9yaXNlZFBlcm1pc3Npb24odGhpcy5uZ3hQZXJtaXNzaW9uc0V4Y2VwdFRoZW4gfHwgdGhpcy5uZ3hQZXJtaXNzaW9uc1RoZW4gfHwgdGhpcy50ZW1wbGF0ZVJlZik7XHJcblxyXG4gICAgICAgICAgICB9KS5jYXRjaCgoKSA9PiB7XHJcbiAgICAgICAgICAgICAgICBpZiAoISF0aGlzLm5neFBlcm1pc3Npb25zT25seSkge1xyXG4gICAgICAgICAgICAgICAgICAgIHRoaXMudmFsaWRhdGVPbmx5UGVybWlzc2lvbnMoKTtcclxuICAgICAgICAgICAgICAgIH0gZWxzZSB7XHJcbiAgICAgICAgICAgICAgICAgICAgdGhpcy5oYW5kbGVBdXRob3Jpc2VkUGVybWlzc2lvbih0aGlzLm5neFBlcm1pc3Npb25zRXhjZXB0VGhlbiB8fCB0aGlzLm5neFBlcm1pc3Npb25zVGhlbiB8fCB0aGlzLnRlbXBsYXRlUmVmKTtcclxuICAgICAgICAgICAgICAgIH1cclxuICAgICAgICB9KTtcclxuICAgIH1cclxuXHJcbiAgICBwcml2YXRlIHZhbGlkYXRlT25seVBlcm1pc3Npb25zKCk6IHZvaWQge1xyXG4gICAgICAgIFByb21pc2UuYWxsKFt0aGlzLnBlcm1pc3Npb25zU2VydmljZS5oYXNQZXJtaXNzaW9uKHRoaXMubmd4UGVybWlzc2lvbnNPbmx5KSwgdGhpcy5yb2xlc1NlcnZpY2UuaGFzT25seVJvbGVzKHRoaXMubmd4UGVybWlzc2lvbnNPbmx5KV0pXHJcbiAgICAgICAgICAgIC50aGVuKChbaGFzUGVybWlzc2lvbnMsIGhhc1JvbGVzXSkgPT4ge1xyXG4gICAgICAgICAgICAgICAgaWYgKGhhc1Blcm1pc3Npb25zIHx8IGhhc1JvbGVzKSB7XHJcbiAgICAgICAgICAgICAgICAgICAgdGhpcy5oYW5kbGVBdXRob3Jpc2VkUGVybWlzc2lvbih0aGlzLm5neFBlcm1pc3Npb25zT25seVRoZW4gfHwgdGhpcy5uZ3hQZXJtaXNzaW9uc1RoZW4gfHwgdGhpcy50ZW1wbGF0ZVJlZik7XHJcbiAgICAgICAgICAgICAgICB9IGVsc2Uge1xyXG4gICAgICAgICAgICAgICAgICAgIHRoaXMuaGFuZGxlVW5hdXRob3Jpc2VkUGVybWlzc2lvbih0aGlzLm5neFBlcm1pc3Npb25zT25seUVsc2UgfHwgdGhpcy5uZ3hQZXJtaXNzaW9uc0Vsc2UpO1xyXG4gICAgICAgICAgICAgICAgfVxyXG4gICAgICAgICAgICB9KS5jYXRjaCgoKSA9PiB7XHJcbiAgICAgICAgICAgICAgICB0aGlzLmhhbmRsZVVuYXV0aG9yaXNlZFBlcm1pc3Npb24odGhpcy5uZ3hQZXJtaXNzaW9uc09ubHlFbHNlIHx8IHRoaXMubmd4UGVybWlzc2lvbnNFbHNlKTtcclxuICAgICAgICB9KTtcclxuICAgIH1cclxuXHJcbiAgICBwcml2YXRlIGhhbmRsZVVuYXV0aG9yaXNlZFBlcm1pc3Npb24odGVtcGxhdGU6IFRlbXBsYXRlUmVmPGFueT4pOiB2b2lkIHtcclxuICAgICAgICBpZiAoaXNCb29sZWFuKHRoaXMuY3VycmVudEF1dGhvcml6ZWRTdGF0ZSkgJiYgIXRoaXMuY3VycmVudEF1dGhvcml6ZWRTdGF0ZSkgcmV0dXJuO1xyXG5cclxuICAgICAgICB0aGlzLmN1cnJlbnRBdXRob3JpemVkU3RhdGUgPSBmYWxzZTtcclxuICAgICAgICB0aGlzLnBlcm1pc3Npb25zVW5hdXRob3JpemVkLmVtaXQoKTtcclxuXHJcbiAgICAgICAgaWYgKHRoaXMuZ2V0VW5BdXRob3JpemVkU3RyYXRlZ3lJbnB1dCgpKSB7XHJcbiAgICAgICAgICAgIHRoaXMuYXBwbHlTdHJhdGVneUFjY29yZGluZ1RvU3RyYXRlZ3lUeXBlKHRoaXMuZ2V0VW5BdXRob3JpemVkU3RyYXRlZ3lJbnB1dCgpKTtcclxuICAgICAgICAgICAgcmV0dXJuO1xyXG4gICAgICAgIH1cclxuXHJcbiAgICAgICAgaWYgKHRoaXMuY29uZmlndXJhdGlvblNlcnZpY2Uub25VbkF1dGhvcmlzZWREZWZhdWx0U3RyYXRlZ3kgJiYgIXRoaXMuZWxzZUJsb2NrRGVmaW5lZCgpKSB7XHJcbiAgICAgICAgICAgIHRoaXMuYXBwbHlTdHJhdGVneSh0aGlzLmNvbmZpZ3VyYXRpb25TZXJ2aWNlLm9uVW5BdXRob3Jpc2VkRGVmYXVsdFN0cmF0ZWd5KTtcclxuICAgICAgICB9IGVsc2Uge1xyXG4gICAgICAgICAgICB0aGlzLnNob3dUZW1wbGF0ZUJsb2NrSW5WaWV3KHRlbXBsYXRlKTtcclxuICAgICAgICB9XHJcblxyXG4gICAgfVxyXG5cclxuICAgIHByaXZhdGUgaGFuZGxlQXV0aG9yaXNlZFBlcm1pc3Npb24odGVtcGxhdGU6IFRlbXBsYXRlUmVmPGFueT4pOiB2b2lkIHtcclxuICAgICAgICBpZiAoaXNCb29sZWFuKHRoaXMuY3VycmVudEF1dGhvcml6ZWRTdGF0ZSkgJiYgdGhpcy5jdXJyZW50QXV0aG9yaXplZFN0YXRlKSByZXR1cm47XHJcblxyXG4gICAgICAgIHRoaXMuY3VycmVudEF1dGhvcml6ZWRTdGF0ZSA9IHRydWU7XHJcbiAgICAgICAgdGhpcy5wZXJtaXNzaW9uc0F1dGhvcml6ZWQuZW1pdCgpO1xyXG5cclxuICAgICAgICBpZiAodGhpcy5nZXRBdXRob3JpemVkU3RyYXRlZ3lJbnB1dCgpKSB7XHJcbiAgICAgICAgICAgIHRoaXMuYXBwbHlTdHJhdGVneUFjY29yZGluZ1RvU3RyYXRlZ3lUeXBlKHRoaXMuZ2V0QXV0aG9yaXplZFN0cmF0ZWd5SW5wdXQoKSk7XHJcbiAgICAgICAgICAgIHJldHVybjtcclxuICAgICAgICB9XHJcblxyXG4gICAgICAgIGlmICh0aGlzLmNvbmZpZ3VyYXRpb25TZXJ2aWNlLm9uQXV0aG9yaXNlZERlZmF1bHRTdHJhdGVneSAmJiAhdGhpcy50aGVuQmxvY2tEZWZpbmVkKCkpIHtcclxuICAgICAgICAgICAgdGhpcy5hcHBseVN0cmF0ZWd5KHRoaXMuY29uZmlndXJhdGlvblNlcnZpY2Uub25BdXRob3Jpc2VkRGVmYXVsdFN0cmF0ZWd5KTtcclxuICAgICAgICB9IGVsc2Uge1xyXG4gICAgICAgICAgICB0aGlzLnNob3dUZW1wbGF0ZUJsb2NrSW5WaWV3KHRlbXBsYXRlKTtcclxuICAgICAgICB9XHJcbiAgICB9XHJcblxyXG4gICAgcHJpdmF0ZSBhcHBseVN0cmF0ZWd5QWNjb3JkaW5nVG9TdHJhdGVneVR5cGUoc3RyYXRlZ3k6IHN0cmluZyB8IEZ1bmN0aW9uKTogdm9pZCB7XHJcbiAgICAgICAgaWYgKGlzU3RyaW5nKHN0cmF0ZWd5KSkge1xyXG4gICAgICAgICAgICB0aGlzLmFwcGx5U3RyYXRlZ3koc3RyYXRlZ3kpO1xyXG4gICAgICAgICAgICByZXR1cm47XHJcbiAgICAgICAgfVxyXG5cclxuICAgICAgICBpZiAoaXNGdW5jdGlvbihzdHJhdGVneSkpIHtcclxuICAgICAgICAgICAgdGhpcy5zaG93VGVtcGxhdGVCbG9ja0luVmlldyh0aGlzLnRlbXBsYXRlUmVmKTtcclxuICAgICAgICAgICAgKHN0cmF0ZWd5IGFzIEZ1bmN0aW9uKSh0aGlzLnRlbXBsYXRlUmVmKTtcclxuICAgICAgICAgICAgcmV0dXJuO1xyXG4gICAgICAgIH1cclxuICAgIH1cclxuXHJcbiAgICBwcml2YXRlIHNob3dUZW1wbGF0ZUJsb2NrSW5WaWV3KHRlbXBsYXRlOiBUZW1wbGF0ZVJlZjxhbnk+KTogdm9pZCB7XHJcbiAgICAgICAgdGhpcy52aWV3Q29udGFpbmVyLmNsZWFyKCk7XHJcbiAgICAgICAgaWYgKCF0ZW1wbGF0ZSkge1xyXG4gICAgICAgICAgICByZXR1cm47XHJcbiAgICAgICAgfVxyXG5cclxuICAgICAgICB0aGlzLnZpZXdDb250YWluZXIuY3JlYXRlRW1iZWRkZWRWaWV3KHRlbXBsYXRlKTtcclxuICAgICAgICB0aGlzLmNoYW5nZURldGVjdG9yLm1hcmtGb3JDaGVjaygpO1xyXG4gICAgfVxyXG5cclxuICAgIHByaXZhdGUgZ2V0QXV0aG9yaXNlZFRlbXBsYXRlcygpOiBUZW1wbGF0ZVJlZjxhbnk+IHtcclxuICAgICAgICByZXR1cm4gdGhpcy5uZ3hQZXJtaXNzaW9uc09ubHlUaGVuXHJcbiAgICAgICAgICAgIHx8IHRoaXMubmd4UGVybWlzc2lvbnNFeGNlcHRUaGVuXHJcbiAgICAgICAgICAgIHx8IHRoaXMubmd4UGVybWlzc2lvbnNUaGVuXHJcbiAgICAgICAgICAgIHx8IHRoaXMudGVtcGxhdGVSZWY7XHJcbiAgICB9XHJcblxyXG4gICAgcHJpdmF0ZSBlbHNlQmxvY2tEZWZpbmVkKCk6IGJvb2xlYW4ge1xyXG4gICAgICAgIHJldHVybiAhIXRoaXMubmd4UGVybWlzc2lvbnNFeGNlcHRFbHNlIHx8ICEhdGhpcy5uZ3hQZXJtaXNzaW9uc0Vsc2U7XHJcbiAgICB9XHJcblxyXG4gICAgcHJpdmF0ZSB0aGVuQmxvY2tEZWZpbmVkKCkge1xyXG4gICAgICAgIHJldHVybiAhIXRoaXMubmd4UGVybWlzc2lvbnNFeGNlcHRUaGVuIHx8ICEhdGhpcy5uZ3hQZXJtaXNzaW9uc1RoZW47XHJcbiAgICB9XHJcblxyXG4gICAgcHJpdmF0ZSBnZXRBdXRob3JpemVkU3RyYXRlZ3lJbnB1dCgpIHtcclxuICAgICAgICByZXR1cm4gdGhpcy5uZ3hQZXJtaXNzaW9uc09ubHlBdXRob3Jpc2VkU3RyYXRlZ3kgfHxcclxuICAgICAgICAgICAgdGhpcy5uZ3hQZXJtaXNzaW9uc0V4Y2VwdEF1dGhvcmlzZWRTdHJhdGVneSB8fFxyXG4gICAgICAgICAgICB0aGlzLm5neFBlcm1pc3Npb25zQXV0aG9yaXNlZFN0cmF0ZWd5O1xyXG4gICAgfVxyXG5cclxuICAgIHByaXZhdGUgZ2V0VW5BdXRob3JpemVkU3RyYXRlZ3lJbnB1dCgpIHtcclxuICAgICAgICByZXR1cm4gdGhpcy5uZ3hQZXJtaXNzaW9uc09ubHlVbmF1dGhvcmlzZWRTdHJhdGVneSB8fFxyXG4gICAgICAgICAgICB0aGlzLm5neFBlcm1pc3Npb25zRXhjZXB0VW5hdXRob3Jpc2VkU3RyYXRlZ3kgfHxcclxuICAgICAgICAgICAgdGhpcy5uZ3hQZXJtaXNzaW9uc1VuYXV0aG9yaXNlZFN0cmF0ZWd5O1xyXG4gICAgfVxyXG5cclxuICAgIHByaXZhdGUgYXBwbHlTdHJhdGVneShzdHI6IGFueSkge1xyXG4gICAgICAgIGlmIChzdHIgPT09IE5neFBlcm1pc3Npb25zUHJlZGVmaW5lZFN0cmF0ZWdpZXMuU0hPVykge1xyXG4gICAgICAgICAgICB0aGlzLnNob3dUZW1wbGF0ZUJsb2NrSW5WaWV3KHRoaXMudGVtcGxhdGVSZWYpO1xyXG4gICAgICAgICAgICByZXR1cm47XHJcbiAgICAgICAgfVxyXG5cclxuICAgICAgICBpZiAoc3RyID09PSBOZ3hQZXJtaXNzaW9uc1ByZWRlZmluZWRTdHJhdGVnaWVzLlJFTU9WRSkge1xyXG4gICAgICAgICAgICB0aGlzLnZpZXdDb250YWluZXIuY2xlYXIoKTtcclxuICAgICAgICAgICAgcmV0dXJuO1xyXG4gICAgICAgIH1cclxuICAgICAgICBjb25zdCBzdHJhdGVneSA9IHRoaXMuY29uZmlndXJhdGlvblNlcnZpY2UuZ2V0U3RyYXRlZ3koc3RyKTtcclxuICAgICAgICB0aGlzLnNob3dUZW1wbGF0ZUJsb2NrSW5WaWV3KHRoaXMudGVtcGxhdGVSZWYpO1xyXG4gICAgICAgIHN0cmF0ZWd5KHRoaXMudGVtcGxhdGVSZWYpO1xyXG4gICAgfVxyXG59XHJcbiJdfQ==