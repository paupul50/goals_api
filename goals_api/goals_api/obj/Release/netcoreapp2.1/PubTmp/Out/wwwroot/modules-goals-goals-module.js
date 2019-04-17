(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["modules-goals-goals-module"],{

/***/ "./src/app/modules/goals/components/goals/create-goal/create-goal.component.css":
/*!**************************************************************************************!*\
  !*** ./src/app/modules/goals/components/goals/create-goal/create-goal.component.css ***!
  \**************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL21vZHVsZXMvZ29hbHMvY29tcG9uZW50cy9nb2Fscy9jcmVhdGUtZ29hbC9jcmVhdGUtZ29hbC5jb21wb25lbnQuY3NzIn0= */"

/***/ }),

/***/ "./src/app/modules/goals/components/goals/create-goal/create-goal.component.html":
/*!***************************************************************************************!*\
  !*** ./src/app/modules/goals/components/goals/create-goal/create-goal.component.html ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"container\">\r\n\r\n  <button mat-raised-button color=\"primary\" [routerLink]=\"['/goals']\">Siekiai</button>\r\n  <button mat-raised-button color=\"primary\" [routerLink]=\"['/goals/group']\">Grupės siekiai</button>\r\n  <br>\r\n  <mat-vertical-stepper *ngIf=\"isWorkoutsLoaded\" [linear]=\"true\" #stepper>\r\n    <mat-step [stepControl]=\"goalNameForm\">\r\n      <form [formGroup]=\"goalNameForm\">\r\n        <ng-template matStepLabel>Siekio pavadinimas</ng-template>\r\n        <mat-form-field>\r\n          <input matInput placeholder=\"Siekio pavadinimas\" formControlName=\"goalNameControl\" required>\r\n        </mat-form-field>\r\n        <div>\r\n          <button mat-raised-button color=\"primary\" matStepperNext>sekantis žingsnis</button>\r\n        </div>\r\n      </form>\r\n    </mat-step>\r\n\r\n\r\n    <mat-step>\r\n      <ng-template matStepLabel>Siekio tipas</ng-template>\r\n      <mat-radio-group aria-label=\"Pasirinkite tipą\" [(ngModel)]=\"goalType\">\r\n        <mat-radio-button value=\"1\">Standartinis</mat-radio-button>\r\n        <mat-radio-button [disabled]=\"workouts.length==0\" value=\"2\">Treniruotės</mat-radio-button>\r\n      </mat-radio-group>\r\n      <ng-container *ngIf=\"goalType=='2'\">\r\n        <mat-radio-group aria-label=\"Pasirinkite treniruotę\" [(ngModel)]=\"workoutId\">\r\n          <ng-container *ngFor=\"let workoutObject of workouts; let i=index\">\r\n            <mat-card>\r\n              Treniruotė {{i+1}} -\r\n              <mat-radio-button value=\"{{workoutObject.id}}\">{{workoutObject.name}} -\r\n                {{workoutObject.id}}\r\n              </mat-radio-button>\r\n            </mat-card>\r\n          </ng-container>\r\n        </mat-radio-group>\r\n      </ng-container>\r\n\r\n      <div>\r\n        <button mat-raised-button [disabled]=\"isTypeDisabled()\" color=\"primary\" matStepperNext>sekantis\r\n          žingsnis</button>\r\n      </div>\r\n    </mat-step>\r\n\r\n\r\n    <mat-step>\r\n      <ng-template matStepLabel>Siekio sukūrimas</ng-template>\r\n\r\n      <div>\r\n        <button mat-raised-button color=\"primary\" matStepperPrevious>Praeitas žingsnis</button>\r\n        <button mat-raised-button color=\"primary\" (click)=\"submit()\">Kurti</button>\r\n      </div>\r\n    </mat-step>\r\n  </mat-vertical-stepper>\r\n\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/modules/goals/components/goals/create-goal/create-goal.component.ts":
/*!*************************************************************************************!*\
  !*** ./src/app/modules/goals/components/goals/create-goal/create-goal.component.ts ***!
  \*************************************************************************************/
/*! exports provided: CreateGoalComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "CreateGoalComponent", function() { return CreateGoalComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _services_goals_goals_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../services/goals/goals.service */ "./src/app/modules/goals/services/goals/goals.service.ts");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var src_app_shared_services_workout_workout_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/services/workout/workout.service */ "./src/app/shared/services/workout/workout.service.ts");






var CreateGoalComponent = /** @class */ (function () {
    function CreateGoalComponent(_formBuilder, _goalsService, _router, _workoutService) {
        var _this = this;
        this._formBuilder = _formBuilder;
        this._goalsService = _goalsService;
        this._router = _router;
        this._workoutService = _workoutService;
        this.goalType = '0';
        this.isWorkoutsLoaded = false;
        this.typeNotValid = true;
        this._workoutService.getUserUnusedWorkouts().subscribe(function (workouts) {
            _this.workouts = workouts;
            _this.isWorkoutsLoaded = true;
        });
    }
    CreateGoalComponent.prototype.ngOnInit = function () {
        this.goalNameForm = this._formBuilder.group({
            goalNameControl: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required]
        });
        // this.goalTypeForm = this._formBuilder.group({
        //   goalTypeControl: [this.goalType, Validators.required]
        // });
    };
    // temp(stuff) {
    //   console.log('stuff', stuff);
    // }
    CreateGoalComponent.prototype.isTypeDisabled = function () {
        if (this.goalType === '1') {
            this.workoutId = null;
            return false;
        }
        if (this.goalType === '2' && this.workoutId) {
            return false;
        }
        return true;
    };
    CreateGoalComponent.prototype.submit = function () {
        var _this = this;
        if (this.goalType === '1') {
            this._goalsService.createUserGoal('1', this.goalNameForm.value.goalNameControl)
                .subscribe(function (anything) { return _this._router.navigate(['/goals']); });
        }
        if (this.goalType === '2') {
            this._goalsService.createUserGoal('2', this.goalNameForm.value.goalNameControl, this.workoutId)
                .subscribe(function (anything) { return _this._router.navigate(['/goals']); });
        }
    };
    CreateGoalComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_2__["Component"])({
            selector: 'app-create-goal',
            template: __webpack_require__(/*! ./create-goal.component.html */ "./src/app/modules/goals/components/goals/create-goal/create-goal.component.html"),
            styles: [__webpack_require__(/*! ./create-goal.component.css */ "./src/app/modules/goals/components/goals/create-goal/create-goal.component.css")]
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["FormBuilder"], _services_goals_goals_service__WEBPACK_IMPORTED_MODULE_1__["GoalsService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_4__["Router"], src_app_shared_services_workout_workout_service__WEBPACK_IMPORTED_MODULE_5__["WorkoutService"]])
    ], CreateGoalComponent);
    return CreateGoalComponent;
}());



/***/ }),

/***/ "./src/app/modules/goals/components/goals/goal-details/goal-details.component.css":
/*!****************************************************************************************!*\
  !*** ./src/app/modules/goals/components/goals/goal-details/goal-details.component.css ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL21vZHVsZXMvZ29hbHMvY29tcG9uZW50cy9nb2Fscy9nb2FsLWRldGFpbHMvZ29hbC1kZXRhaWxzLmNvbXBvbmVudC5jc3MifQ== */"

/***/ }),

/***/ "./src/app/modules/goals/components/goals/goal-details/goal-details.component.html":
/*!*****************************************************************************************!*\
  !*** ./src/app/modules/goals/components/goals/goal-details/goal-details.component.html ***!
  \*****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div *ngIf=\"isGoalLoaded && userGoal\" class=\"container\">\r\n  <mat-card class=\"example-card\">\r\n    <mat-card-header>\r\n      <mat-card-title> {{userGoal.name}}\r\n      </mat-card-title>\r\n      <mat-card-subtitle>Siekio tipas</mat-card-subtitle>\r\n    </mat-card-header>\r\n    <mat-card-content>\r\n      <p>\r\n        Siekio aprašymas\r\n        <br>\r\n        {{userGoal.createdAt | date: 'yyyy-MM-dd'}}\r\n        <br>\r\n        <button mat-raised-button color=\"warn\" (click)=\"removeGoal()\">Pašalinti siekį</button>\r\n      </p>\r\n    </mat-card-content>\r\n    <mat-card-actions>\r\n      <button mat-raised-button color=\"primary\" [routerLink]=\"['/goals']\">Visi siekiai</button>\r\n      <button mat-raised-button color=\"primary\" [routerLink]=\"['/goals/today']\">Šiandienos siekiai</button>\r\n    </mat-card-actions>\r\n  </mat-card>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/modules/goals/components/goals/goal-details/goal-details.component.ts":
/*!***************************************************************************************!*\
  !*** ./src/app/modules/goals/components/goals/goal-details/goal-details.component.ts ***!
  \***************************************************************************************/
/*! exports provided: GoalDetailsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GoalDetailsComponent", function() { return GoalDetailsComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _services_goals_goals_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../services/goals/goals.service */ "./src/app/modules/goals/services/goals/goals.service.ts");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _models_goal_model__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../../models/goal.model */ "./src/app/modules/goals/models/goal.model.ts");





var GoalDetailsComponent = /** @class */ (function () {
    function GoalDetailsComponent(_activatedRoute, _goalsService, _router) {
        var _this = this;
        this._activatedRoute = _activatedRoute;
        this._goalsService = _goalsService;
        this._router = _router;
        this.userGoal = new _models_goal_model__WEBPACK_IMPORTED_MODULE_4__["Goal"]({});
        this.isGoalLoaded = false;
        this._activatedRoute.params.subscribe(function (params) {
            _this.id = params['id'];
            _this._goalsService.getUserGoal(_this.id).subscribe(function (userGoal) {
                _this.userGoal = userGoal;
                _this.isGoalLoaded = true;
            });
        });
    }
    GoalDetailsComponent.prototype.ngOnInit = function () {
    };
    GoalDetailsComponent.prototype.removeGoal = function () {
        var _this = this;
        this._goalsService.deleteUserGoal(this.id).subscribe(function () {
            _this._router.navigate(['/goals']);
        });
    };
    GoalDetailsComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_2__["Component"])({
            selector: 'app-goal-details',
            template: __webpack_require__(/*! ./goal-details.component.html */ "./src/app/modules/goals/components/goals/goal-details/goal-details.component.html"),
            styles: [__webpack_require__(/*! ./goal-details.component.css */ "./src/app/modules/goals/components/goals/goal-details/goal-details.component.css")]
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_3__["ActivatedRoute"], _services_goals_goals_service__WEBPACK_IMPORTED_MODULE_1__["GoalsService"], _angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"]])
    ], GoalDetailsComponent);
    return GoalDetailsComponent;
}());



