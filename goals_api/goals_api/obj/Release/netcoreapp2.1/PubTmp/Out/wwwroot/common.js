(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["common"],{

/***/ "./src/app/shared/services/workout/workout.service.ts":
/*!************************************************************!*\
  !*** ./src/app/shared/services/workout/workout.service.ts ***!
  \************************************************************/
/*! exports provided: WorkoutService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "WorkoutService", function() { return WorkoutService; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _user_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../user.service */ "./src/app/shared/services/user.service.ts");




var WorkoutService = /** @class */ (function () {
    function WorkoutService(_http, _userService) {
        this._http = _http;
        this._userService = _userService;
    }
    WorkoutService.prototype.deleteWorkout = function (id) {
        return this._http.delete(this._userService.BACKURL + 'api/workout/' + id, { headers: this._userService.getHeaders() });
    };
    WorkoutService.prototype.createWorkout = function (name, routePoints) {
        var body = JSON.stringify({
            Name: name,
            RoutePoints: routePoints
        });
        return this._http.post(this._userService.BACKURL + 'api/workout', body, { headers: this._userService.getHeaders() });
    };
    WorkoutService.prototype.getUserWorkouts = function () {
        return this._http.get(this._userService.BACKURL + 'api/workout/user', { headers: this._userService.getHeaders() });
    };
    WorkoutService.prototype.getUserUnusedWorkouts = function () {
        return this._http.get(this._userService.BACKURL + 'api/workout/unused', { headers: this._userService.getHeaders() });
    };
    WorkoutService.prototype.getGroupUnusedWorkouts = function () {
        return this._http.get(this._userService.BACKURL + 'api/workout/groupUnused', { headers: this._userService.getHeaders() });
    };
    WorkoutService.prototype.getGroupWorkouts = function () {
        return this._http.get(this._userService.BACKURL + 'api/workout/group', { headers: this._userService.getHeaders() });
    };
    WorkoutService.prototype.getUserWorkout = function (id) {
        return this._http.get(this._userService.BACKURL + 'api/workout/' + id, { headers: this._userService.getHeaders() });
    };
    WorkoutService = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])({
            providedIn: 'root'
        }),
        tslib__WEBPACK_IMPORTED_MODULE_0__["__metadata"]("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"], _user_service__WEBPACK_IMPORTED_MODULE_3__["UserService"]])
    ], WorkoutService);
    return WorkoutService;
}());



/***/ })

}]);
//# sourceMappingURL=common.js.map