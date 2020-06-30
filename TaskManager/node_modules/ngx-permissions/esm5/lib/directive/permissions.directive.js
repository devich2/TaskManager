/**
 * @fileoverview added by tsickle
 * @suppress {checkTypes,extraRequire,missingOverride,missingReturn,unusedPrivateMembers,uselessCode} checked by tsc
 */
import * as tslib_1 from "tslib";
import { ChangeDetectorRef, Directive, EventEmitter, Input, Output, TemplateRef, ViewContainerRef } from '@angular/core';
import { merge } from 'rxjs';
import { skip, take } from 'rxjs/operators';
import { NgxPermissionsPredefinedStrategies } from '../enums/predefined-strategies.enum';
import { NgxPermissionsConfigurationService } from '../service/configuration.service';
import { NgxPermissionsService } from '../service/permissions.service';
import { NgxRolesService } from '../service/roles.service';
import { isBoolean, isFunction, isString, notEmptyValue } from '../utils/utils';
var NgxPermissionsDirective = /** @class */ (function () {
    function NgxPermissionsDirective(permissionsService, configurationService, rolesService, viewContainer, changeDetector, templateRef) {
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
    NgxPermissionsDirective.prototype.ngOnInit = /**
     * @return {?}
     */
    function () {
        this.viewContainer.clear();
        this.initPermissionSubscription = this.validateExceptOnlyPermissions();
    };
    /**
     * @param {?} changes
     * @return {?}
     */
    NgxPermissionsDirective.prototype.ngOnChanges = /**
     * @param {?} changes
     * @return {?}
     */
    function (changes) {
        var _this = this;
        /** @type {?} */
        var onlyChanges = changes['ngxPermissionsOnly'];
        /** @type {?} */
        var exceptChanges = changes['ngxPermissionsExcept'];
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
            function () {
                if (notEmptyValue(_this.ngxPermissionsExcept)) {
                    _this.validateExceptAndOnlyPermissions();
                    return;
                }
                if (notEmptyValue(_this.ngxPermissionsOnly)) {
                    _this.validateOnlyPermissions();
                    return;
                }
                _this.handleAuthorisedPermission(_this.getAuthorisedTemplates());
            }));
        }
    };
    /**
     * @return {?}
     */
    NgxPermissionsDirective.prototype.ngOnDestroy = /**
     * @return {?}
     */
    function () {
        if (this.initPermissionSubscription) {
            this.initPermissionSubscription.unsubscribe();
        }
    };
    /**
     * @private
     * @return {?}
     */
    NgxPermissionsDirective.prototype.validateExceptOnlyPermissions = /**
     * @private
     * @return {?}
     */
    function () {
        var _this = this;
        return merge(this.permissionsService.permissions$, this.rolesService.roles$)
            .pipe(skip(this.firstMergeUnusedRun))
            .subscribe((/**
         * @return {?}
         */
        function () {
            if (notEmptyValue(_this.ngxPermissionsExcept)) {
                _this.validateExceptAndOnlyPermissions();
                return;
            }
            if (notEmptyValue(_this.ngxPermissionsOnly)) {
                _this.validateOnlyPermissions();
                return;
            }
            _this.handleAuthorisedPermission(_this.getAuthorisedTemplates());
        }));
    };
    /**
     * @private
     * @return {?}
     */
    NgxPermissionsDirective.prototype.validateExceptAndOnlyPermissions = /**
     * @private
     * @return {?}
     */
    function () {
        var _this = this;
        Promise.all([this.permissionsService.hasPermission(this.ngxPermissionsExcept), this.rolesService.hasOnlyRoles(this.ngxPermissionsExcept)])
            .then((/**
         * @param {?} __0
         * @return {?}
         */
        function (_a) {
            var _b = tslib_1.__read(_a, 2), hasPermission = _b[0], hasRole = _b[1];
            if (hasPermission || hasRole) {
                _this.handleUnauthorisedPermission(_this.ngxPermissionsExceptElse || _this.ngxPermissionsElse);
                return;
            }
            if (!!_this.ngxPermissionsOnly)
                throw false;
            _this.handleAuthorisedPermission(_this.ngxPermissionsExceptThen || _this.ngxPermissionsThen || _this.templateRef);
        })).catch((/**
         * @return {?}
         */
        function () {
            if (!!_this.ngxPermissionsOnly) {
                _this.validateOnlyPermissions();
            }
            else {
                _this.handleAuthorisedPermission(_this.ngxPermissionsExceptThen || _this.ngxPermissionsThen || _this.templateRef);
            }
        }));
    };
    /**
     * @private
     * @return {?}
     */
    NgxPermissionsDirective.prototype.validateOnlyPermissions = /**
     * @private
     * @return {?}
     */
    function () {
        var _this = this;
        Promise.all([this.permissionsService.hasPermission(this.ngxPermissionsOnly), this.rolesService.hasOnlyRoles(this.ngxPermissionsOnly)])
            .then((/**
         * @param {?} __0
         * @return {?}
         */
        function (_a) {
            var _b = tslib_1.__read(_a, 2), hasPermissions = _b[0], hasRoles = _b[1];
            if (hasPermissions || hasRoles) {
                _this.handleAuthorisedPermission(_this.ngxPermissionsOnlyThen || _this.ngxPermissionsThen || _this.templateRef);
            }
            else {
                _this.handleUnauthorisedPermission(_this.ngxPermissionsOnlyElse || _this.ngxPermissionsElse);
            }
        })).catch((/**
         * @return {?}
         */
        function () {
            _this.handleUnauthorisedPermission(_this.ngxPermissionsOnlyElse || _this.ngxPermissionsElse);
        }));
    };
    /**
     * @private
     * @param {?} template
     * @return {?}
     */
    NgxPermissionsDirective.prototype.handleUnauthorisedPermission = /**
     * @private
     * @param {?} template
     * @return {?}
     */
    function (template) {
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
    };
    /**
     * @private
     * @param {?} template
     * @return {?}
     */
    NgxPermissionsDirective.prototype.handleAuthorisedPermission = /**
     * @private
     * @param {?} template
     * @return {?}
     */
    function (template) {
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
    };
    /**
     * @private
     * @param {?} strategy
     * @return {?}
     */
    NgxPermissionsDirective.prototype.applyStrategyAccordingToStrategyType = /**
     * @private
     * @param {?} strategy
     * @return {?}
     */
    function (strategy) {
        if (isString(strategy)) {
            this.applyStrategy(strategy);
            return;
        }
        if (isFunction(strategy)) {
            this.showTemplateBlockInView(this.templateRef);
            ((/** @type {?} */ (strategy)))(this.templateRef);
            return;
        }
    };
    /**
     * @private
     * @param {?} template
     * @return {?}
     */
    NgxPermissionsDirective.prototype.showTemplateBlockInView = /**
     * @private
     * @param {?} template
     * @return {?}
     */
    function (template) {
        this.viewContainer.clear();
        if (!template) {
            return;
        }
        this.viewContainer.createEmbeddedView(template);
        this.changeDetector.markForCheck();
    };
    /**
     * @private
     * @return {?}
     */
    NgxPermissionsDirective.prototype.getAuthorisedTemplates = /**
     * @private
     * @return {?}
     */
    function () {
        return this.ngxPermissionsOnlyThen
            || this.ngxPermissionsExceptThen
            || this.ngxPermissionsThen
            || this.templateRef;
    };
    /**
     * @private
     * @return {?}
     */
    NgxPermissionsDirective.prototype.elseBlockDefined = /**
     * @private
     * @return {?}
     */
    function () {
        return !!this.ngxPermissionsExceptElse || !!this.ngxPermissionsElse;
    };
    /**
     * @private
     * @return {?}
     */
    NgxPermissionsDirective.prototype.thenBlockDefined = /**
     * @private
     * @return {?}
     */
    function () {
        return !!this.ngxPermissionsExceptThen || !!this.ngxPermissionsThen;
    };
    /**
     * @private
     * @return {?}
     */
    NgxPermissionsDirective.prototype.getAuthorizedStrategyInput = /**
     * @private
     * @return {?}
     */
    function () {
        return this.ngxPermissionsOnlyAuthorisedStrategy ||
            this.ngxPermissionsExceptAuthorisedStrategy ||
            this.ngxPermissionsAuthorisedStrategy;
    };
    /**
     * @private
     * @return {?}
     */
    NgxPermissionsDirective.prototype.getUnAuthorizedStrategyInput = /**
     * @private
     * @return {?}
     */
    function () {
        return this.ngxPermissionsOnlyUnauthorisedStrategy ||
            this.ngxPermissionsExceptUnauthorisedStrategy ||
            this.ngxPermissionsUnauthorisedStrategy;
    };
    /**
     * @private
     * @param {?} str
     * @return {?}
     */
    NgxPermissionsDirective.prototype.applyStrategy = /**
     * @private
     * @param {?} str
     * @return {?}
     */
    function (str) {
        if (str === NgxPermissionsPredefinedStrategies.SHOW) {
            this.showTemplateBlockInView(this.templateRef);
            return;
        }
        if (str === NgxPermissionsPredefinedStrategies.REMOVE) {
            this.viewContainer.clear();
            return;
        }
        /** @type {?} */
        var strategy = this.configurationService.getStrategy(str);
        this.showTemplateBlockInView(this.templateRef);
        strategy(this.templateRef);
    };
    NgxPermissionsDirective.decorators = [
        { type: Directive, args: [{
                    selector: '[ngxPermissionsOnly],[ngxPermissionsExcept]'
                },] }
    ];
    /** @nocollapse */
    NgxPermissionsDirective.ctorParameters = function () { return [
        { type: NgxPermissionsService },
        { type: NgxPermissionsConfigurationService },
        { type: NgxRolesService },
        { type: ViewContainerRef },
        { type: ChangeDetectorRef },
        { type: TemplateRef }
    ]; };
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
    return NgxPermissionsDirective;
}());
export { NgxPermissionsDirective };
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
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoicGVybWlzc2lvbnMuZGlyZWN0aXZlLmpzIiwic291cmNlUm9vdCI6Im5nOi8vbmd4LXBlcm1pc3Npb25zLyIsInNvdXJjZXMiOlsibGliL2RpcmVjdGl2ZS9wZXJtaXNzaW9ucy5kaXJlY3RpdmUudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7Ozs7QUFBQSxPQUFPLEVBQ0gsaUJBQWlCLEVBQ2pCLFNBQVMsRUFDVCxZQUFZLEVBQ1osS0FBSyxFQUdMLE1BQU0sRUFDTixXQUFXLEVBQ1gsZ0JBQWdCLEVBQ25CLE1BQU0sZUFBZSxDQUFDO0FBRXZCLE9BQU8sRUFBRSxLQUFLLEVBQWdCLE1BQU0sTUFBTSxDQUFDO0FBQzNDLE9BQU8sRUFBRSxJQUFJLEVBQUUsSUFBSSxFQUFFLE1BQU0sZ0JBQWdCLENBQUM7QUFFNUMsT0FBTyxFQUFFLGtDQUFrQyxFQUFFLE1BQU0scUNBQXFDLENBQUM7QUFDekYsT0FBTyxFQUFFLGtDQUFrQyxFQUFvQixNQUFNLGtDQUFrQyxDQUFDO0FBQ3hHLE9BQU8sRUFBRSxxQkFBcUIsRUFBRSxNQUFNLGdDQUFnQyxDQUFDO0FBQ3ZFLE9BQU8sRUFBRSxlQUFlLEVBQUUsTUFBTSwwQkFBMEIsQ0FBQztBQUMzRCxPQUFPLEVBQUUsU0FBUyxFQUFFLFVBQVUsRUFBRSxRQUFRLEVBQUUsYUFBYSxFQUFFLE1BQU0sZ0JBQWdCLENBQUM7QUFFaEY7SUFpQ0ksaUNBQ1ksa0JBQXlDLEVBQ3pDLG9CQUF3RCxFQUN4RCxZQUE2QixFQUM3QixhQUErQixFQUMvQixjQUFpQyxFQUNqQyxXQUE2QjtRQUw3Qix1QkFBa0IsR0FBbEIsa0JBQWtCLENBQXVCO1FBQ3pDLHlCQUFvQixHQUFwQixvQkFBb0IsQ0FBb0M7UUFDeEQsaUJBQVksR0FBWixZQUFZLENBQWlCO1FBQzdCLGtCQUFhLEdBQWIsYUFBYSxDQUFrQjtRQUMvQixtQkFBYyxHQUFkLGNBQWMsQ0FBbUI7UUFDakMsZ0JBQVcsR0FBWCxXQUFXLENBQWtCO1FBZC9CLDBCQUFxQixHQUFHLElB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b3JSZWYsXHJcbiAgICBEaXJlY3RpdmUsXHJcbiAgICBFdmVudEVtaXR0ZXIsXHJcbiAgICBJbnB1dCwgT25DaGFuZ2VzLFxyXG4gICAgT25EZXN0cm95LFxyXG4gICAgT25Jbml0LFxyXG4gICAgT3V0cHV0LCBTaW1wbGVDaGFuZ2VzLFxyXG4gICAgVGVtcGxhdGVSZWYsXHJcbiAgICBWaWV3Q29udGFpbmVyUmVmXHJcbn0gZnJvbSAnQGFuZ3VsYXIvY29yZSc7XHJcblxyXG5pbXBvcnQgeyBtZXJnZSwgU3Vic2NyaXB0aW9uIH0gZnJvbSAncnhqcyc7XHJcbmltcG9ydCB7IHNraXAsIHRha2UgfSBmcm9tICdyeGpzL29wZXJhdG9ycyc7XHJcblxyXG5pbXBvcnQgeyBOZ3hQZXJtaXNzaW9uc1ByZWRlZmluZWRTdHJhdGVnaWVzIH0gZnJvbSAnLi4vZW51bXMvcHJlZGVmaW5lZC1zdHJhdGVnaWVzLmVudW0nO1xyXG5pbXBvcnQgeyBOZ3hQZXJtaXNzaW9uc0NvbmZpZ3VyYXRpb25TZXJ2aWNlLCBTdHJhdGVneUZ1bmN0aW9uIH0gZnJvbSAnLi4vc2VydmljZS9jb25maWd1cmF0aW9uLnNlcnZpY2UnO1xyXG5pbXBvcnQgeyBOZ3hQZXJtaXNzaW9uc1NlcnZpY2UgfSBmcm9tICcuLi9zZXJ2aWNlL3Blcm1pc3Npb25zLnNlcnZpY2UnO1xyXG5pbXBvcnQgeyBOZ3hSb2xlc1NlcnZpY2UgfSBmcm9tICcuLi9zZXJ2aWNlL3JvbGVzLnNlcnZpY2UnO1xyXG5pbXBvcnQgeyBpc0Jvb2xlYW4sIGlzRnVuY3Rpb24sIGlzU3RyaW5nLCBub3RFbXB0eVZhbHVlIH0gZnJvbSAnLi4vdXRpbHMvdXRpbHMnO1xyXG5cclxuQERpcmVjdGl2ZSh7XHJcbiAgICBzZWxlY3RvcjogJ1tuZ3hQZXJtaXNzaW9uc09ubHldLFtuZ3hQZXJtaXNzaW9uc0V4Y2VwdF0nXHJcbn0pXHJcbmV4cG9ydCBjbGFzcyBOZ3hQZXJtaXNzaW9uc0RpcmVjdGl2ZSBpbXBsZW1lbnRzIE9uSW5pdCwgT25EZXN0cm95LCBPbkNoYW5nZXMgIHtcclxuXHJcbiAgICBASW5wdXQoKSBuZ3hQZXJtaXNzaW9uc09ubHk6IHN0cmluZyB8IHN0cmluZ1tdO1xyXG4gICAgQElucHV0KCkgbmd4UGVybWlzc2lvbnNPbmx5VGhlbjogVGVtcGxhdGVSZWY8YW55PjtcclxuICAgIEBJbnB1dCgpIG5neFBlcm1pc3Npb25zT25seUVsc2U6IFRlbXBsYXRlUmVmPGFueT47XHJcblxyXG4gICAgQElucHV0KCkgbmd4UGVybWlzc2lvbnNFeGNlcHQ6IHN0cmluZyB8IHN0cmluZ1tdO1xyXG4gICAgQElucHV0KCkgbmd4UGVybWlzc2lvbnNFeGNlcHRFbHNlOiBUZW1wbGF0ZVJlZjxhbnk+O1xyXG4gICAgQElucHV0KCkgbmd4UGVybWlzc2lvbnNFeGNlcHRUaGVuOiBUZW1wbGF0ZVJlZjxhbnk+O1xyXG5cclxuICAgIEBJbnB1dCgpIG5neFBlcm1pc3Npb25zVGhlbjogVGVtcGxhdGVSZWY8YW55PjtcclxuICAgIEBJbnB1dCgpIG5neFBlcm1pc3Npb25zRWxzZTogVGVtcGxhdGVSZWY8YW55PjtcclxuXHJcbiAgICBASW5wdXQoKSBuZ3hQZXJtaXNzaW9uc09ubHlBdXRob3Jpc2VkU3RyYXRlZ3k6IHN0cmluZyB8IFN0cmF0ZWd5RnVuY3Rpb247XHJcbiAgICBASW5wdXQoKSBuZ3hQZXJtaXNzaW9uc09ubHlVbmF1dGhvcmlzZWRTdHJhdGVneTogc3RyaW5nIHwgU3RyYXRlZ3lGdW5jdGlvbjtcclxuXHJcbiAgICBASW5wdXQoKSBuZ3hQZXJtaXNzaW9uc0V4Y2VwdFVuYXV0aG9yaXNlZFN0cmF0ZWd5OiBzdHJpbmcgfCBTdHJhdGVneUZ1bmN0aW9uO1xyXG4gICAgQElucHV0KCkgbmd4UGVybWlzc2lvbnNFeGNlcHRBdXRob3Jpc2VkU3RyYXRlZ3k6IHN0cmluZyB8IFN0cmF0ZWd5RnVuY3Rpb247XHJcblxyXG4gICAgQElucHV0KCkgbmd4UGVybWlzc2lvbnNVbmF1dGhvcmlzZWRTdHJhdGVneTogc3RyaW5nIHwgU3RyYXRlZ3lGdW5jdGlvbjtcclxuICAgIEBJbnB1dCgpIG5neFBlcm1pc3Npb25zQXV0aG9yaXNlZFN0cmF0ZWd5OiBzdHJpbmcgfCBTdHJhdGVneUZ1bmN0aW9uO1xyXG5cclxuICAgIEBPdXRwdXQoKSBwZXJtaXNzaW9uc0F1dGhvcml6ZWQgPSBuZXcgRXZlbnRFbWl0dGVyKCk7XHJcbiAgICBAT3V0cHV0KCkgcGVybWlzc2lvbnNVbmF1dGhvcml6ZWQgPSBuZXcgRXZlbnRFbWl0dGVyKCk7XHJcblxyXG4gICAgcHJpdmF0ZSBpbml0UGVybWlzc2lvblN1YnNjcmlwdGlvbjogU3Vic2NyaXB0aW9uO1xyXG4gICAgLy8gc2tpcCBmaXJzdCBydW4gY2F1c2UgbWVyZ2Ugd2lsbCBmaXJlIHR3aWNlXHJcbiAgICBwcml2YXRlIGZpcnN0TWVyZ2VVbnVzZWRSdW4gPSAxO1xyXG4gICAgcHJpdmF0ZSBjdXJyZW50QXV0aG9yaXplZFN0YXRlOiBib29sZWFuO1xyXG5cclxuICAgIGNvbnN0cnVjdG9yKFxyXG4gICAgICAgIHByaXZhdGUgcGVybWlzc2lvbnNTZXJ2aWNlOiBOZ3hQZXJtaXNzaW9uc1NlcnZpY2UsXHJcbiAgICAgICAgcHJpdmF0ZSBjb25maWd1cmF0aW9uU2VydmljZTogTmd4UGVybWlzc2lvbnNDb25maWd1cmF0aW9uU2VydmljZSxcclxuICAgICAgICBwcml2YXRlIHJvbGVzU2VydmljZTogTmd4Um9sZXNTZXJ2aWNlLFxyXG4gICAgICAgIHByaXZhdGUgdmlld0NvbnRhaW5lcjogVmlld0NvbnRhaW5lclJlZixcclxuICAgICAgICBwcml2YXRlIGNoYW5nZURldGVjdG9yOiBDaGFuZ2VEZXRlY3RvclJlZixcclxuICAgICAgICBwcml2YXRlIHRlbXBsYXRlUmVmOiBUZW1wbGF0ZVJlZjxhbnk+XHJcbiAgICApIHtcclxuICAgIH1cclxuXHJcbiAgICBuZ09uSW5pdCgpOiB2b2lkIHtcclxuICAgICAgICB0aGlzLnZpZXdDb250YWluZXIuY2xlYXIoKTtcclxuICAgICAgICB0aGlzLmluaXRQZXJtaXNzaW9uU3Vic2NyaXB0aW9uID0gdGhpcy52YWxpZGF0ZUV4Y2VwdE9ubHlQZXJtaXNzaW9ucygpO1xyXG4gICAgfVxyXG5cclxuXHJcbiAgICBuZ09uQ2hhbmdlcyhjaGFuZ2VzOiBTaW1wbGVDaGFuZ2VzKTogdm9pZCB7XHJcbiAgICAgICAgY29uc3Qgb25seUNoYW5nZXMgPSBjaGFuZ2VzWyduZ3hQZXJtaXNzaW9uc09ubHknXTtcclxuICAgICAgICBjb25zdCBleGNlcHRDaGFuZ2VzID0gY2hhbmdlc1snbmd4UGVybWlzc2lvbnNFeGNlcHQnXTtcclxuICAgICAgICBpZiAob25seUNoYW5nZXMgfHwgZXhjZXB0Q2hhbmdlcykge1xyXG4gICAgICAgICAgICAvLyBEdWUgdG8gYnVnIHdoZW4geW91IHBhc3MgZW1wdHkgYXJyYXlcclxuICAgICAgICAgICAgaWYgKG9ubHlDaGFuZ2VzICYmIG9ubHlDaGFuZ2VzLmZpcnN0Q2hhbmdlKSByZXR1cm47XHJcbiAgICAgICAgICAgIGlmIChleGNlcHRDaGFuZ2VzICYmIGV4Y2VwdENoYW5nZXMuZmlyc3RDaGFuZ2UpIHJldHVybjtcclxuXHJcbiAgICAgICAgICAgIG1lcmdlKHRoaXMucGVybWlzc2lvbnNTZXJ2aWNlLnBlcm1pc3Npb25zJCwgdGhpcy5yb2xlc1NlcnZpY2Uucm9sZXMkKVxyXG4gICAgICAgICAgICAgICAgLnBpcGUoc2tpcCh0aGlzLmZpcnN0TWVyZ2VVbnVzZWRSdW4pLCB0YWtlKDEpKVxyXG4gICAgICAgICAgICAgICAgLnN1YnNjcmliZSgoKSA9PiB7XHJcbiAgICAgICAgICAgICAgICAgICAgaWYgKG5vdEVtcHR5VmFsdWUodGhpcy5uZ3hQZXJtaXNzaW9uc0V4Y2VwdCkpIHtcclxuICAgICAgICAgICAgICAgICAgICAgICAgdGhpcy52YWxpZGF0ZUV4Y2VwdEFuZE9ubHlQZXJtaXNzaW9ucygpO1xyXG4gICAgICAgICAgICAgICAgICAgICAgICByZXR1cm47XHJcbiAgICAgICAgICAgICAgICAgICAgfVxyXG5cclxuICAgICAgICAgICAgICAgICAgICBpZiAobm90RW1wdHlWYWx1ZSh0aGlzLm5neFBlcm1pc3Npb25zT25seSkpIHtcclxuICAgICAgICAgICAgICAgICAgICAgICAgdGhpcy52YWxpZGF0ZU9ubHlQZXJtaXNzaW9ucygpO1xyXG4gICAgICAgICAgICAgICAgICAgICAgICByZXR1cm47XHJcbiAgICAgICAgICAgICAgICAgICAgfVxyXG5cclxuICAgICAgICAgICAgICAgICAgICB0aGlzLmhhbmRsZUF1dGhvcmlzZWRQZXJtaXNzaW9uKHRoaXMuZ2V0QXV0aG9yaXNlZFRlbXBsYXRlcygpKTtcclxuICAgICAgICAgICAgICAgIH0pO1xyXG4gICAgICAgIH1cclxuICAgIH1cclxuXHJcbiAgICBuZ09uRGVzdHJveSgpOiB2b2lkIHtcclxuICAgICAgICBpZiAodGhpcy5pbml0UGVybWlzc2lvblN1YnNjcmlwdGlvbikge1xyXG4gICAgICAgICAgICB0aGlzLmluaXRQZXJtaXNzaW9uU3Vic2NyaXB0aW9uLnVuc3Vic2NyaWJlKCk7XHJcbiAgICAgICAgfVxyXG4gICAgfVxyXG5cclxuICAgIHByaXZhdGUgdmFsaWRhdGVFeGNlcHRPbmx5UGVybWlzc2lvbnMoKTogU3Vic2NyaXB0aW9uIHtcclxuICAgICAgICByZXR1cm4gbWVyZ2UodGhpcy5wZXJtaXNzaW9uc1NlcnZpY2UucGVybWlzc2lvbnMkLCB0aGlzLnJvbGVzU2VydmljZS5yb2xlcyQpXHJcbiAgICAgICAgICAgIC5waXBlKHNraXAodGhpcy5maXJzdE1lcmdlVW51c2VkUnVuKSlcclxuICAgICAgICAgICAgLnN1YnNjcmliZSgoKSA9PiB7XHJcbiAgICAgICAgICAgICAgICBpZiAobm90RW1wdHlWYWx1ZSh0aGlzLm5neFBlcm1pc3Npb25zRXhjZXB0KSkge1xyXG4gICAgICAgICAgICAgICAgICAgIHRoaXMudmFsaWRhdGVFeGNlcHRBbmRPbmx5UGVybWlzc2lvbnMoKTtcclxuICAgICAgICAgICAgICAgICAgICByZXR1cm47XHJcbiAgICAgICAgICAgICAgICB9XHJcblxyXG4gICAgICAgICAgICAgICAgaWYgKG5vdEVtcHR5VmFsdWUodGhpcy5uZ3hQZXJtaXNzaW9uc09ubHkpKSB7XHJcbiAgICAgICAgICAgICAgICAgICAgdGhpcy52YWxpZGF0ZU9ubHlQZXJtaXNzaW9ucygpO1xyXG4gICAgICAgICAgICAgICAgICAgIHJldHVybjtcclxuICAgICAgICAgICAgICAgIH1cclxuICAgICAgICAgICAgICAgIHRoaXMuaGFuZGxlQXV0aG9yaXNlZFBlcm1pc3Npb24odGhpcy5nZXRBdXRob3Jpc2VkVGVtcGxhdGVzKCkpO1xyXG4gICAgICAgICAgICB9KTtcclxuICAgIH1cclxuXHJcbiAgICBwcml2YXRlIHZhbGlkYXRlRXhjZXB0QW5kT25seVBlcm1pc3Npb25zKCk6IHZvaWQge1xyXG4gICAgICAgIFByb21pc2UuYWxsKFt0aGlzLnBlcm1pc3Npb25zU2VydmljZS5oYXNQZXJtaXNzaW9uKHRoaXMubmd4UGVybWlzc2lvbnNFeGNlcHQpLCB0aGlzLnJvbGVzU2VydmljZS5oYXNPbmx5Um9sZXModGhpcy5uZ3hQZXJtaXNzaW9uc0V4Y2VwdCldKVxyXG4gICAgICAgICAgICAudGhlbigoW2hhc1Blcm1pc3Npb24sIGhhc1JvbGVdKSA9PiB7XHJcbiAgICAgICAgICAgICAgICBpZiAoaGFzUGVybWlzc2lvbiB8fCBoYXNSb2xlKSB7XHJcbiAgICAgICAgICAgICAgICAgICAgdGhpcy5oYW5kbGVVbmF1dGhvcmlzZWRQZXJtaXNzaW9uKHRoaXMubmd4UGVybWlzc2lvbnNFeGNlcHRFbHNlIHx8IHRoaXMubmd4UGVybWlzc2lvbnNFbHNlKTtcclxuICAgICAgICAgICAgICAgICAgICByZXR1cm47XHJcbiAgICAgICAgICAgICAgICB9XHJcblxyXG4gICAgICAgICAgICAgICAgaWYgKCEhdGhpcy5uZ3hQZXJtaXNzaW9uc09ubHkpICB0aHJvdyBmYWxzZTtcclxuXHJcbiAgICAgICAgICAgICAgICB0aGlzLmhhbmRsZUF1dGhvcmlzZWRQZXJtaXNzaW9uKHRoaXMubmd4UGVybWlzc2lvbnNFeGNlcHRUaGVuIHx8IHRoaXMubmd4UGVybWlzc2lvbnNUaGVuIHx8IHRoaXMudGVtcGxhdGVSZWYpO1xyXG5cclxuICAgICAgICAgICAgfSkuY2F0Y2goKCkgPT4ge1xyXG4gICAgICAgICAgICAgICAgaWYgKCEhdGhpcy5uZ3hQZXJtaXNzaW9uc09ubHkpIHtcclxuICAgICAgICAgICAgICAgICAgICB0aGlzLnZhbGlkYXRlT25seVBlcm1pc3Npb25zKCk7XHJcbiAgICAgICAgICAgICAgICB9IGVsc2Uge1xyXG4gICAgICAgICAgICAgICAgICAgIHRoaXMuaGFuZGxlQXV0aG9yaXNlZFBlcm1pc3Npb24odGhpcy5uZ3hQZXJtaXNzaW9uc0V4Y2VwdFRoZW4gfHwgdGhpcy5uZ3hQZXJtaXNzaW9uc1RoZW4gfHwgdGhpcy50ZW1wbGF0ZVJlZik7XHJcbiAgICAgICAgICAgICAgICB9XHJcbiAgICAgICAgfSk7XHJcbiAgICB9XHJcblxyXG4gICAgcHJpdmF0ZSB2YWxpZGF0ZU9ubHlQZXJtaXNzaW9ucygpOiB2b2lkIHtcclxuICAgICAgICBQcm9taXNlLmFsbChbdGhpcy5wZXJtaXNzaW9uc1NlcnZpY2UuaGFzUGVybWlzc2lvbih0aGlzLm5neFBlcm1pc3Npb25zT25seSksIHRoaXMucm9sZXNTZXJ2aWNlLmhhc09ubHlSb2xlcyh0aGlzLm5neFBlcm1pc3Npb25zT25seSldKVxyXG4gICAgICAgICAgICAudGhlbigoW2hhc1Blcm1pc3Npb25zLCBoYXNSb2xlc10pID0+IHtcclxuICAgICAgICAgICAgICAgIGlmIChoYXNQZXJtaXNzaW9ucyB8fCBoYXNSb2xlcykge1xyXG4gICAgICAgICAgICAgICAgICAgIHRoaXMuaGFuZGxlQXV0aG9yaXNlZFBlcm1pc3Npb24odGhpcy5uZ3hQZXJtaXNzaW9uc09ubHlUaGVuIHx8IHRoaXMubmd4UGVybWlzc2lvbnNUaGVuIHx8IHRoaXMudGVtcGxhdGVSZWYpO1xyXG4gICAgICAgICAgICAgICAgfSBlbHNlIHtcclxuICAgICAgICAgICAgICAgICAgICB0aGlzLmhhbmRsZVVuYXV0aG9yaXNlZFBlcm1pc3Npb24odGhpcy5uZ3hQZXJtaXNzaW9uc09ubHlFbHNlIHx8IHRoaXMubmd4UGVybWlzc2lvbnNFbHNlKTtcclxuICAgICAgICAgICAgICAgIH1cclxuICAgICAgICAgICAgfSkuY2F0Y2goKCkgPT4ge1xyXG4gICAgICAgICAgICAgICAgdGhpcy5oYW5kbGVVbmF1dGhvcmlzZWRQZXJtaXNzaW9uKHRoaXMubmd4UGVybWlzc2lvbnNPbmx5RWxzZSB8fCB0aGlzLm5neFBlcm1pc3Npb25zRWxzZSk7XHJcbiAgICAgICAgfSk7XHJcbiAgICB9XHJcblxyXG4gICAgcHJpdmF0ZSBoYW5kbGVVbmF1dGhvcmlzZWRQZXJtaXNzaW9uKHRlbXBsYXRlOiBUZW1wbGF0ZVJlZjxhbnk+KTogdm9pZCB7XHJcbiAgICAgICAgaWYgKGlzQm9vbGVhbih0aGlzLmN1cnJlbnRBdXRob3JpemVkU3RhdGUpICYmICF0aGlzLmN1cnJlbnRBdXRob3JpemVkU3RhdGUpIHJldHVybjtcclxuXHJcbiAgICAgICAgdGhpcy5jdXJyZW50QXV0aG9yaXplZFN0YXRlID0gZmFsc2U7XHJcbiAgICAgICAgdGhpcy5wZXJtaXNzaW9uc1VuYXV0aG9yaXplZC5lbWl0KCk7XHJcblxyXG4gICAgICAgIGlmICh0aGlzLmdldFVuQXV0aG9yaXplZFN0cmF0ZWd5SW5wdXQoKSkge1xyXG4gICAgICAgICAgICB0aGlzLmFwcGx5U3RyYXRlZ3lBY2NvcmRpbmdUb1N0cmF0ZWd5VHlwZSh0aGlzLmdldFVuQXV0aG9yaXplZFN0cmF0ZWd5SW5wdXQoKSk7XHJcbiAgICAgICAgICAgIHJldHVybjtcclxuICAgICAgICB9XHJcblxyXG4gICAgICAgIGlmICh0aGlzLmNvbmZpZ3VyYXRpb25TZXJ2aWNlLm9uVW5BdXRob3Jpc2VkRGVmYXVsdFN0cmF0ZWd5ICYmICF0aGlzLmVsc2VCbG9ja0RlZmluZWQoKSkge1xyXG4gICAgICAgICAgICB0aGlzLmFwcGx5U3RyYXRlZ3kodGhpcy5jb25maWd1cmF0aW9uU2VydmljZS5vblVuQXV0aG9yaXNlZERlZmF1bHRTdHJhdGVneSk7XHJcbiAgICAgICAgfSBlbHNlIHtcclxuICAgICAgICAgICAgdGhpcy5zaG93VGVtcGxhdGVCbG9ja0luVmlldyh0ZW1wbGF0ZSk7XHJcbiAgICAgICAgfVxyXG5cclxuICAgIH1cclxuXHJcbiAgICBwcml2YXRlIGhhbmRsZUF1dGhvcmlzZWRQZXJtaXNzaW9uKHRlbXBsYXRlOiBUZW1wbGF0ZVJlZjxhbnk+KTogdm9pZCB7XHJcbiAgICAgICAgaWYgKGlzQm9vbGVhbih0aGlzLmN1cnJlbnRBdXRob3JpemVkU3RhdGUpICYmIHRoaXMuY3VycmVudEF1dGhvcml6ZWRTdGF0ZSkgcmV0dXJuO1xyXG5cclxuICAgICAgICB0aGlzLmN1cnJlbnRBdXRob3JpemVkU3RhdGUgPSB0cnVlO1xyXG4gICAgICAgIHRoaXMucGVybWlzc2lvbnNBdXRob3JpemVkLmVtaXQoKTtcclxuXHJcbiAgICAgICAgaWYgKHRoaXMuZ2V0QXV0aG9yaXplZFN0cmF0ZWd5SW5wdXQoKSkge1xyXG4gICAgICAgICAgICB0aGlzLmFwcGx5U3RyYXRlZ3lBY2NvcmRpbmdUb1N0cmF0ZWd5VHlwZSh0aGlzLmdldEF1dGhvcml6ZWRTdHJhdGVneUlucHV0KCkpO1xyXG4gICAgICAgICAgICByZXR1cm47XHJcbiAgICAgICAgfVxyXG5cclxuICAgICAgICBpZiAodGhpcy5jb25maWd1cmF0aW9uU2VydmljZS5vbkF1dGhvcmlzZWREZWZhdWx0U3RyYXRlZ3kgJiYgIXRoaXMudGhlbkJsb2NrRGVmaW5lZCgpKSB7XHJcbiAgICAgICAgICAgIHRoaXMuYXBwbHlTdHJhdGVneSh0aGlzLmNvbmZpZ3VyYXRpb25TZXJ2aWNlLm9uQXV0aG9yaXNlZERlZmF1bHRTdHJhdGVneSk7XHJcbiAgICAgICAgfSBlbHNlIHtcclxuICAgICAgICAgICAgdGhpcy5zaG93VGVtcGxhdGVCbG9ja0luVmlldyh0ZW1wbGF0ZSk7XHJcbiAgICAgICAgfVxyXG4gICAgfVxyXG5cclxuICAgIHByaXZhdGUgYXBwbHlTdHJhdGVneUFjY29yZGluZ1RvU3RyYXRlZ3lUeXBlKHN0cmF0ZWd5OiBzdHJpbmcgfCBGdW5jdGlvbik6IHZvaWQge1xyXG4gICAgICAgIGlmIChpc1N0cmluZyhzdHJhdGVneSkpIHtcclxuICAgICAgICAgICAgdGhpcy5hcHBseVN0cmF0ZWd5KHN0cmF0ZWd5KTtcclxuICAgICAgICAgICAgcmV0dXJuO1xyXG4gICAgICAgIH1cclxuXHJcbiAgICAgICAgaWYgKGlzRnVuY3Rpb24oc3RyYXRlZ3kpKSB7XHJcbiAgICAgICAgICAgIHRoaXMuc2hvd1RlbXBsYXRlQmxvY2tJblZpZXcodGhpcy50ZW1wbGF0ZVJlZik7XHJcbiAgICAgICAgICAgIChzdHJhdGVneSBhcyBGdW5jdGlvbikodGhpcy50ZW1wbGF0ZVJlZik7XHJcbiAgICAgICAgICAgIHJldHVybjtcclxuICAgICAgICB9XHJcbiAgICB9XHJcblxyXG4gICAgcHJpdmF0ZSBzaG93VGVtcGxhdGVCbG9ja0luVmlldyh0ZW1wbGF0ZTogVGVtcGxhdGVSZWY8YW55Pik6IHZvaWQge1xyXG4gICAgICAgIHRoaXMudmlld0NvbnRhaW5lci5jbGVhcigpO1xyXG4gICAgICAgIGlmICghdGVtcGxhdGUpIHtcclxuICAgICAgICAgICAgcmV0dXJuO1xyXG4gICAgICAgIH1cclxuXHJcbiAgICAgICAgdGhpcy52aWV3Q29udGFpbmVyLmNyZWF0ZUVtYmVkZGVkVmlldyh0ZW1wbGF0ZSk7XHJcbiAgICAgICAgdGhpcy5jaGFuZ2VEZXRlY3Rvci5tYXJrRm9yQ2hlY2soKTtcclxuICAgIH1cclxuXHJcbiAgICBwcml2YXRlIGdldEF1dGhvcmlzZWRUZW1wbGF0ZXMoKTogVGVtcGxhdGVSZWY8YW55PiB7XHJcbiAgICAgICAgcmV0dXJuIHRoaXMubmd4UGVybWlzc2lvbnNPbmx5VGhlblxyXG4gICAgICAgICAgICB8fCB0aGlzLm5neFBlcm1pc3Npb25zRXhjZXB0VGhlblxyXG4gICAgICAgICAgICB8fCB0aGlzLm5neFBlcm1pc3Npb25zVGhlblxyXG4gICAgICAgICAgICB8fCB0aGlzLnRlbXBsYXRlUmVmO1xyXG4gICAgfVxyXG5cclxuICAgIHByaXZhdGUgZWxzZUJsb2NrRGVmaW5lZCgpOiBib29sZWFuIHtcclxuICAgICAgICByZXR1cm4gISF0aGlzLm5neFBlcm1pc3Npb25zRXhjZXB0RWxzZSB8fCAhIXRoaXMubmd4UGVybWlzc2lvbnNFbHNlO1xyXG4gICAgfVxyXG5cclxuICAgIHByaXZhdGUgdGhlbkJsb2NrRGVmaW5lZCgpIHtcclxuICAgICAgICByZXR1cm4gISF0aGlzLm5neFBlcm1pc3Npb25zRXhjZXB0VGhlbiB8fCAhIXRoaXMubmd4UGVybWlzc2lvbnNUaGVuO1xyXG4gICAgfVxyXG5cclxuICAgIHByaXZhdGUgZ2V0QXV0aG9yaXplZFN0cmF0ZWd5SW5wdXQoKSB7XHJcbiAgICAgICAgcmV0dXJuIHRoaXMubmd4UGVybWlzc2lvbnNPbmx5QXV0aG9yaXNlZFN0cmF0ZWd5IHx8XHJcbiAgICAgICAgICAgIHRoaXMubmd4UGVybWlzc2lvbnNFeGNlcHRBdXRob3Jpc2VkU3RyYXRlZ3kgfHxcclxuICAgICAgICAgICAgdGhpcy5uZ3hQZXJtaXNzaW9uc0F1dGhvcmlzZWRTdHJhdGVneTtcclxuICAgIH1cclxuXHJcbiAgICBwcml2YXRlIGdldFVuQXV0aG9yaXplZFN0cmF0ZWd5SW5wdXQoKSB7XHJcbiAgICAgICAgcmV0dXJuIHRoaXMubmd4UGVybWlzc2lvbnNPbmx5VW5hdXRob3Jpc2VkU3RyYXRlZ3kgfHxcclxuICAgICAgICAgICAgdGhpcy5uZ3hQZXJtaXNzaW9uc0V4Y2VwdFVuYXV0aG9yaXNlZFN0cmF0ZWd5IHx8XHJcbiAgICAgICAgICAgIHRoaXMubmd4UGVybWlzc2lvbnNVbmF1dGhvcmlzZWRTdHJhdGVneTtcclxuICAgIH1cclxuXHJcbiAgICBwcml2YXRlIGFwcGx5U3RyYXRlZ3koc3RyOiBhbnkpIHtcclxuICAgICAgICBpZiAoc3RyID09PSBOZ3hQZXJtaXNzaW9uc1ByZWRlZmluZWRTdHJhdGVnaWVzLlNIT1cpIHtcclxuICAgICAgICAgICAgdGhpcy5zaG93VGVtcGxhdGVCbG9ja0luVmlldyh0aGlzLnRlbXBsYXRlUmVmKTtcclxuICAgICAgICAgICAgcmV0dXJuO1xyXG4gICAgICAgIH1cclxuXHJcbiAgICAgICAgaWYgKHN0ciA9PT0gTmd4UGVybWlzc2lvbnNQcmVkZWZpbmVkU3RyYXRlZ2llcy5SRU1PVkUpIHtcclxuICAgICAgICAgICAgdGhpcy52aWV3Q29udGFpbmVyLmNsZWFyKCk7XHJcbiAgICAgICAgICAgIHJldHVybjtcclxuICAgICAgICB9XHJcbiAgICAgICAgY29uc3Qgc3RyYXRlZ3kgPSB0aGlzLmNvbmZpZ3VyYXRpb25TZXJ2aWNlLmdldFN0cmF0ZWd5KHN0cik7XHJcbiAgICAgICAgdGhpcy5zaG93VGVtcGxhdGVCbG9ja0luVmlldyh0aGlzLnRlbXBsYXRlUmVmKTtcclxuICAgICAgICBzdHJhdGVneSh0aGlzLnRlbXBsYXRlUmVmKTtcclxuICAgIH1cclxufVxyXG4iXX0=