/***/ }),

/***/ "./src/app/modules/goals/components/goals/goals.component.css":
/*!********************************************************************!*\
  !*** ./src/app/modules/goals/components/goals/goals.component.css ***!
  \********************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "table {\r\n  width: 100%;\r\n}\r\n\r\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvbW9kdWxlcy9nb2Fscy9jb21wb25lbnRzL2dvYWxzL2dvYWxzLmNvbXBvbmVudC5jc3MiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBQUE7RUFDRSxXQUFXO0FBQ2IiLCJmaWxlIjoic3JjL2FwcC9tb2R1bGVzL2dvYWxzL2NvbXBvbmVudHMvZ29hbHMvZ29hbHMuY29tcG9uZW50LmNzcyIsInNvdXJjZXNDb250ZW50IjpbInRhYmxlIHtcclxuICB3aWR0aDogMTAwJTtcclxufVxyXG4iXX0= */"

/***/ }),

/***/ "./src/app/modules/goals/components/goals/goals.component.html":
/*!*********************************************************************!*\
  !*** ./src/app/modules/goals/components/goals/goals.component.html ***!
  \*********************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"container\">\r\n  <div class=\"row\">\r\n    <div class=\"col-sm-10\">\r\n      <h5>Mano siekiai</h5>\r\n    </div>\r\n    <div class=\"col-sm-2\">\r\n      <button mat-raised-button color=\"primary\" [routerLink]=\"['/goals/creategoal']\">create</button>\r\n    </div>\r\n  </div>\r\n  <hr>\r\n</div>\r\n\r\n<div *ngIf=\"isLoaded\" class=\"container\">\r\n  <table mat-table [dataSource]=\"dataSource\" class=\"mat-elevation-z8\">\r\n\r\n    <ng-container *ngFor=\"let goalElement of goals\">\r\n      <!-- HEADER -->\r\n      <ng-container *ngIf=\"goalElement.goal.name=='Data'\" matColumnDef=\"{{goalElement.goal.name}}\" sticky>\r\n        <th mat-header-cell *matHeaderCellDef> {{goalElement.goal.name}} </th>\r\n        <td mat-cell *matCellDef=\"let element\"> {{element[goalElement.goal.name].createdAt | date: 'yyyy-MM-dd'}} </td>\r\n      </ng-container>\r\n      <!-- ROWS -->\r\n      <ng-container *ngIf=\"goalElement.goal.name!='Data'\" matColumnDef=\"{{goalElement.goal.name}}\">\r\n        <th mat-header-cell *matHeaderCellDef>\r\n          <button mat-raised-button color=\"primary\" [routerLink]=\"['/goals', goalElement.goal.id]\">\r\n            {{goalElement.goal.name}} <mat-icon>create</mat-icon>\r\n          </button>\r\n        </th>\r\n        <td mat-cell *matCellDef=\"let element\">\r\n          <!-- REAL DATA -->\r\n          <ng-container *ngIf=\"!element[goalElement.goal.name].isDummy\">\r\n            <mat-checkbox [(ngModel)]=\"element[goalElement.goal.name].isDone\" [disabled]=true></mat-checkbox>\r\n          </ng-container>\r\n          <!-- DUMMY DATA -->\r\n          <ng-container *ngIf=\"element[goalElement.goal.name].isDummy\">\r\n            --\r\n          </ng-container>\r\n        </td>\r\n      </ng-container>\r\n    </ng-container>\r\n\r\n    <tr mat-header-row *matHeaderRowDef=\"displayedColumns\"></tr>\r\n    <tr mat-row *matRowDef=\"let row; columns: displayedColumns;\"></tr>\r\n  </table>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/modules/goals/components/goals/goals.component.ts":
/*!*******************************************************************!*\
  !*** ./src/app/modules/goals/components/goals/goals.component.ts ***!
  \*******************************************************************/
/*! exports provided: GoalsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GoalsComponent", function() { return GoalsComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _services_goals_goals_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../services/goals/goals.service */ "./src/app/modules/goals/services/goals/goals.service.ts");
/* harmony import */ var _models_goal_with_progress_model__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../models/goal-with-progress.model */ "./src/app/modules/goals/models/goal-with-progress.model.ts");
/* harmony import */ var _models_goal_model__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../models/goal.model */ "./src/app/modules/goals/models/goal.model.ts");
/* harmony import */ var src_app_shared_services_message_snackbar_snackbar_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/services/message-snackbar/snackbar.service */ "./src/app/shared/services/message-snackbar/snackbar.service.ts");






var GoalsComponent = /** @class */ (function () {
    function GoalsComponent(_goalService, _snackbarService) {
        var _this = this;
        this._goalService = _goalService;
        this._snackbarService = _snackbarService;
        this.displayedColumns = [];
        this.dataSource = [];
        this.isLoaded = false;
        this.limit = 10;
        var currentDate = new Date();
        this._goalService.GetUserGoalsWithProgress(currentDate, 10).subscribe(function (goals) {
            if (goals.length > 0) {
                _this.mapGoalsToTableDataSource(goals);
                _this.isLoaded = true;
            }
            else {
                _this._snackbarService.openSnackBar('Nėra siekių.');
            }
        });
    }
    GoalsComponent.prototype.mapGoalsToTableDataSource = function (goals) {
        var _this = this;
        var _a, _b;
        this.goals = goals;
        this.displayedColumns.push('Data');
        goals.forEach(function (element) {
            _this.displayedColumns.push(element.goal.name);
        });
        for (var i = 0; i < this.limit; i++) {
            var tableGoalObject = void 0;
            for (var k = 0; k < goals.length; k++) {
                var objectToMerge = void 0;
                if (k === 0) {
                    objectToMerge = (_a = {
                            Data: {
                                createdAt: goals[k].goalProgressCollection[i].createdAt
                            }
                        },
                        _a[goals[k].goal.name] = goals[k].goalProgressCollection[i],
                        _a);
                }
                else {
                    objectToMerge = (_b = {},
                        _b[goals[k].goal.name] = goals[k].goalProgressCollection[i],
                        _b);
                }
                tableGoalObject = tslib__WEBPACK_IMPORTED_MODULE_0__["__assign"]({}, tableGoalObject, objectToMerge);
            }
            this.dataSource.push(tableGoalObject);
        }
        goals.push(new _models_goal_with_progress_model__WEBPACK_IMPORTED_MODULE_3__["GoalWithProgressModel"]({
            goal: new _models_goal_model__WEBPACK_IMPORTED_MODULE_4__["Goal"]({
                name: 'Data'
            })
        }));
    };
    GoalsComponent.prototype.ngOnInit = function () {
    };
    GoalsComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
            selector: 'app-goals',
            template: __webpack_require__(/*! ./goals.component.html */ "./src/app/modules/goals/components/goals/goals.component.html"),
            styles: [__webpack_require__(/*! ./goals.component.css */ "./src/app/modules/goals/components/goals/goals.component.css")]
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_services_goals_goals_service__WEBPACK_IMPORTED_MODULE_2__["GoalsService"], src_app_shared_services_message_snackbar_snackbar_service__WEBPACK_IMPORTED_MODULE_5__["SnackbarService"]])
    ], GoalsComponent);
    return GoalsComponent;
}());



/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/create-group-goal/create-group-goal.component.css":
/*!********************************************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/create-group-goal/create-group-goal.component.css ***!
  \********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL21vZHVsZXMvZ29hbHMvY29tcG9uZW50cy9ncm91cC1nb2Fscy9jcmVhdGUtZ3JvdXAtZ29hbC9jcmVhdGUtZ3JvdXAtZ29hbC5jb21wb25lbnQuY3NzIn0= */"

/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/create-group-goal/create-group-goal.component.html":
/*!*********************************************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/create-group-goal/create-group-goal.component.html ***!
  \*********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"container\">\r\n\r\n  <button mat-raised-button color=\"primary\" [routerLink]=\"['/goals']\">Siekiai</button>\r\n  <button mat-raised-button color=\"primary\" [routerLink]=\"['/goals/group']\">Grupės siekiai</button>\r\n  <br>\r\n  <mat-vertical-stepper [linear]=\"true\" #stepper>\r\n    <mat-step [stepControl]=\"goalNameForm\">\r\n      <form [formGroup]=\"goalNameForm\">\r\n        <ng-template matStepLabel>Siekio pavadinimas</ng-template>\r\n        <mat-form-field>\r\n          <input matInput placeholder=\"Siekio pavadinimas\" formControlName=\"goalNameControl\" required>\r\n        </mat-form-field>\r\n        <div>\r\n          <button mat-raised-button color=\"primary\" matStepperNext>sekantis žingsnis</button>\r\n        </div>\r\n      </form>\r\n    </mat-step>\r\n\r\n    <mat-step>\r\n      <ng-template matStepLabel>Siekio tipas</ng-template>\r\n      <mat-radio-group aria-label=\"Pasirinkite tipą\" [(ngModel)]=\"goalType\">\r\n        <mat-radio-button value=\"1\">Standartinis</mat-radio-button>\r\n        <mat-radio-button [disabled]=\"workouts.length==0\" value=\"2\">Treniruotės</mat-radio-button>\r\n      </mat-radio-group>\r\n      <ng-container *ngIf=\"goalType=='2'\">\r\n        <mat-radio-group aria-label=\"Pasirinkite treniruotę\" [(ngModel)]=\"workoutId\">\r\n          <ng-container *ngFor=\"let workoutObject of workouts; let i=index\">\r\n            <mat-card>\r\n              Treniruotė {{i+1}} -\r\n              <mat-radio-button value=\"{{workoutObject.id}}\">{{workoutObject.name}} -\r\n                {{workoutObject.id}}\r\n              </mat-radio-button>\r\n            </mat-card>\r\n          </ng-container>\r\n        </mat-radio-group>\r\n      </ng-container>\r\n\r\n      <div>\r\n        <button mat-raised-button [disabled]=\"isTypeDisabled()\" color=\"primary\" matStepperNext>sekantis\r\n          žingsnis</button>\r\n      </div>\r\n    </mat-step>\r\n\r\n\r\n\r\n    <mat-step>\r\n      <ng-template matStepLabel>Siekio pasikartojimas</ng-template>\r\n\r\n      <div>\r\n        <button mat-raised-button color=\"primary\" matStepperPrevious>Praeitas žingsnis</button>\r\n        <button mat-raised-button color=\"primary\" (click)=\"submit()\">Kurti</button>\r\n      </div>\r\n    </mat-step>\r\n  </mat-vertical-stepper>\r\n\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/create-group-goal/create-group-goal.component.ts":
/*!*******************************************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/create-group-goal/create-group-goal.component.ts ***!
  \*******************************************************************************************************/
/*! exports provided: CreateGroupGoalComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "CreateGroupGoalComponent", function() { return CreateGroupGoalComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _services_group_group_goal_group_goal_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../../services/group/group-goal/group-goal.service */ "./src/app/modules/goals/services/group/group-goal/group-goal.service.ts");
/* harmony import */ var src_app_shared_services_workout_workout_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/shared/services/workout/workout.service */ "./src/app/shared/services/workout/workout.service.ts");






var CreateGroupGoalComponent = /** @class */ (function () {
    function CreateGroupGoalComponent(_formBuilder, _groupGoalService, _workoutService, _router) {
        var _this = this;
        this._formBuilder = _formBuilder;
        this._groupGoalService = _groupGoalService;
        this._workoutService = _workoutService;
        this._router = _router;
        this.goalType = '0';
        this.isWorkoutsLoaded = false;
        this._workoutService.getGroupUnusedWorkouts().subscribe(function (workouts) {
            _this.workouts = workouts;
            _this.isWorkoutsLoaded = true;
        });
    }
    CreateGroupGoalComponent.prototype.ngOnInit = function () {
        this.goalNameForm = this._formBuilder.group({
            goalNameControl: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]
        });
    };
    CreateGroupGoalComponent.prototype.submit = function () {
        var _this = this;
        if (this.goalType === '1') {
            this._groupGoalService.createGroupGoal('1', this.goalNameForm.value.goalNameControl)
                .subscribe(function (anything) { return _this._router.navigate(['/goals/group']); });
        }
        if (this.goalType === '2') {
            this._groupGoalService.createGroupGoal('2', this.goalNameForm.value.goalNameControl, this.workoutId)
                .subscribe(function (anything) { return _this._router.navigate(['/goals/group']); });
        }
    };
    CreateGroupGoalComponent.prototype.isTypeDisabled = function () {
        if (this.goalType === '1') {
            this.workoutId = null;
            return false;
        }
        if (this.goalType === '2' && this.workoutId) {
            return false;
        }
        return true;
    };
    CreateGroupGoalComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
            selector: 'app-create-group-goal',
            template: __webpack_require__(/*! ./create-group-goal.component.html */ "./src/app/modules/goals/components/group-goals/create-group-goal/create-group-goal.component.html"),
            styles: [__webpack_require__(/*! ./create-group-goal.component.css */ "./src/app/modules/goals/components/group-goals/create-group-goal/create-group-goal.component.css")]
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormBuilder"], _services_group_group_goal_group_goal_service__WEBPACK_IMPORTED_MODULE_4__["GroupGoalService"],
            src_app_shared_services_workout_workout_service__WEBPACK_IMPORTED_MODULE_5__["WorkoutService"], _angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"]])
    ], CreateGroupGoalComponent);
    return CreateGroupGoalComponent;
}());



/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/create-group/create-group.component.css":
/*!**********************************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/create-group/create-group.component.css ***!
  \**********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL21vZHVsZXMvZ29hbHMvY29tcG9uZW50cy9ncm91cC1nb2Fscy9jcmVhdGUtZ3JvdXAvY3JlYXRlLWdyb3VwLmNvbXBvbmVudC5jc3MifQ== */"

/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/create-group/create-group.component.html":
/*!***********************************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/create-group/create-group.component.html ***!
  \***********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"container\">\r\n  <button mat-raised-button color=\"primary\" [routerLink]=\"['/goals/group']\">Grupės siekiai</button>\r\n  <br>\r\n  <mat-vertical-stepper [linear]=\"true\" #stepper>\r\n    <mat-step [stepControl]=\"groupNameForm\">\r\n      <form [formGroup]=\"groupNameForm\">\r\n        <ng-template matStepLabel>Grupės pavadinimas</ng-template>\r\n        <mat-form-field>\r\n          <input matInput placeholder=\"grupės pavadinimas\" formControlName=\"groupNameControl\" required>\r\n        </mat-form-field>\r\n        <div>\r\n          <button mat-raised-button color=\"primary\" matStepperNext>sekantis žingsnis</button>\r\n        </div>\r\n      </form>\r\n    </mat-step>\r\n    <mat-step>\r\n      <ng-template matStepLabel>Kitas žingsnis</ng-template>\r\n\r\n      <div>\r\n        <button mat-raised-button color=\"primary\" matStepperPrevious >Praeitas žingsnis</button>\r\n        <button mat-raised-button color=\"primary\" (click)=\"submit()\">Kurti</button>\r\n      </div>\r\n    </mat-step>\r\n  </mat-vertical-stepper>\r\n\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/create-group/create-group.component.ts":
/*!*********************************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/create-group/create-group.component.ts ***!
  \*********************************************************************************************/
/*! exports provided: CreateGroupComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "CreateGroupComponent", function() { return CreateGroupComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _services_group_group_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../services/group/group.service */ "./src/app/modules/goals/services/group/group.service.ts");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");





var CreateGroupComponent = /** @class */ (function () {
    function CreateGroupComponent(_formBuilder, _groupService, _router) {
        this._formBuilder = _formBuilder;
        this._groupService = _groupService;
        this._router = _router;
    }
    CreateGroupComponent.prototype.ngOnInit = function () {
        this.groupNameForm = this._formBuilder.group({
            groupNameControl: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_3__["Validators"].required]
        });
    };
    CreateGroupComponent.prototype.submit = function () {
        var _this = this;
        this._groupService.createGroup(this.groupNameForm.value.groupNameControl)
            .subscribe(function (anything) { return _this._router.navigate(['/goals/group']); });
    };
    CreateGroupComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_2__["Component"])({
            selector: 'app-create-group',
            template: __webpack_require__(/*! ./create-group.component.html */ "./src/app/modules/goals/components/group-goals/create-group/create-group.component.html"),
            styles: [__webpack_require__(/*! ./create-group.component.css */ "./src/app/modules/goals/components/group-goals/create-group/create-group.component.css")]
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_3__["FormBuilder"], _services_group_group_service__WEBPACK_IMPORTED_MODULE_1__["GroupService"], _angular_router__WEBPACK_IMPORTED_MODULE_4__["Router"]])
    ], CreateGroupComponent);
    return CreateGroupComponent;
}());



/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/group-goal-details/group-goal-details.component.css":
/*!**********************************************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/group-goal-details/group-goal-details.component.css ***!
  \**********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL21vZHVsZXMvZ29hbHMvY29tcG9uZW50cy9ncm91cC1nb2Fscy9ncm91cC1nb2FsLWRldGFpbHMvZ3JvdXAtZ29hbC1kZXRhaWxzLmNvbXBvbmVudC5jc3MifQ== */"

/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/group-goal-details/group-goal-details.component.html":
/*!***********************************************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/group-goal-details/group-goal-details.component.html ***!
  \***********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div *ngIf=\"isGoalLoaded && groupGoal\" class=\"container\">\r\n    <mat-card class=\"example-card\">\r\n      <mat-card-header>\r\n        <mat-card-title> {{groupGoal.name}}\r\n        </mat-card-title>\r\n        <mat-card-subtitle>Siekio tipas</mat-card-subtitle>\r\n      </mat-card-header>\r\n      <mat-card-content>\r\n        <p>\r\n          Siekio aprašymas\r\n          <br>\r\n          {{groupGoal.createdAt | date: 'yyyy-MM-dd'}}\r\n          <br>\r\n          <button *ngIf=\"isCurrentUserGroupLeader()\" mat-raised-button color=\"warn\" (click)=\"removeGoal()\">Pašalinti siekį</button>\r\n        </p>\r\n      </mat-card-content>\r\n      <mat-card-actions>\r\n        <button mat-raised-button color=\"primary\" [routerLink]=\"['/goals/group']\">Grupės siekiai</button>\r\n      </mat-card-actions>\r\n    </mat-card>\r\n  </div>\r\n"

/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/group-goal-details/group-goal-details.component.ts":
/*!*********************************************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/group-goal-details/group-goal-details.component.ts ***!
  \*********************************************************************************************************/
/*! exports provided: GroupGoalDetailsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GroupGoalDetailsComponent", function() { return GroupGoalDetailsComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var src_app_shared_services_user_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! src/app/shared/services/user.service */ "./src/app/shared/services/user.service.ts");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _services_group_group_goal_group_goal_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../../services/group/group-goal/group-goal.service */ "./src/app/modules/goals/services/group/group-goal/group-goal.service.ts");





var GroupGoalDetailsComponent = /** @class */ (function () {
    function GroupGoalDetailsComponent(_activatedRoute, _groupGoalService, _router, _userService) {
        var _this = this;
        this._activatedRoute = _activatedRoute;
        this._groupGoalService = _groupGoalService;
        this._router = _router;
        this._userService = _userService;
        this.isGoalLoaded = false;
        this._activatedRoute.params.subscribe(function (params) {
            _this.id = params['id'];
            _this._groupGoalService.getGroupGoal(_this.id).subscribe(function (groupGoal) {
                console.log('group goal', groupGoal);
                _this.groupGoal = groupGoal;
                _this.isGoalLoaded = true;
            });
        });
    }
    GroupGoalDetailsComponent.prototype.ngOnInit = function () {
    };
    GroupGoalDetailsComponent.prototype.removeGoal = function () {
        var _this = this;
        this._groupGoalService.deleteGroupGaol(this.id).subscribe(function () {
            _this._router.navigate(['/goals/group']);
        });
    };
    GroupGoalDetailsComponent.prototype.isCurrentUserGroupLeader = function () {
        if (this._userService.getCurrentUsername() === this.groupGoal.group.leaderUsername) {
            return true;
        }
        else {
            return false;
        }
    };
    GroupGoalDetailsComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_2__["Component"])({
            selector: 'app-group-goal-details',
            template: __webpack_require__(/*! ./group-goal-details.component.html */ "./src/app/modules/goals/components/group-goals/group-goal-details/group-goal-details.component.html"),
            styles: [__webpack_require__(/*! ./group-goal-details.component.css */ "./src/app/modules/goals/components/group-goals/group-goal-details/group-goal-details.component.css")]
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_3__["ActivatedRoute"],
            _services_group_group_goal_group_goal_service__WEBPACK_IMPORTED_MODULE_4__["GroupGoalService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"],
            src_app_shared_services_user_service__WEBPACK_IMPORTED_MODULE_1__["UserService"]])
    ], GroupGoalDetailsComponent);
    return GroupGoalDetailsComponent;
}());



/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/group-goals.component.css":
/*!********************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/group-goals.component.css ***!
  \********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "table {\r\n  width: 100%;\r\n}\r\n\r\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvbW9kdWxlcy9nb2Fscy9jb21wb25lbnRzL2dyb3VwLWdvYWxzL2dyb3VwLWdvYWxzLmNvbXBvbmVudC5jc3MiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBQUE7RUFDRSxXQUFXO0FBQ2IiLCJmaWxlIjoic3JjL2FwcC9tb2R1bGVzL2dvYWxzL2NvbXBvbmVudHMvZ3JvdXAtZ29hbHMvZ3JvdXAtZ29hbHMuY29tcG9uZW50LmNzcyIsInNvdXJjZXNDb250ZW50IjpbInRhYmxlIHtcclxuICB3aWR0aDogMTAwJTtcclxufVxyXG4iXX0= */"

/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/group-goals.component.html":
/*!*********************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/group-goals.component.html ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div *ngIf=\"isGroupLoaded\" class=\"container\">\r\n  <div *ngIf=\"groupObject.group\">\r\n    <h5>Grupės siekiai</h5>\r\n    <div *ngIf=\"groupObject.isLeader\">\r\n      <div class=\"row\">\r\n        <div class=\"col-sm-10\">\r\n\r\n          <button mat-raised-button color=\"warn\" (click)=\"deleteGroup()\">Pašalinti grupę</button>\r\n          <button mat-raised-button color=\"primary\" [routerLink]=\"['/goals/groupusers']\">grupės nariai</button>\r\n        </div>\r\n        <div class=\"col-sm-2\">\r\n          <button mat-raised-button color=\"primary\" [routerLink]=\"['/goals/creategroupgoal']\">Sukurti siekį</button>\r\n        </div>\r\n      </div>\r\n\r\n    </div>\r\n    <div *ngIf=\"!groupObject.isLeader\">\r\n      <button mat-raised-button color=\"warn\" (click)=\"leaveGroup()\">Palikti grupę</button>\r\n    </div>\r\n  </div>\r\n  <hr>\r\n  <!-- ----------------------------------------------------------------- -->\r\n  <ng-container *ngIf=\"groupObject.group\">\r\n    <table mat-table [dataSource]=\"groupProgressObject\" class=\"mat-elevation-z8\">\r\n\r\n      <ng-container matColumnDef=\"goalDetails\">\r\n        <th mat-header-cell *matHeaderCellDef> </th>\r\n        <td mat-cell *matCellDef=\"let element\">\r\n          <button mat-raised-button color=\"primary\" [routerLink]=\"['/goals/group', element.goal.id]\">\r\n            Siekio aprašymas</button>\r\n        </td>\r\n      </ng-container>\r\n\r\n      <!-- Position Column -->\r\n      <ng-container matColumnDef=\"goal\">\r\n        <th mat-header-cell *matHeaderCellDef> Siekis </th>\r\n        <td mat-cell *matCellDef=\"let element\"> {{element.goal.name}} </td>\r\n      </ng-container>\r\n\r\n      <!-- Name Column -->\r\n      <ng-container matColumnDef=\"userGoalProgresses\">\r\n        <th mat-header-cell *matHeaderCellDef> Narių progresas </th>\r\n        <td mat-cell *matCellDef=\"let element\">\r\n          <ng-container *ngFor=\"let progress of element.userGoalProgresses\">\r\n            <a [routerLink]=\"['/dashboard/profile/others', progress.userDescription.id]\">{{progress.user.username}} </a>\r\n\r\n            <mat-checkbox [(ngModel)]=\"progress.isDone\" [disabled]=true></mat-checkbox>\r\n          </ng-container>\r\n\r\n        </td>\r\n      </ng-container>\r\n\r\n      <tr mat-header-row *matHeaderRowDef=\"displayedColumns\"></tr>\r\n      <tr mat-row *matRowDef=\"let row; columns: displayedColumns;\"></tr>\r\n    </table>\r\n  </ng-container>\r\n\r\n  <!-- ----------------------------------------------------------------- -->\r\n  <div *ngIf=\"!groupObject.group\">\r\n    <button mat-raised-button color=\"primary\" [routerLink]=\"['/goals/creategroup']\">Grupės sukūrimas</button>\r\n    <app-group-invitation></app-group-invitation>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/group-goals.component.ts":
/*!*******************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/group-goals.component.ts ***!
  \*******************************************************************************/
/*! exports provided: GroupGoalsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GroupGoalsComponent", function() { return GroupGoalsComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _services_group_group_members_group_members_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./../../services/group/group-members/group-members.service */ "./src/app/modules/goals/services/group/group-members/group-members.service.ts");
/* harmony import */ var _services_group_group_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../services/group/group.service */ "./src/app/modules/goals/services/group/group.service.ts");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _services_group_group_goal_progress_group_goal_progress_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../services/group/group-goal-progress/group-goal-progress.service */ "./src/app/modules/goals/services/group/group-goal-progress/group-goal-progress.service.ts");





var GroupGoalsComponent = /** @class */ (function () {
    function GroupGoalsComponent(_groupService, _groupMembersService, _groupGoalProgressService) {
        this._groupService = _groupService;
        this._groupMembersService = _groupMembersService;
        this._groupGoalProgressService = _groupGoalProgressService;
        this.isGroupLoaded = false;
        this.displayedColumns = ['goalDetails', 'goal', 'userGoalProgresses'];
        this.setGroupData();
    }
    GroupGoalsComponent.prototype.setGroupData = function () {
        var _this = this;
        this._groupService.getUserGroup().subscribe(function (groupObject) {
            _this.groupObject = groupObject;
            _this.isGroupLoaded = true;
            if (_this.groupObject.group) {
                _this.setGroupProgress();
            }
        });
    };
    GroupGoalsComponent.prototype.setGroupProgress = function () {
        var _this = this;
        var currentDate = new Date();
        this._groupGoalProgressService.getSpecificGroupGoalsDayProgress(currentDate).subscribe(function (progress) {
            _this.groupProgressObject = progress;
            console.log('progress', _this.groupProgressObject);
        });
    };
    GroupGoalsComponent.prototype.ngOnInit = function () {
    };
    GroupGoalsComponent.prototype.deleteGroup = function () {
        this._groupService.deleteGroup().subscribe(function (response) {
            location.reload();
        });
    };
    GroupGoalsComponent.prototype.leaveGroup = function () {
        this._groupMembersService.leaveGroup().subscribe(function (response) {
            location.reload();
        });
    };
    GroupGoalsComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_3__["Component"])({
            selector: 'app-group-goals',
            template: __webpack_require__(/*! ./group-goals.component.html */ "./src/app/modules/goals/components/group-goals/group-goals.component.html"),
            styles: [__webpack_require__(/*! ./group-goals.component.css */ "./src/app/modules/goals/components/group-goals/group-goals.component.css")]
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_services_group_group_service__WEBPACK_IMPORTED_MODULE_2__["GroupService"],
            _services_group_group_members_group_members_service__WEBPACK_IMPORTED_MODULE_1__["GroupMembersService"],
            _services_group_group_goal_progress_group_goal_progress_service__WEBPACK_IMPORTED_MODULE_4__["GroupGoalProgressService"]])
    ], GroupGoalsComponent);
    return GroupGoalsComponent;
}());



/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/group-members/group-invitation/group-invitation.component.css":
/*!********************************************************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/group-members/group-invitation/group-invitation.component.css ***!
  \********************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL21vZHVsZXMvZ29hbHMvY29tcG9uZW50cy9ncm91cC1nb2Fscy9ncm91cC1tZW1iZXJzL2dyb3VwLWludml0YXRpb24vZ3JvdXAtaW52aXRhdGlvbi5jb21wb25lbnQuY3NzIn0= */"

/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/group-members/group-invitation/group-invitation.component.html":
/*!*********************************************************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/group-members/group-invitation/group-invitation.component.html ***!
  \*********************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div *ngIf=\"isInvitationsLoaded\">\r\n  <mat-list role=\"list\">\r\n    <mat-list-item *ngFor=\"let invitation of invitationsObject\" role=\"listitem\">\r\n      {{invitation.leaderUsername}} kviečia į grupę.\r\n      <button mat-button (click)=\"acceptInvitation(invitation.leaderUsername)\">Priimti</button>\r\n      <button mat-button (click)=\"cancelInvitation(invitation)\">Atšaukti</button>\r\n    </mat-list-item>\r\n  </mat-list>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/group-members/group-invitation/group-invitation.component.ts":
/*!*******************************************************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/group-members/group-invitation/group-invitation.component.ts ***!
  \*******************************************************************************************************************/
/*! exports provided: GroupInvitationComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GroupInvitationComponent", function() { return GroupInvitationComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _services_group_group_invitation_group_invitation_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../../services/group/group-invitation/group-invitation.service */ "./src/app/modules/goals/services/group/group-invitation/group-invitation.service.ts");



var GroupInvitationComponent = /** @class */ (function () {
    function GroupInvitationComponent(_groupInvitationService) {
        var _this = this;
        this._groupInvitationService = _groupInvitationService;
        this.isInvitationsLoaded = false;
        this._groupInvitationService.getUserGroupInvitations().subscribe(function (invitations) {
            _this.invitationsObject = invitations;
            _this.isInvitationsLoaded = true;
            console.log(_this.invitationsObject);
        });
    }
    GroupInvitationComponent.prototype.ngOnInit = function () {
    };
    GroupInvitationComponent.prototype.acceptInvitation = function (leaderUsername) {
        this._groupInvitationService.acceptInvitation(leaderUsername).subscribe(function (response) {
            location.reload();
        });
    };
    GroupInvitationComponent.prototype.cancelInvitation = function (invitation) {
        var _this = this;
        console.log('cancel invitation', invitation);
        this._groupInvitationService.cancelInvitation(invitation.id).subscribe(function (response) {
            var newList = [];
            _this.invitationsObject.forEach(function (currentInvitation) {
                if (currentInvitation.id !== invitation.id) {
                    newList.push(currentInvitation);
                }
            });
            _this.invitationsObject = newList;
        });
    };
    GroupInvitationComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
            selector: 'app-group-invitation',
            template: __webpack_require__(/*! ./group-invitation.component.html */ "./src/app/modules/goals/components/group-goals/group-members/group-invitation/group-invitation.component.html"),
            styles: [__webpack_require__(/*! ./group-invitation.component.css */ "./src/app/modules/goals/components/group-goals/group-members/group-invitation/group-invitation.component.css")]
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_services_group_group_invitation_group_invitation_service__WEBPACK_IMPORTED_MODULE_2__["GroupInvitationService"]])
    ], GroupInvitationComponent);
    return GroupInvitationComponent;
}());



/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/group-members/group-users/group-users.component.css":
/*!**********************************************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/group-members/group-users/group-users.component.css ***!
  \**********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL21vZHVsZXMvZ29hbHMvY29tcG9uZW50cy9ncm91cC1nb2Fscy9ncm91cC1tZW1iZXJzL2dyb3VwLXVzZXJzL2dyb3VwLXVzZXJzLmNvbXBvbmVudC5jc3MifQ== */"

/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/group-members/group-users/group-users.component.html":
/*!***********************************************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/group-members/group-users/group-users.component.html ***!
  \***********************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"isMembersLoaded\">\r\n  <button mat-raised-button [routerLink]=\"['/goals/group']\" color=\"primary\">grupės siekiai</button><br><br>\r\n\r\n\r\n  <form [formGroup]=\"newMemberForm\">\r\n    <ng-template>Naudotojo paštas</ng-template>\r\n    <mat-form-field>\r\n      <input matInput formControlName=\"memberUsernameControl\" required>\r\n    </mat-form-field>\r\n    <div>\r\n      <button mat-raised-button color=\"primary\" (click)=\"submit()\">Siųsti pakveitimą</button>\r\n    </div>\r\n  </form>\r\n\r\n  <mat-list role=\"list\">\r\n    <mat-list-item *ngFor=\"let member of membersObject\" role=\"listitem\">\r\n      {{member.firstName}} - {{member.lastName}}\r\n      <button mat-button (click)=\"removeGroupMember(member)\">Pašalinti</button>\r\n    </mat-list-item>\r\n  </mat-list>\r\n\r\n  TODO: GAL PADARYT, KAD RODYTU KAM ISSIUSTA\r\n\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/group-members/group-users/group-users.component.ts":
/*!*********************************************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/group-members/group-users/group-users.component.ts ***!
  \*********************************************************************************************************/
/*! exports provided: GroupUsersComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GroupUsersComponent", function() { return GroupUsersComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _services_group_group_invitation_group_invitation_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./../../../../services/group/group-invitation/group-invitation.service */ "./src/app/modules/goals/services/group/group-invitation/group-invitation.service.ts");
/* harmony import */ var _services_group_group_members_group_members_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./../../../../services/group/group-members/group-members.service */ "./src/app/modules/goals/services/group/group-members/group-members.service.ts");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");





var GroupUsersComponent = /** @class */ (function () {
    function GroupUsersComponent(_formBuilder, _groupMembersService, _groupInvitationService) {
        var _this = this;
        this._formBuilder = _formBuilder;
        this._groupMembersService = _groupMembersService;
        this._groupInvitationService = _groupInvitationService;
        this.isMembersLoaded = false;
        this._groupMembersService.getGroupMembers().subscribe(function (members) {
            _this.membersObject = members;
            _this.isMembersLoaded = true;
            console.log('members:', _this.membersObject);
        });
    }
    GroupUsersComponent.prototype.ngOnInit = function () {
        this.newMemberForm = this._formBuilder.group({
            memberUsernameControl: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_4__["Validators"].email]
        });
    };
    GroupUsersComponent.prototype.removeGroupMember = function (member) {
        var _this = this;
        this._groupMembersService.removeGroupMember(member.username).subscribe(function (response) {
            var newList = [];
            _this.membersObject.forEach(function (currentMember) {
                if (currentMember.username !== member.username) {
                    newList.push(currentMember);
                }
            });
            _this.membersObject = newList;
        });
    };
    GroupUsersComponent.prototype.submit = function () {
        this._groupInvitationService.sentGroupInvitation(this.newMemberForm.value.memberUsernameControl)
            .subscribe(function (anything) { return console.log('išsiųsta'); });
    };
    GroupUsersComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_3__["Component"])({
            selector: 'app-group-users',
            template: __webpack_require__(/*! ./group-users.component.html */ "./src/app/modules/goals/components/group-goals/group-members/group-users/group-users.component.html"),
            styles: [__webpack_require__(/*! ./group-users.component.css */ "./src/app/modules/goals/components/group-goals/group-members/group-users/group-users.component.css")]
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_4__["FormBuilder"],
            _services_group_group_members_group_members_service__WEBPACK_IMPORTED_MODULE_2__["GroupMembersService"],
            _services_group_group_invitation_group_invitation_service__WEBPACK_IMPORTED_MODULE_1__["GroupInvitationService"]])
    ], GroupUsersComponent);
    return GroupUsersComponent;
}());



/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/leaderboard/leaderboard.component.css":
/*!********************************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/leaderboard/leaderboard.component.css ***!
  \********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL21vZHVsZXMvZ29hbHMvY29tcG9uZW50cy9ncm91cC1nb2Fscy9sZWFkZXJib2FyZC9sZWFkZXJib2FyZC5jb21wb25lbnQuY3NzIn0= */"

/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/leaderboard/leaderboard.component.html":
/*!*********************************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/leaderboard/leaderboard.component.html ***!
  \*********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"container\">\r\n    <button mat-raised-button [routerLink]=\"['/goals/group']\" color=\"primary\">group goals</button>\r\n    <mat-card>vardas | taskai</mat-card>\r\n    <mat-card>vardas | taskai</mat-card>\r\n    <mat-card>vardas | taskai</mat-card>\r\n    <mat-card>vardas | taskai</mat-card>\r\n    <mat-card>vardas | taskai</mat-card>\r\n    <mat-card>vardas | taskai</mat-card>\r\n    <mat-card>vardas | taskai</mat-card>\r\n    <mat-card>vardas | taskai</mat-card>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/modules/goals/components/group-goals/leaderboard/leaderboard.component.ts":
/*!*******************************************************************************************!*\
  !*** ./src/app/modules/goals/components/group-goals/leaderboard/leaderboard.component.ts ***!
  \*******************************************************************************************/
/*! exports provided: LeaderboardComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LeaderboardComponent", function() { return LeaderboardComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");


var LeaderboardComponent = /** @class */ (function () {
    function LeaderboardComponent() {
    }
    LeaderboardComponent.prototype.ngOnInit = function () {
    };
    LeaderboardComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
            selector: 'app-leaderboard',
            template: __webpack_require__(/*! ./leaderboard.component.html */ "./src/app/modules/goals/components/group-goals/leaderboard/leaderboard.component.html"),
            styles: [__webpack_require__(/*! ./leaderboard.component.css */ "./src/app/modules/goals/components/group-goals/leaderboard/leaderboard.component.css")]
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [])
    ], LeaderboardComponent);
    return LeaderboardComponent;
}());



/***/ }),

/***/ "./src/app/modules/goals/components/today-goals/today-goals.component.css":
/*!********************************************************************************!*\
  !*** ./src/app/modules/goals/components/today-goals/today-goals.component.css ***!
  \********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "table {\r\n  width: 100%;\r\n}\r\n\r\nbutton {\r\n  margin-left:10px;\r\n}\r\n\r\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvbW9kdWxlcy9nb2Fscy9jb21wb25lbnRzL3RvZGF5LWdvYWxzL3RvZGF5LWdvYWxzLmNvbXBvbmVudC5jc3MiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBQUE7RUFDRSxXQUFXO0FBQ2I7O0FBRUE7RUFDRSxnQkFBZ0I7QUFDbEIiLCJmaWxlIjoic3JjL2FwcC9tb2R1bGVzL2dvYWxzL2NvbXBvbmVudHMvdG9kYXktZ29hbHMvdG9kYXktZ29hbHMuY29tcG9uZW50LmNzcyIsInNvdXJjZXNDb250ZW50IjpbInRhYmxlIHtcclxuICB3aWR0aDogMTAwJTtcclxufVxyXG5cclxuYnV0dG9uIHtcclxuICBtYXJnaW4tbGVmdDoxMHB4O1xyXG59XHJcbiJdfQ== */"

/***/ }),

/***/ "./src/app/modules/goals/components/today-goals/today-goals.component.html":
/*!*********************************************************************************!*\
  !*** ./src/app/modules/goals/components/today-goals/today-goals.component.html ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"container\" *ngIf=\"isGoalsLoaded\">\r\n  <h5>Mano siekiai</h5>\r\n  <table mat-table [dataSource]=\"goalsObject\" class=\"mat-elevation-z8\">\r\n    <!-- Position Column -->\r\n    <ng-container matColumnDef=\"goal\">\r\n      <th mat-header-cell *matHeaderCellDef> Siekis </th>\r\n      <td mat-cell *matCellDef=\"let element\"> {{element.goal.name}} </td>\r\n    </ng-container>\r\n    <ng-container matColumnDef=\"goalProgress\">\r\n      <th mat-header-cell *matHeaderCellDef> Progresas </th>\r\n      <td mat-cell *matCellDef=\"let element\">\r\n        <mat-checkbox (click)=\"changeGoalProgressState(element.goalProgress)\" [checked]=\"element.goalProgress.isDone\">\r\n        </mat-checkbox>\r\n        <button *ngIf=\"element.goal.workoutId > 0\" mat-button\r\n          [routerLink]=\"['/workout/session', element.goal.workoutId]\" color=\"primary\">pradėti\r\n          treniruotę</button>\r\n\r\n      </td>\r\n    </ng-container>\r\n\r\n    <tr mat-header-row *matHeaderRowDef=\"displayedColumns\"></tr>\r\n    <tr mat-row *matRowDef=\"let row; columns: displayedColumns;\"></tr>\r\n  </table>\r\n</div>\r\n\r\n<div class=\"container\" *ngIf=\"isGroupGoalsLoaded\">\r\n  <ng-container *ngIf=\"groupGoalsObject\">\r\n    <h5>Grupės siekiai</h5>\r\n    <table mat-table [dataSource]=\"groupGoalsObject\" class=\"mat-elevation-z8\">\r\n      <!-- Position Column -->\r\n      <ng-container matColumnDef=\"goal\">\r\n        <th mat-header-cell *matHeaderCellDef> Siekis </th>\r\n        <td mat-cell *matCellDef=\"let element\"> {{element.goal.name}} </td>\r\n      </ng-container>\r\n      <ng-container matColumnDef=\"goalProgress\">\r\n        <th mat-header-cell *matHeaderCellDef> Progresas </th>\r\n        <td mat-cell *matCellDef=\"let element\">\r\n          <mat-checkbox (click)=\"changeGroupProgressState(element.goalProgress)\"\r\n            [checked]=\"element.goalProgress.isDone\"></mat-checkbox>\r\n          <button *ngIf=\"element.goal.workoutId > 0\" mat-button\r\n            [routerLink]=\"['/workout/session', element.goal.workoutId]\" color=\"primary\">pradėti\r\n            treniruotę</button>\r\n        </td>\r\n      </ng-container>\r\n\r\n      <tr mat-header-row *matHeaderRowDef=\"displayedColumns\"></tr>\r\n      <tr mat-row *matRowDef=\"let row; columns: displayedColumns;\"></tr>\r\n    </table>\r\n  </ng-container>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/modules/goals/components/today-goals/today-goals.component.ts":
/*!*******************************************************************************!*\
  !*** ./src/app/modules/goals/components/today-goals/today-goals.component.ts ***!
  \*******************************************************************************/
/*! exports provided: TodayGoalsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TodayGoalsComponent", function() { return TodayGoalsComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _services_group_group_goal_progress_group_goal_progress_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./../../services/group/group-goal-progress/group-goal-progress.service */ "./src/app/modules/goals/services/group/group-goal-progress/group-goal-progress.service.ts");
/* harmony import */ var _services_goals_goal_progress_goal_progress_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../services/goals/goal-progress/goal-progress.service */ "./src/app/modules/goals/services/goals/goal-progress/goal-progress.service.ts");
/* harmony import */ var _services_goals_goals_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./../../services/goals/goals.service */ "./src/app/modules/goals/services/goals/goals.service.ts");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");





var TodayGoalsComponent = /** @class */ (function () {
    function TodayGoalsComponent(_goalsService, _goalProgressService, _groupGoalProgressService) {
        this._goalsService = _goalsService;
        this._goalProgressService = _goalProgressService;
        this._groupGoalProgressService = _groupGoalProgressService;
        this.isGroupGoalsLoaded = false;
        this.isGoalsLoaded = false;
        this.displayedColumns = ['goal', 'goalProgress'];
        this.goalsObject = [];
        this.setGoalsProgressData();
    }
    TodayGoalsComponent.prototype.setGoalsProgressData = function () {
        this.setGroupGoalsProgress();
        this.setGoalsProgress();
    };
    TodayGoalsComponent.prototype.setGoalsProgress = function () {
        var _this = this;
        this._goalsService.getUserTodayGoalWithProgress().subscribe(function (goalWithProgress) {
            _this.goalsObject = goalWithProgress;
            console.log('mygoal progress', _this.goalsObject);
            _this.isGoalsLoaded = true;
        });
    };
    TodayGoalsComponent.prototype.setGroupGoalsProgress = function () {
        var _this = this;
        this._groupGoalProgressService.getTodayUserGroupGoalsProgress().subscribe(function (goalWithProgress) {
            _this.groupGoalsObject = goalWithProgress;
            console.log('groupgoal progress', _this.groupGoalsObject);
            _this.isGroupGoalsLoaded = true;
        });
    };
    TodayGoalsComponent.prototype.ngOnInit = function () {
    };
    TodayGoalsComponent.prototype.changeGoalProgressState = function (goalProgress) {
        this._goalProgressService.updateProgressState(goalProgress).subscribe(function (isDone) {
            goalProgress.isDone = isDone;
        });
    };
    TodayGoalsComponent.prototype.changeGroupProgressState = function (GroupGoalProgress) {
        this._groupGoalProgressService.updateGroupGoalProgressState(GroupGoalProgress)
            .subscribe(function (progress) {
            GroupGoalProgress.isDone = progress.isDone;
        });
    };
    TodayGoalsComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_4__["Component"])({
            selector: 'app-today-goals',
            template: __webpack_require__(/*! ./today-goals.component.html */ "./src/app/modules/goals/components/today-goals/today-goals.component.html"),
            styles: [__webpack_require__(/*! ./today-goals.component.css */ "./src/app/modules/goals/components/today-goals/today-goals.component.css")]
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_services_goals_goals_service__WEBPACK_IMPORTED_MODULE_3__["GoalsService"],
            _services_goals_goal_progress_goal_progress_service__WEBPACK_IMPORTED_MODULE_2__["GoalProgressService"],
            _services_group_group_goal_progress_group_goal_progress_service__WEBPACK_IMPORTED_MODULE_1__["GroupGoalProgressService"]])
    ], TodayGoalsComponent);
    return TodayGoalsComponent;
}());



/***/ }),

/***/ "./src/app/modules/goals/goals-routing.module.ts":
/*!*******************************************************!*\
  !*** ./src/app/modules/goals/goals-routing.module.ts ***!
  \*******************************************************/
/*! exports provided: GoalsRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GoalsRoutingModule", function() { return GoalsRoutingModule; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _components_group_goals_group_goal_details_group_goal_details_component__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./components/group-goals/group-goal-details/group-goal-details.component */ "./src/app/modules/goals/components/group-goals/group-goal-details/group-goal-details.component.ts");
/* harmony import */ var _components_group_goals_create_group_goal_create_group_goal_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./components/group-goals/create-group-goal/create-group-goal.component */ "./src/app/modules/goals/components/group-goals/create-group-goal/create-group-goal.component.ts");
/* harmony import */ var _components_group_goals_create_group_create_group_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./components/group-goals/create-group/create-group.component */ "./src/app/modules/goals/components/group-goals/create-group/create-group.component.ts");
/* harmony import */ var _components_group_goals_leaderboard_leaderboard_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./components/group-goals/leaderboard/leaderboard.component */ "./src/app/modules/goals/components/group-goals/leaderboard/leaderboard.component.ts");
/* harmony import */ var _components_group_goals_group_goals_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./components/group-goals/group-goals.component */ "./src/app/modules/goals/components/group-goals/group-goals.component.ts");
/* harmony import */ var _components_goals_create_goal_create_goal_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./components/goals/create-goal/create-goal.component */ "./src/app/modules/goals/components/goals/create-goal/create-goal.component.ts");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _components_goals_goals_component__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./components/goals/goals.component */ "./src/app/modules/goals/components/goals/goals.component.ts");
/* harmony import */ var _components_today_goals_today_goals_component__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./components/today-goals/today-goals.component */ "./src/app/modules/goals/components/today-goals/today-goals.component.ts");
/* harmony import */ var _components_goals_goal_details_goal_details_component__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./components/goals/goal-details/goal-details.component */ "./src/app/modules/goals/components/goals/goal-details/goal-details.component.ts");
/* harmony import */ var src_app_shared_guards_logged_in_guard__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! src/app/shared/guards/logged-in.guard */ "./src/app/shared/guards/logged-in.guard.ts");
/* harmony import */ var _components_group_goals_group_members_group_users_group_users_component__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ./components/group-goals/group-members/group-users/group-users.component */ "./src/app/modules/goals/components/group-goals/group-members/group-users/group-users.component.ts");














var routes = [
    { path: '', component: _components_goals_goals_component__WEBPACK_IMPORTED_MODULE_9__["GoalsComponent"], canActivate: [src_app_shared_guards_logged_in_guard__WEBPACK_IMPORTED_MODULE_12__["LoggedInGuard"]] },
    { path: 'creategoal', component: _components_goals_create_goal_create_goal_component__WEBPACK_IMPORTED_MODULE_6__["CreateGoalComponent"], canActivate: [src_app_shared_guards_logged_in_guard__WEBPACK_IMPORTED_MODULE_12__["LoggedInGuard"]] },
    { path: 'creategroupgoal', component: _components_group_goals_create_group_goal_create_group_goal_component__WEBPACK_IMPORTED_MODULE_2__["CreateGroupGoalComponent"], canActivate: [src_app_shared_guards_logged_in_guard__WEBPACK_IMPORTED_MODULE_12__["LoggedInGuard"]] },
    { path: 'creategroup', component: _components_group_goals_create_group_create_group_component__WEBPACK_IMPORTED_MODULE_3__["CreateGroupComponent"], canActivate: [src_app_shared_guards_logged_in_guard__WEBPACK_IMPORTED_MODULE_12__["LoggedInGuard"]] },
    { path: 'group', component: _components_group_goals_group_goals_component__WEBPACK_IMPORTED_MODULE_5__["GroupGoalsComponent"], canActivate: [src_app_shared_guards_logged_in_guard__WEBPACK_IMPORTED_MODULE_12__["LoggedInGuard"]] },
    { path: 'groupusers', component: _components_group_goals_group_members_group_users_group_users_component__WEBPACK_IMPORTED_MODULE_13__["GroupUsersComponent"], canActivate: [src_app_shared_guards_logged_in_guard__WEBPACK_IMPORTED_MODULE_12__["LoggedInGuard"]] },
    { path: 'groupleaderboard', component: _components_group_goals_leaderboard_leaderboard_component__WEBPACK_IMPORTED_MODULE_4__["LeaderboardComponent"], canActivate: [src_app_shared_guards_logged_in_guard__WEBPACK_IMPORTED_MODULE_12__["LoggedInGuard"]] },
    { path: 'today', component: _components_today_goals_today_goals_component__WEBPACK_IMPORTED_MODULE_10__["TodayGoalsComponent"], canActivate: [src_app_shared_guards_logged_in_guard__WEBPACK_IMPORTED_MODULE_12__["LoggedInGuard"]] },
    { path: ':id', component: _components_goals_goal_details_goal_details_component__WEBPACK_IMPORTED_MODULE_11__["GoalDetailsComponent"], canActivate: [src_app_shared_guards_logged_in_guard__WEBPACK_IMPORTED_MODULE_12__["LoggedInGuard"]] },
    { path: 'group/:id', component: _components_group_goals_group_goal_details_group_goal_details_component__WEBPACK_IMPORTED_MODULE_1__["GroupGoalDetailsComponent"], canActivate: [src_app_shared_guards_logged_in_guard__WEBPACK_IMPORTED_MODULE_12__["LoggedInGuard"]] }
];
var GoalsRoutingModule = /** @class */ (function () {
    function GoalsRoutingModule() {
    }
    GoalsRoutingModule = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_7__["NgModule"])({
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_8__["RouterModule"].forChild(routes)],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_8__["RouterModule"]]
        })
    ], GoalsRoutingModule);
    return GoalsRoutingModule;
}());



/***/ }),

/***/ "./src/app/modules/goals/goals.module.ts":
/*!***********************************************!*\
  !*** ./src/app/modules/goals/goals.module.ts ***!
  \***********************************************/
/*! exports provided: GoalsModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GoalsModule", function() { return GoalsModule; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _components_group_goals_create_group_goal_create_group_goal_component__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./components/group-goals/create-group-goal/create-group-goal.component */ "./src/app/modules/goals/components/group-goals/create-group-goal/create-group-goal.component.ts");
/* harmony import */ var _components_group_goals_group_members_group_invitation_group_invitation_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./components/group-goals/group-members/group-invitation/group-invitation.component */ "./src/app/modules/goals/components/group-goals/group-members/group-invitation/group-invitation.component.ts");
/* harmony import */ var _components_group_goals_create_group_create_group_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./components/group-goals/create-group/create-group.component */ "./src/app/modules/goals/components/group-goals/create-group/create-group.component.ts");
/* harmony import */ var _components_group_goals_leaderboard_leaderboard_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./components/group-goals/leaderboard/leaderboard.component */ "./src/app/modules/goals/components/group-goals/leaderboard/leaderboard.component.ts");
/* harmony import */ var _components_goals_goal_details_goal_details_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./components/goals/goal-details/goal-details.component */ "./src/app/modules/goals/components/goals/goal-details/goal-details.component.ts");
/* harmony import */ var _components_group_goals_group_goals_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./components/group-goals/group-goals.component */ "./src/app/modules/goals/components/group-goals/group-goals.component.ts");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _goals_routing_module__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./goals-routing.module */ "./src/app/modules/goals/goals-routing.module.ts");
/* harmony import */ var _components_goals_goals_component__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./components/goals/goals.component */ "./src/app/modules/goals/components/goals/goals.component.ts");
/* harmony import */ var _components_goals_create_goal_create_goal_component__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./components/goals/create-goal/create-goal.component */ "./src/app/modules/goals/components/goals/create-goal/create-goal.component.ts");
/* harmony import */ var _shared_angular_material_angular_material_module__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ../../shared/angular-material/angular-material.module */ "./src/app/shared/angular-material/angular-material.module.ts");
/* harmony import */ var _components_today_goals_today_goals_component__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ./components/today-goals/today-goals.component */ "./src/app/modules/goals/components/today-goals/today-goals.component.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _components_group_goals_group_members_group_users_group_users_component__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ./components/group-goals/group-members/group-users/group-users.component */ "./src/app/modules/goals/components/group-goals/group-members/group-users/group-users.component.ts");
/* harmony import */ var _components_group_goals_group_goal_details_group_goal_details_component__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ./components/group-goals/group-goal-details/group-goal-details.component */ "./src/app/modules/goals/components/group-goals/group-goal-details/group-goal-details.component.ts");

















var GoalsModule = /** @class */ (function () {
    function GoalsModule() {
    }
    GoalsModule = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_7__["NgModule"])({
            imports: [
                _goals_routing_module__WEBPACK_IMPORTED_MODULE_8__["GoalsRoutingModule"],
                _shared_angular_material_angular_material_module__WEBPACK_IMPORTED_MODULE_11__["MaterialModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_13__["ReactiveFormsModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_13__["FormsModule"],
                _angular_common__WEBPACK_IMPORTED_MODULE_14__["CommonModule"]
            ],
            declarations: [
                _components_goals_goals_component__WEBPACK_IMPORTED_MODULE_9__["GoalsComponent"],
                _components_goals_create_goal_create_goal_component__WEBPACK_IMPORTED_MODULE_10__["CreateGoalComponent"],
                _components_group_goals_group_goals_component__WEBPACK_IMPORTED_MODULE_6__["GroupGoalsComponent"],
                _components_today_goals_today_goals_component__WEBPACK_IMPORTED_MODULE_12__["TodayGoalsComponent"],
                _components_goals_goal_details_goal_details_component__WEBPACK_IMPORTED_MODULE_5__["GoalDetailsComponent"],
                _components_group_goals_group_members_group_users_group_users_component__WEBPACK_IMPORTED_MODULE_15__["GroupUsersComponent"],
                _components_group_goals_leaderboard_leaderboard_component__WEBPACK_IMPORTED_MODULE_4__["LeaderboardComponent"],
                _components_group_goals_create_group_create_group_component__WEBPACK_IMPORTED_MODULE_3__["CreateGroupComponent"],
                _components_group_goals_group_members_group_invitation_group_invitation_component__WEBPACK_IMPORTED_MODULE_2__["GroupInvitationComponent"],
                _components_group_goals_group_goal_details_group_goal_details_component__WEBPACK_IMPORTED_MODULE_16__["GroupGoalDetailsComponent"],
                _components_group_goals_create_group_goal_create_group_goal_component__WEBPACK_IMPORTED_MODULE_1__["CreateGroupGoalComponent"]
            ]
        })
    ], GoalsModule);
    return GoalsModule;
}());



/***/ }),

/***/ "./src/app/modules/goals/models/goal-progress.model.ts":
/*!*************************************************************!*\
  !*** ./src/app/modules/goals/models/goal-progress.model.ts ***!
  \*************************************************************/
/*! exports provided: GoalProgress */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GoalProgress", function() { return GoalProgress; });
var GoalProgress = /** @class */ (function () {
    function GoalProgress(goalProgress) {
        Object.assign(this, goalProgress);
    }
    return GoalProgress;
}());



/***/ }),

/***/ "./src/app/modules/goals/models/goal-with-progress.model.ts":
/*!******************************************************************!*\
  !*** ./src/app/modules/goals/models/goal-with-progress.model.ts ***!
  \******************************************************************/
/*! exports provided: GoalWithProgressModel */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GoalWithProgressModel", function() { return GoalWithProgressModel; });
var GoalWithProgressModel = /** @class */ (function () {
    function GoalWithProgressModel(goalWithProgressModel) {
        this.goalProgressCollection = [];
        Object.assign(this, goalWithProgressModel);
    }
    return GoalWithProgressModel;
}());



/***/ }),

/***/ "./src/app/modules/goals/models/goal.model.ts":
/*!****************************************************!*\
  !*** ./src/app/modules/goals/models/goal.model.ts ***!
  \****************************************************/
/*! exports provided: Goal */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Goal", function() { return Goal; });
var Goal = /** @class */ (function () {
    function Goal(goal) {
        Object.assign(this, goal);
    }
    return Goal;
}());



/***/ }),

/***/ "./src/app/modules/goals/services/goals/goal-progress/goal-progress.service.ts":
/*!*************************************************************************************!*\
  !*** ./src/app/modules/goals/services/goals/goal-progress/goal-progress.service.ts ***!
  \*************************************************************************************/
/*! exports provided: GoalProgressService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GoalProgressService", function() { return GoalProgressService; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var src_app_shared_services_user_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/services/user.service */ "./src/app/shared/services/user.service.ts");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");





var GoalProgressService = /** @class */ (function () {
    function GoalProgressService(_http, _userService) {
        this._http = _http;
        this._userService = _userService;
    }
    GoalProgressService.prototype.updateProgressState = function (goalProgress) {
        var body = JSON.stringify({
            id: goalProgress.id,
            isDone: !goalProgress.isDone
        });
        return this._http.patch(this._userService.BACKURL + 'api/goalProgress', body, { headers: this._userService.getHeaders() }).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (progress) {
            return progress.isDone;
        }));
    };
    GoalProgressService = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])({
            providedIn: 'root'
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"], src_app_shared_services_user_service__WEBPACK_IMPORTED_MODULE_3__["UserService"]])
    ], GoalProgressService);
    return GoalProgressService;
}());



/***/ }),

/***/ "./src/app/modules/goals/services/goals/goals.service.ts":
/*!***************************************************************!*\
  !*** ./src/app/modules/goals/services/goals/goals.service.ts ***!
  \***************************************************************/
/*! exports provided: GoalsService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GoalsService", function() { return GoalsService; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _models_goal_progress_model__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./../../models/goal-progress.model */ "./src/app/modules/goals/models/goal-progress.model.ts");
/* harmony import */ var src_app_shared_services_user_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/shared/services/user.service */ "./src/app/shared/services/user.service.ts");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var _models_goal_with_progress_model__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../../models/goal-with-progress.model */ "./src/app/modules/goals/models/goal-with-progress.model.ts");
/* harmony import */ var _models_goal_model__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ../../models/goal.model */ "./src/app/modules/goals/models/goal.model.ts");








var GoalsService = /** @class */ (function () {
    function GoalsService(_http, _userService) {
        this._http = _http;
        this._userService = _userService;
    }
    GoalsService.prototype.deleteUserGoal = function (id) {
        return this._http.delete(this._userService.BACKURL + 'api/goals/' + id, { headers: this._userService.getHeaders() });
    };
    GoalsService.prototype.createUserGoal = function (goalType, name, workout) {
        if (workout === void 0) { workout = '-1'; }
        var body = JSON.stringify({
            Goalname: name,
            GoalType: goalType,
            WorkoutId: workout
        });
        return this._http.post(this._userService.BACKURL + 'api/goals/create', body, { headers: this._userService.getHeaders() });
        // .pipe(
        //   map((userGoal: any) => {
        //     return this.mapGoal(userGoal);
        //   }));
    };
    GoalsService.prototype.getUserGoal = function (id) {
        var _this = this;
        return this._http.get(this._userService.BACKURL + 'api/goals/' + id, { headers: this._userService.getHeaders() }).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_5__["map"])(function (goal) {
            return _this.mapGoal(goal);
        }));
    };
    GoalsService.prototype.getUserTodayGoalWithProgress = function () {
        return this._http.get(this._userService.BACKURL + 'api/goals/todayProgress', { headers: this._userService.getHeaders() });
        // .pipe(
        //   map((goalWithProgress: any) => {
        //     return this.mapGoalsWithProgress(goalWithProgress);
        //   }));
    };
    GoalsService.prototype.GetUserGoalsWithProgress = function (date, limit) {
        var _this = this;
        if (limit === void 0) { limit = 20; }
        var body = JSON.stringify({
            DateTimeOffset: date.toJSON(),
            DayLimit: limit
        });
        return this._http.post(this._userService.BACKURL + 'api/goals/progressHistory', body, { headers: this._userService.getHeaders() })
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_5__["map"])(function (goalsWithProgress) {
            return _this.mapGoalsWithProgress(goalsWithProgress);
        }));
    };
    GoalsService.prototype.mapGoal = function (goal) {
        if (goal) {
            return new _models_goal_model__WEBPACK_IMPORTED_MODULE_7__["Goal"]({
                id: goal.id,
                createdAt: new Date(goal.createdAt),
                name: goal.name
            });
        }
    };
    GoalsService.prototype.mapGoalsWithProgress = function (goalsWithProgress) {
        var goalsProgress = [];
        goalsWithProgress.forEach(function (goal) {
            var progressCollection = [];
            goal.goalProgressCollection.forEach(function (goalProgress) {
                progressCollection.push(new _models_goal_progress_model__WEBPACK_IMPORTED_MODULE_1__["GoalProgress"]({
                    createdAt: new Date(goalProgress.createdAt),
                    id: goalProgress.id,
                    isDone: goalProgress.isDone,
                    isDummy: goalProgress.isDummy
                }));
            });
            goalsProgress.push(new _models_goal_with_progress_model__WEBPACK_IMPORTED_MODULE_6__["GoalWithProgressModel"]({
                goal: new _models_goal_model__WEBPACK_IMPORTED_MODULE_7__["Goal"]({
                    id: goal.id,
                    createdAt: new Date(goal.createdAt),
                    name: goal.name
                }),
                goalProgressCollection: progressCollection
            }));
        });
        return goalsProgress;
    };
    GoalsService = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_3__["Injectable"])({
            providedIn: 'root'
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_4__["HttpClient"], src_app_shared_services_user_service__WEBPACK_IMPORTED_MODULE_2__["UserService"]])
    ], GoalsService);
    return GoalsService;
}());



/***/ }),

/***/ "./src/app/modules/goals/services/group/group-goal-progress/group-goal-progress.service.ts":
/*!*************************************************************************************************!*\
  !*** ./src/app/modules/goals/services/group/group-goal-progress/group-goal-progress.service.ts ***!
  \*************************************************************************************************/
/*! exports provided: GroupGoalProgressService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GroupGoalProgressService", function() { return GroupGoalProgressService; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var src_app_shared_services_user_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/services/user.service */ "./src/app/shared/services/user.service.ts");




var GroupGoalProgressService = /** @class */ (function () {
    function GroupGoalProgressService(_http, _userService) {
        this._http = _http;
        this._userService = _userService;
    }
    GroupGoalProgressService.prototype.getTodayUserGroupGoalsProgress = function () {
        return this._http.get(this._userService.BACKURL + 'api/groupGoalProgress', { headers: this._userService.getHeaders() });
    };
    GroupGoalProgressService.prototype.getSpecificGroupGoalsDayProgress = function (date) {
        var body = JSON.stringify({
            GroupProgressDate: date,
        });
        return this._http.post(this._userService.BACKURL + 'api/groupGoalProgress', body, { headers: this._userService.getHeaders() });
    };
    GroupGoalProgressService.prototype.updateGroupGoalProgressState = function (goalProgress) {
        var body = JSON.stringify({
            id: goalProgress.id,
            isDone: !goalProgress.isDone
        });
        return this._http.patch(this._userService.BACKURL + 'api/groupGoalProgress', body, { headers: this._userService.getHeaders() });
    };
    GroupGoalProgressService = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])({
            providedIn: 'root'
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"], src_app_shared_services_user_service__WEBPACK_IMPORTED_MODULE_3__["UserService"]])
    ], GroupGoalProgressService);
    return GroupGoalProgressService;
}());



/***/ }),

/***/ "./src/app/modules/goals/services/group/group-goal/group-goal.service.ts":
/*!*******************************************************************************!*\
  !*** ./src/app/modules/goals/services/group/group-goal/group-goal.service.ts ***!
  \*******************************************************************************/
/*! exports provided: GroupGoalService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GroupGoalService", function() { return GroupGoalService; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var src_app_shared_services_user_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/services/user.service */ "./src/app/shared/services/user.service.ts");




var GroupGoalService = /** @class */ (function () {
    function GroupGoalService(_http, _userService) {
        this._http = _http;
        this._userService = _userService;
    }
    GroupGoalService.prototype.deleteGroupGaol = function (id) {
        return this._http.delete(this._userService.BACKURL + 'api/groupGoals/' + id, { headers: this._userService.getHeaders() });
    };
    GroupGoalService.prototype.createGroupGoal = function (goalType, name, workout) {
        if (workout === void 0) { workout = '-1'; }
        var body = JSON.stringify({
            Name: name,
            GoalType: goalType,
            WorkoutId: workout
        });
        return this._http.post(this._userService.BACKURL + 'api/groupGoals', body, { headers: this._userService.getHeaders() });
    };
    GroupGoalService.prototype.getGroupGoal = function (id) {
        return this._http.get(this._userService.BACKURL + 'api/groupGoals/' + id, { headers: this._userService.getHeaders() });
    };
    GroupGoalService = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])({
            providedIn: 'root'
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"], src_app_shared_services_user_service__WEBPACK_IMPORTED_MODULE_3__["UserService"]])
    ], GroupGoalService);
    return GroupGoalService;
}());



/***/ }),

/***/ "./src/app/modules/goals/services/group/group-invitation/group-invitation.service.ts":
/*!*******************************************************************************************!*\
  !*** ./src/app/modules/goals/services/group/group-invitation/group-invitation.service.ts ***!
  \*******************************************************************************************/
/*! exports provided: GroupInvitationService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GroupInvitationService", function() { return GroupInvitationService; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var src_app_shared_services_user_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/services/user.service */ "./src/app/shared/services/user.service.ts");




var GroupInvitationService = /** @class */ (function () {
    function GroupInvitationService(_http, _userService) {
        this._http = _http;
        this._userService = _userService;
    }
    GroupInvitationService.prototype.getUserGroupInvitations = function () {
        return this._http.get(this._userService.BACKURL + 'api/invitation', { headers: this._userService.getHeaders() });
    };
    GroupInvitationService.prototype.sentGroupInvitation = function (memberEmail) {
        var body = JSON.stringify({
            MemberUsername: memberEmail,
        });
        return this._http.post(this._userService.BACKURL + 'api/invitation', body, { headers: this._userService.getHeaders() });
    };
    GroupInvitationService.prototype.cancelInvitation = function (id) {
        return this._http.delete(this._userService.BACKURL + 'api/invitation/' + id, { headers: this._userService.getHeaders() });
    };
    GroupInvitationService.prototype.acceptInvitation = function (LeaderUsername) {
        var body = JSON.stringify({
            LeaderUsername: LeaderUsername,
        });
        return this._http.post(this._userService.BACKURL + 'api/invitation/accept', body, { headers: this._userService.getHeaders() });
    };
    GroupInvitationService = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])({
            providedIn: 'root'
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"], src_app_shared_services_user_service__WEBPACK_IMPORTED_MODULE_3__["UserService"]])
    ], GroupInvitationService);
    return GroupInvitationService;
}());



/***/ }),

/***/ "./src/app/modules/goals/services/group/group-members/group-members.service.ts":
/*!*************************************************************************************!*\
  !*** ./src/app/modules/goals/services/group/group-members/group-members.service.ts ***!
  \*************************************************************************************/
/*! exports provided: GroupMembersService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GroupMembersService", function() { return GroupMembersService; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var src_app_shared_services_user_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/services/user.service */ "./src/app/shared/services/user.service.ts");




var GroupMembersService = /** @class */ (function () {
    function GroupMembersService(_http, _userService) {
        this._http = _http;
        this._userService = _userService;
    }
    GroupMembersService.prototype.getGroupMembers = function () {
        return this._http.get(this._userService.BACKURL + 'api/groupMembers', { headers: this._userService.getHeaders() });
    };
    GroupMembersService.prototype.removeGroupMember = function (username) {
        var body = JSON.stringify({
            MemberUsername: username,
        });
        return this._http.post(this._userService.BACKURL + 'api/groupMembers/specific', body, { headers: this._userService.getHeaders() });
    };
    GroupMembersService.prototype.leaveGroup = function () {
        return this._http.delete(this._userService.BACKURL + 'api/groupMembers', { headers: this._userService.getHeaders() });
    };
    GroupMembersService = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])({
            providedIn: 'root'
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"], src_app_shared_services_user_service__WEBPACK_IMPORTED_MODULE_3__["UserService"]])
    ], GroupMembersService);
    return GroupMembersService;
}());



/***/ }),

/***/ "./src/app/modules/goals/services/group/group.service.ts":
/*!***************************************************************!*\
  !*** ./src/app/modules/goals/services/group/group.service.ts ***!
  \***************************************************************/
/*! exports provided: GroupService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GroupService", function() { return GroupService; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var src_app_shared_services_user_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! src/app/shared/services/user.service */ "./src/app/shared/services/user.service.ts");




var GroupService = /** @class */ (function () {
    function GroupService(_http, _userService) {
        this._http = _http;
        this._userService = _userService;
    }
    GroupService.prototype.getUserGroup = function () {
        return this._http.get(this._userService.BACKURL + 'api/group', { headers: this._userService.getHeaders() });
    };
    GroupService.prototype.createGroup = function (name) {
        var body = JSON.stringify({
            GroupName: name,
        });
        return this._http.post(this._userService.BACKURL + 'api/group', body, { headers: this._userService.getHeaders() });
    };
    GroupService.prototype.deleteGroup = function () {
        return this._http.delete(this._userService.BACKURL + 'api/group', { headers: this._userService.getHeaders() });
    };
    GroupService = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])({
            providedIn: 'root'
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"], src_app_shared_services_user_service__WEBPACK_IMPORTED_MODULE_3__["UserService"]])
    ], GroupService);
    return GroupService;
}());



/***/ })

}]);
//# sourceMappingURL=modules-goals-goals-module.js.map